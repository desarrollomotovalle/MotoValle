// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISeparationSystemsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Body Styles Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Body Styles Repo facade
    /// </summary>
    public interface ISeparationSystemsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of body styles</returns>
        List<SeparationSystems> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>body styles by id</returns>
        SeparationSystems GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Body Styles created</returns>
        SeparationSystems CreateRecord(SeparationSystems item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, SeparationSystems updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
