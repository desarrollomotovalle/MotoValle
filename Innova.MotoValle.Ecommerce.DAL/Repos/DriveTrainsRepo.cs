// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DriveTrainsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Drive Trains Repo Implementations
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
    /// Drive Trains Repo Implementations
    /// </summary>
    public class DriveTrainsRepo : IDriveTrainsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveTrainsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public DriveTrainsRepo(ecommerceContext db)
        {
            this._db = db;
        }
        
        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Drive Trains created
        /// </returns>
        public DriveTrains CreateRecord(DriveTrains item)
        {
            this._db.DriveTrains.Add(item);
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
                this._db.DriveTrains.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Drive Trains
        /// </returns>
        public List<DriveTrains> GetAllRecords()
        {
            return this._db.DriveTrains
                       .AsNoTracking()
                       .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Drive Trains by id
        /// </returns>
        public DriveTrains GetRecord(long id)
        {
            return this._db.DriveTrains
                       .AsNoTracking()
                       .FirstOrDefault(c => c.DriveTrainsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, DriveTrains updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.DriveTrainName = updatedItem.DriveTrainName;
            this._db.DriveTrains.Update(item);
            this._db.SaveChanges();
        }
    }
}