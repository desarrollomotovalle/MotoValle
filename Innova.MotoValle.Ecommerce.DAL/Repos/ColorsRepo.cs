// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Colors Repo Implementations
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
    /// Colors Repo Implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IColorsRepo" />
    public class ColorsRepo : IColorsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ColorsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Colors created
        /// </returns>
        public Colors CreateRecord(Colors item)
        {
            this._db.Colors.Add(item);
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
                this._db.Colors.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of colors
        /// </returns>
        public List<Colors> GetAllRecords()
        {
            return this._db.Colors
                .AsNoTracking()
                .Include(x => x.FkMakes)
                .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Colors by id
        /// </returns>
        public Colors GetRecord(long id)
        {
            return this._db.Colors
                .AsNoTracking()
                .FirstOrDefault(c => c.ColorsId == id);
        }

        /// <summary>
        /// Getfors the product.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>List Of colors for product / makes</returns>
        public List<Colors> GetforProduct(long productsId)
        {
            var makesId = this._db.Products.AsNoTracking().Where(x => x.ProductsId == productsId).FirstOrDefault().FkMakesId;
            return this._db.Colors
                .AsNoTracking()
                .Where(x => x.FkMakesId == makesId)
                .ToList();
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Colors updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.ColorName = updatedItem.ColorName;
            item.FkMakesId = updatedItem.FkMakesId;
            this._db.Colors.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Colors by id
        /// </returns>
        private Colors Get(long id)
        {
            return this._db.Colors
                .AsNoTracking()
                .FirstOrDefault(c => c.ColorsId == id);
        }
    }
}
