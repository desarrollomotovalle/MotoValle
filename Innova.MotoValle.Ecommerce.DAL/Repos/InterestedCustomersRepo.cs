// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterestedCustomersRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Interested Customers Repo Implementation
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
    /// Interested Customers Repo Implementation
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IInterestedCustomersRepo" />
    public class InterestedCustomersRepo : IInterestedCustomersRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterestedCustomersRepo" /> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public InterestedCustomersRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Interested customer created
        /// </returns>
        public InterestedCustomers CreateRecord(InterestedCustomers item)
        {
            this._db.InterestedCustomers.Add(item);
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
                this._db.InterestedCustomers.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Interested customers
        /// </returns>
        public List<InterestedCustomers> GetAllRecords()
        {
            return this._db.InterestedCustomers
                  .AsNoTracking()
                  .Include(x => x.FkInventoryItems.FkProducts)
                  .Include(x => x.FkInventoryItems.FkColors)
                  .Include(x => x.FkInventoryItems.FkWarranties)
                  .Include(x => x.FkInventoryItems)
                  .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Interested customers by id
        /// </returns>
        public InterestedCustomers GetRecord(long id)
        {
            return this._db.InterestedCustomers
                        .AsNoTracking()
                        .Include(x => x.FkInventoryItems)
                        .Include(x => x.FkInventoryItems.FkProducts)
                        .Include(x => x.FkInventoryItems.FkColors)
                        .Include(x => x.FkInventoryItems.FkWarranties)
                        .FirstOrDefault(c => c.InterestedCustomersId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, InterestedCustomers updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.CreateOn = updatedItem.CreateOn;
            item.Email = updatedItem.Email;
            item.FkInventoryItemsId = updatedItem.FkInventoryItemsId;
            item.Managed = updatedItem.Managed;
            item.Name = updatedItem.Name;
            item.PhoneNumber = updatedItem.PhoneNumber;
            item.ManagedOn = updatedItem.ManagedOn;
            item.ManagedFor = updatedItem.ManagedFor;
            item.Retake = updatedItem.Retake;
            item.Headquarter = updatedItem.Headquarter;
            this._db.InterestedCustomers.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Interested customers by id
        /// </returns>
        private InterestedCustomers Get(long id)
        {
            return this._db.InterestedCustomers
                        .AsNoTracking()
                        .FirstOrDefault(c => c.InterestedCustomersId == id);
        }
    }
}
