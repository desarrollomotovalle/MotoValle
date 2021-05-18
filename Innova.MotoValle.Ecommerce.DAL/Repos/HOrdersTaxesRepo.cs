// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HOrdersTaxes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Body Styles Repo implementations
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
    /// Body Styles Repo implementations
    /// </summary>
    public class HOrdersTaxesRepo : IHOrdersTaxesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HOrdersTaxesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public HOrdersTaxesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Body Styles created
        /// </returns>
        public HOrdersTaxes CreateRecord(HOrdersTaxes item)
        {
            this._db.HOrdersTaxes.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Creates the records.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>List of order details for order</returns>
        public List<HOrdersTaxes> CreateRecords(List<HOrdersTaxes> items)
        {
            this._db.HOrdersTaxes.AddRange(items);
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
                this._db.HOrdersTaxes.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of body styles
        /// </returns>
        public List<HOrdersTaxes> GetAllRecords()
        {
            return this._db.HOrdersTaxes
                       .AsNoTracking()
                       .ToList();
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <param name="ordersId"></param>
        /// <returns>
        /// List of H orders taxes
        /// </returns>
        public List<HOrdersTaxes> GetForOrder(long ordersId)
        {
            return this._db.HOrdersTaxes
                .AsNoTracking()
                .Where(x => x.FkOrdersId == ordersId).ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// body styles by id
        /// </returns>
        public HOrdersTaxes GetRecord(long id)
        {
            return this._db.HOrdersTaxes
                .AsNoTracking()
                .FirstOrDefault(c => c.HOrdersTaxesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, HOrdersTaxes updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.FkOrdersId = updatedItem.FkOrdersId;
            item.TaxName = updatedItem.TaxName;
            item.TaxPercent = updatedItem.TaxPercent;
            item.TaxTotal = updatedItem.TaxTotal;
            this._db.HOrdersTaxes.Update(item);
            this._db.SaveChanges();
        }
    }
}