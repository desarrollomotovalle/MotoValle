// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EpaClassesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Epa Classes Controller API
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
    /// Epa Classes Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EpaClassesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpaClassesController" /> class.
        /// </summary>
        /// <param name="epaClassesRepo">The epa classes repo.</param>
        public EpaClassesController(IEpaClassesRepo epaClassesRepo)
        {
            this._epaClassesRepo = epaClassesRepo;
        }

        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        private IEpaClassesRepo _epaClassesRepo { get; set; }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Epa classes</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._epaClassesRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Epa Class by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._epaClassesRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] EpaClasses item)
        {
            var epaClassCreated = this._epaClassesRepo.CreateRecord(item);
            if (epaClassCreated.GetType() != typeof(EpaClasses))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(epaClassCreated);
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
                this._epaClassesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] EpaClasses updatedItem)
        {
            try
            {
                this._epaClassesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}