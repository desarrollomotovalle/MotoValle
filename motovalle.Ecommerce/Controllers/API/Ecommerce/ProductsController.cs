// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Products Controller API
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.Ecommerce
{
    using System;
    using System.Collections.Generic;
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Products Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The products repo.
        /// </value>
        private readonly IProductsRepo _productsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productsRepo">The makes repo.</param>
        public ProductsController(IProductsRepo productsRepo)
        {
            this._productsRepo = productsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._productsRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>products by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._productsRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Server status code</returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Products item)
        {
            var productCreated = this._productsRepo.CreateRecord(item);
            if (productCreated.GetType() != typeof(Products))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(productCreated);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Server status code</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                this._productsRepo.DeleteRecord(id);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <returns>Server status code</returns>
        [HttpPut]
        [Route("Update/{id}")]
        [AllowAnonymous]
        public IActionResult Update(long id, [FromBody] Products updatedItem)
        {
            try
            {
                this._productsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }

        /// <summary>
        /// Gets the product with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>List of Products with details</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{productId}/Details")]
        public IActionResult GetProductWithDetails(int productId)
        {
            var item = this._productsRepo.GetProductWithDetails(productId);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the invetory for product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Product And Make Base
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{productId}/Make/{makesId}/Inventory")]
        public IEnumerable<InventoryForProduct> GetInvetoryForProduct(int productId, int makesId)
            => this._productsRepo.GetInventoryForProduct(productId, makesId);

        /// <summary>
        /// Gets the inventory and product base with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory item with details
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{productId}/Make/{makesId}/Inventory/{inventoryItemsId}/Details")]
        public IEnumerable<InventoryAndProductBaseWithDetails> GetInventoryAndProductBaseWithDetails(int productId, int inventoryItemsId, int makesId)
            => this._productsRepo.GetInventoryForProductWithDetails(productId, inventoryItemsId, makesId);

        /// <summary>
        /// Gets the products with enable maintenance by make.
        /// </summary>
        /// <param name="makesName">The identifier.</param>
        /// <returns>Products with maintenance by make</returns>
        [HttpGet("[action]/{makesName}")]
        [AllowAnonymous]
        public IEnumerable<Products> GetProductsWithEnableMaintenanceByMake(string makesName)
            => this._productsRepo.ProductsWithEnableMaintenanceByMake(makesName);
    }
}