//-----------------------------------------------------------------------
// <copyright file="DirectoryRepresentative.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{
    using System;
    using System.Web;

    /// <summary>
    /// This class represents one Directory Representative object. Each project may have multiple reps and one designated primary rep
    /// </summary>
    [Serializable]
    public class DirectoryRepresentative
    {
        /// <summary>
        /// Initializes a new instance of the DirectoryRepresentative class
        /// </summary>
        public DirectoryRepresentative()
        {
            try
            {
                this.NextDirectory = null;
            }
            catch (Exception es)
            {
                HttpContext.Current.Trace.Write("Empathetic (Directory Rep Class) Error: " + es.Message);
            }
        }
     
        /// <summary>
        /// Gets or sets the directory representative name
        /// </summary>
        public string RepresentativeName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the current rep
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the organization the rep belongs to
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Gets or sets the reps phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the reps address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the reps state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the reps zip code
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the reps city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the reps job title
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets a string URL to the reps photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets a URL to the reps photo
        /// </summary>
        public Microsoft.SharePoint.Client.FieldUrlValue PhotoLink2 { get; set; }

        /// <summary>
        /// Gets or sets the next Directory Rep object in the linked list
        /// </summary>
        public DirectoryRepresentative NextDirectory { get; set; }
    }
}