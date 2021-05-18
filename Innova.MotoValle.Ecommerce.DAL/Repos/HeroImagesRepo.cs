// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroImagesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Hero Images Repo implementations
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
    /// Hero Images Repo implementations
    /// </summary>
    public class HeroImagesRepo : IHeroImagesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeroImagesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public HeroImagesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Hero Images created
        /// </returns>
        public HeroImages CreateRecord(HeroImages item)
        {
            this._db.HeroImages.Add(item);
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
                this._db.HeroImages.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of body styles
        /// </returns>
        public List<HeroImages> GetAllRecords()
        {
            return this._db.HeroImages
                           .Include(x => x.FkMakes)
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the enable records.
        /// </summary>
        /// <returns>
        /// List Of Hero Images
        /// </returns>
        public List<HeroImages> GetEnableRecords()
        {
            return this._db.HeroImages
                           .Include(x => x.FkMakes)
                           .AsNoTracking()
                           .Where(x => x.Enable)
                           .ToList();
        }

        /// <summary>
        /// Gets the home record.
        /// </summary>
        /// <returns></returns>
        public HeroImages GetHomeRecord(int? makesId = null)
        {
            return this._db.HeroImages
                           .Include(x => x.FkMakes)
                           .AsNoTracking()
                           .FirstOrDefault(x => x.Enable && x.FkMakesId == makesId);
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// body styles by id
        /// </returns>
        public HeroImages GetRecord(long id)
        {
            return this._db.HeroImages
                           .Include(x => x.FkMakes)
                           .AsNoTracking()
                           .FirstOrDefault(c => c.HeroImagesId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, HeroImages updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.ImageURL = updatedItem.ImageURL;
            item.HasButton = updatedItem.HasButton;
            item.ButtonURL = updatedItem.ButtonURL;
            item.Enable = updatedItem.Enable;
            item.Index = updatedItem.Index;
            item.ShowSpan = updatedItem.ShowSpan;
            item.SpanText = updatedItem.SpanText;
            item.SpanComplementText = updatedItem.SpanComplementText;
            item.FkMakesId = updatedItem.FkMakesId;

            this._db.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Hero Images </returns>
        private HeroImages Get(long id)
        {
            return this._db.HeroImages
                .AsNoTracking()
                .FirstOrDefault(c => c.HeroImagesId == id);
        }
    }
}