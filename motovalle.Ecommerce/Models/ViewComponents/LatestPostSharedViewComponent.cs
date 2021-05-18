// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LatestPostViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Latest Post View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using Inventek.CMS.DAL.EF;
    using inventek_blog.Models.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Latest Post View Component
    /// </summary>
    public class LatestPostSharedViewComponent : ViewComponent
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly blogContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryListViewComponent"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LatestPostSharedViewComponent(blogContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>
        /// View component
        /// </returns>
        public async Task<IViewComponentResult> InvokeAsync(string from)
        {
            var items = await this.GetItemsAsync(from);
            return this.View(items);
        }

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>List of latest post</returns>
        private async Task<List<Posts>> GetItemsAsync(string from)
        {
            var allPosts = await this._context.Posts.Include(x => x.FkCategories).Where(x => x.IsPublished).OrderByDescending(x => x.PostedOn).ToListAsync();
            const int take = 3;
            return from switch
            {
                "General" => allPosts.Where(x => !string.Equals(x.FkCategories.CategoryName, "ford", System.StringComparison.OrdinalIgnoreCase) && !string.Equals(x.FkCategories.CategoryName, "mazda", System.StringComparison.OrdinalIgnoreCase) && !string.Equals(x.FkCategories.CategoryName, "carshow", System.StringComparison.OrdinalIgnoreCase)
                                              && !string.Equals(x.FkCategories.CategoryName, "masseyferguson", System.StringComparison.OrdinalIgnoreCase) && !string.Equals(x.FkCategories.CategoryName, "respuestos", System.StringComparison.OrdinalIgnoreCase)).Take(take).ToList(),
                "Ford" => allPosts.Where(x => string.Equals(x.FkCategories.CategoryName, "ford", System.StringComparison.OrdinalIgnoreCase)).Take(take).ToList(),
                "Mazda" => allPosts.Where(x => string.Equals(x.FkCategories.CategoryName, "mazda", System.StringComparison.OrdinalIgnoreCase)).Take(take).ToList(),
                "MasseyFerguson" => allPosts.Where(x => string.Equals(x.FkCategories.CategoryName, "masseyferguson", System.StringComparison.OrdinalIgnoreCase)).Take(take).ToList(),
                "Repuestos" => allPosts.Where(x => string.Equals(x.FkCategories.CategoryName, "repuestos", System.StringComparison.OrdinalIgnoreCase)).Take(take).ToList(),
                "CarShow" => allPosts.Where(x => string.Equals(x.FkCategories.CategoryName, "carshow", System.StringComparison.OrdinalIgnoreCase)).Take(take).ToList(),
                _ => allPosts,
            };
        }
    }
}