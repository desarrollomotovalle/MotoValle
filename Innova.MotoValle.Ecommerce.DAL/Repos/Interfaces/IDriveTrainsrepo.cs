// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDriveTrainsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Drive Trains Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Drive Trains Repo facade
    /// </summary>
    public interface IDriveTrainsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Drive Trains</returns>
        List<DriveTrains> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Drive Trains by id</returns>
        DriveTrains GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Drive Trains created</returns>
        DriveTrains CreateRecord(DriveTrains item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, DriveTrains updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
