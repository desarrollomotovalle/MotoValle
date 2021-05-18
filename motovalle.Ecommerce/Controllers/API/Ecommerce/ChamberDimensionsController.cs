// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChamberDimensionsController.cs" company="Innova Marketing Systems S.A.S">
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
    public class ChamberDimensionsController : Controller
    {
        /// <summary>
        /// Gets or sets the body styles repo.
        /// </summary>
        /// <value>
        /// The body styles repo.
        /// </value>
        private readonly IChamberDimensionsRepo _chamberDimensionsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChamberDimensionsController"/> class.
        /// </summary>
        /// <param name="chamberDimensionsRepo">The body styles repo.</param>
        public ChamberDimensionsController(IChamberDimensionsRepo chamberDimensionsRepo)
        {
            this._chamberDimensionsRepo = chamberDimensionsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of body styles</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._chamberDimensionsRepo.GetAllRecords());
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
            var item = this._chamberDimensionsRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] ChamberDimensions item)
        {
            var chamberDimensionsCreated = this._chamberDimensionsRepo.CreateRecord(item);
            if (chamberDimensionsCreated.GetType() != typeof(ChamberDimensions))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(chamberDimensionsCreated);
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
                this._chamberDimensionsRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] ChamberDimensions updatedItem)
        {
            try
            {
                this._chamberDimensionsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}