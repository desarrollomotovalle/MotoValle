// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShopViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shop View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using global::Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Shop View Model
    /// </summary>
    public class ShopViewModel
    {
        /// <summary>
        /// Gets or sets the product and category bases.
        /// </summary>
        /// <value>
        /// The product and make bases.
        /// </value>
        public List<ProductAndMakeBase> ProductAndMakeBases { get; set; }

        /// <summary>
        /// Gets or sets the basic search view model.
        /// </summary>
        /// <value>
        /// The basic search view model.
        /// </value>
        public BasicSearchViewModel BasicSearchViewModel { get; set; }
    }
}
