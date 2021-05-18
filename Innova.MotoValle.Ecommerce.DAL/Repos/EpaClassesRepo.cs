// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EpaClassesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Epa Classes Repo facade
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
    /// Epa Classes Repo facade
    /// </summary>
    public class EpaClassesRepo : IEpaClassesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpaClassesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public EpaClassesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Epa Class created
        /// </returns>
        public EpaClasses CreateRecord(EpaClasses item)
        {
            this._db.EpaClasses.Add(item);
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
                this._db.EpaClasses.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Epa Classes
        /// </returns>
        public List<EpaClasses> GetAllRecords()
        {
            return this._db.EpaClasses
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Epa Class by id
        /// </returns>
        public EpaClasses GetRecord(long id)
        {
            return this._db.EpaClasses
                       .AsNoTracking()
                       .FirstOrDefault(c => c.EpaClassesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, EpaClasses updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.EpaClassName = updatedItem.EpaClassName;
            this._db.EpaClasses.Update(item);
            this._db.SaveChanges();
        }
    }
}