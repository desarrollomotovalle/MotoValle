// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDetailWithProductInfo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order Detail With Product Info view model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Order Detail With Product Info view model
    /// </summary>
    /// <seealso cref="Ecommerce.Models.ViewModels.ProductAndCategoryBase" />
    public class OrderDetailWithProductInfo : ProductAndCategoryBase
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order details identifier.
        /// </summary>
        /// <value>
        /// The order details identifier.
        /// </value>
        public int OrderDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the line item total.
        /// </summary>
        /// <value>
        /// The line item total.
        /// </value>
        [DataType(DataType.Currency), Display(Name = "Total")]
        public decimal? LineItemTotal { get; set; }

        /// <summary>
        /// Gets or sets the line item tax total.
        /// </summary>
        /// <value>
        /// The line item tax total.
        /// </value>
        [DataType(DataType.Currency), Display(Name = "Tax total")]
        public decimal? LineItemTaxTotal { get; set; }

        /// <summary>
        /// Gets or sets the ship date.
        /// </summary>
        /// <value>
        /// The ship date.
        /// </value>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }
    }
}
