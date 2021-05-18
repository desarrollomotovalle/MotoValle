// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignLeadsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Campaign Leads Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Campaign Leads Repo implementations
    /// </summary>
    public class CampaignLeadsRepo : ICampaignLeadsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignLeadsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CampaignLeadsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Campaign Leads created
        /// </returns>
        public CampaignLeads CreateRecord(CampaignLeads item)
        {
            this._db.CampaignLeads.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            var item = this.GetRecord(id);
            if (item != null)
            {
                this._db.CampaignLeads.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Campaign Leads
        /// </returns>
        public List<CampaignLeads> GetAllRecords()
        {
            return this._db.CampaignLeads
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Campaign Leads by id
        /// </returns>
        public CampaignLeads GetRecord(long id)
        {
            return this._db.CampaignLeads
                           .AsNoTracking()
                           .FirstOrDefault(c => c.CampaignLeadsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, CampaignLeads updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            this._db.CampaignLeads.Update(updatedItem);
            this._db.SaveChanges();
        }
    }
}