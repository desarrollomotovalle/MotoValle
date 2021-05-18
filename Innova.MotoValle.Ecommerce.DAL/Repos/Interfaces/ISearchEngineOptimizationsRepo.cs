// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchEngineOptimizationsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Search Engine Optimizations Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Search Engine Optimizations Repo facade
    /// </summary>
    public interface ISearchEngineOptimizationsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Search Engine Optimizations</returns>
        List<SearchEngineOptimizations> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Search Engine Optimizations by id</returns>
        SearchEngineOptimizations GetRecord(long id);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="pagePath">The page path.</param>
        /// <returns>Search Engine Optimizations by page path</returns>
        SearchEngineOptimizations GetRecordByPagePath(string pagePath);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Search Engine Optimizations created</returns>
        SearchEngineOptimizations CreateRecord(SearchEngineOptimizations item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, SearchEngineOptimizations updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
