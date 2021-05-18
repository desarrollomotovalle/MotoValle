// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPicturesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Pictures Repo implementations
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
    /// Category Pictures Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.ICategoryPicturesRepo" />
    public class CategoryPicturesRepo : ICategoryPicturesRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryPicturesRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CategoryPicturesRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Create/Add record
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Category pictures</returns>
        public CategoryPictures CreateRecord(CategoryPictures item)
        {
            this._db.CategoryPictures.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            CategoryPictures item = this.GetRecord(id);
            if (item != null)
            {
                this._db.CategoryPictures.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records
        /// </summary>
        /// <returns>List of category pictures</returns>
        public List<CategoryPictures> GetAllRecords()
        {
            return this._db.CategoryPictures
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets record by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category pictures by id</returns>
        public CategoryPictures GetRecord(long id)
        {
            return this._db.CategoryPictures
                           .AsNoTracking()
                           .FirstOrDefault(c => c.CategoryPicturesId == id);
        }

        /// <summary>
        /// Gets record by name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Category pictures by name</returns>
        public CategoryPictures GetRecordByName(string name)
        {
            return this._db.CategoryPictures
                           .AsNoTracking()
                           .FirstOrDefault(c => c.PictureName == name);
        }

        /// <summary>
        /// Updates record info
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, CategoryPictures updatedRecord)
        {
            ////Check Category Pictures exists
            CategoryPictures item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.Description = updatedRecord.Description;
            item.FkCategoriesId = updatedRecord.FkCategoriesId;
            item.PictureName = updatedRecord.PictureName;
            item.PictureType = updatedRecord.PictureType;
            item.PictureUrl = updatedRecord.PictureUrl;
            this._db.CategoryPictures.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.CategoryPictures.Any(c => c.CategoryPicturesId == id);
        }
    }
}
