// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APIPagesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  API Pages Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API
{
    using Inventek.ERP.DAL.EF;
    using Inventek.ERP.DAL.Repos;
    using Inventek.ERP.Models.Entities.Ecommerce;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// API Pages Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    [Produces("application/json")]
    [Route("api/APIPages")]
    public class APIPagesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly inventekContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="APIPagesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public APIPagesController(inventekContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Post Result</returns>
        [HttpPost]
        public ActionResult Post([FromBody] EcommerceCustomPages value)
        {
            EcommerceCustomPagesRepo _repo = new EcommerceCustomPagesRepo(_context);

            if (value.EcommerceCustomPagesId == 0)
            {

                value.HasChanged = true;
                value.CreationDate = DateTime.Now;
                value.LastEditDate = DateTime.Now;

                _repo.CreateRecord(value);
            }
            else
            {
                value.HasChanged = true;
                value.LastEditDate = DateTime.Now;
                _repo.UpdateRecord(value.EcommerceCustomPagesId, value);
            }

            return Json(value);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>Put Result</returns>
        [HttpPut]
        public ActionResult Put(long id, [FromBody] EcommerceCustomPages value)
        {
            _context.EcommerceCustomPages.Update(value);
            _context.SaveChanges();

            return Json(value);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Result</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            EcommerceCustomPages remove = _context.EcommerceCustomPages.FirstOrDefault(p => p.EcommerceCustomPagesId == id);
            _context.EcommerceCustomPages.Remove(remove);
            _context.SaveChanges();
            return Json(_context.EcommerceCustomPages.ToList());
        }

        /// <summary>
        /// Gets the page posts.
        /// </summary>
        /// <returns>Get Result</returns>
        [HttpGet]
        public IActionResult GetPagePosts()
        {
            List<EcommerceCustomPages> posts = _context.EcommerceCustomPages.ToList();
            return Json(posts);
        }

        /// <summary>
        /// Gets the page post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get Page Post Result</returns>
        [HttpGet("{id}", Name = "GetPagePost")]
        public IActionResult GetPagePost(long id)
        {
            //ViewData["FkAuthorsId"] = new SelectList(_context.Authors, "AuthorsId", "AuthorName");
            //ViewData["FkCategoriesId"] = new SelectList(_context.Categories, "CategoriesId", "CategoryName");

            List<EcommerceCustomPages> posts = _context.EcommerceCustomPages.Where(s => s.EcommerceCustomPagesId == id).ToList();

            if (id == 0)
            {
                return View("ManagePages", new EcommerceCustomPages());
            }
            else
            {
                return View("ManagePages", posts[0]);
            }
        }

        public class RichTextEditorValue
        {
            public string text { get; set; }
            public EcommerceCustomPages page { get; set; }
        }
    }
}
