//-----------------------------------------------------------------------
// <copyright file="Note.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{
    using System;
    using System.Web;

    /// <summary>
    /// This class represents one Note from one project. Each project may have initial notes selected.
    /// </summary>
    [Serializable]
    public class Note
    {
        /// <summary>
        /// Initializes a new instance of the Note class
        /// </summary>
        public Note()
        {
            try
            {
                this.NextNote = null;
            }
            catch (Exception es)
            {
                HttpContext.Current.Trace.Write("Empathetic (Note Class) Error: " + es.Message);
            }
        }

        /// <summary>
        /// Gets or sets the Note Title
        /// </summary>
        public string NoteTitle { get; set; }

        /// <summary>
        /// Gets or sets the note details
        /// </summary>
        public string NoteDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the note is included (not used currently)
        /// </summary>
        public bool NoteInclude { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the note
        /// </summary>
        public DateTime NoteDate { get; set; }

        /// <summary>
        /// Gets or sets the next Note object in the chain of linked notes
        /// </summary>
        public Note NextNote { get; set; }
    }
}