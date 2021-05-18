// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MakesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Makes Repo implementations
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
    /// Makes Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IMakesRepo" />
    public class MakesRepo : IMakesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public MakesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Product created
        /// </returns>
        public Makes CreateRecord(Makes item)
        {
            this._db.Makes.Add(item);
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
                this._db.Makes.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of products
        /// </returns>
        public List<Makes> GetAllRecords()
        {
            return this._db.Makes
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Product by Id
        /// </returns>
        public Makes GetRecord(long id)
        {
            return this._db.Makes
                           .AsNoTracking()
                           .FirstOrDefault(c => c.MakesId == id);
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// Product by Id
        /// </returns>
        public Makes GetRecord(string name)
        {
            return this._db.Makes
                           .AsNoTracking()
                           .FirstOrDefault(c => c.MakeName.ToUpper().Contains(name.ToUpper()));
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Makes updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.MakeName = updatedItem.MakeName;
            item.Description = updatedItem.Description;
            this._db.Makes.Update(item);
            this._db.SaveChanges();
        }
    }
}