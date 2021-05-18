// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICategoryRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Category Repo facade
    /// </summary>
    public interface ICategoryRepo
    {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>List of categories</returns>
        List<Categories> GetAllCategories();

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by Id</returns>
        Categories GetCategory(long id);

        /// <summary>
        /// Get category record by Models Id and make Id
        /// </summary>
        /// <param name="makesId">The identifier.</param>
        /// <param name="modelsId">The identifier.</param>
        /// <returns>Category by Models Id</returns>
        Categories GetCategoryForMakeAndModel(long makesId, long modelsId);

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>Category created</returns>
        Categories CreateCategory(Categories category);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateCategory">The update category.</param>
        void UpdateCategory(long id, Categories updateCategory);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCategory(long id);
    }
}