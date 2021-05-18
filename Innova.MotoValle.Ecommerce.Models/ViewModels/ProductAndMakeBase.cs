// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAndMakeBase.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product And Make Base View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Product And Make Base View Model
    /// </summary>
    public class ProductAndMakeBase
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        [Display(Name = "Product Name"), StringLength(45)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the categories identifier.
        /// </summary>
        /// <value>
        /// The categories identifier.
        /// </value>
        public int CategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        /// <value>
        /// The name of the model.
        /// </value>
        [Display(Name = "Model Name"), StringLength(45)]
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        /// <value>
        /// The product description.
        /// </value>
        [Display(Name = "Product Description"), StringLength(45)]
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Display(Name = "Category Name"), StringLength(255)]
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is featured.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is featured; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Gets or sets the sales price.
        /// </summary>
        /// <value>
        /// The sales price.
        /// </value>
        [Display(Name = "Sales Price"), DataType(DataType.Currency)]
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// Gets or sets the quantity in stock.
        /// </summary>
        /// <value>
        /// The quantity in stock.
        /// </value>
        [Display(Name = "Quantity In Stock")]
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Gets or sets the MSRP.
        /// </summary>
        /// <value>
        /// The MSRP.
        /// </value>
        [DataType(DataType.Currency)]
        public decimal Msrp { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the horse power.
        /// </summary>
        /// <value>
        /// The horse power.
        /// </value>
        public int HorsePower { get; set; }

        /// <summary>
        /// Gets or sets the picture URL.
        /// </summary>
        /// <value>
        /// The picture URL.
        /// </value>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the transmission.
        /// </summary>
        /// <value>
        /// The name of the transmission.
        /// </value>
        [StringLength(255)]
        public string TransmissionName { get; set; }

        /// <summary>
        /// Gets or sets the passengers.
        /// </summary>
        /// <value>
        /// The passengers.
        /// </value>
        public int Passengers { get; set; }

        /// <summary>
        /// Gets or sets the passengers doors.
        /// </summary>
        /// <value>
        /// The passengers doors.
        /// </value>
        public int PassengersDoors { get; set; }

        /// <summary>
        /// Gets or sets the youtube URL.
        /// </summary>
        /// <value>
        /// The youtube URL.
        /// </value>
        public string YoutubeUrl { get; set; }

        /// <summary>
        /// Gets or sets the cupix360 URL.
        /// </summary>
        /// <value>
        /// The cupix360 URL.
        /// </value>
        public string Cupix360Url { get; set; }

        /// <summary>
        /// Gets or sets the allow show.
        /// </summary>
        /// <value>
        /// The allow show.
        /// </value>
        [Display(Name = "Allow Show")]
        public sbyte AllowShow { get; set; }
    }
}
