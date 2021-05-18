// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDetailsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order Details Repo implementations
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
    /// Order Details Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IOrderDetailsRepo" />
    public class OrderDetailsRepo : IOrderDetailsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public OrderDetailsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Create/Add Order details records
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Order details created</returns>
        public OrderDetails CreateRecord(OrderDetails item)
        {
            this._db.OrderDetails.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Creates the records.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>List of order details for order</returns>
        public List<OrderDetails> CreateRecords(List<OrderDetails> items)
        {
            this._db.OrderDetails.AddRange(items);
            this._db.SaveChanges();
            return items;
        }

        /// <summary>
        /// Delete Order details records
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            OrderDetails item = this.GetRecord(id);
            if (item != null)
            {
                this._db.OrderDetails.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        public void DeleteRecordsForOrder(long ordersId)
        {
            List<OrderDetails> items = this.GetRecordsForOrder(ordersId);
            if (items.Count > 0)
            {
                this._db.OrderDetails.RemoveRange(items);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Get all Order details records
        /// </summary>
        /// <returns>List of order details</returns>
        public List<OrderDetails> GetAllRecords()
        {
            return this._db.OrderDetails
                           .Include(x => x.FkProducts)
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Get order details record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order details by Id</returns>
        public OrderDetails GetRecord(long id)
        {
            return this._db.OrderDetails
                       .Include(x => x.FkProducts)
                       .AsNoTracking()
                       .FirstOrDefault(c => c.OrderDetailsId == id);
        }

        /// <summary>
        /// Gets the record for identifier and customer.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Order details for id and customer</returns>
        public List<OrderDetails> GetRecordForIdAndCustomer(long ordersId, long customersId)
        {
            return this._db.OrderDetails
                       .AsNoTracking()
                       .Where(c => c.FkOrdersId == ordersId
                                && c.FkOrders.FkCustomersId == customersId).ToList();
        }

        /// <summary>
        /// Gets the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>List of order details for order</returns>
        public List<OrderDetails> GetRecordsForOrder(long ordersId)
        {
            return this._db.OrderDetails.AsNoTracking()
                       .Where(x => x.FkOrdersId == ordersId)
                       .ToList();
        }

        /// <summary>
        /// Updates Order details record info
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, OrderDetails updatedRecord)
        {
            ////Check Order Details exists
            OrderDetails item = this.Get(id);
            if (item == null) 
            {
                throw new Exception("Record not found!");
            }

            item.FkOrdersId = updatedRecord.FkOrdersId;
            item.FkProductsId = updatedRecord.FkProductsId;
            item.FKInventoryItemsId = updatedRecord.FKInventoryItemsId;
            item.ItemPrice = updatedRecord.ItemPrice;
            item.Notes = updatedRecord.Notes;
            item.Quantity = updatedRecord.Quantity;
            item.ShipDate = updatedRecord.ShipDate;
            item.Tax = updatedRecord.Tax;
            this._db.OrderDetails.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.OrderDetails.Any(c => c.OrderDetailsId == id);
        }

        /// <summary>
        /// Get order details record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order details by Id</returns>
        private OrderDetails Get(long id)
        {
            return this._db.OrderDetails
                       .Include(x => x.FkProducts)
                       .AsNoTracking()
                       .FirstOrDefault(c => c.OrderDetailsId == id);
        }
    }
}