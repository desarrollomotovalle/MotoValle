// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductDocumentsCategories.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Product Documents Categories Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Product Documents Categories Repo facade
    /// </summary>
    public interface IProductDocumentsCategoriesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of body styles</returns>
        List<ProductDocumentsCategories> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Documents Categories by id</returns>
        ProductDocumentsCategories GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product Documents Categories created</returns>
        ProductDocumentsCategories CreateRecord(ProductDocumentsCategories item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, ProductDocumentsCategories updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
