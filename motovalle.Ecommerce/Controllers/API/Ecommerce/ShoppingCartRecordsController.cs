// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records Controller API
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
    /// Shopping Cart Records Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartRecordsController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The shopping cart records repo.
        /// </value>
        private readonly IShoppingCartRecordsRepo _shoppingCartRecordsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartRecordsController" /> class.
        /// </summary>
        /// <param name="shoppingCartRecordsRepo">The makes repo.</param>
        public ShoppingCartRecordsController(IShoppingCartRecordsRepo shoppingCartRecordsRepo)
        {
            this._shoppingCartRecordsRepo = shoppingCartRecordsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Shopping cart records</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._shoppingCartRecordsRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Shopping cart record by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._shoppingCartRecordsRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Shopping cart checkout view model for customer</returns>
        [HttpGet("Checkout/{customersId}")]
        [AllowAnonymous]
        public IActionResult GetShoppingCartCheckout(long customersId)
        {
            var items = this._shoppingCartRecordsRepo.GetShoppingCartCheckout(customersId);
            if (items == null)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Shopping cart checkout view model for customer</returns>
        [HttpGet("TaxesInfo/{customersId}")]
        [AllowAnonymous]
        public IActionResult GetTaxesInfoForShoppingCart(long customersId)
        {
            var items = this._shoppingCartRecordsRepo.GetTaxesInfoForShoppingCart(customersId);
            if (items == null)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Gets for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of shopping cart records for customer</returns>
        [HttpGet("ForCustomer/{customersId}")]
        [AllowAnonymous]
        public IActionResult GetForCustomer(long customersId) => this.Ok(this._shoppingCartRecordsRepo.GetRecordsForCustumer(customersId));

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Shopping cart created</returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] ShoppingCartRecords item)
        {
            var shoppingCartRecordCreated = this._shoppingCartRecordsRepo.CreateRecord(item);
            if (shoppingCartRecordCreated.GetType() != typeof(ShoppingCartRecords))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(shoppingCartRecordCreated);
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
                this._shoppingCartRecordsRepo.DeleteRecord(id);
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
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Server status code</returns>
        [HttpDelete]
        [Route("Delete/ForCustomer/{customersId}")]
        public IActionResult DeleteForCustomer(long customersId)
        {
            try
            {
                this._shoppingCartRecordsRepo.DeleteRecordsForCustomer(customersId);
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
        public IActionResult Update(long id, [FromBody] ShoppingCartRecords updatedItem)
        {
            try
            {
                this._shoppingCartRecordsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}