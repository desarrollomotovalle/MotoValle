// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransmissionsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Transmissions Repo Implementations
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
    /// Transmissions Repo Implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.ITransmissionsRepo" />
    public class TransmissionsRepo : ITransmissionsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public TransmissionsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Transmissions created
        /// </returns>
        public Transmissions CreateRecord(Transmissions item)
        {
            this._db.Transmissions.Add(item);
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
                this._db.Transmissions.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Transmissions
        /// </returns>
        public List<Transmissions> GetAllRecords()
        {
            return this._db.Transmissions
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Transmissions by id
        /// </returns>
        public Transmissions GetRecord(long id)
        {
            return this._db.Transmissions
                           .AsNoTracking()
                           .FirstOrDefault(c => c.TransmissionsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Transmissions updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.TransmissionName = updatedItem.TransmissionName;
            this._db.Transmissions.Update(item);
            this._db.SaveChanges();
        }
    }
}