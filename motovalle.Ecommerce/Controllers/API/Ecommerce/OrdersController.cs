// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders Controller API
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
    /// Orders Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        private readonly IOrdersRepo _ordersRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="ordersRepo">The makes repo.</param>
        public OrdersController(IOrdersRepo ordersRepo)
        {
            this._ordersRepo = ordersRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of orders</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._ordersRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Inventory item pictures by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._ordersRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Orders for customer</returns>
        [HttpGet("ForCustomer/{customersId}")]
        [AllowAnonymous]
        public IActionResult GetForCustomer(long customersId)
        {
            var items = this._ordersRepo.GetRecordsForCustomer(customersId);
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
        /// <returns>Orders for customer</returns>
        [HttpGet("ForOrderNumber/{orderNumber}")]
        [AllowAnonymous]
        public IActionResult GetForOrderNumber(long orderNumber)
        {
            var items = this._ordersRepo.GetRecordForOrderNumber(orderNumber);
            if (items == null)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Gets for customer.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// Orders for customer
        /// </returns>
        [HttpGet("ForTransaction/{transactionId}")]
        [AllowAnonymous]
        public IActionResult GetForTransaction(string transactionId)
        {
            var items = this._ordersRepo.GetRecordForTransactionId(transactionId);
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
        /// <returns>Orders for customer</returns>
        [HttpGet("GetDetailsWithProductInfo/{ordersId}/Customer/{customersId}")]
        [AllowAnonymous]
        public IActionResult GetDetailsWithProductInfo(long ordersId, long customersId)
        {
            var items = this._ordersRepo.GetOrderWithDetailsAndProductInfo(ordersId, customersId);
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
        [AllowAnonymous]
        public IActionResult Create([FromBody] Orders item)
        {
            var orderCreated = this._ordersRepo.CreateRecord(item);
            if (orderCreated.GetType() != typeof(Orders))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(orderCreated);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Server status code</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [AllowAnonymous]
        public IActionResult Delete(long id)
        {
            try
            {
                this._ordersRepo.DeleteRecord(id);
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
        [AllowAnonymous]
        public IActionResult Update(long id, [FromBody] Orders updatedItem)
        {
            try
            {
                this._ordersRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}