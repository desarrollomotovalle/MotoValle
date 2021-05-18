// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MasseyFergusonBrandViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Massey Ferguson Brand View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using global::Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Massey Ferguson Brand View Model
    /// </summary>
    public class MasseyFergusonBrandViewModel
    {
        /// <summary>
        /// Gets or sets the operators club view model.
        /// </summary>
        /// <value>
        /// The operators club view model.
        /// </value>
        public OperatorsClubViewModel OperatorsClubViewModel { get; set; }

        /// <summary>
        /// Gets or sets the products and make base.
        /// </summary>
        /// <value>
        /// The products and make base.
        /// </value>
        public List<ProductAndMakeBase> ProductsAndMakeBase  { get; set; }
    }
}
