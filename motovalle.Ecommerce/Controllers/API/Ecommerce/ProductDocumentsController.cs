// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsController.cs" company="Innova Marketing Systems S.A.S">
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
    public class ProductDocumentsController : Controller
    {
        /// <summary>
        /// Gets or sets the Product Documents Categories repo.
        /// </summary>
        /// <value>
        /// The Product Documents Categories repo.
        /// </value>
        private readonly IProductDocumentsRepo _productDocumentsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsController" /> class.
        /// </summary>
        /// <param name="productDocumentsRepo">The Product Documents Categories repo.</param>
        public ProductDocumentsController(IProductDocumentsRepo productDocumentsRepo)
        {
            this._productDocumentsRepo = productDocumentsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Product Documents Categories</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._productDocumentsRepo.GetAllRecords());
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
            var item = this._productDocumentsRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>
        /// Category by id on Json format
        /// </returns>
        [HttpGet("ForProduct/{productsId}")]
        [AllowAnonymous]
        public IActionResult GetForProduct(int productsId)
        {
            var item = this._productDocumentsRepo.GetRecordsForProduct(productsId);
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
        public IActionResult Create([FromBody] ProductDocuments item)
        {
            var bodyStylesCreated = this._productDocumentsRepo.CreateRecord(item);
            if (bodyStylesCreated.GetType() != typeof(ProductDocuments))
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
                this._productDocumentsRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] ProductDocuments updatedItem)
        {
            try
            {
                this._productDocumentsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}