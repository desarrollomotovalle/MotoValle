// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductMaintenancesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Hero Images Repo implementations
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
    /// Hero Images Repo implementations
    /// </summary>
    public class ProductMaintenancesRepo : IProductMaintenancesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMaintenancesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ProductMaintenancesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Hero Images created
        /// </returns>
        public ProductMaintenances CreateRecord(ProductMaintenances item)
        {
            this._db.ProductMaintenances.Add(item);
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
                this._db.ProductMaintenances.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of body styles
        /// </returns>
        public List<ProductMaintenances> GetAllRecords()
        {
            return this._db.ProductMaintenances
                           .Include(x => x.FkProducts)
                           .ThenInclude(x => x.FkMakes)
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the enable records by make.
        /// </summary>
        /// <param name="makesName">Name of the makes.</param>
        /// <returns>
        /// List Of Hero Images
        /// </returns>
        public List<ProductMaintenances> GetEnableRecordsByMake(string makesName)
        {
            return this._db.ProductMaintenances
                           .Include(x => x.FkProducts)
                           .ThenInclude(x => x.FkMakes)
                           .AsNoTracking()
                           .Where(x => x.Enable && x.FkProducts.FkMakes.MakeName.Contains(makesName, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(x => x.MaintenanceIndex)
                           .ToList();
        }

        /// <summary>
        /// Gets the home record.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <param name="onlyEnables">if set to <c>true</c> [only enables].</param>
        /// <returns>
        /// Product Maintenances
        /// </returns>
        public List<ProductMaintenances> GetEnableRecordsByProduct(int productsId, bool onlyEnables = true)
        {
            var productMaintenancesQuery = this._db.ProductMaintenances
                               .Include(x => x.FkProducts)
                               .ThenInclude(x => x.FkMakes)
                               .Where(x => x.FkProductsId == productsId)
                               .AsNoTracking();

            if (onlyEnables)
            {
                productMaintenancesQuery = productMaintenancesQuery.Where(x => x.Enable);
            }

            return productMaintenancesQuery
                               .OrderBy(x => x.MaintenanceIndex)
                               .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// body styles by id
        /// </returns>
        public ProductMaintenances GetRecord(long id)
        {
            return this._db.ProductMaintenances
                           .Include(x => x.FkProducts)
                           .ThenInclude(x => x.FkMakes)
                           .AsNoTracking()
                           .FirstOrDefault(c => c.ProductMaintenancesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, ProductMaintenances updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.ProductMaintenancesId = updatedItem.ProductMaintenancesId;
            item.Description = updatedItem.Description;
            item.Enable = updatedItem.Enable;
            item.FkProductsId = updatedItem.FkProductsId;
            item.MaintenanceIndex= updatedItem.MaintenanceIndex;
            item.Title = updatedItem.Title;
            item.SalesPrice = updatedItem.SalesPrice;

            this._db.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Hero Images </returns>
        private ProductMaintenances Get(long id)
        {
            return this._db.ProductMaintenances
                .AsNoTracking()
                .FirstOrDefault(c => c.ProductMaintenancesId == id);
        }
    }
}