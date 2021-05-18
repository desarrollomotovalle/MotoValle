// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchEngineOptimizationsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Search Engine Optimizations Repo implementations
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
    /// Search Engine Optimizations Repo implementations
    /// </summary>
    public class SearchEngineOptimizationsRepo : ISearchEngineOptimizationsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEngineOptimizationsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public SearchEngineOptimizationsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Search Engine Optimizations created
        /// </returns>
        public SearchEngineOptimizations CreateRecord(SearchEngineOptimizations item)
        {
            this._db.SearchEngineOptimizations.Add(item);
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
                this._db.SearchEngineOptimizations.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Search Engine Optimizations
        /// </returns>
        public List<SearchEngineOptimizations> GetAllRecords()
        {
            return this._db.SearchEngineOptimizations
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Search Engine Optimizations by id
        /// </returns>
        public SearchEngineOptimizations GetRecord(long id)
        {
            return this._db.SearchEngineOptimizations
                           .AsNoTracking()
                           .FirstOrDefault(c => c.SearchEngineOptimizationsId == id);
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="pagePath">The page path.</param>
        /// <returns>
        /// Search Engine Optimizations by page path
        /// </returns>
        public SearchEngineOptimizations GetRecordByPagePath(string pagePath)
        {
            return this._db.SearchEngineOptimizations.AsNoTracking().FirstOrDefault(x => x.PagePath == pagePath);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, SearchEngineOptimizations updatedItem)
        {
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.PagePath = updatedItem.PagePath;
            item.Title = updatedItem.Title;
            item.Description = updatedItem.Description;
            item.Keywords = updatedItem.Keywords;
            this._db.SearchEngineOptimizations.Update(item);
            this._db.SaveChanges();
        }
    }
}