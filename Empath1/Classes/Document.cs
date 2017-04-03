//-----------------------------------------------------------------------
// <copyright file="Document.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{
    using System;
    using System.Web;

    /// <summary>
    /// This class represents a document object mapping meta data values from SharePoint found in the document library. 
    /// Each project may be linked to multiple documents
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Initializes a new instance of the Document class
        /// </summary>
        public Document()
        {
            try
            {
                this.NextDocument = null;
            }
            catch (Exception es)
            {
                HttpContext.Current.Trace.Write("Empathetic (Document Class) Error: " + es.Message);
            }
        }

        /// <summary>
        /// Gets or sets the name of the document
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the TAGS value which may be used later. This stores any taxonomy terms applied to the document.
        /// </summary>
        public Microsoft.SharePoint.Client.Taxonomy.TaxonomyFieldValueCollection Tags { get; set; }

        /// <summary>
        /// Gets or sets the document executive summary metadata
        /// </summary>
        public string ExecutiveSummary { get; set; }

        /// <summary>
        /// Gets or sets the document executive summary details metadata
        /// </summary>
        public string ExecutiveSummaryDetails { get; set; }

        /// <summary>
        /// Gets or sets the next Document found in the linked list chain of docs
        /// </summary>
        public Document NextDocument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current object is a folder
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// Gets or sets the actual file item data of the document
        /// </summary>
        public Microsoft.SharePoint.Client.File ItemFile { get; set; }

        /// <summary>
        /// Gets or sets a URL link to the document
        /// </summary>
        public string DocumentLink { get; set; }
    }
}