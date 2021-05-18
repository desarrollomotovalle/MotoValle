// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryAndProductBaseWithDetails.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory And Product Base With Details View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Ecommerce.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Inventory And Product Base With Details View Model
    /// </summary>
    /// <seealso cref="Ecommerce.Models.ViewModels.ProductWithDetails" />
    public class InventoryAndProductBaseWithDetails : ProductWithDetails
    {
        /// <summary>
        /// Gets or sets the inventory items identifier.
        /// </summary>
        /// <value>
        /// The inventory items identifier.
        /// </value>
        public int InventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the vin.
        /// </summary>
        /// <value>
        /// The vin.
        /// </value>
        public string Vin { get; set; }

        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        /// <value>
        /// The name of the color.
        /// </value>
        [Display(Name = "Color Name"), StringLength(45)]
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
        /// Gets or sets the mileage.
        /// </summary>
        /// <value>
        /// The mileage.
        /// </value>
        public int Mileage { get; set; }

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
        /// Gets or sets a value indicating whether this instance is new.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for IsNew
        [Display(Name = "Is New")]
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the cover picture URL.
        /// </summary>
        /// <value>
        /// The cover picture URL.
        /// </value>
        [Display(Name = "Cover Picture Url"), StringLength(255)]
        public string CoverPictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the picture360 URL inventory.
        /// </summary>
        /// <value>
        /// The picture360 URL inventory.
        /// </value>
        public string Picture360UrlInventory { get; set; }

        /// <summary>
        /// Gets or sets the picture360 quantity inventory.
        /// </summary>
        /// <value>
        /// The picture360 quantity inventory.
        /// </value>
        public int Picture360QuantityInventory { get; set; }

        /// <summary>
        /// Gets or sets the cupix360 URL inventory.
        /// </summary>
        /// <value>
        /// The cupix360 URL inventory.
        /// </value>
        public string Cupix360UrlInventory { get; set; }

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
        [Display(Name = "Inventory Item Pictures")]
        public List<InventoryItemPictures> InventoryItemPictures { get; set; }

        /// <summary>
        /// Gets or sets the taxes for inventory.
        /// </summary>
        /// <value>
        /// The taxes for inventory.
        /// </value>
        public List<TaxesForInventory> TaxesForInventory { get; set; }

        /// <summary>
        /// Gets or sets the product documents.
        /// </summary>
        /// <value>
        /// The product documents.
        /// </value>
        public List<ProductDocuments> ProductDocuments { get; set; }
    }
}
