// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Models Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Models Repo implementations
    /// </summary>
    public class ModelsRepo : IModelsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ModelsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Model created
        /// </returns>
        public Models CreateRecord(Models item)
        {
            this._db.Models.Add(item);
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
                this._db.Models.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of Models
        /// </returns>
        public List<Models> GetAllRecords()
        {
            return this._db.Models
                       .AsNoTracking()
                       .Include(x => x.FkCategories)
                       .Include(x => x.FkMakes)
                       .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Model by Id
        /// </returns>
        public Models GetRecord(long id)
        {
            return this._db.Models.AsNoTracking()
                       .Include(x => x.FkCategories)
                       .Include(x => x.FkMakes)
                       .FirstOrDefault(c => c.ModelsId == id);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Models updatedItem)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.ModelName = updatedItem.ModelName;
            item.FkCategoriesId = updatedItem.FkCategoriesId;
            item.FkMakesId = updatedItem.FkMakesId;
            this._db.Models.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the models for make.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="modelsId">The models identifier.</param>
        /// <returns>Models for make</returns>
        public IEnumerable<ModelAndCategoryBase> GetModelsForMake(int makesId = 0, int modelsId = 0)
        {
            return this._db.Models
                        .AsNoTracking()
                        .Where(x => x.FkMakesId == (makesId == 0 ? x.FkMakesId : makesId)
                                 && x.ModelsId == (modelsId == 0 ? x.ModelsId : modelsId))
                        .Select(x => new ModelAndCategoryBase()
                        {
                            CategoriesId = x.FkCategoriesId,
                            CategoryName = x.FkCategories.CategoryName,
                            MakeName = x.FkMakes.MakeName,
                            ModelName = x.ModelName,
                            ModelsId = x.ModelsId
                        });
        }

        /// <summary>
        /// Gets the models for category.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoriesId">The categories identifier.</param>
        /// <returns>
        /// List of models for category by id
        /// </returns>
        public IEnumerable<ModelAndCategoryBase> GetModelsForMakeAndCategory(int makesId, int categoriesId = 0)
        {
            return this._db.InventoryItems
                       .Include(x => x.FkProducts)
                       .AsNoTracking()
                       .Where(x => x.FkProducts.FkMakesId == makesId
                                && x.FkProducts.FkCategoriesId == (categoriesId == 0 ? x.FkProducts.FkCategoriesId : categoriesId)
                                && x.FkProducts.AllowShow > 0
                                && x.FkProducts.QuantityInStock > 0
                                && x.AllowShow > 0
                                && x.QuantityInStock > 0)
                       .Select(x => new ModelAndCategoryBase()
                       {
                           CategoriesId = x.FkProducts.FkCategoriesId ?? 0,
                           CategoryName = x.FkProducts.FkCategories.CategoryName,
                           MakeName = x.FkProducts.FkMakes.MakeName,
                           ModelName = x.FkProducts.FkModels.ModelName,
                           ModelsId = x.FkProducts.FkModelsId
                       }).Distinct();
        }


        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Model by Id
        /// </returns>
        private Models Get(long id)
        {
            return this._db.Models
                       .AsNoTracking()
                       .FirstOrDefault(c => c.ModelsId == id);
        }
    }
}