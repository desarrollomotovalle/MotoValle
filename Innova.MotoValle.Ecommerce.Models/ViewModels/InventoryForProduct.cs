// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryForProduct.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory For Product View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Ecommerce.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Inventory For Product View Model
    /// </summary>
    public class InventoryForProduct : ProductAndMakeBase
    {
        /// <summary>
        /// Gets or sets the inventory items identifier.
        /// </summary>
        /// <value>
        /// The inventory items identifier.
        /// </value>
        public int InventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        /// <value>
        /// The name of the color.
        /// </value>
        [Display(Name = "Color Name")]
        public string ColorName { get; set; }

        /// <summary>
        /// Gets or sets the sales price inventory.
        /// </summary>
        /// <value>
        /// The sales price inventory.
        /// </value>
        [Display(Name = "Sales Price Inventory"), DataType(DataType.Currency)]
        public decimal SalesPriceInventory { get; set; }

        /// <summary>
        /// Gets or sets the millage.
        /// </summary>
        /// <value>
        /// The millage.
        /// </value>
        public int Millage { get; set; }

        /// <summary>
        /// Gets or sets the name of the warranty.
        /// </summary>
        /// <value>
        /// The name of the warranty.
        /// </value>
        [Display(Name = "Warranty Name"), StringLength(45)]
        public string WarrantyName { get; set; }

        /// <summary>
        /// Gets or sets the warranty description.
        /// </summary>
        /// <value>
        /// The warranty description.
        /// </value>
        [Display(Name = "Warranty Description")]
        public string WarrantyDescription { get; set; }

        /// <summary>
        /// Gets or sets the cover picture URL.
        /// </summary>
        /// <value>
        /// The cover picture URL.
        /// </value>
        [Display(Name = "Cover Picture Url"), StringLength(255)]
        public string CoverPictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the quantity in stock inventory.
        /// </summary>
        /// <value>
        /// The quantity in stock inventory.
        /// </value>
        [Display(Name = "Quantity In Stock Inventory")]
        public int QuantityInStockInventory { get; set; }

        /// <summary>
        /// Gets or sets the allow show.
        /// </summary>
        /// <value>
        /// The allow show.
        /// </value>
        [Display(Name = "Allow Show Inventory")]
        public sbyte AllowShowInventory { get; set; }

        /// <summary>
        /// Gets or sets the inventory item pictures.
        /// </summary>
        /// <value>
        /// The inventory item pictures.
        /// </value>
        public List<InventoryItemPictures> InventoryItemPictures { get; set; }

        /// <summary>
        /// Gets or sets the taxes for inventory.
        /// </summary>
        /// <value>
        /// The taxes for inventory.
        /// </value>
        public List<TaxesForInventory> TaxesForInventory { get; set; }
    }
}
