// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APICategoriesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  API Categories Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.CMS
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Inventek.CMS.DAL.EF;
    using inventek_blog.Models.Entities;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// API Categories Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/APICategories")]
    [Authorize(Roles = "ADMIN,BLOGADMIN")]
    public class APICategoriesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly blogContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="APICategoriesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public APICategoriesController(blogContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Category created on Json format</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Categories value)
        {
            this._context.Categories.Add(value);
            this._context.SaveChanges();
            return Json(value);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>Category updated on Json format</returns>
        [HttpPut]
        public ActionResult Put(long id, [FromBody] Categories value)
        {
            this._context.Categories.Update(value);
            this._context.SaveChanges();

            return Json(value);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of categories on database</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var remove = this._context.Categories.FirstOrDefault(p => p.CategoriesId == id);
            this._context.Categories.Remove(remove);
            this._context.SaveChanges();
            return Json(this._context.Posts.ToList());
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns>List of categories on Json format</returns>
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = this._context.Categories.ToList();
            return Json(new
            {
                Items = categories.Select(c => new
                {
                    CategoriesID = c.CategoriesId,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    URLSlug = c.UrlSlug
                }),
                Count = categories.Count()
            });
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category for Id on Json format</returns>
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetCategory(long id)
        {
            var category = this._context.Categories.Where(s => s.CategoriesId == id).ToList();
            if (id == 0)
            {
                return null;
            }
            else
            {
                return Json(new
                {
                    Items = category.Select(c => new
                    {
                        CategoriesID = c.CategoriesId,
                        CategoryName = c.CategoryName,
                        Description = c.Description,
                        URLSlug = c.UrlSlug
                    }),
                    Count = category.Count()
                });
            }
        }
    }
}