// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecords.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Shopping Cart Records model
    /// </summary>
    public partial class ShoppingCartRecords
    {
        /// <summary>
        /// Gets or sets the shopping cart records identifier.
        /// </summary>
        /// <value>
        /// The shopping cart records identifier.
        /// </value>
        public int ShoppingCartRecordsId { get; set; }

        /// <summary>
        /// Gets or sets the fk customers identifier.
        /// </summary>
        /// <value>
        /// The fk customers identifier.
        /// </value>
        [Required]
        public int FkCustomersId { get; set; }

        /// <summary>
        /// Gets or sets the fk products identifier.
        /// </summary>
        /// <value>
        /// The fk products identifier.
        /// </value>
        [Required]
        public int FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items identifier.
        /// </summary>
        /// <value>
        /// The fk inventory items identifier.
        /// </value>
        [Required]
        public int FkInventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the fk customers.
        /// </summary>
        /// <value>
        /// The fk customers.
        /// </value>
        public virtual Customers FkCustomers { get; set; }

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
