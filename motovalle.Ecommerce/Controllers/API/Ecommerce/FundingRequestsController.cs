// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FundingRequestController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Funding Request Controller API
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
    /// Funding Request Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FundingRequestsController : Controller
    {
        /// <summary>
        /// The funding requests repo
        /// </summary>
        private readonly IFundingRequestsRepo _fundingRequestsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FundingRequestsController"/> class.
        /// </summary>
        /// <param name="fundingRequestsRepo">The funding requests repo.</param>
        public FundingRequestsController(IFundingRequestsRepo fundingRequestsRepo)
        {
            this._fundingRequestsRepo = fundingRequestsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Funding requests</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._fundingRequestsRepo.GetAllRecords());
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
            var item = this._fundingRequestsRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets for orden number.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>Funding request by orderNumber on Json format</returns>
        [HttpGet("ForOrder/{id}")]
        [AllowAnonymous]
        public IActionResult GetForOrder(int ordersId)
        {
            var item = this._fundingRequestsRepo.GetRercordForOrder(ordersId);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets for orden number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>Funding request by orderNumber on Json format</returns>
        [HttpGet("ForOrderNuber/{id}")]
        [AllowAnonymous]
        public IActionResult GetForOrderNumber(int orderNumber)
        {
            var item = this._fundingRequestsRepo.GetRercordForOrderNumber(orderNumber);
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
        public IActionResult Create([FromBody] FundingRequests item)
        {
            var bodyStylesCreated = this._fundingRequestsRepo.CreateRecord(item);
            if (bodyStylesCreated.GetType() != typeof(FundingRequests))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(bodyStylesCreated);
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
                this._fundingRequestsRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] FundingRequests updatedItem)
        {
            try
            {
                this._fundingRequestsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}