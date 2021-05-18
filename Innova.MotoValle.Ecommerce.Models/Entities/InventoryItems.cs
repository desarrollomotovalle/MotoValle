// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItems.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Items model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Inventory Items model
    /// </summary>
    public partial class InventoryItems
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItems" /> class.
        /// </summary>
        public InventoryItems()
        {
            this.InventoryItemPictures = new HashSet<InventoryItemPictures>();
            this.ShoppingCartRecords = new HashSet<ShoppingCartRecords>();
            this.ShoppingCartRecordsGuest = new HashSet<ShoppingCartRecordsGuest>();
            this.OrderDetails = new HashSet<OrderDetails>();
            this.InterestedCustomers = new HashSet<InterestedCustomers>();
            this.TaxesForInventory = new HashSet<TaxesForInventory>();
        }

        /// <summary>
        /// Gets or sets the inventory items identifier.
        /// </summary>
        /// <value>
        /// The inventory items identifier.
        /// </value>
        public int InventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the fk products identifier.
        /// </summary>
        /// <value>
        /// The fk products identifier.
        /// </value>
        [Required]
        [Display(Name = "Product")]
        public int FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the vin.
        /// </summary>
        /// <value>
        /// The vin.
        /// </value>
        public string Vin { get; set; }

        /// <summary>
        /// Gets or sets the fk colors identifier.
        /// </summary>
        /// <value>
        /// The fk colors identifier.
        /// </value>
        [Display(Name = "Color")]
        public int? FkColorsId { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Sales Price must be mayor than 0")]
        //[RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$", ErrorMessage = "Maximum 16 integer and Two Decimal Points")]
        public decimal? Cost { get; set; }

        /// <summary>
        /// Gets or sets the sales price.
        /// </summary>
        /// <value>
        /// The sales price.
        /// </value>
        [DataType(DataType.Currency)]
        [Display(Name = "Sales Price")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Sales Price must be  mayor than 0")]
        //[RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$", ErrorMessage = "Maximum 16 integer and Two Decimal Points")]
        public decimal? SalesPrice { get; set; }

        /// <summary>
        /// Gets or sets the mileage.
        /// </summary>
        /// <value>
        /// The mileage.
        /// </value>
        [Range(0, double.PositiveInfinity, ErrorMessage = "Mileage must be mayor or equals than 0")]
        public int? Mileage { get; set; }

        /// <summary>
        /// Gets or sets the fk warranties identifier.
        /// </summary>
        /// <value>
        /// The fk warranties identifier.
        /// </value>
        [Display(Name = "Warranty")]
        public int? FkWarrantiesId { get; set; }

        /// <summary>
        /// Gets or sets the is new.
        /// </summary>
        /// <value>
        /// The is new.
        /// </value>
        [Display(Name = "Is New")]
        public sbyte? IsNew { get; set; }

        /// <summary>
        /// Gets or sets the cover picture URL.
        /// </summary>
        /// <value>
        /// The cover picture URL.
        /// </value>
        [Display(Name = "Cover Picture Url")]
        public string CoverPictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the is sold.
        /// </summary>
        /// <value>
        /// The is sold.
        /// </value>
        [Display(Name = "Is Sold")]
        public sbyte? IsSold { get; set; }

        /// <summary>
        /// Gets or sets the allow show.
        /// </summary>
        /// <value>
        /// The allow show.
        /// </value>
        [Display(Name = "Allow Show")]
        public sbyte? AllowShow { get; set; }

        /// <summary>
        /// Gets or sets the picture360 URL.
        /// </summary>
        /// <value>
        /// The picture360 URL.
        /// </value>
        [Display(Name = "360 Pictures Path")]
        public string Picture360Url { get; set; }

        /// <summary>
        /// Gets or sets the picture360 quantity.
        /// </summary>
        /// <value>
        /// The picture360 quantity.
        /// </value>
        [Display(Name = "360 Pictures Quantity")]
        public int Picture360Quantity { get; set; }

        /// <summary>
        /// Gets or sets the cupix360 URL.
        /// </summary>
        /// <value>
        /// The cupix360 URL.
        /// </value>
        [Display(Name = "Cupix 360 Url")]
        public string Cupix360Url { get; set; }

        /// <summary>
        /// Gets or sets the quantity in stock.
        /// </summary>
        /// <value>
        /// The quantity in stock.
        /// </value>
        [Display(Name = "Quantity In Stock")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Quantity In Stock must be mayor or equals than 0")]
        public int? QuantityInStock { get; set; }

        /// <summary>
        /// Gets or sets the fk colors.
        /// </summary>
        /// <value>
        /// The fk colors.
        /// </value>
        [Display(Name = "Color")]
        public virtual Colors FkColors { get; set; }

        /// <summary>
        /// Gets or sets the fk products.
        /// </summary>
        /// <value>
        /// The fk products.
        /// </value>
        [Display(Name = "Product")]
        public virtual Products FkProducts { get; set; }

        /// <summary>
        /// Gets or sets the fk warranties.
        /// </summary>
        /// <value>
        /// The fk warranties.
        /// </value>
        [Display(Name = "Warranty")]
        public virtual Warranties FkWarranties { get; set; }

        /// <summary>
        /// Gets or sets the inventory item pictures.
        /// </summary>
        /// <value>
        /// The inventory item pictures.
        /// </value>
        public virtual ICollection<InventoryItemPictures> InventoryItemPictures { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records.
        /// </summary>
        /// <value>
        /// The shopping cart records.
        /// </value>
        public virtual ICollection<ShoppingCartRecords> ShoppingCartRecords { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records guest.
        /// </summary>
        /// <value>
        /// The shopping cart records guest.
        /// </value>
        public virtual ICollection<ShoppingCartRecordsGuest> ShoppingCartRecordsGuest { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the interested customers.
        /// </summary>
        /// <value>
        /// The interested customers.
        /// </value>
        public virtual ICollection<InterestedCustomers> InterestedCustomers { get; set; }

        /// <summary>
        /// Gets or sets the taxes for inventory.
        /// </summary>
        /// <value>
        /// The taxes for inventory.
        /// </value>
        public virtual ICollection<TaxesForInventory> TaxesForInventory { get; set; }
    }
}
