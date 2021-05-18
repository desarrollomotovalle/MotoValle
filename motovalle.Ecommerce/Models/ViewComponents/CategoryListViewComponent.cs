// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryListViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category List View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;   
    using inventek_blog.Models.Entities;
    using Inventek.CMS.DAL.EF;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Category List View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class CategoryListViewComponent : ViewComponent
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly blogContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryListViewComponent"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CategoryListViewComponent(blogContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <returns>View component</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await this.GetItemsAsync();
            return this.View(items);
        }

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <returns>List of categories</returns>
        private async Task<List<Categories>> GetItemsAsync()
        {
            return await this._context.Categories.ToListAsync();
        }
    }
}
