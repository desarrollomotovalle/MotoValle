// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductDocumentsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Product Documents Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Product Documents Repo facade
    /// </summary>
    public interface IProductDocumentsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Product Documents</returns>
        List<ProductDocuments> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Documents by id</returns>
        ProductDocuments GetRecord(long id);

        /// <summary>
        /// Gets the records for product.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>List of Product Documents</returns>
        List<ProductDocuments> GetRecordsForProduct(long productsId);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product Documents created</returns>
        ProductDocuments CreateRecord(ProductDocuments item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, ProductDocuments updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
