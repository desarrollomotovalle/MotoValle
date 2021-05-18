// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Products.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Products model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Products model
    /// </summary>
    public partial class Products
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Products" /> class.
        /// </summary>
        public Products()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
            this.InventoryItems = new HashSet<InventoryItems>();
            this.ShoppingCartRecords = new HashSet<ShoppingCartRecords>();
            this.ShoppingCartRecordsGuest = new HashSet<ShoppingCartRecordsGuest>();
            this.ProductDocuments = new HashSet<ProductDocuments>();
            this.ProductMaintenances = new HashSet<ProductMaintenances>();
        }

        /// <summary>
        /// Gets or sets the products identifier.
        /// </summary>
        /// <value>
        /// The products identifier.
        /// </value>
        public int ProductsId { get; set; }

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        [Required]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>
        /// The upc.
        /// </value>
        public string Upc { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        [Required]
        [Display(Name = "Product Name"), StringLength(45)]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Short Description"), StringLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>
        /// The long description.
        /// </value>
        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        /// <summary>
        /// Gets or sets the fk categories identifier.
        /// </summary>
        /// <value>
        /// The fk categories identifier.
        /// </value>
        [Display(Name = "Category")]
        public int? FkCategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the is featured.
        /// </summary>
        /// <value>
        /// The is featured.
        /// </value>
        [Display(Name = "Is Featured")]
        public ulong? IsFeatured { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Cost must be mayor than 0")]
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
        [Range(0, double.PositiveInfinity, ErrorMessage = "Sales Price must be mayor than 0")]
        //[RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$", ErrorMessage = "Maximum 16 integer and Two Decimal Points")]
        public decimal? SalesPrice { get; set; }

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
        /// Gets or sets the trim.
        /// </summary>
        /// <value>
        /// The trim.
        /// </value>
        public string Trim { get; set; }

        /// <summary>
        /// Gets or sets the MSRP.
        /// </summary>
        /// <value>
        /// The MSRP.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Mrsp must be mayor than 0")]
        //[RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$", ErrorMessage = "Maximum 16 integer and Two Decimal Points")]
        public decimal? Msrp { get; set; }

        /// <summary>
        /// Gets or sets the fk makes identifier.
        /// </summary>
        /// <value>
        /// The fk makes identifier.
        /// </value>
        [Display(Name = "Make")]
        public int FkMakesId { get; set; }

        /// <summary>
        /// Gets or sets the fk models identifier.
        /// </summary>
        /// <value>
        /// The fk models identifier.
        /// </value>
        [Required]
        [Display(Name = "Model")]
        public int FkModelsId { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the horsepower.
        /// </summary>
        /// <value>
        /// The horsepower.
        /// </value>
        [Display(Name = "Horse Power")]
        public int? Horsepower { get; set; }

        /// <summary>
        /// Gets or sets the gas mileage.
        /// </summary>
        /// <value>
        /// The gas mileage.
        /// </value>
        [Display(Name = "Gas Mileage")]
        public string GasMileage { get; set; }

        /// <summary>
        /// Gets or sets the fk engine types identifier.
        /// </summary>
        /// <value>
        /// The fk engine types identifier.
        /// </value>
        [Display(Name = "Engine Type")]
        public int? FkEngineTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk epa classes identifier.
        /// </summary>
        /// <value>
        /// The fk epa classes identifier.
        /// </value>
        [Display(Name = "EPA Class")]
        public int? FkEpaClassesId { get; set; }

        /// <summary>
        /// Gets or sets the fk drive trains identifier.
        /// </summary>
        /// <value>
        /// The fk drive trains identifier.
        /// </value>
        [Display(Name = "Drive Train")]
        public int? FkDriveTrainsId { get; set; }

        /// <summary>
        /// Gets or sets the passengers.
        /// </summary>
        /// <value>
        /// The passengers.
        /// </value>
        public int? Passengers { get; set; }

        /// <summary>
        /// Gets or sets the passenger doors.
        /// </summary>
        /// <value>
        /// The passenger doors.
        /// </value>
        [Display(Name = "Passenger Doors")]
        public int? PassengerDoors { get; set; }

        /// <summary>
        /// Gets or sets the fk body styles identifier.
        /// </summary>
        /// <value>
        /// The fk body styles identifier.
        /// </value>
        [Display(Name = "Body Style")]
        public int? FkBodyStylesId { get; set; }

        /// <summary>
        /// Gets or sets the fk transmissions identifier.
        /// </summary>
        /// <value>
        /// The fk transmissions identifier.
        /// </value>
        [Display(Name = "Transmission")]
        public int? FkTransmissionsId { get; set; }

        /// <summary>
        /// Gets or sets the base weight.
        /// </summary>
        /// <value>
        /// The base weight.
        /// </value>
        [Display(Name = "Base Weight")]
        public int? BaseWeight { get; set; }

        /// <summary>
        /// Gets or sets the picture URL.
        /// </summary>
        /// <value>
        /// The picture URL.
        /// </value>
        [Display(Name = "Cover Picture Path")]
        public string PictureUrl { get; set; }

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
        /// Gets or sets the youtube URL.
        /// </summary>
        /// <value>
        /// The youtube URL.
        /// </value>
        [Display(Name = "Youtube Url")]
        public string YoutubeUrl { get; set; }

        /// <summary>
        /// Gets or sets the allow show.
        /// </summary>
        /// <value>
        /// The allow show.
        /// </value>
        [Display(Name = "Allow Show")]
        public sbyte? AllowShow { get; set; }

        /// <summary>
        /// Gets or sets the cylinders.
        /// </summary>
        /// <value>
        /// The cylinders.
        /// </value>
        public int? Cylinders { get; set; }

        /// <summary>
        /// Gets or sets the load capacity.
        /// </summary>
        /// <value>
        /// The load capacity.
        /// </value>
        [Display(Name = "Load Capacity")]
        public int? LoadCapacity { get; set; }

        /// <summary>
        /// Gets or sets the weight without platform.
        /// </summary>
        /// <value>
        /// The weight without platform.
        /// </value>
        [Display(Name = "Weight without platform")]
        public int? SingleWeight { get; set; }

        /// <summary>
        /// Gets or sets the productivity.
        /// </summary>
        /// <value>
        /// The productivity.
        /// </value>
        [StringLength(45)]
        public string Productivity { get; set; }

        /// <summary>
        /// Gets or sets the length of the bale.
        /// </summary>
        /// <value>
        /// The length of the bale.
        /// </value>
        [Display(Name = "Bale Length")]
        public string BaleLength { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        [StringLength(45)]
        public string Speed { get; set; }

        /// <summary>
        /// Gets or sets the recommendedpower.
        /// </summary>
        /// <value>
        /// The recommendedpower.
        /// </value>
        [Display(Name = "Recommended Power")]
        [StringLength(45)]
        public string RecommendedPower { get; set; }

        /// <summary>
        /// Gets or sets the width of the working.
        /// </summary>
        /// <value>
        /// The width of the working.
        /// </value>
        [Display(Name = "Working Width")]
        public decimal? WorkingWidth { get; set; }

        /// <summary>
        /// Gets or sets the number of lines.
        /// </summary>
        /// <value>
        /// The number of lines.
        /// </value>
        [Display(Name = "Number Of Lines")]
        public int? NumberOfLines { get; set; }

        /// <summary>
        /// Gets or sets the number of discs.
        /// </summary>
        /// <value>
        /// The number of discs.
        /// </value>
        [Display(Name = "Number Of Discs")]
        public int? NumberOfDiscs { get; set; }

        /// <summary>
        /// Gets or sets the input rotation.
        /// </summary>
        /// <value>
        /// The input rotation.
        /// </value>
        [Display(Name = "Input rotation (TDF)")]
        [StringLength(45)]
        public string InputRotation { get; set; }

        /// <summary>
        /// Gets or sets the standard cover.
        /// </summary>
        /// <value>
        /// The standard cover.
        /// </value>
        [Display(Name = "Standard Cover")]
        [StringLength(45)]
        public string StandardCover { get; set; }

        /// <summary>
        /// Gets or sets the width of the cutting.
        /// </summary>
        /// <value>
        /// The width of the cutting.
        /// </value>
        [Display(Name = "Cutting Width")]
        [StringLength(45)]
        public string CuttingWidth { get; set; }

        /// <summary>
        /// Gets or sets the aspiration types identifier.
        /// </summary>
        /// <value>
        /// The aspiration types identifier.
        /// </value>
        [Display(Name = "Aspiration")]
        public int? FkAspirationTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk rear tires types identifier.
        /// </summary>
        /// <value>
        /// The fk rear tires types identifier.
        /// </value>
        [Display(Name = "Rear Tires Type")]
        public int? FkRearTiresTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk front tires types identifier.
        /// </summary>
        /// <value>
        /// The fk front tires types identifier.
        /// </value>
        [Display(Name = "Front Tires Type")]
        public int? FkFrontTiresTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk separation systems identifier.
        /// </summary>
        /// <value>
        /// The fk separation systems identifier.
        /// </value>
        [Display(Name = "Separation System")]
        public int? FkSeparationSystemsId { get; set; }

        /// <summary>
        /// Gets or sets the fk hitch systems identifier.
        /// </summary>
        /// <value>
        /// The fk hitch systems identifier.
        /// </value>
        [Display(Name = "Hitch System")]
        public int? FkHitchSystemsId { get; set; }

        /// <summary>
        /// Gets or sets the fk hitch pin identifier.
        /// </summary>
        /// <value>
        /// The fk hitch pin identifier.
        /// </value>
        [Display(Name = "Hitch Pin")]
        public int? FkHitchPinesId { get; set; }

        /// <summary>
        /// Gets or sets the fk chamber dimensions identifier.
        /// </summary>
        /// <value>
        /// The fk chamber dimensions identifier.
        /// </value>
        [Display(Name = "Chamber Dimensions")]
        public int? FkChamberDimensionsId { get; set; }

        /// <summary>
        /// Gets or sets the fk configuration types identifier.
        /// </summary>
        /// <value>
        /// The fk configuration types identifier.
        /// </value>
        [Display(Name = "Configuration Type")]
        public int? FkConfigurationTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk conditioning systems identifier.
        /// </summary>
        /// <value>
        /// The fk conditioning systems identifier.
        /// </value>
        [Display(Name = "Conditioning System")]
        public int? FkConditioningSystemsId { get; set; }

        /// <summary>
        /// Gets or sets the fk blades types identifier.
        /// </summary>
        /// <value>
        /// The fk blades types identifier.
        /// </value>
        [Display(Name = "Blades")]
        public int? FkBladesTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk turning radius types identifier.
        /// </summary>
        /// <value>
        /// The fk turning radius types identifier.
        /// </value>
        [Display(Name = "Turning Radius Type")]
        public int? FkTurningRadiusTypesId { get; set; }

        /// <summary>
        /// Gets or sets the fk body styles.
        /// </summary>
        /// <value>
        /// The fk body styles.
        /// </value>
        [Display(Name = "Body Style")]
        public virtual BodyStyles FkBodyStyles { get; set; }

        /// <summary>
        /// Gets or sets the fk categories.
        /// </summary>
        /// <value>
        /// The fk categories.
        /// </value>
        [Display(Name = "Category")]
        public virtual Categories FkCategories { get; set; }

        /// <summary>
        /// Gets or sets the fk drive trains.
        /// </summary>
        /// <value>
        /// The fk drive trains.
        /// </value>
        [Display(Name = "Drive Train")]
        public virtual DriveTrains FkDriveTrains { get; set; }

        /// <summary>
        /// Gets or sets the fk engine types.
        /// </summary>
        /// <value>
        /// The fk engine types.
        /// </value>
        [Display(Name = "Engine Type")]
        public virtual EngineTypes FkEngineTypes { get; set; }

        /// <summary>
        /// Gets or sets the fk epa classes.
        /// </summary>
        /// <value>
        /// The fk epa classes.
        /// </value>
        [Display(Name = "EPA Class")]
        public virtual EpaClasses FkEpaClasses { get; set; }

        /// <summary>
        /// Gets or sets the fk makes.
        /// </summary>
        /// <value>
        /// The fk makes.
        /// </value>
        [Display(Name = "Make")]
        public virtual Makes FkMakes { get; set; }

        /// <summary>
        /// Gets or sets the fk models.
        /// </summary>
        /// <value>
        /// The fk models.
        /// </value>
        [Display(Name = "Model")]
        public virtual Models FkModels { get; set; }

        /// <summary>
        /// Gets or sets the fk transmissions.
        /// </summary>
        /// <value>
        /// The fk transmissions.
        /// </value>
        [Display(Name = "Transmission")]
        public virtual Transmissions FkTransmissions { get; set; }

        /// <summary>
        /// Gets or sets the fk aspiration types.
        /// </summary>
        /// <value>
        /// The fk aspiration types.
        /// </value>
        [Display(Name = "Aspiration Type")]
        public virtual AspirationTypes FkAspirationTypes { get; set; }

        /// <summary>
        /// Gets or sets the fk rear tires types.
        /// </summary>
        /// <value>
        /// The fk rear tires types.
        /// </value>
        [Display(Name = "Rear Tires Type")]
        public virtual RearTiresTypes FkRearTiresTypes { get; set; }

        /// <summary>
        /// Gets or sets the fk front tires types.
        /// </summary>
        /// <value>
        /// The fk front tires types.
        /// </value>
        [Display(Name = "Front Tires Type")]
        public virtual FrontTiresTypes FkFrontTiresTypes { get; set; }

        /// <summary>
        /// Gets or sets the fk separation systems.
        /// </summary>
        /// <value>
        /// The fk separation systems.
        /// </value>
        [Display(Name = "Separation System")]
        public virtual SeparationSystems FkSeparationSystems { get; set; }

        /// <summary>
        /// Gets or sets the fk hitch systems.
        /// </summary>
        /// <value>
        /// The fk hitch systems.
        /// </value>
        [Display(Name = "Hitch System")]
        public virtual HitchSystems FkHitchSystems { get; set; }

        /// <summary>
        /// Gets or sets the fk hitch pines.
        /// </summary>
        /// <value>
        /// The fk hitch pines.
        /// </value>
        [Display(Name = "Hitch System")]
        public virtual HitchPines FkHitchPines { get; set; }

        /// <summary>
        /// Gets or sets the fk chamber dimensions.
        /// </summary>
        /// <value>
        /// The fk chamber dimensions.
        /// </value>
        [Display(Name = "Chamber Dimensions")]
        public virtual ChamberDimensions FkChamberDimensions { get; set; }

        /// <summary>
        /// Gets or sets the fk configuration types.
        /// </summary>
        /// <value>
        /// The fk configuration types.
        /// </value>
        [Display(Name = "Configuration Types")]
        public virtual ConfigurationTypes FkConfigurationTypes { get; set; }

        /// <summary>
        /// Gets or sets the fk conditioning systems.
        /// </summary>
        /// <value>
        /// The fk conditioning systems.
        /// </value>
        [Display(Name = "Conditioning System")]
        public virtual ConditioningSystems FkConditioningSystems { get; set; }

        /// <summary>
        /// Gets or sets the fk blade types.
        /// </summary>
        /// <value>
        /// The fk blade types.
        /// </value>
        [Display(Name = "Blade Types")]
        public virtual BladeTypes FkBladeTypes { get; set; }

        /// <summary>
        /// Gets or sets the fk turning radius types.
        /// </summary>
        /// <value>
        /// The fk turning radius types.
        /// </value>
        [Display(Name = "Turning Radius Types")]
        public virtual TurningRadiusTypes FkTurningRadiusTypes { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the inventory items.
        /// </summary>
        /// <value>
        /// The inventory items.
        /// </value>
        public virtual ICollection<InventoryItems> InventoryItems { get; set; }

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
        /// Gets or sets the product documents.
        /// </summary>
        /// <value>
        /// The product documents.
        /// </value>
        public virtual ICollection<ProductDocuments> ProductDocuments { get; set; }

        /// <summary>
        /// Gets or sets the product maintenances.
        /// </summary>
        /// <value>
        /// The product maintenances.
        /// </value>
        public virtual ICollection<ProductMaintenances> ProductMaintenances { get; set; }
    }
}