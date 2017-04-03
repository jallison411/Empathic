//-----------------------------------------------------------------------
// <copyright file="Contacts.aspx.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1
{    
    using System;
    using System.IO;
    using Empath1.Classes;

    /// <summary>
    /// This class is for the main contacts page of the application
    /// </summary>
    public partial class Contacts : System.Web.UI.Page
    {
        /// <summary>
        /// Gets or sets a value indicating whether the logging is turned on or off
        /// </summary>
        public bool LoggingActivated { get; set; }
           
        /// <summary>
        /// Gets or sets a value for the logged in client object
        /// </summary>
        public Client LoggedInClient { get; set; }

        /// <summary>
        /// Gets or sets a value for the output log file.
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// This is the main entry point of the page
        /// </summary>
        /// <param name="sender">requires sender</param>
        /// <param name="e">requires e parameter</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Empath1.Properties.Settings.Default.LoggingOn.ToString().ToUpper().Trim() == "TRUE")
                {
                    this.LoggingActivated = true;
                }
                else
                {
                    this.LoggingActivated = false;
                }

                string timestamp = DateTime.Now.ToLongTimeString().Replace(" ", string.Empty).Replace(":", "-");
                this.LogFile = Empath1.Properties.Settings.Default.LogFilePath + @"\ClientForm_" + timestamp.Trim() + ".txt";
                this.WriteLog("Client PageLoad Started");
                this.ClearProject();

                try
                {
                    if (this.Session["Client"] != null)
                    {
                        this.LoggedInClient = (Client)Session["Client"];
                    }
                    else
                    {
                        // If we don't have a valid client can't continue
                        Session.Clear();
                        Response.Redirect("/login.aspx");
                    }                 
                }
                catch (Exception clientError)
                {
                    // If we don't have a valid client can't continue
                    string errorMessage = clientError.Message;
                    this.WriteLog("Client Error " + errorMessage);
                    Session.Clear();
                    Response.Redirect("/login.aspx");
                }

                if (this.LoggedInClient != null)
                {
                    this.WriteLog("Contacts page Client from session found - Display data now...");

                    if (this.LoggedInClient.Projects != null)
                    {
                        // Now we should have a client, projects and all project data. Display it!
                        this.DisplayMainData();
                    }
                }
            }
            catch (Exception es)
            {
                string error = es.Message;
                this.WriteLog("Contacts Page Load Error " + error);
            }
        }

        /// <summary>
        /// This method fires when the project drop down list is changed
        /// </summary>
        /// <param name="sender">requires sender</param>
        /// <param name="e">requires e parameter</param>
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Project looper = this.LoggedInClient.Projects;
            while (looper != null)
            {
                if (DropDownList1.SelectedValue == looper.ProjectName)
                {
                    this.DisplayProject(looper);
                    this.Session.Add("SelectedProject", looper);
                }

                looper = looper.NextProject;
            }
        }

        /// <summary>
        /// This method just removes all of the default html code from the form.
        /// </summary>
        private void ClearProject()
        {
            clientContacts.InnerHtml = string.Empty;
            partnerBlock.InnerHtml = string.Empty;
        }

        /// <summary>
        /// This method displays the main html data on the form
        /// </summary>
        private void DisplayMainData()
        {
            Project sessionProject = null;

            if (this.Session["SelectedProject"] != null)
            {
                sessionProject = (Project)this.Session["SelectedProject"];
                this.DisplayProject(sessionProject);
            }
            else
            {
                // no project selected so just display the first project
                this.DisplayProject(this.LoggedInClient.Projects);
                this.Session.Add("SelectedProject", this.LoggedInClient.Projects);
            }

            // Populate Projects DropDown list
            string projectListHTML = string.Empty;
            int projectIndex = 0;
            int counter = 0;

            if (DropDownList1.Items.Count == 0)
            {
                Project looper = this.LoggedInClient.Projects;
                while (looper != null)
                {
                    DropDownList1.Items.Add(looper.ProjectName);
                    if (sessionProject != null)
                    {
                        if (sessionProject.ProjectName == looper.ProjectName)
                        {
                            projectIndex = counter;
                        }
                    }

                    looper = looper.NextProject;
                    counter++;
                }

                DropDownList1.SelectedIndex = projectIndex;
            }
        }
                
        /// <summary>
        /// This method will display the html for contacts and partners on the page
        /// </summary>
        /// <param name="project">requires a project object</param>
        private void DisplayProject(Project project)
        {
           clientContacts.InnerHtml = this.ContactsContainerHTLM(project.DirectoryRepresentative, project.PrimaryRepObject);
            partnerBlock.InnerHtml = this.PartnersContainerHTLM(project.ProjectPartners);
        }

        /// <summary>
        /// This method will generate all of the html in the contacts section
        /// </summary>
        /// <param name="contactsChain">requires a linked list of contact objects</param>
        /// <param name="primaryRep">requires the primary rep so we can place it at the top</param>
        /// <returns>returns a string of html</returns>
        private string ContactsContainerHTLM(Empath1.Classes.DirectoryRepresentative contactsChain, Empath1.Classes.DirectoryRepresentative primaryRep)
        {
            string html = string.Empty;
            string outputHtml = string.Empty;
            try
            {
                //// < div class="contact-item">
                ////  <div class="contact-avatar">
                ////    <img src = "edde-master/img/contact.png" >
                ////  </ div >
                ////  < div class="contact-info">
                ////    <div class="contact-name"><span>John Smithterson</span></div>
                ////    <div class="contact-title"><span>Job Title Goes Here</span></div>
                ////    <div class="contact-department"><span>Macomb County Economic Development</span></div>
                ////    <div class="contact-email"><span><a href = "#" > john.smith@macombcounty.gov</a></span></div>
                ////    <div class="contact-phone"><span>234-567-4567</span></div>
                ////  </div>
                //// </div>              

                string blueDivHeader = "<div class='contact-item'>";
                string greyDivHeader = "<div class='contact-item grey'>";
                string grey2DivHeader = "<div class='contact-item grey2'>";

                int i = 0;
                bool switcher = true;

                // Check if we have a primary rep - if so - place at top
                if (primaryRep != null)
                {
                    html = blueDivHeader;
                    string defaultDiv = "<div class='contact-avatar'>" +
                                                           "<img src = '" + primaryRep.Photo + "' >" +
                                                           "</div>" +
                                                           "<div class='contact-info'>" +
                                                           "<div class='contact-name'><span>" + primaryRep.RepresentativeName + "</span></div>" +
                                                           "<div class='contact-title'><span>" + primaryRep.JobTitle + "</span></div>" +
                                                           "<div class='contact-department'><span>" + primaryRep.Organization + "</span></div>" +
                                                           "<div class='contact-email'><span><a href = '#' >" + primaryRep.Email + @"</a></span></div>" +
                                                           "<div class='contact-phone'><span>" + primaryRep.Phone + "</span></div>" + "</div></div>";
                    outputHtml = html + defaultDiv;
                    i++;
                }

                Empath1.Classes.DirectoryRepresentative currentRep = contactsChain;
              
                while (currentRep != null)
                {
                    // skip the rep in the chain if they are the primary rep. (Already added above)
                    if (currentRep != primaryRep)
                    {
                        string defaultDiv = "<div class='contact-avatar'>" +
                                            "<img src = '" + currentRep.Photo + "' >" +
                                            "</div>" +
                                            "<div class='contact-info'>" +
                                            "<div class='contact-name'><span>" + currentRep.RepresentativeName + "</span></div>" +
                                            "<div class='contact-title'><span>" + currentRep.JobTitle + "</span></div>" +
                                            "<div class='contact-department'><span>" + currentRep.Organization + "</span></div>" +
                                            "<div class='contact-email'><span><a href = '#' >" + currentRep.Email + @"</a></span></div>" +
                                            "<div class='contact-phone'><span>" + currentRep.Phone + "</span></div>" + "</div></div>";

                        if (i == 0)
                        {
                            // The top header is always blue - indicating the primary rep  -- Add the primary rep to the top of the html built so far
                            html = blueDivHeader;
                        }
                        else
                        {
                            if (switcher)
                            {
                                html = greyDivHeader;
                                switcher = false;
                            }
                            else
                            {
                                html = grey2DivHeader;
                                switcher = true;
                            }
                        }

                        html = html + defaultDiv;
                        outputHtml += html;

                        i++;
                    }

                    currentRep = currentRep.NextDirectory;
                }
            }
            catch (Exception es)
            {
                outputHtml = es.Message;
            }

            return outputHtml;
        }

        /// <summary>
        /// This method will generate the html output for the partners section
        /// </summary>
        /// <param name="firstPartner">requires the first partner object in the linked list</param>
        /// <returns>returns a string of html</returns>
        private string PartnersContainerHTLM(Partner firstPartner)
        {
            string html = string.Empty;
            string outputHtml = string.Empty;
            try
            {
                Partner currentPartner = firstPartner;
               
                while (currentPartner != null)
                {
                    string defaultDiv = "<div class='partner-item'>" +
                                        "<div class='partner-left'>" +
                                        "<div class='partner-name'>" + currentPartner.PartnerName + "</div>" +
                                        "<div class='partner-title'>" + currentPartner.JobTitle + "</div>" +
                                        "<div class='partner-company'>" + currentPartner.CompanyName + "</div>" +
                                        "</div>" +
                                        "<div class='partner-right'>" +
                                        "<div class='partner-info'>" +
                                        "<p><a href = '#' > " + currentPartner.PartnerEmail + "</a></p>" +
                                        "<p>" + currentPartner.ContactPhone + "</p>" +
                                        "</div></div></div>";
                    
                    outputHtml += defaultDiv;                   
                    currentPartner = currentPartner.NextPartner;
                }
            }
            catch (Exception es)
            {
                outputHtml = es.Message;
            }

            return outputHtml;
        }

        /// <summary>
        /// This method will write the passed in string to a log file defined earlier
        /// </summary>
        /// <param name="output">string to write to the log file</param>
        private void WriteLog(string output)
        {
            if (this.LoggingActivated)
            {
                // this will append to an existing logfile
                using (StreamWriter stream = new StreamWriter(this.LogFile, true))
                {
                    stream.WriteLine(output);
                }
            }
        } 
    }
}
