// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Items Repo implementations
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
    /// Inventory Items Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IInventoryItemsRepo" />
    public class InventoryItemsRepo : IInventoryItemsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public InventoryItemsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Epa Class created
        /// </returns>
        public InventoryItems CreateRecord(InventoryItems item)
        {
            this._db.InventoryItems.Add(item);
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
                this._db.InventoryItems.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Inventory Items
        /// </returns>
        public List<InventoryItems> GetAllRecords()
        {
            return this._db.InventoryItems
                .AsNoTracking()
                .Include(i => i.FkColors)
                .Include(i => i.FkProducts)
                .Include(i => i.FkWarranties)
                .Include(i => i.TaxesForInventory)
                .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Inventory Items by id
        /// </returns>
        public InventoryItems GetRecord(long id)
        {
            return this._db.InventoryItems
                .AsNoTracking()
                .Include(i => i.FkColors)
                .Include(i => i.FkProducts)
                .Include(i => i.FkWarranties)
                .Include(i => i.OrderDetails)
                .Include(i => i.ShoppingCartRecords)
                .Include(i => i.InterestedCustomers)
                .Include(i => i.TaxesForInventory)
                .ThenInclude(t => t.FkTaxes)
                .FirstOrDefault(c => c.InventoryItemsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, InventoryItems updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.FkProductsId = updatedItem.FkProductsId;
            item.Vin = updatedItem.Vin;
            item.FkColorsId = updatedItem.FkColorsId;
            item.Cost = updatedItem.Cost;
            item.SalesPrice = updatedItem.SalesPrice;
            item.Mileage = updatedItem.Mileage;
            item.FkWarrantiesId = updatedItem.FkWarrantiesId;
            item.IsNew = updatedItem.IsNew;
            item.IsSold = updatedItem.IsSold;
            item.CoverPictureUrl = updatedItem.CoverPictureUrl;
            item.AllowShow = updatedItem.AllowShow;
            item.Cupix360Url = updatedItem.Cupix360Url;
            item.Picture360Url = updatedItem.Picture360Url;
            item.Picture360Quantity = updatedItem.Picture360Quantity;
            item.FkProductsId = updatedItem.FkProductsId;
            item.QuantityInStock = updatedItem.QuantityInStock;
            this._db.InventoryItems.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Inventory Items by id
        /// </returns>
        private InventoryItems Get(long id)
        {
            return this._db.InventoryItems
                .AsNoTracking()
                .FirstOrDefault(c => c.InventoryItemsId == id);
        }
    }
}