// --------------------------------------------------------------------------------------------------------------------
// <copyright file="APIAuthorsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  API Authors Controller
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
    /// API Authors Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/APIAuthors")]
    [Authorize(Roles = "ADMIN,BLOGADMIN")]
    public class APIAuthorsController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly blogContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="APIAuthorsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public APIAuthorsController(blogContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Author create on Json format</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Authors value)
        {
            this._context.Authors.Add(value);
            this._context.SaveChanges();
            return Json(value);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>Author updated on Json format</returns>
        [HttpPut]
        public ActionResult Put(long id, [FromBody] Authors value)
        {
            this._context.Authors.Update(value);
            this._context.SaveChanges();

            return Json(value);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of Post on Database</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Authors remove = this._context.Authors.FirstOrDefault(p => p.AuthorsId == id);

            this._context.Authors.Remove(remove);
            this._context.SaveChanges();

            return Json(this._context.Posts.ToList());
        }

        /// <summary>
        /// Gets the authors.
        /// </summary>
        /// <returns>List of all Autors on Json format</returns>
        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = this._context.Authors.ToList();
            return Json(new
            {
                Items = authors.Select(c => new
                {
                    AuthorsID = c.AuthorsId,
                    AuthorName = c.AuthorName,
                    PictureURL = c.PictureUrl
                }),
                Count = authors.Count()
            });
        }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(long id)
        {
            var authors = this._context.Authors.Where(s => s.AuthorsId == id).ToList();

            if (id == 0)
            {
                return null;
            }
            else
            {
                return Json(new
                {
                    Items = authors.Select(c => new
                    {
                        AuthorsID = c.AuthorsId,
                        AuthorName = c.AuthorName,
                        PictureURL = c.PictureUrl
                    }),
                    Count = authors.Count()
                });
            }
        }
    }
}