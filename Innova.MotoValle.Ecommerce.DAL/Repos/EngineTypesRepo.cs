// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineTypesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Engine Types Repo implementations
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
    /// Engine Types Repo implementations
    /// </summary>
    public class EngineTypesRepo : IEngineTypesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineTypesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public EngineTypesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Engine Types created
        /// </returns>
        public EngineTypes CreateRecord(EngineTypes item)
        {
            this._db.EngineTypes.Add(item);
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
                this._db.EngineTypes.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Engine Types
        /// </returns>
        public List<EngineTypes> GetAllRecords()
        {
            return this._db.EngineTypes
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Engine Types by id
        /// </returns>
        public EngineTypes GetRecord(long id)
        {
            return this._db.EngineTypes
                           .AsNoTracking()
                           .FirstOrDefault(c => c.EngineTypesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, EngineTypes updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.EngineTypeName = updatedItem.EngineTypeName;
            this._db.EngineTypes.Update(item);
            this._db.SaveChanges();
        }
    }
}