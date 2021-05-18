// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordsGuestRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Shopping Cart Records Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IShoppingCartRecordsGuestRepo" />
    public class ShoppingCartRecordsGuestRepo : IShoppingCartRecordsGuestRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartRecordsGuestRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ShoppingCartRecordsGuestRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Shopping Cart Records created</returns>
        public ShoppingCartRecordsGuest CreateRecord(ShoppingCartRecordsGuest item)
        {
            this._db.ShoppingCartRecordsGuest.Add(item);
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
                this._db.ShoppingCartRecordsGuest.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the records for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        public void DeleteRecordsForCustomer(string guestId)
        {
            var items = this.GetRecordsForCustumer(guestId);
            if (items.Count > 0)
            {
                this._db.ShoppingCartRecordsGuest.RemoveRange(items);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of shopping cart records</returns>
        public List<ShoppingCartRecordsGuest> GetAllRecords()
        {
            return this._db.ShoppingCartRecordsGuest
                       .AsNoTracking()
                       .Include(x => x.FkProducts)
                       .Include(x => x.FKInventoryItems)
                       .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Shopping cart record by id</returns>
        public ShoppingCartRecordsGuest GetRecord(long id)
        {
            return this._db.ShoppingCartRecordsGuest
                       .AsNoTracking()
                       .FirstOrDefault(c => c.ShoppingCartRecordsGuestId == id);
        }

        /// <summary>
        /// Gets the records for custumer.
        /// </summary>
        /// <param name="guestId">The guest identifier.</param>
        /// <returns>
        /// List of shopping cart records for customer
        /// </returns>
        public List<ShoppingCartRecordsGuest> GetRecordsForCustumer(string guestId)
        {
            return this._db.ShoppingCartRecordsGuest
                       .AsNoTracking()
                       .Where(x => x.GuestId == guestId)
                       .ToList();
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of Shopping cart view model</returns>
        public List<ShoppingCartCheckoutViewModel> GetShoppingCartCheckout(string guestId)
        {
            var taxesForInventory = this._db.TaxesForInventory
                                        .Include(x => x.FkInventoryItems)
                                        .Include(x => x.FkTaxes)
                                        .AsNoTracking();

            var shopingCartCheckoutViewModel = this._db.ShoppingCartRecordsGuest
                                                   .AsNoTracking()
                                                   .Include(x => x.FKInventoryItems)
                                                   .Include(x => x.FkProducts)
                                                   .Where(x => x.GuestId == guestId)
                                                   .OrderBy(x => x.FkProductsId)
                                                   .OrderBy(x => x.FkInventoryItemsId)
                                                   .Select(x => new ShoppingCartCheckoutViewModel()
                                                   {
                                                       Color = x.FKInventoryItems.FkColors.ColorName,
                                                       Doors = x.FkProducts.PassengerDoors ?? 0,
                                                       InventoryItemsId = x.FKInventoryItems.InventoryItemsId,
                                                       MakesId = x.FkProducts.FkMakesId,
                                                       Passengers = x.FkProducts.Passengers ?? 0,
                                                       PictureUrl = x.FKInventoryItems.CoverPictureUrl,
                                                       ProductId = x.FkProductsId,
                                                       ProductName = x.FkProducts.ProductName,
                                                       SalesPriceInventory = x.FKInventoryItems.SalesPrice ?? 0,
                                                       Shipping = 0,
                                                       ShortDescription = x.FkProducts.Description,
                                                       ShoppingCartRecordsId = x.ShoppingCartRecordsGuestId,
                                                       SubTotal = x.FKInventoryItems.SalesPrice ?? 0,
                                                       Total = 0,
                                                       TransmissionName = x.FkProducts.FkTransmissions.TransmissionName,
                                                       Year = x.FkProducts.Year
                                                   }).ToList();

            //Set Taxes For inventory
            foreach (var item in shopingCartCheckoutViewModel)
            {
                item.SalesPriceInventory = Math.Floor(item.SalesPriceInventory);
                item.Taxes = taxesForInventory.Where(x => x.FkInventoryItemsId == item.InventoryItemsId).Select(x => new TaxForInventoryWithValue()
                {
                    TaxName = x.FkTaxes.TaxName,
                    TaxPercent = x.FkTaxes.TaxPercent,
                    TaxValue = 0
                }).ToList();

                decimal totalTaxValue = 0;
                foreach (var tax in item.Taxes)
                {
                    tax.TaxValue = item.SubTotal * tax.TaxPercent / 100;
                    totalTaxValue += tax.TaxValue;
                }

                item.Total += Math.Ceiling(item.SubTotal + totalTaxValue);
                item.Total += item.Shipping;

                ////Set missing values
                if (item.Taxes.Sum(x => x.TaxValue) + item.SubTotal != item.Total)
                {
                    var tax = item.Taxes.FirstOrDefault();
                    var missing = item.Taxes.Sum(x => x.TaxValue) + item.SubTotal - item.Total;
                    tax.TaxValue += missing < 0 ? Math.Abs(missing) : missing;
                }
            }

            //Set Total Taxes Values
            return shopingCartCheckoutViewModel;
        }

        /// <summary>
        /// Gets the taxes information for shopping cart.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns></returns>
        public List<TaxesTotalViewModel> GetTaxesInfoForShoppingCart(string guestId)
        {
            var shoppingCartRecords = this._db.ShoppingCartRecordsGuest.AsNoTracking().Where(x => x.GuestId == guestId).ToList();
            var taxesForInventory = new List<TaxesForInventory>();
            var taxes = new List<Taxes>();
            foreach (var item in shoppingCartRecords)
            {
                var taxesForRecord = this._db.TaxesForInventory.Where(x => x.FkInventoryItemsId == item.FkInventoryItemsId).ToList();
                taxesForInventory.AddRange(taxesForRecord);
            }

            foreach (var item in taxesForInventory)
            {
                var taxesSubTotal = this._db.Taxes.AsNoTracking().Where(x => x.TaxesId == item.FkTaxesId);
                taxes.AddRange(taxesSubTotal);
            }

            var taxesNames = taxes.Select(x => x.TaxesId).Distinct().AsEnumerable().ToList();
            var taxesTotal = new List<TaxesTotalViewModel>();
            foreach (var item in taxesNames)
            {
                var taxInfo = this._db.Taxes.FirstOrDefault(x => x.TaxesId == item);
                taxesTotal.Add(
                    new TaxesTotalViewModel()
                    {
                        TaxesId = taxInfo.TaxesId,
                        TaxName = taxInfo.TaxName,
                        TaxPercent = taxInfo.TaxPercent,
                        TaxTotal = 0
                    });
            }

            return taxesTotal.ToList();
        }


        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, ShoppingCartRecordsGuest updatedRecord)
        {
            ////Check Product pictures exists
            ShoppingCartRecordsGuest item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.GuestId = updatedRecord.GuestId;
            item.FkProductsId = updatedRecord.FkProductsId;
            item.Quantity = updatedRecord.Quantity;
            item.FkInventoryItemsId = updatedRecord.FkInventoryItemsId;
            this._db.ShoppingCartRecordsGuest.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.ShoppingCartRecordsGuest.Any(c => c.ShoppingCartRecordsGuestId == id);
        }
    }
}
