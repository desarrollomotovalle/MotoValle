// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ecommerceContext.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  ecommerce Context
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.EF
{
    using Ecommerce.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    /// <summary>
    /// ecommerce Context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class ecommerceContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ecommerceContext"/> class.
        /// </summary>
        public ecommerceContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ecommerceContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ecommerceContext(DbContextOptions<ecommerceContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the body styles.
        /// </summary>
        /// <value>
        /// The body styles.
        /// </value>
        public virtual DbSet<BodyStyles> BodyStyles { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual DbSet<Categories> Categories { get; set; }

        /// <summary>
        /// Gets or sets the category pictures.
        /// </summary>
        /// <value>
        /// The category pictures.
        /// </value>
        public virtual DbSet<CategoryPictures> CategoryPictures { get; set; }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>
        /// The colors.
        /// </value>
        public virtual DbSet<Colors> Colors { get; set; }

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public virtual DbSet<Customers> Customers { get; set; }

        /// <summary>
        /// Gets or sets the drive trains.
        /// </summary>
        /// <value>
        /// The drive trains.
        /// </value>
        public virtual DbSet<DriveTrains> DriveTrains { get; set; }

        /// <summary>
        /// Gets or sets the engine types.
        /// </summary>
        /// <value>
        /// The engine types.
        /// </value>
        public virtual DbSet<EngineTypes> EngineTypes { get; set; }

        /// <summary>
        /// Gets or sets the epa classes.
        /// </summary>
        /// <value>
        /// The epa classes.
        /// </value>
        public virtual DbSet<EpaClasses> EpaClasses { get; set; }

        /// <summary>
        /// Gets or sets the inventory item pictures.
        /// </summary>
        /// <value>
        /// The inventory item pictures.
        /// </value>
        public virtual DbSet<InventoryItemPictures> InventoryItemPictures { get; set; }

        /// <summary>
        /// Gets or sets the inventory items.
        /// </summary>
        /// <value>
        /// The inventory items.
        /// </value>
        public virtual DbSet<InventoryItems> InventoryItems { get; set; }

        /// <summary>
        /// Gets or sets the makes.
        /// </summary>
        /// <value>
        /// The makes.
        /// </value>
        public virtual DbSet<Makes> Makes { get; set; }

        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public virtual DbSet<Models> Models { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public virtual DbSet<Orders> Orders { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual DbSet<Products> Products { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records.
        /// </summary>
        /// <value>
        /// The shopping cart records.
        /// </value>
        public virtual DbSet<ShoppingCartRecords> ShoppingCartRecords { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records guest.
        /// </summary>
        /// <value>
        /// The shopping cart records guest.
        /// </value>
        public virtual DbSet<ShoppingCartRecordsGuest> ShoppingCartRecordsGuest { get; set; }

        /// <summary>
        /// Gets or sets the transmissions.
        /// </summary>
        /// <value>
        /// The transmissions.
        /// </value>
        public virtual DbSet<Transmissions> Transmissions { get; set; }

        /// <summary>
        /// Gets or sets the warranties.
        /// </summary>
        /// <value>
        /// The warranties.
        /// </value>
        public virtual DbSet<Warranties> Warranties { get; set; }

        /// <summary>
        /// Gets or sets the news letter subscriptions.
        /// </summary>
        /// <value>
        /// The news letter subscriptions.
        /// </value>
        public virtual DbSet<NewsLetterSubscriptions> NewsLetterSubscriptions { get; set; }

        /// <summary>
        /// Gets or sets the funding requests.
        /// </summary>
        /// <value>
        /// The funding requests.
        /// </value>
        public virtual DbSet<FundingRequests> FundingRequests { get; set; }

        /// <summary>
        /// Gets or sets the interested customers.
        /// </summary>
        /// <value>
        /// The interested customers.
        /// </value>
        public virtual DbSet<InterestedCustomers> InterestedCustomers { get; set; }

        /// <summary>
        /// Gets or sets the taxes.
        /// </summary>
        /// <value>
        /// The taxes.
        /// </value>
        public virtual DbSet<Taxes> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the taxes for inventory.
        /// </summary>
        /// <value>
        /// The taxes for inventory.
        /// </value>
        public virtual DbSet<TaxesForInventory> TaxesForInventory { get; set; }

        /// <summary>
        /// Gets or sets the h orders taxes.
        /// </summary>
        /// <value>
        /// The h orders taxes.
        /// </value>
        public virtual DbSet<HOrdersTaxes> HOrdersTaxes { get; set; }

        /// <summary>
        /// Gets or sets the product documents categories.
        /// </summary>
        /// <value>
        /// The product documents categories.
        /// </value>
        public virtual DbSet<ProductDocumentsCategories> ProductDocumentsCategories { get; set; }

        /// <summary>
        /// Gets or sets the product documents.
        /// </summary>
        /// <value>
        /// The product documents.
        /// </value>
        public virtual DbSet<ProductDocuments> ProductDocuments { get; set; }

        /// <summary>
        /// Gets or sets the aspiration types.
        /// </summary>
        /// <value>
        /// The aspiration types.
        /// </value>
        public virtual DbSet<AspirationTypes> AspirationTypes { get; set; }

        /// <summary>
        /// Gets or sets the blade types.
        /// </summary>
        /// <value>
        /// The blade types.
        /// </value>
        public virtual DbSet<BladeTypes> BladeTypes { get; set; }

        /// <summary>
        /// Gets or sets the chamber dimensions.
        /// </summary>
        /// <value>
        /// The chamber dimensions.
        /// </value>
        public virtual DbSet<ChamberDimensions> ChamberDimensions { get; set; }

        /// <summary>
        /// Gets or sets the conditioning systems.
        /// </summary>
        /// <value>
        /// The conditioning systems.
        /// </value>
        public virtual DbSet<ConditioningSystems> ConditioningSystems { get; set; }

        /// <summary>
        /// Gets or sets the configuration types.
        /// </summary>
        /// <value>
        /// The configuration types.
        /// </value>
        public virtual DbSet<ConfigurationTypes> ConfigurationTypes { get; set; }

        /// <summary>
        /// Gets or sets the hitch pines.
        /// </summary>
        /// <value>
        /// The hitch pines.
        /// </value>
        public virtual DbSet<HitchPines> HitchPines { get; set; }

        /// <summary>
        /// Gets or sets the hitch systems.
        /// </summary>
        /// <value>
        /// The hitch systems.
        /// </value>
        public virtual DbSet<HitchSystems> HitchSystems { get; set; }

        /// <summary>
        /// Gets or sets the rear tires types.
        /// </summary>
        /// <value>
        /// The rear tires types.
        /// </value>
        public virtual DbSet<RearTiresTypes> RearTiresTypes { get; set; }

        /// <summary>
        /// Gets or sets the front tires types.
        /// </summary>
        /// <value>
        /// The front tires types.
        /// </value>
        public virtual DbSet<FrontTiresTypes> FrontTiresTypes { get; set; }

        /// <summary>
        /// Gets or sets the separation systems.
        /// </summary>
        /// <value>
        /// The separation systems.
        /// </value>
        public virtual DbSet<SeparationSystems> SeparationSystems { get; set; }

        /// <summary>
        /// Gets or sets the turning radius types.
        /// </summary>
        /// <value>
        /// The turning radius types.
        /// </value>
        public virtual DbSet<TurningRadiusTypes> TurningRadiusTypes { get; set; }

        /// <summary>
        /// Gets or sets the campaign leads.
        /// </summary>
        /// <value>
        /// The campaign leads.
        /// </value>
        public virtual DbSet<CampaignLeads> CampaignLeads { get; set; }

        /// <summary>
        /// Gets or sets the search engine optimizations.
        /// </summary>
        /// <value>
        /// The search engine optimizations.
        /// </value>
        public virtual DbSet<SearchEngineOptimizations> SearchEngineOptimizations { get; set; }

        /// <summary>
        /// Gets or sets the hero images.
        /// </summary>
        /// <value>
        /// The hero images.
        /// </value>
        public virtual DbSet<HeroImages> HeroImages { get; set; }

        /// <summary>
        /// Gets or sets the product maintenances.
        /// </summary>
        /// <value>
        /// The product maintenances.
        /// </value>
        public virtual DbSet<ProductMaintenances> ProductMaintenances { get; set; }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BodyStyles>(entity =>
            {
                entity.ToTable("body_styles");

                entity.HasIndex(e => e.BodyStyleName)
                    .HasName("idxbodystyle");

                entity.Property(e => e.BodyStylesId)
                    .HasColumnName("body_styles_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BodyStyleName)
                    .IsRequired()
                    .HasColumnName("body_style_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryName)
                    .HasName("idxcategoryname");

                entity.Property(e => e.CategoriesId)
                    .HasColumnName("categories_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<CategoryPictures>(entity =>
            {
                entity.ToTable("category_pictures");

                entity.HasIndex(e => e.FkCategoriesId)
                    .HasName("category_pictures_categories_idx");

                entity.HasIndex(e => e.PictureName)
                    .HasName("idxpicturename");

                entity.Property(e => e.CategoryPicturesId)
                    .HasColumnName("category_pictures_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkCategoriesId)
                    .HasColumnName("fk_categories_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureName)
                    .IsRequired()
                    .HasColumnName("picture_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PictureType)
                    .IsRequired()
                    .HasColumnName("picture_type")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PictureUrl)
                    .IsRequired()
                    .HasColumnName("picture_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkCategories)
                    .WithMany(p => p.CategoryPictures)
                    .HasForeignKey(d => d.FkCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category_pictures_categories");
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.ToTable("colors");

                entity.HasIndex(e => e.ColorName)
                    .HasName("idxcolorname");

                entity.HasIndex(e => e.FkMakesId)
                    .HasName("colors_makes_idx");

                entity.Property(e => e.ColorsId)
                    .HasColumnName("colors_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasColumnName("color_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkMakesId)
                    .HasColumnName("fk_makes_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkMakes)
                    .WithMany(p => p.Colors)
                    .HasForeignKey(d => d.FkMakesId)
                    .HasConstraintName("colors_makes");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.AccountNumber)
                    .HasName("idxaccountnumber");

                entity.HasIndex(e => e.AlternateNumber)
                    .HasName("idxalternate");

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("idxemail");

                entity.HasIndex(e => e.FullName)
                    .HasName("idxfullname");

                entity.HasIndex(e => e.PhoneNumber)
                    .HasName("idxphonenumber");

                entity.Property(e => e.CustomersId)
                    .HasColumnName("customers_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountNumber)
                    .HasColumnName("account_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AlternateNumber)
                    .HasColumnName("alternate_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToAddress)
                    .HasColumnName("bill_to_address")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToCity)
                    .HasColumnName("bill_to_city")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToState)
                    .HasColumnName("bill_to_state")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToZipcode)
                    .HasColumnName("bill_to_zipcode")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToAddress)
                    .HasColumnName("ship_to_address")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToCity)
                    .HasColumnName("ship_to_city")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToState)
                    .HasColumnName("ship_to_state")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToZipcode)
                    .HasColumnName("ship_to_zipcode")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<DriveTrains>(entity =>
            {
                entity.ToTable("drive_trains");

                entity.HasIndex(e => e.DriveTrainName)
                    .HasName("idxdrivetrainname");

                entity.Property(e => e.DriveTrainsId)
                    .HasColumnName("drive_trains_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DriveTrainName)
                    .IsRequired()
                    .HasColumnName("drive_train_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<EngineTypes>(entity =>
            {
                entity.ToTable("engine_types");

                entity.HasIndex(e => e.EngineTypeName)
                    .HasName("idxenginetypename");

                entity.Property(e => e.EngineTypesId)
                    .HasColumnName("engine_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EngineTypeName)
                    .IsRequired()
                    .HasColumnName("engine_type_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<EpaClasses>(entity =>
            {
                entity.ToTable("epa_classes");

                entity.HasIndex(e => e.EpaClassName)
                    .HasName("idxepaclassname");

                entity.Property(e => e.EpaClassesId)
                    .HasColumnName("epa_classes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EpaClassName)
                    .IsRequired()
                    .HasColumnName("epa_class_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<InventoryItemPictures>(entity =>
            {
                entity.ToTable("inventory_item_pictures");

                entity.HasIndex(e => e.FkInventoryItemsId)
                    .HasName("inventory_items_inv_itmpic_idx");

                entity.HasIndex(e => e.PictureName)
                    .HasName("idxpicturename");

                entity.Property(e => e.InventoryItemPicturesId)
                    .HasColumnName("inventory_item_pictures_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AlternateText)
                    .HasColumnName("alternate_text")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkInventoryItemsId)
                    .HasColumnName("fk_inventory_items_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureHeight)
                    .HasColumnName("picture_height")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureName)
                    .IsRequired()
                    .HasColumnName("picture_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PictureUrl)
                    .IsRequired()
                    .HasColumnName("picture_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PictureWidth)
                    .HasColumnName("picture_width")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkInventoryItems)
                    .WithMany(p => p.InventoryItemPictures)
                    .HasForeignKey(d => d.FkInventoryItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_items_inv_itmpic");
            });

            modelBuilder.Entity<InventoryItems>(entity =>
            {
                entity.ToTable("inventory_items");

                entity.HasIndex(e => e.FkColorsId)
                    .HasName("colors_inventory_Items_idx");

                entity.HasIndex(e => e.FkProductsId)
                    .HasName("products_inventory_items_idx");

                entity.HasIndex(e => e.FkWarrantiesId)
                    .HasName("warranties_inventory_items_idx");

                entity.HasIndex(e => e.Vin)
                    .HasName("idxvin");

                entity.Property(e => e.InventoryItemsId)
                    .HasColumnName("inventory_items_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.FkColorsId)
                    .HasColumnName("fk_colors_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkProductsId)
                    .HasColumnName("fk_products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkWarrantiesId)
                    .HasColumnName("fk_warranties_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Picture360Quantity)
                   .HasColumnName("picture_360_quantity")
                   .HasColumnType("int(11)");

                entity.Property(e => e.QuantityInStock)
                   .HasColumnName("quantity_in_stock")
                   .HasColumnType("int(11)");

                entity.Property(e => e.IsNew)
                    .HasColumnName("is_new")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.IsSold)
                    .HasColumnName("is_sold")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.AllowShow)
                   .HasColumnName("allow_show")
                   .HasColumnType("tinyint(4)");

                entity.Property(e => e.Mileage)
                    .HasColumnName("mileage")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SalesPrice)
                    .HasColumnName("sales_price")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Vin)
                    .HasColumnName("vin")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Picture360Url)
                   .HasColumnName("picture_360_url")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Cupix360Url)
                   .HasColumnName("cupix_360_url")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CoverPictureUrl)
                    .HasColumnName("cover_picture_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkColors)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.FkColorsId)
                    .HasConstraintName("colors_inventory_Items");

                entity.HasOne(d => d.FkProducts)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.FkProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_inventory_items");

                entity.HasOne(d => d.FkWarranties)
                    .WithMany(p => p.InventoryItems)
                    .HasForeignKey(d => d.FkWarrantiesId)
                    .HasConstraintName("warranties_inventory_items");
            });

            modelBuilder.Entity<Makes>(entity =>
            {
                entity.ToTable("makes");

                entity.HasIndex(e => e.MakeName)
                    .HasName("idxmakename");

                entity.Property(e => e.MakesId)
                    .HasColumnName("makes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.MakeName)
                    .IsRequired()
                    .HasColumnName("make_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Models>(entity =>
            {
                entity.ToTable("models");

                entity.HasIndex(e => e.FkMakesId)
                    .HasName("makes_models_idx");

                entity.HasIndex(e => e.ModelName)
                    .HasName("idxmodelname");

                entity.Property(e => e.ModelsId)
                    .HasColumnName("models_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkMakesId)
                    .HasColumnName("fk_makes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkCategoriesId)
                    .HasColumnName("fk_categories_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasColumnName("model_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkMakes)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.FkMakesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("makes_models");

                entity.HasOne(d => d.FkCategories)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.FkCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("categories_models");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.ToTable("order_details");

                entity.HasIndex(e => e.FkOrdersId)
                    .HasName("order_details_orders_idx");

                entity.HasIndex(e => e.FkProductsId)
                    .HasName("order_details_products_idx");

                entity.HasIndex(e => e.FKInventoryItemsId)
                    .HasName("order_details_inventory_items_idx");

                entity.Property(e => e.OrderDetailsId)
                    .HasColumnName("order_details_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkOrdersId)
                    .HasColumnName("fk_orders_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkProductsId)
                    .HasColumnName("fk_products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FKInventoryItemsId)
                    .HasColumnName("fk_inventory_items_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemPrice)
                    .HasColumnName("item_price")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.ShipDate)
                    .HasColumnName("ship_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .IsRequired(false)
                    .HasColumnType("mediumtext")
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Tax)
                    .HasColumnName("tax")
                    .HasColumnType("decimal(19,2)");

                entity.HasOne(d => d.FkOrders)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FkOrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_details_orders");

                entity.HasOne(d => d.FkProducts)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FkProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_details_products");

                entity.HasOne(d => d.FKInventoryItems)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FKInventoryItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_details_inventory_items");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.FkCustomersId)
                    .HasName("orders_customers_idx");

                entity.HasIndex(e => e.FkCustomersId)
                    .HasName("order_funding_requests_idx");

                entity.Property(e => e.OrdersId)
                    .HasColumnName("orders_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkCustomersId)
                    .HasColumnName("fk_customers_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionId)
                   .HasColumnName("transaction_id")
                   .HasColumnType("varchar(64)")
                   .IsRequired(false);

                entity.Property(e => e.FKFundingRequestsId)
                   .HasColumnName("fk_funding_requests_id")
                   .HasColumnType("int(11)")
                   .IsRequired(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PaymentMethod)
                    .HasColumnName("payment_method")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.InvoiceUrl)
                    .HasColumnName("invoice_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdentificationType)
                    .HasColumnName("identification_type")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdentificationNumber)
                    .HasColumnName("identification_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AlternateNumber)
                    .HasColumnName("alternate_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToAddress)
                    .HasColumnName("bill_to_address")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToCity)
                    .HasColumnName("bill_to_city")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToState)
                    .HasColumnName("bill_to_state")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BillToZipcode)
                    .HasColumnName("bill_to_zipcode")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToAddress)
                    .HasColumnName("ship_to_address")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToCity)
                    .HasColumnName("ship_to_city")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToState)
                    .HasColumnName("ship_to_state")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShipToZipcode)
                    .HasColumnName("ship_to_zipcode")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderNumber)
                    .HasColumnName("order_number")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("subtotal")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Tax)
                    .HasColumnName("tax")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Shipping)
                   .HasColumnName("shipping")
                   .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Discount)
                   .HasColumnName("discount")
                   .HasColumnType("decimal(19,2)");

                entity.Property(e => e.PartialAmountPaid)
                  .HasColumnName("partial_amount_paid")
                  .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Tax)
                    .HasColumnName("tax")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(19,2)");

                entity.HasOne(d => d.FkCustomers)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FkCustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_customers");

                entity.HasOne(d => d.FKFundingRequests)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FKFundingRequestsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_funding_requests");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.Description)
                    .HasName("idxdescription");

                entity.HasIndex(e => e.FkBodyStylesId)
                    .HasName("body_styles_products_idx");

                entity.HasIndex(e => e.FkCategoriesId)
                    .HasName("product_categories_idx");

                entity.HasIndex(e => e.FkDriveTrainsId)
                    .HasName("drive_trains_products_idx");

                entity.HasIndex(e => e.FkEngineTypesId)
                    .HasName("engine_types_products_idx");

                entity.HasIndex(e => e.FkEpaClassesId)
                    .HasName("epa_classes_products_idx");

                entity.HasIndex(e => e.FkMakesId)
                    .HasName("makes_products_idx");

                entity.HasIndex(e => e.FkModelsId)
                    .HasName("models_products_idx");

                entity.HasIndex(e => e.FkTransmissionsId)
                    .HasName("transmissions_products_idx");

                entity.HasIndex(e => e.Sku)
                    .HasName("idxsku");

                entity.HasIndex(e => e.Upc)
                    .HasName("idxupc");

                ////Massey Index
                entity.HasIndex(e => e.FkAspirationTypesId)
                    .HasName("aspiration_types_products_idx");

                entity.HasIndex(e => e.FkRearTiresTypesId)
                    .HasName("rear_tires_types_products_idx");

                entity.HasIndex(e => e.FkFrontTiresTypesId)
                    .HasName("front_tires_types_products_idx");

                entity.HasIndex(e => e.FkSeparationSystemsId)
                    .HasName("separation_systems_products_idx");

                entity.HasIndex(e => e.FkHitchSystemsId)
                    .HasName("hitch_systems_products_idx");

                entity.HasIndex(e => e.FkHitchPinesId)
                    .HasName("hitch_pines_products_idx");

                entity.HasIndex(e => e.FkChamberDimensionsId)
                    .HasName("chamber_dimensions_products_idx");

                entity.HasIndex(e => e.FkConfigurationTypesId)
                    .HasName("configuration_types_products_idx");

                entity.HasIndex(e => e.FkConditioningSystemsId)
                    .HasName("conditioning_systems_products_idx");

                entity.HasIndex(e => e.FkBladesTypesId)
                    .HasName("blade_types_idx");

                entity.HasIndex(e => e.FkTurningRadiusTypesId)
                    .HasName("turning_radius_types_products_idx");
                //////
                
                entity.Property(e => e.ProductsId)
                    .HasColumnName("products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BaseWeight)
                    .HasColumnName("base_weight")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AllowShow)
                   .HasColumnName("allow_show")
                   .HasColumnType("tinyint(4)");

                entity.Property(e => e.Picture360Quantity)
                    .HasColumnName("picture_360_quantity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AllowShow)
                   .HasColumnName("allow_show")
                   .HasColumnType("tinyint(4)");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkBodyStylesId)
                    .HasColumnName("fk_body_styles_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkCategoriesId)
                    .HasColumnName("fk_categories_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkDriveTrainsId)
                    .HasColumnName("fk_drive_trains_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkEngineTypesId)
                    .HasColumnName("fk_engine_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkEpaClassesId)
                    .HasColumnName("fk_epa_classes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkMakesId)
                    .HasColumnName("fk_makes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkModelsId)
                    .HasColumnName("fk_models_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkTransmissionsId)
                    .HasColumnName("fk_transmissions_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GasMileage)
                    .HasColumnName("gas_mileage")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Horsepower)
                    .HasColumnName("horsepower")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsFeatured)
                    .HasColumnName("is_featured")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LongDescription)
                    .HasColumnName("long_description")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Msrp)
                    .HasColumnName("msrp")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.PassengerDoors)
                    .HasColumnName("passenger_doors")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Passengers)
                    .HasColumnName("passengers")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductName)
                   .HasColumnName("product_name")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.QuantityInStock)
                    .HasColumnName("quantity_in_stock")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SalesPrice)
                    .HasColumnName("sales_price")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("sku")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Trim)
                    .HasColumnName("trim")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Upc)
                    .HasColumnName("upc")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PictureUrl)
                   .HasColumnName("picture_url")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Picture360Url)
                   .HasColumnName("picture_360_url")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Cupix360Url)
                   .HasColumnName("cupix_360_url")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.YoutubeUrl)
                   .HasColumnName("youtube_url")
                   .HasColumnType("varchar(255)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                ////Massey Fields
                entity.Property(e => e.Cylinders)
                    .HasColumnName("cylinders")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LoadCapacity)
                    .HasColumnName("load_capacity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SingleWeight)
                    .HasColumnName("single_weight")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Productivity)
                    .HasColumnName("productivity")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BaleLength)
                    .HasColumnName("bale_length")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CuttingWidth)
                   .HasColumnName("cutting_width")
                   .HasColumnType("varchar(45)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Speed)
                    .HasColumnName("speed")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RecommendedPower)
                    .HasColumnName("recommended_power")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.WorkingWidth)
                    .HasColumnName("working_width")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.NumberOfLines)
                    .HasColumnName("number_of_lines")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfDiscs)
                    .HasColumnName("number_of_discs")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InputRotation)
                    .HasColumnName("input_rotation")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StandardCover)
                    .HasColumnName("standard_cover")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.FkAspirationTypesId)
                    .HasColumnName("fk_aspiration_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkRearTiresTypesId)
                    .HasColumnName("fk_rear_tires_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkFrontTiresTypesId)
                    .HasColumnName("fk_front_tires_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkSeparationSystemsId)
                    .HasColumnName("fk_separation_systems_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkHitchSystemsId)
                    .HasColumnName("fk_hitch_systems_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkHitchPinesId)
                    .HasColumnName("fk_hitch_pines_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkChamberDimensionsId)
                    .HasColumnName("fk_chamber_dimensions_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkConfigurationTypesId)
                    .HasColumnName("fk_configuration_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkConditioningSystemsId)
                    .HasColumnName("fk_conditioning_systems_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkBladesTypesId)
                    .HasColumnName("fk_blades_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkTurningRadiusTypesId)
                    .HasColumnName("fk_turning_radius_types_id")
                    .HasColumnType("int(11)");
                //////

                entity.HasOne(d => d.FkBodyStyles)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkBodyStylesId)
                    .HasConstraintName("body_styles_products");

                entity.HasOne(d => d.FkCategories)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkCategoriesId)
                    .HasConstraintName("product_categories");

                entity.HasOne(d => d.FkDriveTrains)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkDriveTrainsId)
                    .HasConstraintName("drive_trains_products");

                entity.HasOne(d => d.FkEngineTypes)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkEngineTypesId)
                    .HasConstraintName("engine_types_products");

                entity.HasOne(d => d.FkEpaClasses)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkEpaClassesId)
                    .HasConstraintName("epa_classes_products");

                entity.HasOne(d => d.FkMakes)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkMakesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("makes_products");

                entity.HasOne(d => d.FkModels)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkModelsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("models_products");

                entity.HasOne(d => d.FkTransmissions)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkTransmissionsId)
                    .HasConstraintName("transmissions_products");

                ////Massey Relationships
                entity.HasOne(d => d.FkAspirationTypes)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkAspirationTypesId)
                   .HasConstraintName("aspiration_types_products");

                entity.HasOne(d => d.FkRearTiresTypes)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkRearTiresTypesId)
                   .HasConstraintName("rear_tires_types_products");

                entity.HasOne(d => d.FkFrontTiresTypes)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkFrontTiresTypesId)
                   .HasConstraintName("front_tires_types_products");

                entity.HasOne(d => d.FkSeparationSystems)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkBodyStylesId)
                   .HasConstraintName("separation_systems_products");

                entity.HasOne(d => d.FkHitchSystems)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkHitchSystemsId)
                   .HasConstraintName("hitch_systems_products");

                entity.HasOne(d => d.FkHitchPines)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkHitchPinesId)
                   .HasConstraintName("hitch_pines_products");

                entity.HasOne(d => d.FkChamberDimensions)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkChamberDimensionsId)
                   .HasConstraintName("chamber_dimensions_products");

                entity.HasOne(d => d.FkConfigurationTypes)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkConfigurationTypesId)
                   .HasConstraintName("configuration_types_products");

                entity.HasOne(d => d.FkConditioningSystems)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkConditioningSystemsId)
                   .HasConstraintName("fk_conditioning_systems_id");

                entity.HasOne(d => d.FkBladeTypes)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkBladesTypesId)
                   .HasConstraintName("fk_blades_types_id");

                entity.HasOne(d => d.FkTurningRadiusTypes)
                   .WithMany(p => p.Products)
                   .HasForeignKey(d => d.FkTurningRadiusTypesId)
                   .HasConstraintName("turning_radius_types_products");

            });

            modelBuilder.Entity<ShoppingCartRecords>(entity =>
            {
                entity.ToTable("shopping_cart_records");

                entity.HasIndex(e => e.FkCustomersId)
                    .HasName("shopping_customers_idx");

                entity.HasIndex(e => e.FkProductsId)
                    .HasName("shopping_products_idx");

                entity.HasIndex(e => e.FkInventoryItemsId)
                    .HasName("shopping_inventory_items_idx");

                entity.Property(e => e.ShoppingCartRecordsId)
                    .HasColumnName("shopping_cart_records_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkCustomersId)
                    .HasColumnName("fk_customers_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkProductsId)
                    .HasColumnName("fk_products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkInventoryItemsId)
                    .HasColumnName("fk_inventory_items_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkCustomers)
                    .WithMany(p => p.ShoppingCartRecords)
                    .HasForeignKey(d => d.FkCustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shopping_customers");

                entity.HasOne(d => d.FkProducts)
                    .WithMany(p => p.ShoppingCartRecords)
                    .HasForeignKey(d => d.FkProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shopping_products");

                entity.HasOne(d => d.FKInventoryItems)
                    .WithMany(p => p.ShoppingCartRecords)
                    .HasForeignKey(d => d.FkInventoryItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("shopping_inventory_items");
            });

            modelBuilder.Entity<ShoppingCartRecordsGuest>(entity =>
            {
                entity.ToTable("shopping_cart_records_guest");

                entity.HasIndex(e => e.FkInventoryItemsId)
                    .HasName("fk_cats_shop_guest_inventory_items_id");

                entity.HasIndex(e => e.FkProductsId)
                    .HasName("fk_prod_cats_shop_guest_recs_idx");

                entity.Property(e => e.ShoppingCartRecordsGuestId)
                    .HasColumnName("shopping_cart_records_guest_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkInventoryItemsId)
                    .HasColumnName("fk_inventory_items_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkProductsId)
                    .HasColumnName("fk_products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GuestId)
                    .HasColumnName("guest_id")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkProducts)
                    .WithMany(p => p.ShoppingCartRecordsGuest)
                    .HasForeignKey(d => d.FkProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_inv_itms_guest_ibfk_1");

                entity.HasOne(d => d.FKInventoryItems)
                    .WithMany(p => p.ShoppingCartRecordsGuest)
                    .HasForeignKey(d => d.FkInventoryItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prod_shop_guest_recs");
            });

            modelBuilder.Entity<Transmissions>(entity =>
            {
                entity.ToTable("transmissions");

                entity.HasIndex(e => e.TransmissionName)
                    .HasName("idxtransname");

                entity.Property(e => e.TransmissionsId)
                    .HasColumnName("transmissions_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransmissionName)
                    .IsRequired()
                    .HasColumnName("transmission_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Warranties>(entity =>
            {
                entity.ToTable("warranties");

                entity.HasIndex(e => e.WarrantyName)
                    .HasName("idxwarrantyname");

                entity.Property(e => e.WarrantiesId)
                    .HasColumnName("warranties_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.WarrantyName)
                    .IsRequired()
                    .HasColumnName("warranty_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<NewsLetterSubscriptions>(entity =>
            {
                entity.ToTable("newsletter_subscriptions");

                entity.HasIndex(e => e.NewsletterSubscriptionsId)
                    .HasName("idxnewsletter_subscriptionsid");

                entity.Property(e => e.NewsletterSubscriptionsId)
                    .HasColumnName("newsletter_subscriptions_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<FundingRequests>(entity =>
            {
                entity.ToTable("funding_requests");

                entity.HasIndex(e => e.FundingRequestId)
                    .HasName("idxfunding_requestsid");

                entity.Property(e => e.FundingRequestId)
                    .HasColumnName("funding_requests_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FKCustomersId)
                    .IsRequired()
                    .HasColumnName("fk_customers_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Installments)
                   .IsRequired()
                   .HasColumnName("installments")
                   .HasColumnType("int(11)");

                entity.Property(e => e.DateOfBirth)
                   .IsRequired()
                   .HasColumnName("date_of_birth")
                   .HasColumnType("datetime");

                entity.Property(e => e.RequestDate)
                    .IsRequired()
                    .HasColumnName("request_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Names)
                   .HasColumnName("names")
                   .HasColumnType("varchar(70)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastName)
                   .HasColumnName("lastname")
                   .HasColumnType("varchar(70)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdentificationType)
                    .HasColumnName("identification_type")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdentificationNumber)
                    .IsRequired()
                    .HasColumnName("identification_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CityOfResidence)
                    .HasColumnName("city_of_residence")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UpdatedFor)
                    .HasColumnName("updated_for")
                    .HasColumnType("varchar(105)")
                    .IsRequired(false)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TotalAmountRequest)
                    .IsRequired()
                    .HasColumnName("total_amount_request")
                    .HasColumnType("decimla(19,2)");

                entity.Property(e => e.InitialFee)
                    .IsRequired()
                    .HasColumnName("initial_fee")
                    .HasColumnType("decimla(19,2)");

                entity.Property(e => e.MonthlyIncome)
                    .HasColumnName("monthly_income")
                    .HasColumnType("decimla(19,2)");

                entity.Property(e => e.Profession)
                    .HasColumnName("profession")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EconomicActivity)
                    .HasColumnName("economic_activity")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IndependentActivity)
                    .HasColumnName("independent_activity")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FKCustomers)
                    .WithMany(p => p.FundingRequests)
                    .HasForeignKey(d => d.FKCustomersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("funding_requests_customers");
            });

            modelBuilder.Entity<InterestedCustomers>(entity =>
            {
                entity.ToTable("interested_customers");

                entity.HasIndex(e => e.InterestedCustomersId)
                    .HasName("idxinterested_customers");

                entity.Property(e => e.InterestedCustomersId)
                    .HasColumnName("interested_customers_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .HasColumnName("name")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ManagedFor)
                    .IsRequired(false)
                    .HasColumnName("managed_for")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Retake)
                   .IsRequired(false)
                   .HasColumnName("retake")
                   .HasColumnType("varchar(200)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Headquarter)
                   .IsRequired(false)
                   .HasColumnName("headquarter")
                   .HasColumnType("varchar(200)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreateOn)
                   .IsRequired(true)
                   .HasColumnName("create_on")
                   .HasColumnType("datetime");

                entity.Property(e => e.ManagedOn)
                  .IsRequired(false)
                  .HasColumnName("managed_on")
                  .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired(false)
                    .HasColumnName("email")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired(true)
                    .HasColumnName("phone_number")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Managed)
                   .IsRequired(true)
                   .HasColumnName("managed")
                   .HasColumnType("tinyint(4)");

                entity.Property(e => e.FkInventoryItemsId)
                   .HasColumnName("fk_inventory_items_id")
                   .HasColumnType("int(11)");

                entity.HasOne(d => d.FkInventoryItems)
                    .WithMany(p => p.InterestedCustomers)
                    .HasForeignKey(d => d.FkInventoryItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interested_customers_inventory_items");
            });

            modelBuilder.Entity<Taxes>(entity =>
            {
                entity.ToTable("taxes");

                entity.HasIndex(e => e.TaxesId)
                    .HasName("idx_taxes_id");

                entity.Property(e => e.TaxesId)
                    .HasColumnName("taxes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxName)
                    .IsRequired(true)
                    .HasColumnName("tax_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.TaxPercent)
                    .IsRequired(true)
                    .HasColumnName("tax_percent")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.TaxDescription)
                   .IsRequired(false)
                   .HasColumnName("tax_description")
                   .HasColumnType("varchar(800)");
            });

            modelBuilder.Entity<TaxesForInventory>(entity =>
            {
                entity.ToTable("taxes_for_inventory");

                entity.HasIndex(e => e.TaxesForInventoryId)
                    .HasName("taxes_for_inventory_id");

                entity.Property(e => e.TaxesForInventoryId)
                    .HasColumnName("taxes_for_inventory_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkInventoryItemsId)
                    .IsRequired(true)
                    .HasColumnName("fk_inventory_items_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkTaxesId)
                    .IsRequired(true)
                    .HasColumnName("fk_taxes_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<HOrdersTaxes>(entity =>
            {
                entity.ToTable("h_orders_taxes");

                entity.HasIndex(e => e.FkOrdersId)
                    .HasName("h_orders_taxes_orders_idx");

                entity.Property(e => e.HOrdersTaxesId)
                    .HasColumnName("h_orders_taxes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkOrdersId)
                    .HasColumnName("fk_orders_id")
                    .IsRequired(true)
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaxName)
                    .HasColumnName("tax_name")
                    .IsRequired(true)
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.TaxPercent)
                    .IsRequired(true)
                    .HasColumnName("tax_percent")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.TaxTotal)
                    .HasColumnName("tax_total")
                    .HasColumnType("decimal(19,2)");

                entity.HasOne(d => d.FkOrders)
                    .WithMany(p => p.HOrdersTaxes)
                    .HasForeignKey(d => d.FkOrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_details_orders");
            });

            modelBuilder.Entity<ProductDocumentsCategories>(entity =>
            {
                entity.ToTable("product_documents_categories");

                entity.HasIndex(e => e.ProductDocumentsCategoriesId)
                    .HasName("idxproductdocumentscategories");

                entity.Property(e => e.ProductDocumentsCategoriesId)
                    .HasColumnName("product_documents_categories_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryName)
                    .IsRequired(true)
                    .HasColumnName("category_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ProductDocuments>(entity =>
            {
                entity.ToTable("product_documents");

                entity.HasIndex(e => e.ProductDocumentsId)
                    .HasName("idxproduct_documents");

                entity.HasIndex(e => e.FkProductsId)
                    .HasName("product_documents_products_idx");

                entity.HasIndex(e => e.FkProductDocumentsCategoriesId)
                    .HasName("product_documents_category_documents_idx");

                entity.Property(e => e.ProductDocumentsId)
                    .HasColumnName("product_documents_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .IsRequired(true)
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FilePath)
                    .HasColumnName("file_path")
                    .IsRequired(true)
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.FkProductsId)
                    .IsRequired(false)
                    .HasColumnName("fk_products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkProductDocumentsCategoriesId)
                    .IsRequired(false)
                    .HasColumnName("fk_product_documents_categories_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkProductDocumentsCategories)
                    .WithMany(p => p.ProductDocuments)
                    .HasForeignKey(d => d.FkProductDocumentsCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_documents_category_documents");

                entity.HasOne(d => d.FkProducts)
                   .WithMany(p => p.ProductDocuments)
                   .HasForeignKey(d => d.FkProductsId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("product_documents_products");
            });

            modelBuilder.Entity<CampaignLeads>(entity =>
            {
                entity.ToTable("campaign_leads");

                entity.HasIndex(e => e.CampaignLeadsId)
                    .HasName("idxcampaign_leads");

                entity.Property(e => e.CampaignLeadsId)
                    .HasColumnName("campaign_leads_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .IsRequired(true)
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .HasColumnName("name")
                    .HasColumnType("varchar(105)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Email)
                    .IsRequired(true)
                    .HasColumnName("email")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.City)
                    .IsRequired(false)
                    .HasColumnName("city")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.City)
                    .IsRequired(false)
                    .HasColumnName("city")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired(false)
                    .HasColumnName("phone_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Remarks)
                    .IsRequired(false)
                    .HasColumnName("remarks")
                    .HasColumnType("varchar(800)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Campaign)
                    .IsRequired(true)
                    .HasColumnName("campaign")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InfoFrom)
                    .IsRequired(false)
                    .HasColumnName("info_from")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.EmailSentTo)
                   .IsRequired(false)
                   .HasColumnName("email_sent_to")
                   .HasColumnType("varchar(100)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Status)
                   .IsRequired(true)
                   .HasColumnName("status")
                   .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SearchEngineOptimizations>(entity =>
            {
                entity.ToTable("search_engine_optimization");

                entity.HasIndex(e => e.PagePath)
                    .HasName("idxpagepath");

                entity.Property(e => e.SearchEngineOptimizationsId)
                    .HasColumnName("search_engine_optimization_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PagePath)
                    .IsRequired()
                    .HasColumnName("page_path")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                   .HasColumnName("description")
                   .HasColumnType("varchar(800)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Keywords)
                   .HasColumnName("keywords")
                   .HasColumnType("varchar(800)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<HeroImages>(entity =>
            {
                entity.ToTable("hero_images");

                entity.HasIndex(e => e.HeroImagesId)
                    .HasName("idxheroimagesid");

                entity.Property(e => e.HeroImagesId)
                    .HasColumnName("hero_images_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkMakesId)
                    .HasColumnName("fk_makes_id")
                    .HasColumnType("int(11)")
                    .IsRequired(false);

                entity.Property(e => e.Enable)
                    .IsRequired()
                    .HasColumnName("enable")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Index)
                    .IsRequired()
                    .HasColumnName("index")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImageURL)
                    .IsRequired()
                    .HasColumnName("image_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.HasButton)
                    .IsRequired()
                    .HasColumnName("has_button")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ButtonURL)
                    .HasColumnName("button_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ShowSpan)
                   .IsRequired()
                   .HasColumnName("show_span")
                   .HasColumnType("tinyint(1)");

                entity.Property(e => e.SpanText)
                   .HasColumnName("span_text")
                   .HasColumnType("varchar(70)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SpanComplementText)
                   .HasColumnName("span_complement_text")
                   .HasColumnType("varchar(70)")
                   .HasCharSet("utf8mb4")
                   .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.FkMakes)
                  .WithMany(p => p.HeroImages)
                  .HasForeignKey(d => d.FkMakesId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_hero_makes");
            });

            modelBuilder.Entity<ProductMaintenances>(entity =>
            {
                entity.ToTable("product_maintenances");

                entity.HasIndex(e => e.FkProductsId)
                    .HasName("fk_products_maintenance_idx");

                entity.Property(e => e.ProductMaintenancesId)
                    .HasColumnName("product_maintenances_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkProductsId)
                    .IsRequired(true)
                    .HasColumnName("fk_products_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MaintenanceIndex)
                    .HasColumnName("maintenance_index")
                    .IsRequired(true)
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .IsRequired(true)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SalesPrice)
                    .HasColumnName("sales_price")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.Description)
                    .IsRequired(false)
                    .HasColumnName("description")
                    .HasColumnType("longtext");

                entity.Property(e => e.Enable)
                    .IsRequired(true)
                    .HasColumnName("enable")
                    .HasColumnType("tinyint(1)");

                entity.HasOne(d => d.FkProducts)
                   .WithMany(p => p.ProductMaintenances)
                   .HasForeignKey(d => d.FkProductsId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("product_documents_products");
            });

            /////Massey Ferfusson tables
            #region Massey Tables
            modelBuilder.Entity<AspirationTypes>(entity =>
                {
                    entity.ToTable("aspiration_types");

                    entity.HasIndex(e => e.AspirationTypeName)
                        .HasName("idxaspirationtypes");

                    entity.Property(e => e.AspirationTypesId)
                        .HasColumnName("aspiration_types_id")
                        .HasColumnType("int(11)");

                    entity.Property(e => e.AspirationTypeName)
                        .IsRequired()
                        .HasColumnName("aspiration_type_name")
                        .HasColumnType("varchar(255)")
                        .HasCharSet("utf8mb4")
                        .HasCollation("utf8mb4_0900_ai_ci");
                });

            modelBuilder.Entity<BladeTypes>(entity =>
            {
                entity.ToTable("blade_types");

                entity.HasIndex(e => e.BladeTypeName)
                    .HasName("idxbladetypes");

                entity.Property(e => e.BladeTypesId)
                    .HasColumnName("blade_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BladeTypeName)
                    .IsRequired()
                    .HasColumnName("blade_type_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ChamberDimensions>(entity =>
            {
                entity.ToTable("chamber_dimensions");

                entity.HasIndex(e => e.ChamberDimensionName)
                    .HasName("idxchamberdimensions");

                entity.Property(e => e.ChamberDimensionsId)
                    .HasColumnName("chamber_dimensions_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChamberDimensionName)
                    .IsRequired()
                    .HasColumnName("chamber_dimension_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ConditioningSystems>(entity =>
            {
                entity.ToTable("conditioning_systems");

                entity.HasIndex(e => e.ConditioningSystemName)
                    .HasName("idxconditioningsystems");

                entity.Property(e => e.ConditioningSystemsId)
                    .HasColumnName("conditioning_systems_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConditioningSystemName)
                    .IsRequired()
                    .HasColumnName("conditioning_system_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ConfigurationTypes>(entity =>
            {
                entity.ToTable("configuration_types");

                entity.HasIndex(e => e.ConfigurationTypeName)
                    .HasName("idxconfigurationtypes");

                entity.Property(e => e.ConfigurationTypesId)
                    .HasColumnName("configuration_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConfigurationTypeName)
                    .IsRequired()
                    .HasColumnName("configuration_type_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<FrontTiresTypes>(entity =>
            {
                entity.ToTable("front_tires_types");

                entity.HasIndex(e => e.FrontTiresTypeName)
                    .HasName("idxfront_tirestype");

                entity.Property(e => e.FrontTiresTypesId)
                    .HasColumnName("front_tires_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FrontTiresTypeName)
                    .IsRequired()
                    .HasColumnName("front_tires_type_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<HitchPines>(entity =>
            {
                entity.ToTable("hitch_pines");

                entity.HasIndex(e => e.HitchPinName)
                    .HasName("idxhitchpines");

                entity.Property(e => e.HitchPinesId)
                    .HasColumnName("hitch_pines_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HitchPinName)
                    .IsRequired()
                    .HasColumnName("hitch_pin_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<HitchSystems>(entity =>
            {
                entity.ToTable("hitch_systems");

                entity.HasIndex(e => e.HitchSystemName)
                    .HasName("idxhitch_systems");

                entity.Property(e => e.HitchSystemsId)
                    .HasColumnName("hitch_systems_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HitchSystemName)
                    .IsRequired()
                    .HasColumnName("hitch_system_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<RearTiresTypes>(entity =>
            {
                entity.ToTable("rear_tires_types");

                entity.HasIndex(e => e.RearTiresTypeName)
                    .HasName("idxrear_tires_types");

                entity.Property(e => e.RearTiresTypesId)
                    .HasColumnName("rear_tires_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RearTiresTypeName)
                    .IsRequired()
                    .HasColumnName("rear_tires_type_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<SeparationSystems>(entity =>
            {
                entity.ToTable("separation_systems");

                entity.HasIndex(e => e.SeparationSystemName)
                    .HasName("idxseparationsystems");

                entity.Property(e => e.SeparationSystemsId)
                    .HasColumnName("separation_systems_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SeparationSystemName)
                    .IsRequired()
                    .HasColumnName("separation_system_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<TurningRadiusTypes>(entity =>
            {
                entity.ToTable("turning_radius_types");

                entity.HasIndex(e => e.TurningRadiusTypeName)
                    .HasName("idxturningradiustypes");

                entity.Property(e => e.TurningRadiusTypesId)
                    .HasColumnName("turning_radius_types_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TurningRadiusTypeName)
                    .IsRequired()
                    .HasColumnName("turning_radius_type_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            }); 
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
