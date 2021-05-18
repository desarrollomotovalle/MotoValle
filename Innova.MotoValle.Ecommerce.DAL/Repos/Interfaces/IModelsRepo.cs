// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModelsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Models Repo Facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Models Repo Facade
    /// </summary>
    public interface IModelsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Models</returns>
        List<Models> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Model by Id</returns>
        Models GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Model created</returns>
        Models CreateRecord(Models item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, Models updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);

        /// <summary>
        /// Gets the models for category.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoriesId">The categories identifier.</param>
        /// <returns>
        /// List of models for category by id
        /// </returns>
        IEnumerable<ModelAndCategoryBase> GetModelsForMakeAndCategory(int makesId, int categoriesId = 0);

        /// <summary>
        /// Gets the models for make.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="modelsId">The models identifier.</param>
        /// <returns>List of models for make</returns>
        IEnumerable<ModelAndCategoryBase> GetModelsForMake(int makesId = 0, int modelsId = 0);
    }
}
