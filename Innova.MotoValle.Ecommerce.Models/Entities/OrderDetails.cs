// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDetails.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//   Order Details entity model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Order Details entity model
    /// </summary>
    public partial class OrderDetails
    {
        /// <summary>
        /// Gets or sets the order details identifier.
        /// </summary>
        /// <value>
        /// The order details identifier.
        /// </value>
        public int OrderDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the fk orders identifier.
        /// </summary>
        /// <value>
        /// The fk orders identifier.
        /// </value>
        [Display(Name = "Order")]
        public int FkOrdersId { get; set; }

        /// <summary>
        /// Gets or sets the fk products identifier.
        /// </summary>
        /// <value>
        /// The fk products identifier.
        /// </value>
        [Display(Name = "Product")]
        public int FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items identifier.
        /// </summary>
        /// <value>
        /// The fk inventory items identifier.
        /// </value>
        [Display(Name = "Inventory")]
        public int FKInventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the item price.
        /// </summary>
        /// <value>
        /// The item price.
        /// </value>
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Item Price must be mayor or equals than 0")]
        public decimal ItemPrice { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Quantity must be mayor than 0")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the ship date.
        /// </summary>
        /// <value>
        /// The ship date.
        /// </value>
        [Display(Name = "Ship Date")]
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Tax must be mayor or equals than 0")]
        public decimal? Tax { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [Required]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the fk orders.
        /// </summary>
        /// <value>
        /// The fk orders.
        /// </value>
        public virtual Orders FkOrders { get; set; }

        /// <summary>
        /// Gets or sets the fk products.
        /// </summary>
        /// <value>
        /// The fk products.
        /// </value>
        public virtual Products FkProducts { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items.
        /// </summary>
        /// <value>
        /// The fk inventory items.
        /// </value>
        public virtual InventoryItems FKInventoryItems { get; set; }
    }
}
