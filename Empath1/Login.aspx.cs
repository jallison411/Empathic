//-----------------------------------------------------------------------
// <copyright file="Login.aspx.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1
{ 
    using System;
    using System.Configuration;
    using System.IO;
    using System.Security;
    using System.Threading;
    using Empath1.Classes;
    using Microsoft.SharePoint.Client;

    /// <summary>
    /// Initializes a new instance of the Login form
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Gets or sets the O365 Account login from the settings file
        /// </summary>
        public string O365AccountLogin { get; set; }
    
        /// <summary>
        /// Gets or sets the O365 Account Password from the settings file converted to secure string
        /// </summary>
        public SecureString O365AccountPassword { get; set; }
   
        /// <summary>
        /// Gets or sets the main O365 site URL for this project from the settings file
        /// </summary>
        public string O365SiteURL { get; set; }

        /// <summary>
        /// This is the main entry point of the login form
        /// </summary>
        /// <param name="sender">required sender</param>
        /// <param name="e">required e parameter</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            loginButton.ServerClick += new EventHandler(this.LoginButton_Click);
            newAccount.ServerClick += new EventHandler(this.NewAccountButton_Click);
         if (Secure.ResetPasswordFlag() == true)
            {
                this.ShowPasswordModal();
            }

            this.O365AccountPassword = Secure.GetO365Password();
        }

    /// <summary>
    /// This is the main login submit button routine
    /// </summary>
    /// <param name="sender">required sender</param>
    /// <param name="e">required e parameter</param>
    protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Login procedures: User account entered should exist in the 'Clients' library as email address. Password stored in plain text. 
            // Accessing O365 for now is completed using anonymous account.
            string userName = userNameTextBox.Value;
            string password = passwordTextBox.Value;

            // temp for now
            userName = "empathicdesign2017@hotmail.com";
            password = "12345";

            string pwdS = password;

            // We will not be encrypting the password for now. This is just a check of the user email against a list value
            this.O365AccountLogin = Empath1.Properties.Settings.Default.O365LoginAccount;

            this.O365SiteURL = Empath1.Properties.Settings.Default.O365URL;

            if (userName.Trim().Length > 0)
            {
                if (this.AuthenticateUser(userName, password))
                {
                    Session.Add("LoginName", userName);
                    Response.Redirect("/main.aspx");
                    this.Dispose();
                }
                else
                {
                    // login fail             
                    loginErrorDiv.Style["Visibility"] = "visible";
                }
            }
        }

        /// <summary>
        /// This is the new account button which redirects user to the Microsoft account signup page
        /// </summary>
        /// <param name="sender">required sender</param>
        /// <param name="e">required e parameter</param>
        protected void NewAccountButton_Click(object sender, EventArgs e)
        {
            string accountSignup = "https://signup.live.com/signup.aspx";
            string s = "window.open('" + accountSignup + "', 'popup_window', 'width=800,height=1500,left=100,top=100,resizable=yes,scrollbars=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        /// <summary>
        /// This method is the ok to continue button on the password dialog
        /// </summary>
        /// <param name="sender">requires sender</param>
        /// <param name="e">requires e parameter</param>
        protected void NewPasswordButtonOK_Click(object sender, EventArgs e)
        {
            SecureString mySecureString = Secure.ToSecureString(NewPasswordTextBox.Text);
            Secure.ResetPassword(mySecureString);
            this.HidePasswordModal();
        }

        /// <summary>
        /// This method authenticates the logging in user. It will only check the SharePoint 'Clients' list to see it the user email and password exist. 
        /// </summary>
        /// <param name="loginName">Requires a user email address as login name</param>
        /// <param name="password">Requires a clear text password</param>
        /// <returns>Returns a boolean true if user and password are confirmed</returns>
        private bool AuthenticateUser(string loginName, string password)
        {
            bool result = false;
            try
            {   
                using (var ctx = new ClientContext(this.O365SiteURL))
                {                   
                    ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);                  
                    ctx.RequestTimeout = Timeout.Infinite;                   
                    List projectsList = ctx.Web.GetListByTitle("Clients");

                    // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                    // so that it grabs all list items, regardless of the folder they are in. 
                    CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                    query.ViewXml = "<View><Query><Where><Eq><FieldRef Name = 'LoginAccount' /><Value Type = 'Text'>" + loginName.Trim().ToLower() + "</Value></Eq></Where></Query></View>";
                    Microsoft.SharePoint.Client.ListItemCollection items = projectsList.GetItems(query);

                    // Retrieve all items in the ListItemCollection from List.GetItems(Query). 
                    // ctx.Load(items);
                    ctx.Load(
                        items,
                        c => c.Include(
                        li => li["ID"],
                        li => li["ClientClearPassword"],
                        li => li["ClientMessage"],
                        li => li["Title"],
                        li => li["LoginAccount"]));

                    ctx.ExecuteQuery();

                    if (items.Count == 1)
                    {
                        Microsoft.SharePoint.Client.ListItem listItem = items[0];
                        if (listItem["ClientClearPassword"] != null)
                        {
                            string storedPassword = listItem["ClientClearPassword"].ToString();
                            if (storedPassword.Trim() == password.Trim())
                            {
                                Client client = new Client();
                                client.ClientMessage = string.Empty;

                                if (listItem["Title"] != null)
                                {
                                    client.ClientName = listItem["Title"].ToString();
                                }

                                if (listItem["ClientMessage"] != null)
                                {
                                    client.ClientMessage = listItem["ClientMessage"].ToString();
                                }

                                if (listItem["ID"] != null)
                                {
                                    client.ID = listItem["ID"].ToString();
                                }

                                if (listItem["LoginAccount"] != null)
                                {
                                    client.LoginEmail = listItem["LoginAccount"].ToString();
                                }

                                Session.Add("Client", client);
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string message = "Login Error: " + es.Message;
                string timestamp = DateTime.Now.ToLongTimeString().Replace(" ", string.Empty).Replace(":", "-");
                string logFile = @"C:\MyLogs\LoginFormError_" + timestamp.Trim() + ".txt";
                using (StreamWriter stream = new StreamWriter(logFile, true))
                {
                    stream.WriteLine(message);
                }
            }

            return result;
        }

        /// <summary>
        /// This method will place encrypt the provided section key
        /// </summary>
        /// <param name="sectionKey">requires key to encrypt</param>
        private void EncryptConfigSection(string sectionKey)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection(sectionKey);
            if (section != null)
            {
                if (!section.SectionInformation.IsProtected)
                {
                    if (!section.ElementInformation.IsLocked)
                    {
                        section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                        section.SectionInformation.ForceSave = true;
                        config.Save(ConfigurationSaveMode.Full);
                    }
                }
            }
        }

        /// <summary>
        /// This method will show the password popup box on the main form
        /// </summary>
        private void ShowPasswordModal()
        {
            this.NewPasswordDiv.Visible = true;
            this.NewPasswordButtonOK.Enabled = true;
        }

        /// <summary>
        /// This method will hide the password popup box on the main form
        /// </summary>
        private void HidePasswordModal()
        {
            this.NewPasswordDiv.Visible = false;
            this.NewPasswordButtonOK.Enabled = false;
        }
    }
}
