// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesTotalViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Taxes Total View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Taxes Total View Model
    /// </summary>
    public class TaxesTotalViewModel
    {
        /// <summary>
        /// Gets or sets the taxes identifier.
        /// </summary>
        /// <value>
        /// The taxes identifier.
        /// </value>
        public int TaxesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the tax.
        /// </summary>
        /// <value>
        /// The name of the tax.
        /// </value>
        public string TaxName { get; set; }

        /// <summary>
        /// Gets or sets the tax percent.
        /// </summary>
        /// <value>
        /// The tax percent.
        /// </value>
        public decimal TaxPercent { get; set; }

        /// <summary>
        /// Gets or sets the tax total.
        /// </summary>
        /// <value>
        /// The tax total.
        /// </value>
        public decimal TaxTotal { get; set; }
    }
}
