// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroImageWithBrandsHomeViewComponent.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Hero Image With Brands Home View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// Hero Image With Brands Home View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class HeroImageWithBrandsHomeViewComponent : ViewComponent
    {
        /// <summary>
        /// The hero images repo
        /// </summary>
        private readonly IHeroImagesRepo _heroImagesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeroImageWithBrandsHomeViewComponent"/> class.
        /// </summary>
        /// <param name="heroImagesRepo">The hero images repo.</param>
        public HeroImageWithBrandsHomeViewComponent(IHeroImagesRepo heroImagesRepo)
        {
            this._heroImagesRepo = heroImagesRepo;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <returns>View Component</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await this.GetItemAsync();
            return this.View(item);
        }

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <returns>Hero Home</returns>
        private async Task<HeroImages> GetItemAsync()
        {
            await Task.CompletedTask;
            return this._heroImagesRepo.GetHomeRecord();
        }
    }
}
