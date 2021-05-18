// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomersRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Customers Repo implementations
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
    /// Customers Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.ICustomersRepo" />
    public class CustomersRepo : ICustomersRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CustomersRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Create/Add customer record
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Customer created</returns>
        public Customers CreateRecord(Customers item)
        {
            this._db.Customers.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Delete record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            Customers item = this.GetRecord(id);
            if (item != null)
            {
                this._db.Customers.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Get all customer records
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customers> GetAllRecords()
        {
            return this._db.Customers
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets customer record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Customer by id</returns>
        public Customers GetRecord(long id)
        {
            return this._db.Customers
                           .AsNoTracking()
                           .FirstOrDefault(c => c.CustomersId == id);
        }

        /// <summary>
        /// Get customer record by name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Customer by name</returns>
        public Customers GetRecordByName(string name)
        {
            return this._db.Customers
                           .AsNoTracking()
                           .FirstOrDefault(c => c.FullName == name);
        }

        /// <summary>
        /// Updates record info
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Customers updatedRecord)
        {
            ////Check Customer exists
            Customers item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.AccountNumber = updatedRecord.AccountNumber;
            item.AlternateNumber = updatedRecord.AlternateNumber;
            item.BillToAddress = updatedRecord.BillToAddress;
            item.BillToCity = updatedRecord.BillToCity;
            item.BillToState = updatedRecord.BillToState;
            item.BillToZipcode = updatedRecord.BillToZipcode;
            item.EmailAddress = updatedRecord.EmailAddress;
            item.FullName = updatedRecord.FullName;
            item.PhoneNumber = updatedRecord.PhoneNumber;
            item.ShipToAddress = updatedRecord.ShipToAddress;
            item.ShipToCity = updatedRecord.ShipToCity;
            item.ShipToState = updatedRecord.ShipToState;
            item.ShipToZipcode = updatedRecord.ShipToZipcode;
            this._db.Customers.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.Customers.Any(c => c.CustomersId == id);
        }
    }
}
