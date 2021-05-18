// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Colors Controller API
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
    /// Colors Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : Controller
    {
        /// <summary>
        /// Gets or sets the colors repo.
        /// </summary>
        /// <value>
        /// The colors repo.
        /// </value>
        private readonly IColorsRepo _colorsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorsController"/> class.
        /// </summary>
        /// <param name="colorsRepo">The colors repo.</param>
        public ColorsController(IColorsRepo colorsRepo)
        {
            this._colorsRepo = colorsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of colors</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._colorsRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Color by id on Json format</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var item = this._colorsRepo.GetRecord(id);
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
        /// Color by id on Json format
        /// </returns>
        [HttpGet("GetForProduct/{productsId}")]
        [AllowAnonymous]
        public IActionResult GetForProduct(int productsId)
        {
            return this.Ok(this._colorsRepo.GetforProduct(productsId));
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Server status code</returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Colors item)
        {
            var colorCreated = this._colorsRepo.CreateRecord(item);
            if (colorCreated.GetType() != typeof(Colors))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(colorCreated);
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
                this._colorsRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] Colors updatedItem)
        {
            try
            {
                this._colorsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}