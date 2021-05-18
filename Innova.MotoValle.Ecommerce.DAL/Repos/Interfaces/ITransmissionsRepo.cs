// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransmissionsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Transmissions Repo Facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Transmissions Repo Facade
    /// </summary>
    public interface ITransmissionsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Transmissions</returns>
        List<Transmissions> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Transmissions by id</returns>
        Transmissions GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Transmissions created</returns>
        Transmissions CreateRecord(Transmissions item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, Transmissions updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
