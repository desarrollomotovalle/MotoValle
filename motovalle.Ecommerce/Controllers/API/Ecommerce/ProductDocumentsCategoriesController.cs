// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsCategoriesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Body Styles Controller API
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.Ecommerce
{
    using System;
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Body Styles Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDocumentsCategoriesController : Controller
    {
        /// <summary>
        /// Gets or sets the Product Documents Categories repo.
        /// </summary>
        /// <value>
        /// The Product Documents Categories repo.
        /// </value>
        private readonly IProductDocumentsCategoriesRepo _productDocumentsCategoriesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsCategoriesController" /> class.
        /// </summary>
        /// <param name="productDocumentsCategoriesRepo">The Product Documents Categories repo.</param>
        public ProductDocumentsCategoriesController(IProductDocumentsCategoriesRepo productDocumentsCategoriesRepo)
        {
            this._productDocumentsCategoriesRepo = productDocumentsCategoriesRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Product Documents Categories</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._productDocumentsCategoriesRepo.GetAllRecords());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by id on Json format</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var item = this._productDocumentsCategoriesRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] ProductDocumentsCategories item)
        {
            var bodyStylesCreated = this._productDocumentsCategoriesRepo.CreateRecord(item);
            if (bodyStylesCreated.GetType() != typeof(ProductDocumentsCategories))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(bodyStylesCreated);
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
                this._productDocumentsCategoriesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] ProductDocumentsCategories updatedItem)
        {
            try
            {
                this._productDocumentsCategoriesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}