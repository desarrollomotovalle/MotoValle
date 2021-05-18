// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductsViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Products View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Admin
{
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    /// <summary>
    /// Products View Model
    /// </summary>
    public class ProductsViewModel : Products
    {
        /// <summary>
        /// Gets or sets the cover picture.
        /// </summary>
        /// <value>
        /// The cover picture.
        /// </value>
        public IFormFile CoverPicture { get; set; }

        /// <summary>
        /// Gets or sets the pictures360.
        /// </summary>
        /// <value>
        /// The pictures360.
        /// </value>
        public IList<IFormFile> Pictures360 { get; set; }
    }
}
