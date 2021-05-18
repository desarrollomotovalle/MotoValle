// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemPicturesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Item Pictures Controller API
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
    /// Inventory Item Pictures Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemPicturesController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        private readonly IInventoryItemPicturesRepo _inventoryItemPictures;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemPicturesController" /> class.
        /// </summary>
        /// <param name="inventoryItemPictures">The inventory item pictures.</param>
        public InventoryItemPicturesController(IInventoryItemPicturesRepo inventoryItemPictures)
        {
            this._inventoryItemPictures = inventoryItemPictures;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of inventory item pictures</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._inventoryItemPictures.GetAllRecords());

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of inventory item pictures</returns>
        [HttpGet("[action]/{inventoryItemsId}")]
        [AllowAnonymous]
        public IActionResult GetForInventory(long inventoryItemsId) => this.Ok(this._inventoryItemPictures.GetRecordsForInventory(inventoryItemsId));

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Inventory item pictures by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._inventoryItemPictures.GetRecord(id);
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
        public IActionResult Create([FromBody] InventoryItemPictures item)
        {
            var inventoryItemPictureCreated = this._inventoryItemPictures.CreateRecord(item);
            if (inventoryItemPictureCreated.GetType() != typeof(InventoryItemPictures))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(inventoryItemPictureCreated);
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
                this._inventoryItemPictures.DeleteRecord(id);
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
        /// <param name="id">The identifier.</param>
        /// <returns>Server status code</returns>
        [HttpDelete]
        [Route("Delete/ForInventory/{inventoryItemsId}")]
        public IActionResult ForInventory(long inventoryItemsId)
        {
            try
            {
                this._inventoryItemPictures.DeleteForInventory(inventoryItemsId);
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
        public IActionResult Update(long id, [FromBody] InventoryItemPictures updatedItem)
        {
            try
            {
                this._inventoryItemPictures.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}