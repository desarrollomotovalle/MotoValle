// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchEngineOptimizationsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  API SearchEngineOptimizations Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.API.CMS
{
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    /// <summary>
    /// API SearchEngineOptimizations Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/SearchEngineOptimizations")]
    [Authorize(Roles = "ADMIN")]
    public class SearchEngineOptimizationsController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ISearchEngineOptimizationsRepo _seoRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEngineOptimizationsController" /> class.
        /// </summary>
        /// <param name="seoRepo">The context.</param>
        public SearchEngineOptimizationsController(ISearchEngineOptimizationsRepo seoRepo)
        {
            this._seoRepo = seoRepo;
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Author create on Json format</returns>
        [HttpPost]
        public ActionResult Post([FromBody] SearchEngineOptimizations value)
        {
            this._seoRepo.CreateRecord(value);
            return Json(value);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>Author updated on Json format</returns>
        [HttpPut]
        public ActionResult Put([FromBody] SearchEngineOptimizations value)
        {
            this._seoRepo.UpdateRecord(value.SearchEngineOptimizationsId, value);
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
            this._seoRepo.DeleteRecord(id);
            return Json(this._seoRepo.GetAllRecords());
        }

        /// <summary>
        /// Gets the authors.
        /// </summary>
        /// <returns>List of all Autors on Json format</returns>
        [HttpGet]
        public IActionResult GetSearchEngineOptimizations()
        {
            var seos = this._seoRepo.GetAllRecords();
            return Json(new
            {
                Items = seos.Select(c => new
                {
                    SearchEngineOptimizationsId = c.SearchEngineOptimizationsId,
                    Description = c.Description,
                    PagePath = c.PagePath,
                    Title = c.Title,
                    Keywords = c.Keywords
                }),
                Count = seos.Count()
            });
        }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSearchEngineOptimizations")]
        public IActionResult GetAuthor(long id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                var seo = this._seoRepo.GetRecord(id);
                return Json(new
                {
                    Items = new
                    {
                        SearchEngineOptimizationsId = seo.SearchEngineOptimizationsId,
                        Description = seo.Description,
                        PagePath = seo.PagePath,
                        Title = seo.Title,
                        Keywords = seo.Keywords
                    },
                    Count = 1
                });
            }
        }
    }
}