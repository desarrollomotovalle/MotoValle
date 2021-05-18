// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Products Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Products Repo facade
    /// </summary>
    public interface IProductsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products</returns>
        List<Products> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product by Id</returns>
        Products GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product created</returns>
        Products CreateRecord(Products item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, Products updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of Product and category base by id</returns>
        IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int id);

        /// <summary>
        /// Gets the product with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>List of products with details</returns>
        IEnumerable<ProductWithDetails> GetProductWithDetails(int productId);

        /// <summary>
        /// Gets the product and make base.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="modelId">The model identifier.</param>
        /// <param name="minYear">The minimum year.</param>
        /// <param name="maxYear">The maximum year.</param>
        /// <param name="minPrice">The minimum price.</param>
        /// <param name="maxPrice">The maximum price.</param>
        /// <returns>
        /// List of products for make
        /// </returns>
        IEnumerable<ProductAndMakeBase> GetProductAndMakeBase(int makesId, int categoryId, int modelId, int minYear, int maxYear, decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Gets the inventory for product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory For Product
        /// </returns>
        IEnumerable<InventoryForProduct> GetInventoryForProduct(int productId, int makesId);

        /// <summary>
        /// Gets the inventory for product with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory And Product Base With Details
        /// </returns>
        IEnumerable<InventoryAndProductBaseWithDetails> GetInventoryForProductWithDetails(int productId, int inventoryItemsId, int makesId);

        /// <summary>
        /// Productses the with maintenance by make.
        /// </summary>
        /// <param name="makesName">Name of the makes.</param>
        /// <returns>
        /// List of products with maintenance
        /// </returns>
        IEnumerable<Products> ProductsWithEnableMaintenanceByMake(string makesName);
    }
}
