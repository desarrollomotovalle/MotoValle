// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesForInventory.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Taxes For Inventory Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Taxes For Inventory Model
    /// </summary>
    public partial class TaxesForInventory
    {
        /// <summary>
        /// Gets or sets the taxes for inventory identifier.
        /// </summary>
        /// <value>
        /// The taxes for inventory identifier.
        /// </value>
        [Display(Name = "Taxes For Inventory Id")]
        [Required]
        public int TaxesForInventoryId { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items identifier.
        /// </summary>
        /// <value>
        /// The fk inventory items identifier.
        /// </value>
        [Display(Name = "Inventory Items")]
        public int FkInventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the fk taxes identifier.
        /// </summary>
        /// <value>
        /// The fk taxes identifier.
        /// </value>
        [Display(Name = "Taxes")]
        public int FkTaxesId { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items.
        /// </summary>
        /// <value>
        /// The fk inventory items.
        /// </value>
        [Display(Name = "Inventory Items")]
        public virtual InventoryItems FkInventoryItems { get; set; }

        /// <summary>
        /// Gets or sets the fk taxes.
        /// </summary>
        /// <value>
        /// The fk taxes.
        /// </value>
        [Display(Name = "Taxes")]
        public virtual Taxes FkTaxes { get; set; }
    }
}
