// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterestedCustomersController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Interested Customers Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.Ecommerce
{
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Interested Customers Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InterestedCustomersController : Controller
    {
        /// <summary>
        /// The interested customers
        /// </summary>
        private readonly IInterestedCustomersRepo _interestedCustomers;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterestedCustomersController"/> class.
        /// </summary>
        /// <param name="interestedCustomers">The interested customers.</param>
        public InterestedCustomersController(IInterestedCustomersRepo interestedCustomers)
        {
            this._interestedCustomers = interestedCustomers;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of body styles</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this._interestedCustomers.GetAllRecords());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by id on Json format</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = this._interestedCustomers.GetRecord(id);
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
        public IActionResult Create([FromBody] InterestedCustomers item)
        {
            var bodyStylesCreated = this._interestedCustomers.CreateRecord(item);
            if (bodyStylesCreated.GetType() != typeof(InterestedCustomers))
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
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(long id)
        {
            try
            {
                this._interestedCustomers.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] InterestedCustomers updatedItem)
        {
            try
            {
                this._interestedCustomers.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}