// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Taxes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Taxes model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Taxes model
    /// </summary>
    public partial class Taxes
    {
        /// <summary>
        /// Gets or sets the taxes identifier.
        /// </summary>
        /// <value>
        /// The taxes identifier.
        /// </value>
        [Display(Name = "Taxes Id")]
        [Required]
        public int TaxesId { get; set; }

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
        [Display(Name = "Percent"), Range(0, double.PositiveInfinity, ErrorMessage = "Tax Percent should be mayor or equals to 0")]
        public decimal TaxPercent { get; set; }

        /// <summary>
        /// Gets or sets the tax description.
        /// </summary>
        /// <value>
        /// The tax description.
        /// </value>
        [Display(Name = "Description"), StringLength(800)]
        public string TaxDescription { get; set; }
    }
}
