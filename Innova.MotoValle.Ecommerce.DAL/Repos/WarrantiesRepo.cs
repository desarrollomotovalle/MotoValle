// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WarrantiesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Warranties Repo Implementations
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
    /// Warranties Repo Implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IWarrantiesRepo" />
    public class WarrantiesRepo : IWarrantiesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarrantiesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public WarrantiesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Warranties created
        /// </returns>
        public Warranties CreateRecord(Warranties item)
        {
            this._db.Warranties.Add(item);
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
                this._db.Warranties.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Warranties
        /// </returns>
        public List<Warranties> GetAllRecords()
        {
            return this._db.Warranties.AsNoTracking().ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Warranties by id
        /// </returns>
        public Warranties GetRecord(long id)
        {
            return this._db.Warranties
                           .AsNoTracking()
                           .FirstOrDefault(c => c.WarrantiesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Warranties updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.WarrantyName = updatedItem.WarrantyName;
            item.Description = updatedItem.Description;
            this._db.Warranties.Update(item);
            this._db.SaveChanges();
        }
    }
}
