// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartCheckoutViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Checkout View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Ecommerce.Models.Entities;
using System.Collections.Generic;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Shopping Cart Checkout View Model
    /// </summary>
    public class ShoppingCartCheckoutViewModel
    {
        /// <summary>
        /// Gets or sets the makes identifier.
        /// </summary>
        /// <value>
        /// The makes identifier.
        /// </value>
        public int MakesId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the inventory items identifier.
        /// </summary>
        /// <value>
        /// The inventory items identifier.
        /// </value>
        public int InventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the customers identifier.
        /// </summary>
        /// <value>
        /// The customers identifier.
        /// </value>
        public int CustomersId { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records identifier.
        /// </summary>
        /// <value>
        /// The shopping cart records identifier.
        /// </value>
        public int ShoppingCartRecordsId { get; set; }

        /// <summary>
        /// Gets or sets the item picture URL.
        /// </summary>
        /// <value>
        /// The item picture URL.
        /// </value>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the item short description.
        /// </summary>
        /// <value>
        /// The item short description.
        /// </value>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the color of the item.
        /// </summary>
        /// <value>
        /// The color of the item.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the item year.
        /// </summary>
        /// <value>
        /// The item year.
        /// </value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the name of the item transmission.
        /// </summary>
        /// <value>
        /// The name of the item transmission.
        /// </value>
        public string TransmissionName { get; set; }

        /// <summary>
        /// Gets or sets the passengers.
        /// </summary>
        /// <value>
        /// The passengers.
        /// </value>
        public int Passengers { get; set; }

        /// <summary>
        /// Gets or sets the doors.
        /// </summary>
        /// <value>
        /// The doors.
        /// </value>
        public int Doors { get; set; }

        /// <summary>
        /// Gets or sets the sales price inventory.
        /// </summary>
        /// <value>
        /// The sales price inventory.
        /// </value>
        public decimal SalesPriceInventory { get; set; }

        /// <summary>
        /// Gets or sets the sub total.
        /// </summary>
        /// <value>
        /// The sub total.
        /// </value>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        public List<TaxForInventoryWithValue> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the shipping.
        /// </summary>
        /// <value>
        /// The shipping.
        /// </value>
        public decimal Shipping { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total { get; set; }
    }
}
