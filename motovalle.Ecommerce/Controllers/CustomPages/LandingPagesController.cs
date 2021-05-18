// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomLandingPagesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Custom Landing Pages Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers.CustomPages
{
    using Inventek.ERP.DAL.EF;
    using Inventek.ERP.DAL.Repos;
    using Inventek.ERP.Models.Entities.Ecommerce;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Custom Landing Pages Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />

    public class LandingPagesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly inventekContext _context;

        /// <summary>
        /// The repo
        /// </summary>
        private readonly EcommerceCustomPagesRepo _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLandingPagesController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LandingPagesController(inventekContext context)
        {
            this._context = context;
            this._repo = new EcommerceCustomPagesRepo(context);
        }

        // GET: Colors
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index View</returns>
        public async Task<IActionResult> Index()
        {
            List<EcommerceCustomPages> pages = new List<EcommerceCustomPages>();
            try
            {
                pages = await _context.EcommerceCustomPages.ToListAsync();
            }
            catch { }

            return View(pages);
        }

        [Authorize(Roles = "ADMIN,BLOGADMIN")]
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Create View</returns>
        /// 
        public IActionResult Create()
        {
            EcommerceCustomPages page = new EcommerceCustomPages
            {
                EcommerceCustomPagesId = 0
            };
            return View("ManagePages", page);
        }

        public async Task<IActionResult> Page(string page)
        {
            if (page == null)
            {
                return NotFound();
            }

            var colors = await this._context.EcommerceCustomPages
                .FirstOrDefaultAsync(m => m.URL == page);
            if (colors == null)
            {
                return NotFound();
            }

            return View(colors);
        }

        //sobre-motovalle


        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Saves the page.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Save Page Result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,BLOGADMIN")]
        public async Task<IActionResult> SavePage([Bind("EcommerceCustomPagesId,PageTitle,PageDescription,pageKeywords,FkEcommerceCustomMenuID,PageHTML,URL")] EcommerceCustomPages item)
        {
            if (ModelState.IsValid)
            {
                if (item.EcommerceCustomPagesId == 0)
                {
                    _context.EcommerceCustomPages.Add(item);

                }
                else
                {
                    _context.EcommerceCustomPages.Update(item);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", item);
        }

        // GET: Colors/Edit/5
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Edit View</returns>
        /// 
        [Authorize(Roles = "ADMIN,BLOGADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await this._context.EcommerceCustomPages.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View("ManagePages", item);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns>Edit Result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN,BLOGADMIN")]
        public async Task<IActionResult> Edit(int id, [Bind("EcommerceCustomPagesId,PageTitle,PageDescription,pageKeywords,FkEcommerceCustomMenuID,PageHTML,URL")] EcommerceCustomPages item)
        {
            if (id != item.EcommerceCustomPagesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.EcommerceCustomPages.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.EcommerceCustomPagesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Colors/Delete/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete Result</returns>
        [Authorize(Roles = "ADMIN,BLOGADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this._repo.DeleteRecord((long)id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Items the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Boll Control</returns>
        private bool ItemExists(int id)
        {
            return this._context.EcommerceCustomPages.Any(e => e.EcommerceCustomPagesId == id);
        }
    }
}
