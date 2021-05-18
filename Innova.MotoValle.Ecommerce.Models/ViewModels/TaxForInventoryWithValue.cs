// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxForInventoryWithValue.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Tax For Inventory With Value View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Tax For Inventory With Value View Model
    /// </summary>
    public class TaxForInventoryWithValue
    {
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
        /// Gets or sets the tax value.
        /// </summary>
        /// <value>
        /// The tax value.
        /// </value>
        public decimal TaxValue { get; set; }
    }
}
