// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEpaClassesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Epa Classes Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using System.Collections.Generic;
    using Ecommerce.Models.Entities;

    /// <summary>
    /// Epa Classes Repo facade
    /// </summary>
    public interface IEpaClassesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Epa Classes</returns>
        List<EpaClasses> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Epa Class by id</returns>
        EpaClasses GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Epa Class created</returns>
        EpaClasses CreateRecord(EpaClasses item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, EpaClasses updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
