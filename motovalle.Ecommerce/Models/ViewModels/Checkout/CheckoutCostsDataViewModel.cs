// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutCostsDataViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Customer Personal Data View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout
{
    using global::Ecommerce.Models.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Checkout Costs Data View Model
    /// </summary>
    public class CheckoutCostsDataViewModel
    {
        /// <summary>
        /// Gets or sets the sub total.
        /// </summary>
        /// <value>
        /// The sub total.
        /// </value>
        [Display(Name = "SubTotal")]
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost.
        /// </summary>
        /// <value>
        /// The shipping cost.
        /// </value>
        [Display(Name = "Costo de envío")]
        public decimal ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the total taxes.
        /// </summary>
        /// <value>
        /// The total taxes.
        /// </value>
        [Display(Name = "Total Impuestos")]
        public decimal TotalTaxes { get; set; }

        /// <summary>
        /// Gets or sets the total taxes detailed.
        /// </summary>
        /// <value>
        /// The total taxes detailed.
        /// </value>
        public List<TaxesTotalViewModel> TotalTaxesDetailed { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart checkout view model.
        /// </summary>
        /// <value>
        /// The shopping cart checkout view model.
        /// </value>
        public List<ShoppingCartCheckoutViewModel> ShoppingCartCheckoutViewModel { get; set; }
    }
}
