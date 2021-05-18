// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Documents Repo implementations
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
    /// Product Documents Repo implementations
    /// </summary>
    public class ProductDocumentsRepo : IProductDocumentsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ProductDocumentsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Product Documents created
        /// </returns>
        public ProductDocuments CreateRecord(ProductDocuments item)
        {
            this._db.ProductDocuments.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            ProductDocuments item = this.GetRecord(id);
            if (item != null)
            {
                this._db.ProductDocuments.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of body styles
        /// </returns>
        public List<ProductDocuments> GetAllRecords()
        {
            return this._db.ProductDocuments
                           .AsNoTracking()
                           .Include(x => x.FkProductDocumentsCategories)
                           .ToList();
        }

        /// <summary>
        /// Gets the records for product.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>
        /// List of Product Documents
        /// </returns>
        public List<ProductDocuments> GetRecordsForProduct(long productsId)
        {
            return this._db.ProductDocuments
                           .AsNoTracking()
                           .Include(x => x.FkProductDocumentsCategories)
                           .Where(x => x.FkProductsId == productsId)
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// body styles by id
        /// </returns>
        public ProductDocuments GetRecord(long id)
        {
            return this._db.ProductDocuments
                           .AsNoTracking()
                           .Include(x => x.FkProductDocumentsCategories)
                           .FirstOrDefault(c => c.ProductDocumentsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, ProductDocuments updatedItem)
        {
            ////Check Products pictures exists
            ProductDocuments item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.FileName = updatedItem.FileName;
            item.FilePath = updatedItem.FilePath;
            item.FkProductDocumentsCategoriesId = updatedItem.FkProductDocumentsCategoriesId;
            item.FkProductsId = updatedItem.FkProductsId;
            this._db.ProductDocuments.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// body styles by id
        /// </returns>
        private ProductDocuments Get(long id)
        {
            return this._db.ProductDocuments
                           .AsNoTracking()
                           .FirstOrDefault(c => c.ProductDocumentsId == id);
        }
    }
}