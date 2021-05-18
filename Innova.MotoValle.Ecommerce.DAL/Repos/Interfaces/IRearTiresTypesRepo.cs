// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRearTiresTypesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Rear Tires Types Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Rear Tires Types Repo facade
    /// </summary>
    public interface IRearTiresTypesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of body styles</returns>
        List<RearTiresTypes> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>body styles by id</returns>
        RearTiresTypes GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Rear Tires Types created</returns>
        RearTiresTypes CreateRecord(RearTiresTypes item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, RearTiresTypes updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
