// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MakesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Makes Controller API
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
    /// Makes Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        private readonly IMakesRepo _makesRepo;

        /// <summary>
        /// Gets or sets the models repo.
        /// </summary>
        /// <value>
        /// The models repo.
        /// </value>
        private readonly IModelsRepo _modelsRepo;

        /// <summary>
        /// Gets or sets the products repo.
        /// </summary>
        /// <value>
        /// The products repo.
        /// </value>
        private readonly IProductsRepo _productsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesController" /> class.
        /// </summary>
        /// <param name="makesRepo">The makes repo.</param>
        /// <param name="modelsRepo">The models repo.</param>
        /// <param name="productsRepo">The products repo.</param>
        public MakesController(IMakesRepo makesRepo, IModelsRepo modelsRepo, IProductsRepo productsRepo)
        {
            this._makesRepo = makesRepo;
            this._modelsRepo = modelsRepo;
            this._productsRepo = productsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of inventory item pictures</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._makesRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Inventory item pictures by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._makesRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the specified make name.
        /// </summary>
        /// <param name="makeName">Name of the make.</param>
        /// <returns>Make for name on Json Format</returns>
        [HttpGet("ByName/{makeName}")]
        [AllowAnonymous]
        public IActionResult Get(string makeName)
        {
            var item = this._makesRepo.GetRecord(makeName);
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
        public IActionResult Create([FromBody] Makes item)
        {
            var makeCreated = this._makesRepo.CreateRecord(item);
            if (makeCreated.GetType() != typeof(Makes))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(makeCreated);
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
                this._makesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] Makes updatedItem)
        {
            try
            {
                this._makesRepo.UpdateRecord(id, updatedItem);
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
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="modelsId">The models identifier.</param>
        /// <returns>
        /// Products by category Id
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{makesId}/models/{modelsId}")]
        public IEnumerable<ModelAndCategoryBase> GetModelsForMake(int makesId, int modelsId)
            => this._modelsRepo.GetModelsForMake(makesId, modelsId).ToList();

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoriesId">The categories identifier.</param>
        /// <returns>
        /// Products by category Id
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{makesId}/category/{categoriesId}")]
        public IEnumerable<ModelAndCategoryBase> GetModelsForMakeAndCateogry(int makesId, int categoriesId)
            => this._modelsRepo.GetModelsForMakeAndCategory(makesId, categoriesId).ToList();

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
        /// List of Product And Make Base
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{makesId}/category/{categoryId}/model/{modelId}/products")]
        public IEnumerable<ProductAndMakeBase> GetProductAndMakeBase(int makesId, int categoryId, int modelId, int minYear, int maxYear, decimal minPrice, decimal maxPrice)
            => this._productsRepo.GetProductAndMakeBase(makesId, categoryId, modelId, minYear, maxYear, minPrice, maxPrice);
    }
}