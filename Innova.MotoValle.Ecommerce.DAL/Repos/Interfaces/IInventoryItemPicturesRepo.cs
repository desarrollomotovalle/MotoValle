// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInventoryItemPicturesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Pictures Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Product Pictures Repo facade
    /// </summary>
    public interface IInventoryItemPicturesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products pictures</returns>
        List<InventoryItemPictures> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product pictures by Id</returns>
        InventoryItemPictures GetRecord(long id);

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products pictures</returns>
        List<InventoryItemPictures> GetRecordsForInventory(long inventoryItemsId);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product Pictures created</returns>
        InventoryItemPictures CreateRecord(InventoryItemPictures item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, InventoryItemPictures updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);

        /// <summary>
        /// Deletes for Inventory
        /// </summary>
        /// <param name="inventoryItemsId">The identifier.</param>
        void DeleteForInventory(long inventoryItemsId);
    }
}
