// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignLeads.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Campaign Leads
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Search Engine Optimization Entitty
    /// </summary>
    public class SearchEngineOptimizations
    {
        /// <summary>
        /// Gets or sets the search engine optimization identifier.
        /// </summary>
        /// <value>
        /// The search engine optimization identifier.
        /// </value>
        public int SearchEngineOptimizationsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        /// <value>
        /// The name of the page.
        /// </value>
        public string PagePath { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public string Keywords { get; set; }
    }
}
