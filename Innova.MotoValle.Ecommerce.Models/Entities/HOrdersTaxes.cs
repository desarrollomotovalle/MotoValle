// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HOrdersTaxes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  H Orders Taxes model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// H Orders Taxes model
    /// </summary>
    public partial class HOrdersTaxes
    {
        /// <summary>
        /// Gets or sets the h orders taxes identifier.
        /// </summary>
        /// <value>
        /// The h orders taxes identifier.
        /// </value>
        public int HOrdersTaxesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the tax.
        /// </summary>
        /// <value>
        /// The name of the tax.
        /// </value>
        [Required]
        [Display(Name = "Tax Name"), StringLength(45)]
        public string TaxName { get; set; }

        /// <summary>
        /// Gets or sets the tax percent.
        /// </summary>
        /// <value>
        /// The tax percent.
        /// </value>
        [Required]
        [Display(Name = "Tax Percent"), Range(0, double.PositiveInfinity, ErrorMessage = "Tax Percent should be mayor or equals to 0")]
        public decimal TaxPercent { get; set; }

        /// <summary>
        /// Gets or sets the tax total.
        /// </summary>
        /// <value>
        /// The tax total.
        /// </value>
        [Required]
        [Display(Name = "Tax Total"), Range(0, double.PositiveInfinity, ErrorMessage = "Tax Total should be mayor or equals to 0")]
        public decimal TaxTotal { get; set; }

        /// <summary>
        /// Gets or sets the fk orders identifier.
        /// </summary>
        /// <value>
        /// The fk orders identifier.
        /// </value>
        [Required]
        [Display(Name = "Order")]
        public int FkOrdersId { get; set; }

        /// <summary>
        /// Gets or sets the fk orders.
        /// </summary>
        /// <value>
        /// The fk orders.
        /// </value>
        public virtual Orders FkOrders { get; set; }
    }
}
