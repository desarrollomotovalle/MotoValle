// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroImagesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Body Styles Controller API
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
    /// Body Styles Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class HeroImagesController : Controller
    {
        /// <summary>
        /// Gets or sets the Hero Images repo.
        /// </summary>
        /// <value>
        /// The Hero Images repo.
        /// </value>
        private readonly IHeroImagesRepo _heroImagesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeroImagesController"/> class.
        /// </summary>
        /// <param name="heroImagesRepo">The Hero Images repo.</param>
        public HeroImagesController(IHeroImagesRepo heroImagesRepo)
        {
            this._heroImagesRepo = heroImagesRepo;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of Hero Images</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return this.Ok(this._heroImagesRepo.GetAllRecords());
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HeroImages by id on Json format</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var item = this._heroImagesRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] HeroImages item)
        {
            var heroImagesCreated = this._heroImagesRepo.CreateRecord(item);
            if (heroImagesCreated.GetType() != typeof(HeroImages))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(heroImagesCreated);
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
                this._heroImagesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] HeroImages updatedItem)
        {
            try
            {
                this._heroImagesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}