// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LatestPostsHomeViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Latest Posts Home View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using Inventek.CMS.DAL.Repos.Interfaces;
    using inventek_blog.Models.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Latest Posts Home View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class LatestPostsHomeViewComponent : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LatestPostsHomeViewComponent"/> class.
        /// </summary>
        /// <param name="blogRepository">The blog repository.</param>
        public LatestPostsHomeViewComponent(IBlogRepository blogRepository)
        {
            this._blogRepository = blogRepository;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <returns>Component</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = this.Get();
            return this.View(items);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>List of posts</returns>
        private List<Posts> Get()
        {
            return this._blogRepository.GetAllPosts().Where(x => x.IsPublished == true).OrderByDescending(x => x.PostedOn).Take(3).ToList();
        }
    }
}
