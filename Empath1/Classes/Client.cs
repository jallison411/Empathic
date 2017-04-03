//-----------------------------------------------------------------------
// <copyright file="Client.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{
    /// <summary>
    /// This class contains the client object which represents the logged in user. User ID is checked in the client table for email / password
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the name of the client
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the record
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the client email address
        /// </summary>
        public string LoginEmail { get; set; }

        /// <summary>
        /// Gets or sets the client main welcome message displayed on the screen
        /// </summary>
        public string ClientMessage { get; set; }

        /// <summary>
        /// Gets or sets a linked object list of all client projects
        /// </summary>
        public Project Projects { get; set; }

        /// <summary>
        /// Returns a total project count
        /// </summary>
        /// <returns>returns an a project count</returns>
        public int TotalProjects()
        {
            int result = 0;

            Project looper = this.Projects;
            while (looper != null)
            {
                result++;
                looper = looper.NextProject;
            }

            return result;
        }
    }
}