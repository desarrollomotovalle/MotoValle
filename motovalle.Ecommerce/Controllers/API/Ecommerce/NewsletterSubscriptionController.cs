// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsletterSubscriptionController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Newsletter Subscription Controller
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
    /// Newsletter Subscription Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterSubscriptionController : Controller
    {
        /// <summary>
        /// Gets or sets the newsletter subscriptions repo.
        /// </summary>
        /// <value>
        /// The newsletter subscriptions repo.
        /// </value>
        private readonly INewsletterSubscriptionsRepo _newsletterSubscriptionsRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsletterSubscriptionController" /> class.
        /// </summary>
        /// <param name="newsletterSubscriptionsRepo">The newsletter subscriptions repo.</param>
        public NewsletterSubscriptionController(INewsletterSubscriptionsRepo newsletterSubscriptionsRepo)
        {
            this._newsletterSubscriptionsRepo = newsletterSubscriptionsRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of inventory Items</returns>
        [HttpGet]
        public IActionResult Get() => this.Ok(this._newsletterSubscriptionsRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsletterSubscription by id</returns>
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var item = this._newsletterSubscriptionsRepo.GetRecord(id);
            if (item == null)
            {
                return this.NotFound();
            }

            return this.Json(item);
        }

        /// <summary>
        /// Gets for email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>NewsletterSubscription by email</returns>
        [HttpGet("ForEmail/{email}")]
        public IActionResult GetForEmail(string email)
        {
            var item = this._newsletterSubscriptionsRepo.GetRecord(email);
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
        public IActionResult Create([FromBody] NewsLetterSubscriptions item)
        {
            var productCreated = this._newsletterSubscriptionsRepo.CreateRecord(item);
            if (productCreated.GetType() != typeof(NewsLetterSubscriptions))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(productCreated);
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
                this._newsletterSubscriptionsRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] NewsLetterSubscriptions updatedItem)
        {
            try
            {
                this._newsletterSubscriptionsRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}