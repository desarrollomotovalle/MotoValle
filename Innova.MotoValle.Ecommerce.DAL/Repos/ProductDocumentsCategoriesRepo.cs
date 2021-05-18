// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsCategoriesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Documents Categories Repo implementations
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
    /// Product Documents Categories Repo implementations
    /// </summary>
    public class ProductDocumentsCategoriesRepo : IProductDocumentsCategoriesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsCategoriesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ProductDocumentsCategoriesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Product Documents Categories created
        /// </returns>
        public ProductDocumentsCategories CreateRecord(ProductDocumentsCategories item)
        {
            this._db.ProductDocumentsCategories.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            ProductDocumentsCategories item = this.GetRecord(id);
            if (item != null)
            {
                this._db.ProductDocumentsCategories.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of body styles
        /// </returns>
        public List<ProductDocumentsCategories> GetAllRecords()
        {
            return this._db.ProductDocumentsCategories
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
        public ProductDocumentsCategories GetRecord(long id)
        {
            return this._db.ProductDocumentsCategories
                           .AsNoTracking()
                           .FirstOrDefault(c => c.ProductDocumentsCategoriesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, ProductDocumentsCategories updatedItem)
        {
            ////Check Products pictures exists
            ProductDocumentsCategories item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.CategoryName = updatedItem.CategoryName;
            this._db.ProductDocumentsCategories.Update(item);
            this._db.SaveChanges();
        }
    }
}