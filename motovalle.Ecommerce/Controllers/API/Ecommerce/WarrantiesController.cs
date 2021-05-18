// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WarrantiesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Warranties Controller API
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
    /// Warranties Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantiesController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The products repo.
        /// </value>
        private readonly IWarrantiesRepo _warrantiesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarrantiesController"/> class.
        /// </summary>
        /// <param name="warrantiesRepo">The makes repo.</param>
        public WarrantiesController(IWarrantiesRepo warrantiesRepo)
        {
            this._warrantiesRepo = warrantiesRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Warranties</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._warrantiesRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Warranties by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._warrantiesRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] Warranties item)
        {
            var warrantyCreated = this._warrantiesRepo.CreateRecord(item);
            if (warrantyCreated.GetType() != typeof(Warranties))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(warrantyCreated);
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
                this._warrantiesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] Warranties updatedItem)
        {
            try
            {
                this._warrantiesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}