// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordsGuestController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records Controller API
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

    /// <summary>
    /// Shopping Cart Records Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ShoppingCartRecordsGuestController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The shopping cart records repo.
        /// </value>
        private readonly IShoppingCartRecordsGuestRepo _shoppingCartRecordsGuestRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartRecordsGuestController" /> class.
        /// </summary>
        /// <param name="shoppingCartRecordsGuestRepo">The makes repo.</param>
        public ShoppingCartRecordsGuestController(IShoppingCartRecordsGuestRepo shoppingCartRecordsGuestRepo)
        {
            this._shoppingCartRecordsGuestRepo = shoppingCartRecordsGuestRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Shopping cart records</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this._shoppingCartRecordsGuestRepo.GetAllRecords());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Shopping cart record by id</returns>
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var item = this._shoppingCartRecordsGuestRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        /// <returns>Shopping cart checkout view model for customer</returns>
        [HttpGet("Checkout/{guestId}")]
        public IActionResult GetShoppingCartCheckout(string guestId)
        {
            var items = this._shoppingCartRecordsGuestRepo.GetShoppingCartCheckout(guestId);
            if (items == null)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        /// <returns>Shopping cart checkout view model for customer</returns>
        [HttpGet("TaxesInfo/{guestId}")]
        public IActionResult GetTaxesInfoForShoppingCart(string guestId)
        {
            var items = this._shoppingCartRecordsGuestRepo.GetTaxesInfoForShoppingCart(guestId);
            if (items == null)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Gets for customer.
        /// </summary>
        /// <param name="guestId">The guest identifier.</param>
        /// <returns>
        /// List of shopping cart records for customer
        /// </returns>
        [HttpGet("ForCustomer/{guestId}")]
        public IActionResult GetForCustomer(string guestId)
        {
            return this.Ok(this._shoppingCartRecordsGuestRepo.GetRecordsForCustumer(guestId));
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Shopping cart created</returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] ShoppingCartRecordsGuest item)
        {
            var shoppingCartRecordGuestCreated = this._shoppingCartRecordsGuestRepo.CreateRecord(item);
            if (shoppingCartRecordGuestCreated.GetType() != typeof(ShoppingCartRecordsGuest))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(shoppingCartRecordGuestCreated);
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
                this._shoppingCartRecordsGuestRepo.DeleteRecord(id);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }

        /// <summary>
        /// Deletes for customer.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        /// <returns>Server status code</returns>
        [HttpDelete]
        [Route("Delete/ForCustomer/{guestId}")]
        public IActionResult DeleteForCustomer(string guestId)
        {
            try
            {
                this._shoppingCartRecordsGuestRepo.DeleteRecordsForCustomer(guestId);
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
        public IActionResult Update(long id, [FromBody] ShoppingCartRecordsGuest updatedItem)
        {
            try
            {
                this._shoppingCartRecordsGuestRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}