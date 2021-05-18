// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryPicturesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Pictures Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Category Pictures Repo facade
    /// </summary>
    public interface ICategoryPicturesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of category pictures</returns>
        List<CategoryPictures> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category pictures by id</returns>
        CategoryPictures GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Category pictures</returns>
        CategoryPictures CreateRecord(CategoryPictures item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, CategoryPictures updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}