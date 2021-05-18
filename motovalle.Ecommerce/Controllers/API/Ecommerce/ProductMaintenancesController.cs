// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductMaintenancesController.cs" company="Innova Marketing Systems S.A.S">
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
    public class ProductMaintenancesController : Controller
    {
        /// <summary>
        /// The hero images repo
        /// </summary>
        private readonly IProductMaintenancesRepo _productMaintenancesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMaintenancesController" /> class.
        /// </summary>
        /// <param name="productMaintenancesRepo">The Hero Images repo.</param>
        public ProductMaintenancesController(IProductMaintenancesRepo productMaintenancesRepo)
        {
            this._productMaintenancesRepo = productMaintenancesRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Hero Images</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._productMaintenancesRepo.GetAllRecords());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductMaintenances by id on Json format</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var item = this._productMaintenancesRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the enable records by make.
        /// </summary>
        /// <param name="makeName">The identifier.</param>
        /// <returns>
        /// ProductMaintenances by make on Json format
        /// </returns>
        [HttpGet("ByMake/{makeName}")]
        [AllowAnonymous]
        public IActionResult GetEnableRecordsByMake(string makeName)
        {
            var item = this._productMaintenancesRepo.GetEnableRecordsByMake(makeName);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the enable records by product.
        /// </summary>
        /// <param name="productsId">The identifier.</param>
        /// <returns>Product Maintenances by make on Json format</returns>
        [HttpGet("ByProduct/{productsId}/All/{onlyEnables}")]
        [AllowAnonymous]
        public IActionResult GetEnableRecordsByProduct(int productsId, bool onlyEnables = true)
        {
            var item = this._productMaintenancesRepo.GetEnableRecordsByProduct(productsId, onlyEnables);
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
        public IActionResult Create([FromBody] ProductMaintenances item)
        {
            var productMaintenancesCreated = this._productMaintenancesRepo.CreateRecord(item);
            if (productMaintenancesCreated.GetType() != typeof(ProductMaintenances))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(productMaintenancesCreated);
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
                this._productMaintenancesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] ProductMaintenances updatedItem)
        {
            try
            {
                this._productMaintenancesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}