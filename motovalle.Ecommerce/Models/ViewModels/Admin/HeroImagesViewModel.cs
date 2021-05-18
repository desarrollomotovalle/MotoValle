// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroImagesViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Hero Images View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Admin
{
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Hero Images View Model
    /// </summary>
    /// <seealso cref="Ecommerce.Models.Entities.HeroImages" />
    public class HeroImagesViewModel : HeroImages
    {
        /// <summary>
        /// Gets or sets the image URL file.
        /// </summary>
        /// <value>
        /// The image URL file.
        /// </value>
        public IFormFile ImageUrlFile { get; set; }
    }
}