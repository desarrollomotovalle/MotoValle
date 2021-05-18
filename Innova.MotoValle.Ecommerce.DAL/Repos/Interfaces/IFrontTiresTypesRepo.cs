// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFrontTiresTypesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Front Tires Types Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Front Tires Types Repo facade
    /// </summary>
    public interface IFrontTiresTypesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of body styles</returns>
        List<FrontTiresTypes> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>body styles by id</returns>
        FrontTiresTypes GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Front Tires Types created</returns>
        FrontTiresTypes CreateRecord(FrontTiresTypes item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, FrontTiresTypes updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
