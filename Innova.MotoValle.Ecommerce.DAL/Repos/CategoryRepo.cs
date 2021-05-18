// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Repo implementations
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
    /// Category Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.ICategoryRepo" />
    public class CategoryRepo : ICategoryRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CategoryRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Add/Create category
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Category created</returns>
        public Categories CreateCategory(Categories item)
        {
            this._db.Categories.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Delete category by Id
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCategory(long id)
        {
            Categories item = this.GetCategory(id);
            if (item != null)
            {
                this._db.Categories.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>List of categories</returns>
        public List<Categories> GetAllCategories()
        {
            return this._db.Categories
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Get category record by Id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by Id</returns>
        public Categories GetCategory(long id)
        {
            return this._db.Categories
                       .AsNoTracking()
                       .FirstOrDefault(c => c.CategoriesId == id);
        }

        /// <summary>
        /// Get category by name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Category by name</returns>
        public Categories GetCategoryByName(string name)
        {
            return this._db.Categories
                       .AsNoTracking()
                       .FirstOrDefault(c => c.CategoryName == name);
        }

        /// <summary>
        /// Get category record by Models Id and make Id
        /// </summary>
        /// <param name="makesId">The identifier.</param>
        /// <param name="modelsId">The identifier.</param>
        /// <returns>Category by Models Id</returns>
        public Categories GetCategoryForMakeAndModel(long makesId, long modelsId)
        {
            return this._db.Models
                       .AsNoTracking()
                       .Include(x => x.FkCategories)
                       .FirstOrDefault(x => x.FkMakesId == makesId && x.ModelsId == modelsId)
                       .FkCategories;
        }

        /// <summary>
        /// Update record info
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateCategory(long id, Categories updatedRecord)
        {
            ////Check Category exists
            Categories item = this.GetCategory(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.CategoryName = updatedRecord.CategoryName;
            item.Description = updatedRecord.Description;
            this._db.Categories.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Boolean control</returns>
        private bool CategoryExists(long id)
        {
            return this._db.Categories.Any(c => c.CategoriesId == id);
        }
    }
}
