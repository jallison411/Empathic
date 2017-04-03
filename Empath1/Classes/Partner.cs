//-----------------------------------------------------------------------
// <copyright file="Partner.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{
    using System;
    using System.Web;

    /// <summary>
    /// This class represents one partner for a project. Each project may have multiple partners.
    /// </summary>
    [Serializable]
    public class Partner
    {
        /// <summary>
        /// Initializes a new instance of the Partner class
        /// </summary>
        public Partner()
        {
            try
            {
                this.NextPartner = null;
            }
            catch (Exception es)
            {
                HttpContext.Current.Trace.Write("Empathetic (Partner Class) Error: " + es.Message);
            }
        }

        /// <summary>
        /// Gets or sets the Partner name
        /// </summary>
        public string PartnerName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the current partner
        /// </summary>
        public string PartnerEmail { get; set; }

        /// <summary>
        /// Gets or sets the contact phone of the current partner
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Gets or sets the company name of the current partner
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the job title of the current partner
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the next Partner object in the linked list of Partners.
        /// </summary>
        public Partner NextPartner { get; set; }
    }
}