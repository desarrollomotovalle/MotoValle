// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CartRecordWithProductInfo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Cart Record With Product Info view model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Cart Record With Product Info view model
    /// </summary>
    /// <seealso cref="Ecommerce.Models.ViewModels.ProductAndCategoryBase" />
    public class CartRecordWithProductInfo : ProductAndCategoryBase
    {
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the line item total.
        /// </summary>
        /// <value>
        /// The line item total.
        /// </value>
        [DataType(DataType.Currency), Display(Name = "Line Total")]
        public decimal LineItemTotal { get; set; }
    }
}
