// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FordBrandViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Ford Brand View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using global::Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Ford Brand View Model
    /// </summary>
    public class FordBrandViewModel
    {
        /// <summary>
        /// Gets or sets the test drive ford view model.
        /// </summary>
        /// <value>
        /// The test drive ford view model.
        /// </value>
        public TestDriveFordViewModel TestDriveFordViewModel { get; set; }

        /// <summary>
        /// Gets or sets the products and make base.
        /// </summary>
        /// <value>
        /// The products and make base.
        /// </value>
        public List<ProductAndMakeBase> ProductsAndMakeBase { get; set; }
    }
}
