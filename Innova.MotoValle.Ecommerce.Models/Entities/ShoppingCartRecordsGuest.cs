// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordsGuest.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records Guest model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Shopping Cart Records Guest
    /// </summary>
    public partial class ShoppingCartRecordsGuest
    {
        /// <summary>
        /// Gets or sets the shopping cart records identifier.
        /// </summary>
        /// <value>
        /// The shopping cart records identifier.
        /// </value>
        [Display(Name = "Id")]
        public int ShoppingCartRecordsGuestId { get; set; }

        /// <summary>
        /// Gets or sets the fk customers identifier.
        /// </summary>
        /// <value>
        /// The fk customers identifier.
        /// </value>
        [Display(Name = "ID Invitado")]
        [Required]
        public string GuestId { get; set; }

        /// <summary>
        /// Gets or sets the fk product catalog identifier.
        /// </summary>
        /// <value>
        /// The fk product catalog identifier.
        /// </value>
        [Display(Name = "Product")]
        [Required]
        public int FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items identifier.
        /// </summary>
        /// <value>
        /// The fk inventory items identifier.
        /// </value>
        [Display(Name = "Inventory Item")]
        public int? FkInventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

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
