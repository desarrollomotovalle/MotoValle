// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HOrdersTaxes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Body Styles Controller API
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.Ecommerce
{
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Body Styles Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class HOrdersTaxesController : Controller
    {
        /// <summary>
        /// Gets or sets the body styles repo.
        /// </summary>
        /// <value>
        /// The body styles repo.
        /// </value>
        private readonly IHOrdersTaxesRepo _hOrdersTaxesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HOrdersTaxes" /> class.
        /// </summary>
        /// <param name="bodyStylesRepo">The body styles repo.</param>
        public HOrdersTaxesController(IHOrdersTaxesRepo bodyStylesRepo)
        {
            this._hOrdersTaxesRepo = bodyStylesRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of body styles</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._hOrdersTaxesRepo.GetAllRecords());
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
            var item = this._hOrdersTaxesRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>
        /// Category by id on Json format
        /// </returns>
        [HttpGet("ForOrder/{ordersId}")]
        [AllowAnonymous]
        public IActionResult GetForOrder(int ordersId)
        {
            var item = this._hOrdersTaxesRepo.GetRecord(ordersId);
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
        [AllowAnonymous]
        public IActionResult Create([FromBody] HOrdersTaxes item)
        {
            var bodyStylesCreated = this._hOrdersTaxesRepo.CreateRecord(item);
            if (bodyStylesCreated.GetType() != typeof(HOrdersTaxes))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(bodyStylesCreated);
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Server status code</returns>
        [HttpPost]
        [Route("Create/Massive")]
        [AllowAnonymous]
        public IActionResult CreateMassive([FromBody] List<HOrdersTaxes> items)
        {
            var ordersDetailsCreated = this._hOrdersTaxesRepo.CreateRecords(items);
            if (ordersDetailsCreated.GetType() != typeof(List<HOrdersTaxes>))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(ordersDetailsCreated);
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
                this._hOrdersTaxesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] HOrdersTaxes updatedItem)
        {
            try
            {
                this._hOrdersTaxesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}