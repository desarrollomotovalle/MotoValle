// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInventoryItemsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Items Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Inventory Items Repo facade
    /// </summary>
    public interface IInventoryItemsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Inventory Items</returns>
        List<InventoryItems> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns> Inventory Items by id</returns>
        InventoryItems GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Epa Class created</returns>
        InventoryItems CreateRecord(InventoryItems item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, InventoryItems updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}