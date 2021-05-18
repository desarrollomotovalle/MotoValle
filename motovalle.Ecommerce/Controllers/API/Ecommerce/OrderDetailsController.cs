// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDetailsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order Details Controller API
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
    /// Order Details Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : Controller
    {
        /// <summary>
        /// Gets or sets the repo.
        /// </summary>
        /// <value>
        /// The repo.
        /// </value>
        private readonly IOrderDetailsRepo _orderDetailsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsController" /> class.
        /// </summary>
        /// <param name="orderDetailsRepo">The inventory item pictures.</param>
        public OrderDetailsController(IOrderDetailsRepo orderDetailsRepo)
        {
            this._orderDetailsRepo = orderDetailsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of order details</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => this.Ok(this._orderDetailsRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order details by id</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var item = this._orderDetailsRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>Orders details for order</returns>
        [HttpGet("GetForOrder/{ordersId}")]
        [AllowAnonymous]
        public IActionResult GetForOrder(long ordersId)
        {
            var items = this._orderDetailsRepo.GetRecordsForOrder(ordersId);
            if (items.Count <= 0)
            {
                return this.NotFound();
            }

            return this.Json(items);
        }

        /// <summary>
        /// Gets the record for identifier and customer.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List Orders details for Id and customer</returns>
        [HttpGet("{ordersId}/Customer/{customersId}")]
        [AllowAnonymous]
        public IActionResult GetRecordForIdAndCustomer(long ordersId, long customersId)
        {
            var items = this._orderDetailsRepo.GetRecordForIdAndCustomer(ordersId, customersId);
            if (items.Count <= 0)
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
        public IActionResult Create([FromBody] OrderDetails item)
        {
            var orderDetailsCreated = this._orderDetailsRepo.CreateRecord(item);
            if (orderDetailsCreated.GetType() != typeof(OrderDetails))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(orderDetailsCreated);
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Server status code</returns>
        [HttpPost]
        [Route("Create/Massive")]
        [AllowAnonymous]
        public IActionResult CreateMassive([FromBody] List<OrderDetails> items)
        {
            var ordersDetailsCreated = this._orderDetailsRepo.CreateRecords(items);
            if (ordersDetailsCreated.GetType() != typeof(List<OrderDetails>))
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
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(long id)
        {
            try
            {
                this._orderDetailsRepo.DeleteRecord(id);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }

        /// <summary>
        /// Deletes for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>Server status</returns>
        [HttpDelete]
        [Route("Delete/ForOrder/{ordersId}")]
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteForOrder(long ordersId)
        {
            try
            {
                this._orderDetailsRepo.DeleteRecordsForOrder(ordersId);
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
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(long id, [FromBody] OrderDetails updatedItem)
        {
            try
            {
                this._orderDetailsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}