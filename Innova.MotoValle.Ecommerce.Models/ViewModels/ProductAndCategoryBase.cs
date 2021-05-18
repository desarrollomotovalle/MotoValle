// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductAndCategoryBase.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order With Details And Product Info view model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Order With Details And Product Info view model
    /// </summary>
    public class ProductAndCategoryBase
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
        [MaxLength(45)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>
        /// The long description.
        /// </value>
        [MaxLength(3800)]
        public string LongDescription { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the category description.
        /// </summary>
        /// <value>
        /// The category description.
        /// </value>
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is featured.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is featured; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Is Featured Product")]
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Gets or sets the unit cost.
        /// </summary>
        /// <value>
        /// The unit cost.
        /// </value>
        [DataType(DataType.Currency), Display(Name = "Cost")]
        public decimal UnitCost { get; set; }

        /// <summary>
        /// Gets or sets the current price.
        /// </summary>
        /// <value>
        /// The current price.
        /// </value>
        [DataType(DataType.Currency), Display(Name = "Price")]
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// Gets or sets the units in stock.
        /// </summary>
        /// <value>
        /// The units in stock.
        /// </value>
        [Display(Name = "In Stock")]
        public int UnitsInStock { get; set; }

        /// <summary>
        /// Gets or sets the trim.
        /// </summary>
        /// <value>
        /// The trim.
        /// </value>
        [MaxLength(255)]
        public string Trim { get; set; }

        /// <summary>
        /// Gets or sets the MSRP.
        /// </summary>
        /// <value>
        /// The MSRP.
        /// </value>
        [DataType(DataType.Currency)]
        public decimal Msrp { get; set; }

        /// <summary>
        /// Gets or sets the name of the make.
        /// </summary>
        /// <value>
        /// The name of the make.
        /// </value>
        [MaxLength(45)]
        [Display(Name = "Make Name")]
        public string MakeName { get; set; }

        /// <summary>
        /// Gets or sets the make description.
        /// </summary>
        /// <value>
        /// The make description.
        /// </value>
        [MaxLength(255)]
        [Display(Name = "Make Description")]
        public string MakeDescription { get; set; }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        /// <value>
        /// The name of the model.
        /// </value>
        [MaxLength(50)]
        [Display(Name = "Model")]
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the model number.
        /// </summary>
        /// <value>
        /// The model number.
        /// </value>
        [MaxLength(50)]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

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
        [Display(Name = "Horse Power")]
        public int HorsePower { get; set; }

        /// <summary>
        /// Gets or sets the gas mileage.
        /// </summary>
        /// <value>
        /// The gas mileage.
        /// </value>
        [Display(Name = "Gas Mileage")]
        public string GasMileage { get; set; }

        /// <summary>
        /// Gets or sets the name of the engine type.
        /// </summary>
        /// <value>
        /// The name of the engine type.
        /// </value>
        [MaxLength(255)]
        [Display(Name = "Engine Type")]
        public string EngineTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the epa class.
        /// </summary>
        /// <value>
        /// The name of the epa class.
        /// </value>
        [MaxLength(45)]
        [Display(Name = "Epa Class")]
        public string EpaClassName { get; set; }

        /// <summary>
        /// Gets or sets the name of the drive train.
        /// </summary>
        /// <value>
        /// The name of the drive train.
        /// </value>
        [MaxLength(255)]
        [Display(Name = "Drive Train")]
        public string DriveTrainName { get; set; }

        /// <summary>
        /// Gets or sets the passengers.
        /// </summary>
        /// <value>
        /// The passengers.
        /// </value>
        public int Passengers { get; set; }

        /// <summary>
        /// Gets or sets the passenger doors.
        /// </summary>
        /// <value>
        /// The passenger doors.
        /// </value>
        [Display(Name = "Passenger Doors")]
        public int PassengerDoors { get; set; }

        /// <summary>
        /// Gets or sets the name of the body style.
        /// </summary>
        /// <value>
        /// The name of the body style.
        /// </value>
        [MaxLength(255)]
        [Display(Name = "Body Style")]
        public string BodyStyleName { get; set; }

        /// <summary>
        /// Gets or sets the transmission.
        /// </summary>
        /// <value>
        /// The transmission.
        /// </value>
        [MaxLength(255)]
        public string Transmission { get; set; }

        /// <summary>
        /// Gets or sets the base weight.
        /// </summary>
        /// <value>
        /// The base weight.
        /// </value>
        public int BaseWeight { get; set; }

        /// <summary>
        /// Gets or sets the product image.
        /// </summary>
        /// <value>
        /// The product image.
        /// </value>
        [MaxLength(150)]
        public string ProductImage { get; set; }

        /// <summary>
        /// Gets or sets the product image large.
        /// </summary>
        /// <value>
        /// The product image large.
        /// </value>
        [MaxLength(150)]
        public string ProductImageLarge { get; set; }

        /// <summary>
        /// Gets or sets the product image thumb.
        /// </summary>
        /// <value>
        /// The product image thumb.
        /// </value>
        [MaxLength(150)]
        public string ProductImageThumb { get; set; }

        /// <summary>
        /// Gets or sets the allow show.
        /// </summary>
        /// <value>
        /// The allow show.
        /// </value>
        public sbyte AllowShow { get; set; }
    }
}
