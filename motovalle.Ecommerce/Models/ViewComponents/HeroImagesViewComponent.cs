// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroImagesViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Hero Images View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Hero Images View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class HeroImagesViewComponent : ViewComponent
    {
        /// <summary>
        /// The hero images repo
        /// </summary>
        private readonly IHeroImagesRepo _heroImagesRepo;

        /// <summary>
        /// The makes repo
        /// </summary>
        private readonly IMakesRepo _makesRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeroImagesViewComponent" /> class.
        /// </summary>
        /// <param name="heroImagesRepo">The hero images repo.</param>
        /// <param name="makesRepo">The makes repo.</param>
        public HeroImagesViewComponent(IHeroImagesRepo heroImagesRepo, IMakesRepo makesRepo)
        {
            this._heroImagesRepo = heroImagesRepo;
            this._makesRepo = makesRepo;
        }
        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="makesName">Name of the makes.</param>
        /// <returns>
        /// Component Result
        /// </returns>
        public async Task<IViewComponentResult> InvokeAsync(string makesName)
        {
            var items = this.Get(makesName);
            await Task.CompletedTask;
            return this.View(items);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Hero Images by Make</returns>
        private List<HeroImages> Get(string makesName)
        {
            var make = this._makesRepo.GetAllRecords().FirstOrDefault(x => x.MakeName.Contains(makesName, System.StringComparison.OrdinalIgnoreCase));
            return this._heroImagesRepo.GetAllRecords().Where(x => x.Enable && x.FkMakesId == make?.MakesId).OrderBy(x => x.Index).ToList();
        }
    }
}
