// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAspirationTypesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Aspiration Types Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Aspiration Types Repo facade
    /// </summary>
    public interface IAspirationTypesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of body styles</returns>
        List<AspirationTypes> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>body styles by id</returns>
        AspirationTypes GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Aspiration Types created</returns>
        AspirationTypes CreateRecord(AspirationTypes item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, AspirationTypes updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
