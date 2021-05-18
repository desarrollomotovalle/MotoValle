// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OurHeadquartersViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Our Headquarters View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Models.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Our Headquarters View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class OurHeadquartersViewComponent : ViewComponent
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="OurHeadquartersViewComponent"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public OurHeadquartersViewComponent(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>Headquarters component</returns>
        public async Task<IViewComponentResult> InvokeAsync(string from = null)
        {
            var headquarters = this.GetHearquarters(from);
            return this.View(headquarters);
        }

        /// <summary>
        /// Gets the hearquarters.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>Headquarters records</returns>
        private List<HeadquartersViewModel> GetHearquarters(string from)
        {
            var headquarters = this._configuration.GetSection("Headquarters").Get<List<HeadquartersViewModel>>();
            headquarters = from switch
            {
                "Ford" => headquarters.Where(x => x.Make.ToLower().Trim() == "ford").Take(2).ToList(),
                "Mazda" => headquarters.Where(x => x.Make.ToLower().Trim() == "mazda").Take(2).ToList(),
                "MasseyFerguson" => headquarters.Where(x => x.Make.ToLower().Trim().Contains("massey")).Take(2).ToList(),
                "General" => headquarters.OrderBy(x => Guid.NewGuid()).Take(2).ToList(),
                _ => headquarters.OrderBy(x => Guid.NewGuid()).Take(2).ToList(),
            };

            return headquarters;
        }
    }
}
