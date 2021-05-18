// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemPicturesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Pictures Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Product Pictures Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IInventoryItemPicturesRepo" />
    public class InventoryItemPicturesRepo : IInventoryItemPicturesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemPicturesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public InventoryItemPicturesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product Pictures created</returns>
        public InventoryItemPictures CreateRecord(InventoryItemPictures item)
        {
            this._db.InventoryItemPictures.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            var item = this.GetRecord(id);
            if (item != null)
            {
                this._db.InventoryItemPictures.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes for inventory.
        /// </summary>
        /// <param name="inventoryItemsId">The identifier.</param>
        public void DeleteForInventory(long inventoryItemsId)
        {
            var items = this.GetRecordsForInventory(inventoryItemsId);
            if (items.Count > 0)
            {
                this._db.InventoryItemPictures.RemoveRange(items);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products pictures</returns>
        public List<InventoryItemPictures> GetAllRecords()
        {
            return this._db.InventoryItemPictures
                           .AsNoTracking()
                           .Include(x => x.FkInventoryItems)
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product pictures by Id</returns>
        public InventoryItemPictures GetRecord(long id)
        {
            return this._db.InventoryItemPictures
                           .AsNoTracking()
                           .Include(x => x.FkInventoryItems)
                           .FirstOrDefault(c => c.InventoryItemPicturesId == id);
        }

        /// <summary>
        /// Gets Records For Inventory
        /// </summary>
        /// <param name="inventoryItemsId">The identifier.</param>
        /// <returns>Product pictures by Id</returns>
        public List<InventoryItemPictures> GetRecordsForInventory(long inventoryItemsId)
        {
            return this._db.InventoryItemPictures
                           .AsNoTracking()
                           .Include(x => x.FkInventoryItems)
                           .Where(x => x.FkInventoryItemsId == inventoryItemsId)
                           .ToList();
        }

        /// <summary>
        /// Gets the name of the record by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Product pictures by name</returns>
        public InventoryItemPictures GetRecordByName(string name)
        {
            return this._db.InventoryItemPictures
                           .AsNoTracking()
                           .Include(x => x.FkInventoryItems)
                           .FirstOrDefault(c => c.PictureName == name);
        }

        /// <summary>
        /// Updates the Product pictures record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, InventoryItemPictures updatedRecord)
        {
            ////Check Product pictures exists
            var item = this.Get(id);
            if (item == null) 
            {
                throw new Exception("Record not found!");
            }

            item.FkInventoryItemsId = updatedRecord.FkInventoryItemsId;
            item.PictureName = updatedRecord.PictureName;
            item.PictureHeight = updatedRecord.PictureHeight;
            item.PictureWidth = updatedRecord.PictureWidth;
            item.PictureUrl = updatedRecord.PictureUrl;
            item.AlternateText = updatedRecord.AlternateText;
            this._db.InventoryItemPictures.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.InventoryItemPictures.Any(c => c.InventoryItemPicturesId == id);
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product pictures by Id</returns>
        private InventoryItemPictures Get(long id)
        {
            return this._db.InventoryItemPictures
                           .AsNoTracking()
                           .FirstOrDefault(c => c.InventoryItemPicturesId == id);
        }
    }
}
