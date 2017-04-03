//-----------------------------------------------------------------------
// <copyright file="Project.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{    
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Threading;
    using System.Web;
    using Microsoft.SharePoint.Client;

    /// <summary>
    /// This class represents one project for a client
    /// </summary>
    [Serializable]
    public class Project
    {
        /// <summary>
        /// Initializes a new instance of the Project class
        /// </summary>
        /// <param name="projectID">requires a project id to load</param>
        /// <param name="logFile">location of the output log file</param>
        /// <param name="logActivated">flag indicating if the logging is turned on</param>
        public Project(string projectID, string logFile, bool logActivated)
        {
            try
            {
                this.LogFile = logFile;
                this.LoggingActivated = logActivated;
                this.LoadProject(projectID);
            }
            catch (Exception es)
            {
                this.WriteLog("Error: Projects Class Instance start: " + es.Message);
                HttpContext.Current.Trace.Write("Empathetic (Project Class) Error: " + es.Message);
            }
        }

        /// <summary>
        /// Gets or sets the primary directory representative object
        /// </summary>
        public DirectoryRepresentative PrimaryRepObject { get; set; }

        /// <summary>
        /// Gets or sets the next project in a linked list of projects
        /// </summary>
        public Project NextProject { get; set; }

        /// <summary>
        /// Gets or sets the log file location
        /// </summary>
        public string LogFile { get; set; }

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
        /// Gets or sets a value indicating whether logging is activated
        /// </summary>
        public bool LoggingActivated { get; set; }

        /// <summary>
        /// Gets or sets the project name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the first representative of the project. This will be a linked list of reps. Note this is not the primary rep.
        /// </summary>
        public DirectoryRepresentative DirectoryRepresentative { get; set; }

        /// <summary>
        /// Gets or sets the first note of the project. This will be a linked list of notes
        /// </summary>
        public Note ProjectNotes { get; set; }

        /// <summary>
        /// Gets or sets the first partner object of the project. This will be a linked list of partners
        /// </summary>
        public Partner ProjectPartners { get; set; }

        /// <summary>
        /// Gets or sets the first document element of the project. This will be a linked list of documents
        /// </summary>
        public Document ProjectDocuments { get; set; }

        /// <summary>
        /// Gets or Sets the Count of the Project Documents
        /// </summary>
        /// <returns>returns a count of total documents</returns>
        public int TotalDocuments()
        {
            int result = 0;

            Document looper = this.ProjectDocuments;
            while (looper != null)
            {
                result++;
                looper = looper.NextDocument;
            }

            return result;
        }

        /// <summary>
        /// Gets or Sets the Count of the Project Partners
        /// </summary>
        /// <returns>returns a count of total partners</returns>
        public int TotalPartners()
        {
            int result = 0;

            Partner looper = this.ProjectPartners;
            while (looper != null)
            {
                result++;
                looper = looper.NextPartner;
            }

            return result;
        }

        /// <summary>
        /// Gets or Sets the Count of the Project Notes
        /// </summary>
        /// <returns>returns a count of total notes</returns>
        public int TotalNotes()
        {
            int result = 0;

            Note looper = this.ProjectNotes;
            while (looper != null)
            {
                result++;
                looper = looper.NextNote;
            }

            return result;
        }

        /// <summary>
        /// Gets or Sets the Count of the Project Representatives
        /// </summary>
        /// <returns>returns a count of total reps</returns>
        public int TotalDirectoryRepresentatives()
        {
            int result = 0;

            DirectoryRepresentative looper = this.DirectoryRepresentative;
            while (looper != null)
            {
                result++;
                looper = looper.NextDirectory;
            }

            return result;
        }

        /// <summary>
        /// This method loads all of the main project properties
        /// </summary>
        /// <param name="id">requires the id record of the project</param>
        private void LoadProject(string id)
        {
            try
            {
                this.O365AccountLogin = Empath1.Properties.Settings.Default.O365LoginAccount;
                this.O365AccountPassword = Secure.GetO365Password();
                this.O365SiteURL = Empath1.Properties.Settings.Default.O365URL;

                using (var ctx = new ClientContext(this.O365SiteURL))
                {
                    ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);
                    ctx.RequestTimeout = Timeout.Infinite;

                    List projectsList = ctx.Web.GetListByTitle("Projects");

                    // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                    // so that it grabs all list items, regardless of the folder they are in. 
                    CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                    query.ViewXml = "<View><Query><Where><Eq><FieldRef Name = 'ID' /><Value Type = 'Text'>" + id + "</Value></Eq></Where></Query></View>";
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
                        li => li["Partners"],
                        li => li["PrimaryRep"]));

                    ctx.ExecuteQuery();

                    if (items.Count > 0)
                    {
                        FieldLookupValue primaryRep = null;

                        Microsoft.SharePoint.Client.ListItem listItem = items[0];
                        if (listItem["Title"] != null)
                        {
                            this.ProjectName = listItem["Title"].ToString();
                            this.WriteLog("Project Loading: " + this.ProjectName);
                        }

                        if (listItem["PrimaryRep"] != null)
                        {
                            string xrimaryRep = listItem["PrimaryRep"].ToString();
                            primaryRep = (FieldLookupValue)listItem["PrimaryRep"];
                        }

                        if (listItem["NotesAttached"] != null)
                        {
                            this.WriteLog("Project Loading: Notes Found Loading...");
                            FieldLookupValue[] fl = (FieldLookupValue[])listItem["NotesAttached"];
                            this.LoadNotes(fl);
                        }

                        if (listItem["DocumentsAttached"] != null)
                        {
                            this.WriteLog("Project Loading: Docs Found Loading...");
                            FieldLookupValue[] docs = (FieldLookupValue[])listItem["DocumentsAttached"];
                            this.LoadDocuments(docs);
                        }

                        if (listItem["Partners"] != null)
                        {
                            this.WriteLog("Project Loading: Partners Found Loading...");
                            FieldLookupValue[] partners = (FieldLookupValue[])listItem["Partners"];
                            this.LoadPartners(partners);
                        }

                        if (listItem["Rep"] != null)
                        {
                            this.WriteLog("Project Loading: Reps Found Loading...");
                            FieldLookupValue[] reps = (FieldLookupValue[])listItem["Rep"];
                            this.LoadReps(reps, primaryRep);
                        }
                    }
                }
            }
            catch (Exception es)
            {
                this.WriteLog("ERROR: Project Class Loading: " + es.Message);
            }
        }

        /// <summary>
        /// This method will load all of the partners for the project
        /// </summary>
        /// <param name="partners">requires a field lookup value array of partner object ids</param>
        private void LoadPartners(FieldLookupValue[] partners)
        {
            try
            {
                this.WriteLog("Starting to load Partners Start");

                string camlQuery = string.Empty;
                if (partners != null && partners.Count() > 0)
                {
                    ArrayList array = new ArrayList();
                    foreach (FieldLookupValue fl in partners)
                    {
                        array.Add(fl.LookupId);
                    }

                    string output = this.QueryWrapper(array, string.Empty);
                    camlQuery = "<View><Query><Where>" + output + "</Where></Query></View>";
                    this.WriteLog(camlQuery);

                    using (var ctx = new ClientContext(this.O365SiteURL))
                    {
                        ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);
                        ctx.RequestTimeout = Timeout.Infinite;

                        List projectsList = ctx.Web.GetListByTitle("Partners");

                        // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                        // so that it grabs all list items, regardless of the folder they are in. 
                        //  CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
                        CamlQuery query = new CamlQuery();
                        query.ViewXml = camlQuery;

                        Microsoft.SharePoint.Client.ListItemCollection items = projectsList.GetItems(query);
                      
                        ctx.Load(
                           items, 
                           c => c.Include(
                           li => li["ID"],
                           li => li["Title"],
                           li => li["JobTitle"],
                           li => li["Email"],
                           li => li["ContactPhone"],
                           li => li["CompanyName"]));

                        ctx.ExecuteQuery();

                        Partner partner = new Partner();
                        Partner nextPartner = null;

                        // now loop through all of the incentive projects
                        foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                        {
                            partner = new Partner();

                            if (listItem["Title"] != null)
                            {
                                partner.PartnerName = listItem["Title"].ToString();
                            }

                            this.WriteLog("Starting to load Partners LOOP " + partner.PartnerName);

                            if (listItem["JobTitle"] != null)
                            {
                                partner.JobTitle = listItem["JobTitle"].ToString();
                            }

                            if (listItem["ContactPhone"] != null)
                            {
                                partner.ContactPhone = listItem["ContactPhone"].ToString();
                            }

                            if (listItem["CompanyName"] != null)
                            {
                                partner.CompanyName = listItem["CompanyName"].ToString();
                            }

                            if (listItem["Email"] != null)
                            {
                                partner.PartnerEmail = listItem["Email"].ToString();
                            }

                            // Set the document to the head of the chain if first one
                            if (this.ProjectPartners == null)
                            {
                                this.ProjectPartners = partner;
                                nextPartner = partner;
                            }
                            else
                            {
                                nextPartner.NextPartner = partner;

                                // Now set the next document to this current document and loop back
                                nextPartner = partner;
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string error = "Error loading partners: " + es.Message;
                this.WriteLog("ERROR: Project Class Loading Partners: " + error);
            }
        }

        /// <summary>
        /// This recursive method will create the CAML query string required. It will nest or statements from the supplied values.
        /// </summary>
        /// <param name="values">requires an array of id values for row lookups</param>
        /// <param name="returnString">recursive method input default empty to start</param>
        /// <returns>returns a string CAML query</returns>
        private string QueryWrapper(ArrayList values, string returnString)
        {
            if (values.Count > 0)
            {
                if (values.Count == 1)
                {
                   returnString = returnString + "<Eq><FieldRef Name = 'ID' /><Value Type = 'Text'>" + values[0] + "</Value></Eq>";
                }
                else
                {
                    // At this point there must be at least 2 left
                    returnString = returnString + "<Or><Eq><FieldRef Name = 'ID' /><Value Type = 'Text'>" + values[0] + "</Value></Eq>";
                    
                    // remove the first element
                    values.RemoveAt(0);

                    returnString = returnString + "<Eq><FieldRef Name = 'ID' /><Value Type = 'Text'>" + values[0] + "</Value></Eq></Or>";

                    // remove the first element
                    values.RemoveAt(0);

                    if (values.Count > 0)
                    {
                        returnString = "<Or>" + this.QueryWrapper(values, returnString) + "</Or>";
                    }
                }
            }

            return returnString;
        }

       /// <summary>
       /// This method will load all of the directory representatives for the project
       /// </summary>
       /// <param name="reps">requires a field lookup array of the reps</param>
       /// <param name="primaryRep">requires a field lookup value of the primary rep</param>
        private void LoadReps(FieldLookupValue[] reps, FieldLookupValue primaryRep)
        {
            try
            {
                string camlQuery = string.Empty;
                if (reps != null && reps.Count() > 0)
                {
                    ArrayList array = new ArrayList();
                    foreach (FieldLookupValue fl in reps)
                    {
                        array.Add(fl.LookupId);
                    }

                    string output = this.QueryWrapper(array, string.Empty);
                    this.WriteLog(output);

                    camlQuery = "<View><Query><Where>" + output + "</Where></Query></View>";

                    this.WriteLog("Looking for Reps Query: " + camlQuery);

                    using (var ctx = new ClientContext(this.O365SiteURL))
                    {
                        ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);
                        ctx.RequestTimeout = Timeout.Infinite;

                        List projectsList = ctx.Web.GetListByTitle("Directory");

                        // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                        // so that it grabs all list items, regardless of the folder they are in. 
                        CamlQuery query = new CamlQuery();
                        query.ViewXml = camlQuery;

                        Microsoft.SharePoint.Client.ListItemCollection items = projectsList.GetItems(query);

                        ctx.Load(
                            items, 
                            c => c.Include(
                            li => li["ID"],
                            li => li["Title"],
                            li => li["PersonTitle"],
                            li => li["City"],
                            li => li["State"],
                            li => li["Zip"],
                            li => li["Organization"],
                            li => li["Email"],
                            li => li["photo"],
                            li => li["Cell_x0020_Number"]));
                        
                        ctx.ExecuteQuery();

                        DirectoryRepresentative representative = new DirectoryRepresentative();
                        DirectoryRepresentative nextRep = null;

                        int i = 0;

                        // now loop through all of the representatives
                        foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                        {
                            i++;
                            representative = new DirectoryRepresentative();
                            this.WriteLog("Starting to load Reps LOOP: " + i.ToString());

                            if (listItem["Title"] != null)
                            {
                                string repName = listItem["Title"].ToString();
                                representative.RepresentativeName = repName;
                            }

                            this.WriteLog("Starting to load Reps LOOP " + representative.RepresentativeName);
                            
                            if (listItem["PersonTitle"] != null)
                            {
                                representative.JobTitle = listItem["PersonTitle"].ToString();
                            }

                            if (listItem["Email"] != null)
                            {
                                representative.Email = listItem["Email"].ToString();
                            }

                            if (listItem["City"] != null)
                            {
                                representative.City = listItem["City"].ToString();
                            }

                            if (listItem["State"] != null)
                            {
                                representative.State = listItem["State"].ToString();
                            }

                            if (listItem["Zip"] != null)
                            {
                                representative.Zip = listItem["Zip"].ToString();
                            }

                            if (listItem["Organization"] != null)
                            {
                                representative.Organization = listItem["Organization"].ToString();
                            }

                            if (listItem["Cell_x0020_Number"] != null)
                            {
                                representative.Phone = listItem["Cell_x0020_Number"].ToString();
                            }

                            if (listItem["photo"] != null)
                            {
                                Microsoft.SharePoint.Client.FieldUrlValue photo = (Microsoft.SharePoint.Client.FieldUrlValue)listItem["photo"];
                                representative.Photo = photo.Url;
                                representative.PhotoLink2 = photo;
                            }

                            if (primaryRep != null)
                            {                                 
                                if (primaryRep.LookupValue.Trim() == representative.RepresentativeName.Trim())
                                {
                                    this.PrimaryRepObject = representative;
                                }
                            }

                            // Set the document to the head of the chain if first one
                            if (this.DirectoryRepresentative == null)
                            {
                                this.DirectoryRepresentative  = representative;
                                nextRep = representative;
                            }
                            else
                            {
                                nextRep.NextDirectory = representative;

                                // Now set the next document to this current document and loop back
                                nextRep = representative;
                            }                           
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string error = "Error loading representatives: " + es.Message;
                this.WriteLog("ERROR: Project Class Loading Reps: " + error);
            }
        }

        /// <summary>
        /// This method will load all of the notes for the project
        /// </summary>
        /// <param name="notes">requires a field lookup value of index keys for each note</param>
        private void LoadNotes(FieldLookupValue[] notes)
        {
            try
            {
                this.WriteLog("Starting to load Notes");

                string camlQuery = string.Empty;
                if (notes != null && notes.Count() > 0)
                {
                    ArrayList array = new ArrayList();
                    foreach (FieldLookupValue fl in notes)
                    {
                        array.Add(fl.LookupId);
                    }

                    string output = this.QueryWrapper(array, string.Empty);
                    camlQuery = "<View><Query><Where>" + output + "</Where></Query></View>";

                    using (var ctx = new ClientContext(this.O365SiteURL))
                    {
                        ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);
                        ctx.RequestTimeout = Timeout.Infinite;

                        List projectsList = ctx.Web.GetListByTitle("Notes");

                        // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
                        // so that it grabs all list items, regardless of the folder they are in. 
                        CamlQuery query = new CamlQuery();
                        query.ViewXml = camlQuery;

                        this.WriteLog("Looking for notes using this query:...");
                        this.WriteLog(camlQuery);

                        Microsoft.SharePoint.Client.ListItemCollection items = projectsList.GetItems(query);

                        ctx.Load(
                            items, 
                            c => c.Include(
                            li => li["ID"],
                            li => li["Title"],
                            li => li["Note_x0020_Details"],                         
                            li => li["Include"]));

                        ctx.ExecuteQuery();

                        Note note = new Note();
                        Note nextNote = null;

                        // now loop through all of the representatives
                        foreach (Microsoft.SharePoint.Client.ListItem listItem in items)
                        {
                            note = new Note();

                            if (listItem["Title"] != null)
                            {
                                note.NoteTitle = listItem["Title"].ToString();                                
                            }

                            this.WriteLog("Starting to load Notes LOOP " + note.NoteTitle);

                            if (listItem["Note_x0020_Details"] != null)
                            {
                                note.NoteDetails = listItem["Note_x0020_Details"].ToString();
                            }

                            if (listItem["Include"] != null)
                            {
                                if (listItem["Include"].ToString() == "True")
                                {
                                    note.NoteInclude = true;
                                }
                                else
                                {
                                    note.NoteInclude = false;
                                }
                            }

                            // Set the document to the head of the chain if first one
                            if (this.ProjectNotes == null)
                            {
                                this.ProjectNotes = note;
                                nextNote = note;
                            }
                            else
                            {
                                nextNote.NextNote = note;

                                // Now set the next document to this current document and loop back
                                nextNote = note;
                            }                            
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string error = "Error loading Notes: " + es.Message;
                this.WriteLog("ERROR: Project Class Loading Notes: " + error);
            }
        }

        /// <summary>
        /// This method will load all of the documents for the project
        /// </summary>
        /// <param name="documents">requires a field lookup value array of the index keys for each document</param>
        private void LoadDocuments(FieldLookupValue[] documents)
        {
            try
            {
                this.WriteLog("Starting to load Documents");

                Document previousDocument = null;

                string camlQuery = string.Empty;
                if (documents != null && documents.Count() > 0)
                {
                    ArrayList array = new ArrayList();
                    foreach (FieldLookupValue fl in documents)
                    {
                        array.Add(fl.LookupId);
                    }

                    string output = this.QueryWrapper(array, string.Empty);

                    camlQuery = "<View Scope='RecursiveAll'><Query><Where>" + output + "</Where></Query></View>";

                    using (var ctx = new ClientContext(this.O365SiteURL))
                    {
                        ctx.Credentials = new SharePointOnlineCredentials(this.O365AccountLogin, this.O365AccountPassword);
                        ctx.RequestTimeout = Timeout.Infinite;

                        List documentsList = ctx.Web.GetListByTitle("PDF Library");

                        ctx.Load(documentsList);
                        CamlQuery query = new CamlQuery();
                        query.ViewXml = camlQuery;

                        ListItemCollection listItems = documentsList.GetItems(query);

                        ctx.Load(
                            listItems,
                            items => items.Include(
                            item => item["Created"],
                            item => item["Executive_x0020_Summary"],
                            item => item["Executive_x0020_Summary_x0020_Details"],
                            item => item["Tags"],
                            item => item["EncodedAbsUrl"],
                            item => item["FileRef"],
                            item => item.DisplayName,
                            item => item.Folder,
                            item => item.File.Exists,
                            item => item.File));

                        // Execute the query
                        ctx.ExecuteQuery();
                        Document document = new Document();

                        bool isFile = false;

                        foreach (Microsoft.SharePoint.Client.ListItem listItem in listItems)
                        {
                            document = new Document();

                            // Display name will either be file name or folder name
                            document.Name = listItem.DisplayName;
                            document.IsFolder = true;

                            isFile = false;

                            try
                            {
                                if (listItem.File.Exists)
                                {
                                    isFile = true;
                                    document.IsFolder = false;
                                }
                            }
                            catch (Exception ignore)
                            {
                                // ignore the exception;
                                this.WriteLog("Ignore this error during file check: " + ignore.Message);
                            }
                                                        
                            if (isFile)
                            {
                                document.ItemFile = listItem.File;

                                this.WriteLog("Starting to load Documents LOOP " + document.Name);

                                 if (listItem.FieldValues["Executive_x0020_Summary"] != null)
                                {
                                    document.ExecutiveSummary = listItem.FieldValues["Executive_x0020_Summary"].ToString();
                                }

                                if (listItem.FieldValues["Executive_x0020_Summary_x0020_Details"] != null)
                                {
                                    document.ExecutiveSummaryDetails = listItem.FieldValues["Executive_x0020_Summary_x0020_Details"].ToString();
                                }

                                if (listItem.FieldValues["Tags"] != null)
                                {
                                    Microsoft.SharePoint.Client.Taxonomy.TaxonomyFieldValueCollection tags = (Microsoft.SharePoint.Client.Taxonomy.TaxonomyFieldValueCollection)listItem.FieldValues["Tags"];
                                    document.Tags = tags;
                                }

                                if (listItem.FieldValues["EncodedAbsUrl"] != null)
                                {
                                   // Microsoft.SharePoint.Client.FieldUrlValue fileURL = (Microsoft.SharePoint.Client.FieldUrlValue)listItem.FieldValues["EncodedAbsUrl"];
                                    document.DocumentLink = listItem.FieldValues["EncodedAbsUrl"].ToString();
                                }

                                // Set the document to the head of the chain if first one
                                if (this.ProjectDocuments == null)
                                {
                                    this.ProjectDocuments = document;
                                    previousDocument = document;
                                }
                                else
                                {
                                    previousDocument.NextDocument = document;

                                    // Now set the next document to this current document and loop back
                                    previousDocument = document;
                                }
                            }
                            else
                            {
                                // Possibly a folder                                
                                this.WriteLog("Folder: " + listItem.Folder.Name);
                            }                            
                        }
                    }
                }
            }
            catch (Exception es)
            {
                string error = "Error loading docuemnts: " + es.Message;
                this.WriteLog("ERROR: Project Class Loading Documents: " + error);
            }
        }

        /// <summary>
        /// This method will generate a summary help file for debugging purposes. Turn off in web config. File location specified in web config.
        /// </summary>
        /// <param name="output">Requires string to output to file.</param>
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