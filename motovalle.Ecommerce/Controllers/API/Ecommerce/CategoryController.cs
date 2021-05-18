// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Controller API
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.Ecommerce
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Category Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        private readonly ICategoryRepo _categoryRepo;

        /// <summary>
        /// Gets or sets the product repo.
        /// </summary>
        /// <value>
        /// The product repo.
        /// </value>
        private readonly IProductsRepo _productRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryRepo">The repo.</param>
        /// <param name="productRepo">The product repo.</param>
        public CategoryController(ICategoryRepo categoryRepo, IProductsRepo productRepo)
        {
            this._categoryRepo = categoryRepo;
            this._productRepo = productRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._categoryRepo.GetAllCategories());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by id on Json format</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var item = this._categoryRepo.GetCategory(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by id on Json format</returns>
        [HttpGet("Make/{makesId}/Model/{modelsId}")]
        [AllowAnonymous]
        public IActionResult GetForMakeAndModel(long makesId, long modelsId)
        {
            var item = this._categoryRepo.GetCategoryForMakeAndModel(makesId, modelsId);
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
        public IActionResult Create([FromBody] Categories item)
        {
            var categoryCreated = this._categoryRepo.CreateCategory(item);
            if (categoryCreated.GetType() != typeof(Categories))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(categoryCreated);
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
                this._categoryRepo.DeleteCategory(id);
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
        public IActionResult Update(long id, [FromBody] Categories updatedItem)
        {
            try
            {
                this._categoryRepo.UpdateCategory(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Products by category Id</returns>
        [HttpGet("{categoryId}/products")]
        [AllowAnonymous]
        public IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int categoryId)
            => this._productRepo.GetProductsForCategory(categoryId).ToList();
    }
}