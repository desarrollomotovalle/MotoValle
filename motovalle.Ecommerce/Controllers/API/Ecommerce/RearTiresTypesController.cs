// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RearTiresTypesController.cs" company="Innova Marketing Systems S.A.S">
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
    public class RearTiresTypesController : Controller
    {
        /// <summary>
        /// Gets or sets the body styles repo.
        /// </summary>
        /// <value>
        /// The body styles repo.
        /// </value>
        private readonly IRearTiresTypesRepo _rearTiresTypesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RearTiresTypesController"/> class.
        /// </summary>
        /// <param name="rearTiresTypesRepo">The body styles repo.</param>
        public RearTiresTypesController(IRearTiresTypesRepo rearTiresTypesRepo)
        {
            this._rearTiresTypesRepo = rearTiresTypesRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of body styles</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._rearTiresTypesRepo.GetAllRecords());
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
            var item = this._rearTiresTypesRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] RearTiresTypes item)
        {
            var rearTiresTypesCreated = this._rearTiresTypesRepo.CreateRecord(item);
            if (rearTiresTypesCreated.GetType() != typeof(RearTiresTypes))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(rearTiresTypesCreated);
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
                this._rearTiresTypesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] RearTiresTypes updatedItem)
        {
            try
            {
                this._rearTiresTypesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}