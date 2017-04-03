//-----------------------------------------------------------------------
// <copyright file="Main.aspx.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1
{   
    using System;
    using System.IO;
    using System.Security;
    using System.Threading;
    using Empath1.Classes;
    using Microsoft.SharePoint.Client;

    /// <summary>
    /// This is the main form in the project. Main entry point after login.
    /// </summary>
    public partial class Main : System.Web.UI.Page
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
     /// Gets or sets the main client object representing the logged in user
     /// </summary>
        public Client LoggedInClient { get; set; }

        /// <summary>
        /// Gets or sets the log file output location
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the logging features are activated
        /// </summary>
        public bool LoggingActivated { get; set; }

        /// <summary>
        /// This is the main entry point of the login form
        /// </summary>
        /// <param name="sender">required sender</param>
        /// <param name="e">required e parameter</param>
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

                if (this.Page.IsPostBack == true)
                {
                    if (this.Session["LogFile"] != null)
                    {
                        this.LogFile = this.Session["LogFile"].ToString();
                    }
                    else
                    {
                        string timestamp = DateTime.Now.ToLongTimeString().Replace(" ", string.Empty).Replace(":", "-");
                        this.LogFile = Empath1.Properties.Settings.Default.LogFilePath + @"\mainForm_" + timestamp.Trim() + ".txt";
                        this.Session.Add("LogFile", this.LogFile);
                    }
                }
                else
                {
                    string timestamp = DateTime.Now.ToLongTimeString().Replace(" ", string.Empty).Replace(":", "-");
                    this.LogFile = Empath1.Properties.Settings.Default.LogFilePath + @"\mainForm_" + timestamp.Trim() + ".txt";
                    this.Session.Add("LogFile", this.LogFile);
                }             

                this.ClearProject();
                this.O365AccountLogin = Empath1.Properties.Settings.Default.O365LoginAccount;
               
                this.O365AccountPassword = Secure.GetO365Password();
                this.O365SiteURL = Empath1.Properties.Settings.Default.O365URL;  

                try
                {
                    if (this.Session["Client"] != null)
                    {
                        this.LoggedInClient = (Client)this.Session["Client"];
                    }
                }
                catch (Exception clientError)
                {
                    // If we don't have a valid client can't continue
                    string errorMessage = clientError.Message;
                    this.WriteLog("Client Error " + errorMessage);
                }

                if (this.LoggedInClient != null)
                {
                    this.WriteLog("Client Found - Loading projects...");

                    // Only reload the projects if nothing is found. - will use a refresh button to reset
                    if (this.LoggedInClient.Projects == null)
                    {
                        this.GetProjects();
                    }

                        // write a summary report to the output file.
                        this.ReportOnClient();

                        if (this.LoggedInClient.Projects != null)
                        {
                            // At this point we have updated the client object - Update the session variables
                            Session.Add("Client", this.LoggedInClient);

                            // Now we should have a client, projects and all project data. Display it!
                            this.DisplayMainData();
                        }
                    }                
            }
            catch (Exception es)
            {
                string error = es.Message;
                this.WriteLog("Main Page Load Error " + error);
            }
        }

        /// <summary>
        /// This button click will change the selected project
        /// </summary>
        /// <param name="sender">required sender</param>
        /// <param name="e">required e parameter</param>
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
        /// This method will write a summary count of items to the output text file.
        /// </summary>
        private void ReportOnClient()
        {
            if (this.LoggedInClient != null)
            {
                this.WriteLog("Client Name:" + this.LoggedInClient.ClientName);
                this.WriteLog("Total Projects:" + this.LoggedInClient.TotalProjects().ToString());

                Project p = this.LoggedInClient.Projects;
                while (p != null)
                {
                    this.WriteLog("Project Name:" + p.ProjectName);
                    this.WriteLog("Project Document Count:" + p.TotalDocuments().ToString());
                    Document d = p.ProjectDocuments;
                    while (d != null)
                    {
                        this.WriteLog("Document Name:" + d.Name);
                        d = d.NextDocument;
                    }

                    this.WriteLog("Project Notes Count:" + p.TotalNotes().ToString());
                    Note n = p.ProjectNotes;
                    while (n != null)
                    {
                        this.WriteLog("Notes Title:" + n.NoteTitle);
                        n = n.NextNote;
                    }

                    this.WriteLog("Project Partners Count:" + p.TotalPartners().ToString());
                    Partner pr = p.ProjectPartners;
                    while (pr != null)
                    {
                        this.WriteLog("Partner Name:" + pr.PartnerName);
                        pr = pr.NextPartner;
                    }

                    this.WriteLog("Project Reps Count:" + p.TotalDirectoryRepresentatives().ToString());
                    Empath1.Classes.DirectoryRepresentative dr = p.DirectoryRepresentative;
                    while (dr != null)
                    {
                        this.WriteLog("Rep Name:" + dr.RepresentativeName);
                        dr = dr.NextDirectory;
                    }

                    this.WriteLog(string.Empty);
                    p = p.NextProject;
                }
            }
        }
      
        /// <summary>
        /// This method will display the projects found in the projects section div tag
        /// </summary>
        private void DisplayMainData()
        {
            // Update side messages...
            sideWelcomeMessage.InnerHtml = "<span id='welcome-header'> Welcome to the " + this.LoggedInClient.ClientName.Trim() + " Economic Development Dashboard</span>" +
            this.LoggedInClient.ClientMessage.Trim() + "</ div >";

            sideSignedInAs.InnerHtml = "Signed in as " + "<b> " + this.LoggedInClient.ClientName + @"</b> | <a href = '#'> sign out</a>";

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
                sessionProject = this.LoggedInClient.Projects;
            }

            if (sessionProject.PrimaryRepObject != null)
            {
                mainContactImage.Src = sessionProject.PrimaryRepObject.Photo;
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
        /// This method will call out the display methods for notes and documents for the selected project
        /// </summary>
        /// <param name="project">requires the first project in the list</param>
        private void DisplayProject(Project project)
        {
            MainDocContainerRow.InnerHtml = this.DocumentContainerHTLM(project.ProjectDocuments);
            NoteContentContainer.InnerHtml = this.NotesContainerHTLM(project.ProjectNotes);
        }

        /// <summary>
        /// This method will clear out the default html values of the page
        /// </summary>
        private void ClearProject()
        {
            MainDocContainerRow.InnerHtml = string.Empty;
            NoteContentContainer.InnerHtml = string.Empty;
        }

        /// <summary>
        /// This method produces the html string output for the documents section on the page
        /// </summary>
        /// <param name="firstDocument">requires the first document in the linked list</param>       
        /// <returns>returns a string of html for the documents div</returns>
        private string DocumentContainerHTLM(Document firstDocument)
        {
            string html = string.Empty;
            string outputHtml = string.Empty;
            try
            {
                string defaultDiv1Header = "<div class='document-item document-item-blue col-md-6' data-url='pdf.html'>";
                string defaultDiv2Header = "<div class='document-item document-item-bg1 col-md-6' data-url='pdf.html'>";
                string defaultDiv3Header = "<div class='document-item document-item-bg2 col-md-6' data-url='pdf.html'>";
                string defaultDiv4Header = "<div class='document-item document-item-white col-md-6' data-url='pdf.html'>";

                Document currentDocument = firstDocument;
                int i = 0;
                while (currentDocument != null)
                {                  
                    DateTime docTime = DateTime.Now;
                    string docTitle = currentDocument.Name; 
                    string docSummary = currentDocument.ExecutiveSummary;

                    string fileURL = currentDocument.ItemFile.LinkingUri;
                     fileURL = currentDocument.ItemFile.LinkingUrl;
                     fileURL = currentDocument.ItemFile.ServerRelativeUrl;
                     fileURL = currentDocument.DocumentLink;

                    string defaultDiv = "<div class='document-time'>" + docTime.ToShortDateString() +
                                        "</div>" +
                                        "<div class='document-title'>" + docTitle + "</div>" +
                                        "<div class='document-summary'>" + docSummary +
                                        "</div>" +
                                        "<div class='document-nav'>" +
                                        "<a href = '" + fileURL + "' class='document-nav-square'>" +
                                        "<span class='glyphicon glyphicon-menu-right'></span>" +
                                        "</a></div></div>";
                   
                        if (i == 0 | i == 4 | i > 7)
                        {
                            html = defaultDiv1Header;
                        }
                        else if (i == 1 | i == 5)
                        {
                            html = defaultDiv2Header;
                        }
                        else if (i == 2 | i == 6)
                        {
                            html = defaultDiv3Header;
                        }
                        else if (i == 3 | i == 7)
                        {
                            html = defaultDiv4Header;
                        }

                    docTitle = currentDocument.Name; 
                    docSummary = currentDocument.ExecutiveSummary; 

                        defaultDiv = "<div class='document-time'>" + docTime.ToShortDateString() +
                                        "</div>" +
                                        "<div class='document-title'>" + docTitle + "</div>" +
                                        "<div class='document-summary'>" + docSummary +
                                        "</div>" +
                                        "<div class='document-nav'>" +
                                        "<a href = '" + fileURL + "' class='document-nav-square'>" +
                                        "<span class='glyphicon glyphicon-menu-right'></span>" +
                                        "</a></div></div>";

                        html = html + defaultDiv;
                        outputHtml += html;

                    i++;
                    currentDocument = currentDocument.NextDocument;
                }
            }
            catch (Exception es)
            {
                outputHtml = es.Message;
            }

            return outputHtml;
        }

        /// <summary>
        /// This method produces the html string output for the notes section on the page
        /// </summary>
        /// <param name="firstNote">requires the first note object in the linked list</param>
        /// <returns>returns a string of html for the notes div</returns>
        private string NotesContainerHTLM(Note firstNote)
        {
            string html = string.Empty;
            string outputHtml = string.Empty;
            try
            {
                //// < div class="note-item">
                //// <div class="note-time">12/12/2017</div>
                //// <div class="note-title">Descriptive title of note goes here.</div>
                //// <div class="note-detail">
                //// Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.Detailed notes here.
                //// </div>
                //// </div>              
                Note currentNote = firstNote;
                int i = 0;

                while (currentNote != null)
                {
                    DateTime noteTime = DateTime.Now;
                    string noteTitle = "Note Title";
                    string noteDetails = "Some notes Summary description";

                    string defaultDiv = "<div class='note-item'>" +
                                        "<div class='note-time'>" + noteTime.ToShortDateString() + "</div>" +
                                        "<div class='note-title'>" + noteTitle + "</div>" +
                                        "<div class='note-detail'>" + noteDetails + "</div></div>";

                    noteTitle = currentNote.NoteTitle;
                    noteDetails = currentNote.NoteDetails;

                    defaultDiv = "<div class='note-item'>" +
                                        "<div class='note-time'>" + noteTime.ToShortDateString() + "</div>" +
                                        "<div class='note-title'>" + noteTitle + "</div>" +
                                        "<div class='note-detail'>" + noteDetails + "</div></div>";

                    outputHtml += defaultDiv;
                    i++;
                    currentNote = currentNote.NextNote;
                }
            }
            catch (Exception es)
            {
                outputHtml = es.Message;
            }

            return outputHtml;
        }

        /// <summary>
        /// This method uses the logged in client to find a list of projects related to that client. When a project is instantiated the project
        /// and all properties associated are loaded into the object
        /// </summary>
        private void GetProjects()
        {
            try
            {
                this.WriteLog("Loading projects Start");
                using (var ctx = new ClientContext(this.O365SiteURL))
                {
                    ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);
                    ctx.RequestTimeout = Timeout.Infinite;
          
                    List projectsList = ctx.Web.GetListByTitle("Projects");

                    // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                    // so that it grabs all list items, regardless of the folder they are in. 
                    CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                    query.ViewXml = "<View><Query><Where><Eq><FieldRef Name = 'Client' LookupId='TRUE'/><Value Type = 'Text'>" + this.LoggedInClient.ID + "</Value></Eq></Where></Query></View>";

                    Microsoft.SharePoint.Client.ListItemCollection items = projectsList.GetItems(query);
                                       
                    ctx.Load(
                        items,
                        c => c.Include(
                            li => li["ID"],
                            li => li["Title"],
                            li => li["ID"],
                            li => li["NotesAttached"],
                            li => li["Rep"],
                            li => li["DocumentsAttached"],
                            li => li["Client"],
                            li => li["Client_x003a_ID"],
                            li => li["Partners"],
                            li => li["PrimaryRep"]));

                    ctx.ExecuteQuery();
                    string projectListHTML = string.Empty;
                    bool first = true;
                    Project nextProject = null;

                    // now loop through all of the incentive projects
                    foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                    {
                        this.WriteLog("Projects Looper...");

                        if (listItem["ID"] != null)
                        {
                            string projectID = listItem["ID"].ToString();
                            Project project = new Project(projectID, this.LogFile, this.LoggingActivated);
                            project.NextProject = null;

                            if (first)
                            {
                                this.LoggedInClient.Projects = project;
                                nextProject = project;
                                first = false;
                            }
                            else
                            {
                                // Build the chain of projects
                                nextProject.NextProject = project;
                                nextProject = project;
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string error = es.Message;
                this.WriteLog("Error Loading projects in Main.aspx loader: " + es.Message);
            }
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