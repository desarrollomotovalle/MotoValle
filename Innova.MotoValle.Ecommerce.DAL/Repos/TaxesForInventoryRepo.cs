// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesForInventoryRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  TaxesForInventory Repo implementations
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
    /// TaxesForInventory Repo implementations
    /// </summary>
    public class TaxesForInventoryRepo : ITaxesForInventoryRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxesForInventoryForInventoryRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public TaxesForInventoryRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// TaxesForInventory created
        /// </returns>
        public TaxesForInventory CreateRecord(TaxesForInventory item)
        {
            this._db.TaxesForInventory.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Creates the records.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>
        /// Taxes For Inventory created
        /// </returns>
        public List<TaxesForInventory> CreateRecords(List<TaxesForInventory> items)
        {
            this._db.TaxesForInventory.AddRange(items);
            this._db.SaveChanges();
            return items;
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
                this._db.TaxesForInventory.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the records.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        public void DeleteRecords(long inventoryItemsId)
        {
            var items = this.GetAllRecordsForInventory(inventoryItemsId);
            if (items.Count > 0)
            {
                this._db.TaxesForInventory.RemoveRange(items);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of TaxesForInventory
        /// </returns>
        public List<TaxesForInventory> GetAllRecords()
        {
            return this._db.TaxesForInventory
                           .AsNoTracking()
                           .Include(x => x.FkInventoryItems)
                           .Include(x => x.FkTaxes)
                           .ToList();
        }

        /// <summary>
        /// Gets all records for inventory.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <returns>
        /// List of Taxes For Inventory
        /// </returns>
        public List<TaxesForInventory> GetAllRecordsForInventory(long inventoryItemsId)
        {
            return this._db.TaxesForInventory
                       .AsNoTracking()
                       .Include(x => x.FkInventoryItems)
                       .Include(x => x.FkTaxes)
                       .Where(x => x.FkInventoryItemsId == inventoryItemsId)
                       .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// TaxesForInventory by id
        /// </returns>
        public TaxesForInventory GetRecord(long id)
        {
            return this._db.TaxesForInventory
                           .AsNoTracking()
                           .Include(x => x.FkInventoryItems)
                           .Include(x => x.FkTaxes)
                           .FirstOrDefault(c => c.TaxesForInventoryId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, TaxesForInventory updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.FkInventoryItemsId = updatedItem.FkInventoryItemsId;
            item.FkTaxesId = updatedItem.FkTaxesId;
            this._db.TaxesForInventory.Update(item);
            this._db.SaveChanges();
        }


        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// TaxesForInventory by id
        /// </returns>
        private TaxesForInventory Get(long id)
        {
            return this._db.TaxesForInventory
                           .AsNoTracking()
                           .FirstOrDefault(c => c.TaxesForInventoryId == id);
        }
    }
}