// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPicturesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Pictures Controller API
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
    /// Category Pictures Controller API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryPicturesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryPicturesController" /> class.
        /// </summary>
        /// <param name="categoryPicturesRepo">The category pictures repo.</param>
        public CategoryPicturesController(ICategoryPicturesRepo categoryPicturesRepo)
        {
            this._categoryPicturesRepo = categoryPicturesRepo;
        }

        /// <summary>
        /// Gets or sets the category pictures repo.
        /// </summary>
        /// <value>
        /// The category pictures repo.
        /// </value>
        private ICategoryPicturesRepo _categoryPicturesRepo { get; set; }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of category pictures</returns>
        [HttpGet]
        public IActionResult Get() => this.Ok(this._categoryPicturesRepo.GetAllRecords());

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by id on Json format</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var item = this._categoryPicturesRepo.GetRecord(id);
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
        public IActionResult Create([FromBody] CategoryPictures item)
        {
            var categoryPicturesCreated = this._categoryPicturesRepo.CreateRecord(item);
            if (categoryPicturesCreated.GetType() != typeof(CategoryPictures))
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok(categoryPicturesCreated);
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
                this._categoryPicturesRepo.DeleteRecord(id);
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
        public IActionResult Update(long id, [FromBody] CategoryPictures updatedItem)
        {
            try
            {
                this._categoryPicturesRepo.UpdateRecord(id, updatedItem);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return this.Ok();
        }
    }
}