// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMakesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Makes facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Makes facade
    /// </summary>
    public interface IMakesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products</returns>
        List<Makes> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product by Id</returns>
        Makes GetRecord(long id);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Product by Id</returns>
        Makes GetRecord(string name);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product created</returns>
        Makes CreateRecord(Makes item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, Makes updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
