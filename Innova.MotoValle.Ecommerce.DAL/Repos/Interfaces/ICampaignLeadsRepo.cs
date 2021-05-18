// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICampaignLeadsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Campaign Leads Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Campaign Leads Repo facade
    /// </summary>
    public interface ICampaignLeadsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Campaign Leads</returns>
        List<CampaignLeads> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Campaign Leads by id</returns>
        CampaignLeads GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Campaign Leads created</returns>
        CampaignLeads CreateRecord(CampaignLeads item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, CampaignLeads updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
