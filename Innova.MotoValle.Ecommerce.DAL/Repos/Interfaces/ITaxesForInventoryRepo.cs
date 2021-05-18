// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaxesForInventoryRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Taxes For Inventory Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Taxes For Inventory Repo facade
    /// </summary>
    public interface ITaxesForInventoryRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Taxes For Inventory</returns>
        List<TaxesForInventory> GetAllRecords();

        /// <summary>
        /// Gets all records for inventory.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <returns>List of Taxes For Inventory</returns>
        List<TaxesForInventory> GetAllRecordsForInventory(long inventoryItemsId);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Taxes For Inventory by id</returns>
        TaxesForInventory GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Taxes For Inventory created</returns>
        TaxesForInventory CreateRecord(TaxesForInventory item);

        /// <summary>
        /// Creates the records.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>Taxes For Inventory created</returns>
        List<TaxesForInventory> CreateRecords(List<TaxesForInventory> items);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, TaxesForInventory updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);

        /// <summary>
        /// Deletes the records.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        void DeleteRecords(long inventoryItemsId);
    }
}
