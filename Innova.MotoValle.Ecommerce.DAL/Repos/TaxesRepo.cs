// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Taxes Repo implementations
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
    /// Taxes Repo implementations
    /// </summary>
    public class TaxesRepo : ITaxesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public TaxesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Taxes created
        /// </returns>
        public Taxes CreateRecord(Taxes item)
        {
            this._db.Taxes.Add(item);
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
                this._db.Taxes.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Taxes
        /// </returns>
        public List<Taxes> GetAllRecords()
        {
            return this._db.Taxes
                       .AsNoTracking()
                       .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Taxes by id
        /// </returns>
        public Taxes GetRecord(long id)
        {
            return this._db.Taxes
                           .AsNoTracking()
                           .FirstOrDefault(c => c.TaxesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Taxes updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.TaxName = updatedItem.TaxName;
            item.TaxPercent = updatedItem.TaxPercent;
            item.TaxDescription = updatedItem.TaxDescription;
            this._db.Taxes.Update(item);
            this._db.SaveChanges();
        }
    }
}