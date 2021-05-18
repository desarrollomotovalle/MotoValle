// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChamberDimensionsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Body Styles Repo implementations
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
    /// Body Styles Repo implementations
    /// </summary>
    public class ChamberDimensionsRepo : IChamberDimensionsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChamberDimensionsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ChamberDimensionsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Body Styles created
        /// </returns>
        public ChamberDimensions CreateRecord(ChamberDimensions item)
        {
            this._db.ChamberDimensions.Add(item);
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
                this._db.ChamberDimensions.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of body styles
        /// </returns>
        public List<ChamberDimensions> GetAllRecords()
        {
            return this._db.ChamberDimensions
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// body styles by id
        /// </returns>
        public ChamberDimensions GetRecord(long id)
        {
            return this._db.ChamberDimensions
                           .AsNoTracking()
                           .FirstOrDefault(c => c.ChamberDimensionsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, ChamberDimensions updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.ChamberDimensionName = updatedItem.ChamberDimensionName;
            this._db.ChamberDimensions.Update(item);
            this._db.SaveChanges();
        }
    }
}