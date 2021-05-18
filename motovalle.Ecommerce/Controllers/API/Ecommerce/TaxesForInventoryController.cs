// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesForInventoryForInventoryController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// TaxesForInventory For InventoryController Controller API
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.Ecommerce
{
    using System;
    using System.Collections.Generic;
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// TaxesForInventory For InventoryController Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesForInventoryController : Controller
    {
        /// <summary>
        /// The taxes for inventory repo
        /// </summary>
        private readonly ITaxesForInventoryRepo _taxesForInventoryRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxesForInventoryController" /> class.
        /// </summary>
        /// <param name="taxesForInventoryRepo">The body styles repo.</param>
        public TaxesForInventoryController(ITaxesForInventoryRepo taxesForInventoryRepo)
        {
            this._taxesForInventoryRepo = taxesForInventoryRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of body styles</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._taxesForInventoryRepo.GetAllRecords());
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
            var item = this._taxesForInventoryRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <returns>
        /// Category by id on Json format
        /// </returns>
        [HttpGet("ForInventory/{inventoryItemsId}")]
        [AllowAnonymous]
        public IActionResult GetForInventory(long inventoryItemsId)
        {
            var items = this._taxesForInventoryRepo.GetAllRecordsForInventory(inventoryItemsId);
            if (items == null)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Server status code</returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] TaxesForInventory item)
        {
            var taxesForInventoryCreated = this._taxesForInventoryRepo.CreateRecord(item);
            if (taxesForInventoryCreated.GetType() != typeof(TaxesForInventory))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(taxesForInventoryCreated);
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Server status code</returns>
        [HttpPost]
        [Route("Create/Massive")]
        public IActionResult Create([FromBody] List<TaxesForInventory> items)
        {
            var taxesForInventoryCreated = this._taxesForInventoryRepo.CreateRecords(items);
            if (taxesForInventoryCreated.GetType() != typeof(List<TaxesForInventory>))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(taxesForInventoryCreated);
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
                this._taxesForInventoryRepo.DeleteRecord(id);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <returns>
        /// Server status code
        /// </returns>
        [HttpDelete]
        [Route("Delete/ForInventory/{inventoryItemsId}")]
        public IActionResult DeleteForInventory(long inventoryItemsId)
        {
            try
            {
                this._taxesForInventoryRepo.DeleteRecords(inventoryItemsId);
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
        public IActionResult Update(long id, [FromBody] TaxesForInventory updatedItem)
        {
            try
            {
                this._taxesForInventoryRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}