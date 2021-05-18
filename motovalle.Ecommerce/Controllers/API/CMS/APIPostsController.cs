// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APIPostsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  API Posts Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.CMS
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Inventek.CMS.DAL.EF;
    using inventek_blog.Models.Entities;
    using Inventek.CMS.DAL.Repos;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// API Posts Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/APIPosts")]
    [Authorize(Roles = "ADMIN,BLOGADMIN")]
    public class APIPostsController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly blogContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="APIPostsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public APIPostsController(blogContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Post created or edited on Json format</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Posts value)
        {
            var blog = new BlogRepository(this._context);

            if (value.PostsId == 0)
            {
                value.ShortDescription = value.ShortDescription.Length > 254 ? value.ShortDescription.Substring(0, 254) : value.ShortDescription;
                value.PostedOn = DateTime.Now;
                blog.AddPost(value);
            }
            else
            {
                var postBefore = this._context.Posts.AsNoTracking().FirstOrDefault(x => x.PostsId == value.PostsId);
                value.ModifiedDate = DateTime.Now;
                value.PostedOn = postBefore.PostedOn;
                blog.EditPost(value);
            }

            return Json(value);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>Updated Post on Json format</returns>
        [HttpPut]
        public ActionResult Put(long id, [FromBody] Posts value)
        {
            this._context.Posts.Update(value);
            this._context.SaveChanges();
            return Json(value);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of Post on database without deleted item</returns>
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var remove = this._context.Posts.FirstOrDefault(p => p.PostsId == id);
            this._context.Posts.Remove(remove);
            this._context.SaveChanges();
            await Task.Delay(500);
            return Json(this._context.Posts.ToList());
        }

        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <returns>All Post on Json format</returns>
        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = this._context.Posts.ToList();
            return Json(new
            {
                Items = posts.Select(c => new
                {
                    PostsID = c.PostsId,
                    Title = c.Title,
                    ShortDescription = c.ShortDescription,
                    IsPublished = c.IsPublished,
                    PostedOn = c.PostedOn
                }),
                Count = posts.Count()
            });
        }

        /// <summary>
        /// Gets the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Post for Id on Json format</returns>
        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult GetPost(long id)
        {
            ViewData["FkAuthorsId"] = new SelectList(this._context.Authors, "AuthorsId", "AuthorName");
            ViewData["FkCategoriesId"] = new SelectList(this._context.Categories, "CategoriesId", "CategoryName");
            var posts = this._context.Posts.Where(s => s.PostsId == id).ToList();

            if (id == 0)
            {
                return View("ManagePosts", new Posts() { IsPublished = true });
            }
            else
            {
                return View("ManagePosts", posts[0]);
            }
        }

        /// <summary>
        /// Rich Text Editor Value view model
        /// </summary>
        public class RichTextEditorValue
        {
            /// <summary>
            /// Gets or sets the text.
            /// </summary>
            /// <value>
            /// The text.
            /// </value>
            public string text { get; set; }

            /// <summary>
            /// Gets or sets the post.
            /// </summary>
            /// <value>
            /// The post.
            /// </value>
            public Posts post { get; set; }
        }
    }
}