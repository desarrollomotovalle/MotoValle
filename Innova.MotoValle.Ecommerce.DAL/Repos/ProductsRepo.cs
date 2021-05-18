// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Pictures Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Product Pictures Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.IProductsRepo" />
    public class ProductsRepo : IProductsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public ProductsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Product created</returns>
        public Products CreateRecord(Products item)
        {
            this._db.Products.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            var item = this.GetRecord(id);
            if (item != null)
            {
                this._db.Products.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products</returns>
        public List<Products> GetAllRecords()
        {
            return this._db.Products
                .AsNoTracking()
                .Include(p => p.FkBodyStyles)
                .Include(p => p.FkCategories)
                .Include(p => p.FkDriveTrains)
                .Include(p => p.FkEngineTypes)
                .Include(p => p.FkEpaClasses)
                .Include(p => p.FkMakes)
                .Include(p => p.FkModels)
                .Include(p => p.FkTransmissions)
                .Include(p => p.ProductDocuments)
                .Include(p => p.FkAspirationTypes)
                .Include(p => p.FkBladeTypes)
                .Include(p => p.FkChamberDimensions)
                .Include(p => p.FkConditioningSystems)
                .Include(p => p.FkConfigurationTypes)
                .Include(p => p.FkFrontTiresTypes)
                .Include(p => p.FkHitchPines)
                .Include(p => p.FkHitchSystems)
                .Include(p => p.FkRearTiresTypes)
                .Include(p => p.FkSeparationSystems)
                .Include(p => p.FkTurningRadiusTypes)
                .Include(p => p.ShoppingCartRecords)
                .Include(p => p.ProductDocuments)
                .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product by Id</returns>
        public Products GetRecord(long id)
        {
            return this._db.Products
                .AsNoTracking()
                .Include(p => p.FkBodyStyles)
                .Include(p => p.FkCategories)
                .Include(p => p.FkDriveTrains)
                .Include(p => p.FkEngineTypes)
                .Include(p => p.FkEpaClasses)
                .Include(p => p.FkMakes)
                .Include(p => p.FkModels)
                .Include(p => p.FkTransmissions)
                .Include(p => p.ProductDocuments)
                .Include(p => p.InventoryItems)
                .Include(p => p.OrderDetails)
                .Include(p => p.FkAspirationTypes)
                .Include(p => p.FkBladeTypes)
                .Include(p => p.FkChamberDimensions)
                .Include(p => p.FkConditioningSystems)
                .Include(p => p.FkConfigurationTypes)
                .Include(p => p.FkFrontTiresTypes)
                .Include(p => p.FkHitchPines)
                .Include(p => p.FkHitchSystems)
                .Include(p => p.FkRearTiresTypes)
                .Include(p => p.FkSeparationSystems)
                .Include(p => p.FkTurningRadiusTypes)
                .Include(p => p.ShoppingCartRecords)
                .Include(p => p.ProductDocuments)
                .FirstOrDefault(c => c.ProductsId == id);
        }

        /// <summary>
        /// Gets the name of the record by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Product by name</returns>
        public Products GetRecordByName(string name)
        {
            return this._db.Products.FirstOrDefault(c => c.Sku == name);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedRecord">The updated record.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, Products updatedRecord)
        {
            ////Check Products pictures exists
            var item = this.Get(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.Cost = updatedRecord.Cost;
            item.Description = updatedRecord.Description;
            item.FkCategoriesId = updatedRecord.FkCategoriesId;
            item.IsFeatured = updatedRecord.IsFeatured;
            item.LongDescription = updatedRecord.LongDescription;
            item.QuantityInStock = updatedRecord.QuantityInStock;
            item.SalesPrice = updatedRecord.SalesPrice;
            item.Sku = updatedRecord.Sku;
            item.Upc = updatedRecord.Upc;
            item.Cupix360Url = updatedRecord.Cupix360Url;
            item.BaseWeight = updatedRecord.BaseWeight;
            item.FkBodyStylesId = updatedRecord.FkBodyStylesId;
            item.FkDriveTrainsId = updatedRecord.FkDriveTrainsId;
            item.FkEngineTypesId = updatedRecord.FkEngineTypesId;
            item.FkEpaClassesId = updatedRecord.FkEpaClassesId;
            item.FkMakesId = updatedRecord.FkMakesId;
            item.FkModelsId = updatedRecord.FkModelsId;
            item.FkTransmissionsId = updatedRecord.FkTransmissionsId;
            item.Horsepower = updatedRecord.Horsepower;
            item.Msrp = updatedRecord.Msrp;
            item.PassengerDoors = updatedRecord.PassengerDoors;
            item.Passengers = updatedRecord.Passengers;
            item.Picture360Quantity = updatedRecord.Picture360Quantity;
            item.Picture360Url = updatedRecord.Picture360Url;
            item.ProductName = updatedRecord.ProductName;
            item.QuantityInStock = updatedRecord.QuantityInStock;
            item.SalesPrice = updatedRecord.SalesPrice;
            item.Trim = updatedRecord.Trim;
            item.Year = updatedRecord.Year;
            item.YoutubeUrl = updatedRecord.YoutubeUrl;
            item.AllowShow = updatedRecord.AllowShow;
            item.PictureUrl = updatedRecord.PictureUrl;
            item.GasMileage = updatedRecord.GasMileage;
            //// Massey Fields
            item.Cylinders = updatedRecord.Cylinders;
            item.LoadCapacity = updatedRecord.LoadCapacity;
            item.SingleWeight = updatedRecord.SingleWeight;
            item.Productivity = updatedRecord.Productivity;
            item.BaleLength = updatedRecord.BaleLength;
            item.RecommendedPower = updatedRecord.RecommendedPower;
            item.WorkingWidth = updatedRecord.WorkingWidth;
            item.NumberOfLines = updatedRecord.NumberOfLines;
            item.NumberOfDiscs = updatedRecord.NumberOfDiscs;
            item.InputRotation = updatedRecord.InputRotation;
            item.StandardCover = updatedRecord.StandardCover;
            item.CuttingWidth = updatedRecord.CuttingWidth;
            item.FkAspirationTypesId = updatedRecord.FkAspirationTypesId;
            item.FkRearTiresTypesId = updatedRecord.FkRearTiresTypesId;
            item.FkFrontTiresTypesId = updatedRecord.FkFrontTiresTypesId;
            item.FkSeparationSystemsId = updatedRecord.FkSeparationSystemsId;
            item.FkHitchSystemsId = updatedRecord.FkHitchSystemsId;
            item.FkHitchPinesId = updatedRecord.FkHitchPinesId;
            item.FkChamberDimensionsId = updatedRecord.FkChamberDimensionsId;
            item.FkConfigurationTypesId = updatedRecord.FkConfigurationTypesId;
            item.FkConditioningSystemsId = updatedRecord.FkConditioningSystemsId;
            item.FkBladesTypesId = updatedRecord.FkBladesTypesId;
            item.FkTurningRadiusTypesId = updatedRecord.FkTurningRadiusTypesId;

            this._db.Products.Update(item);
            this._db.SaveChanges();
        }

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of Product and category base by id</returns>
        public IEnumerable<ProductAndCategoryBase> GetProductsForCategory(int id)
        {
            return this._db.Products
                        .Include(x => x.FkCategories)
                        .Include(x => x.FkModels)
                        .AsNoTracking()
                        .Where(x => x.FkCategoriesId == (id == 0 ? x.FkCategoriesId : id) && x.AllowShow > 1)
                        .Select(x => new ProductAndCategoryBase()
                        {
                            AllowShow = x.AllowShow ?? 0,
                            CategoryId = x.FkCategoriesId ?? 0,
                            CategoryName = x.FkCategories.CategoryName,
                            CategoryDescription = x.FkCategories.Description,
                            CurrentPrice = x.SalesPrice ?? 0,
                            Description = x.Description,
                            IsFeatured = Convert.ToBoolean(x.IsFeatured),
                            LongDescription = x.LongDescription,
                            ModelName = x.FkModels.ModelName,
                            ModelNumber = x.FkModels.ModelsId.ToString(),
                            ProductId = x.ProductsId,
                            ProductImage = string.Empty,
                            ProductImageLarge = string.Empty,
                            ProductImageThumb = string.Empty,
                            UnitCost = x.Cost ?? 0,
                            UnitsInStock = x.QuantityInStock ?? 0
                        });
        }

        /// <summary>
        /// Gets the product with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>List of Product With Details</returns>
        public IEnumerable<ProductWithDetails> GetProductWithDetails(int productId)
        {
            return this._db.Products
                       .Include(x => x.FkCategories)
                       .Include(x => x.FkDriveTrains)
                       .Include(x => x.FkEngineTypes)
                       .Include(x => x.FkEpaClasses)
                       .Include(x => x.FkTransmissions)
                       .Include(x => x.FkAspirationTypes)
                       .Include(x => x.FkBladeTypes)
                       .Include(x => x.FkChamberDimensions)
                       .Include(x => x.FkConditioningSystems)
                       .Include(x => x.FkConfigurationTypes)
                       .Include(x => x.FkFrontTiresTypes)
                       .Include(x => x.FkHitchPines)
                       .Include(x => x.FkHitchSystems)
                       .Include(x => x.FkRearTiresTypes)
                       .Include(x => x.FkSeparationSystems)
                       .Include(x => x.FkTurningRadiusTypes)
                       .AsNoTracking()
                       .Where(x => x.ProductsId == (productId == 0 ? x.ProductsId : productId))
                       .Select(x => new ProductWithDetails()
                       {
                           AllowShow = x.AllowShow ?? 0,
                           BaseWeight = x.BaseWeight ?? 0,
                           CategoriesId = x.FkCategoriesId ?? 0,
                           CategoryName = x.FkCategories.CategoryName,
                           Cupix360Url = x.Cupix360Url,
                           DriveTrainName = x.FkDriveTrains.DriveTrainName,
                           EngineTypeName = x.FkEngineTypes.EngineTypeName,
                           EpaClassName = x.FkEpaClasses.EpaClassName,
                           GasMileage = x.GasMileage,
                           HorsePower = x.Horsepower ?? 0,
                           IsFeatured = Convert.ToBoolean(x.IsFeatured),
                           ModelName = x.FkModels.ModelName,
                           Msrp = x.Msrp ?? 0,
                           Passengers = x.Passengers ?? 0,
                           PassengersDoors = x.PassengerDoors ?? 0,
                           PictureUrl = x.PictureUrl,
                           Picture360Url = x.Picture360Url,
                           ProductDescription = x.Description,
                           ProductName = x.ProductName,
                           ProductId = x.ProductsId,
                           ProductLongDescription = x.LongDescription,
                           QuantityInStock = x.QuantityInStock ?? 0,
                           SalesPrice = x.SalesPrice ?? 0,
                           Sku = x.Sku,
                           TransmissionName = x.FkTransmissions.TransmissionName,
                           Trim = x.Trim,
                           Upc = x.Upc,
                           Year = x.Year,
                           YoutubeUrl = x.YoutubeUrl,
                           ////Massey Fields
                           Cylinders = x.Cylinders,
                           LoadCapacity = x.LoadCapacity,
                           SingleWeight = x.SingleWeight,
                           Productivity = x.Productivity,
                           BaleLength = x.BaleLength,
                           Speed = x.Speed,
                           RecommendedPower = x.RecommendedPower,
                           WorkingWidth = x.WorkingWidth,
                           NumberOfLines = x.NumberOfLines,
                           NumberOfDiscs = x.NumberOfDiscs,
                           InputRotation = x.InputRotation,
                           StandardCover = x.StandardCover,
                           CuttingWidth = x.CuttingWidth,
                           AspirationTypeName = x.FkAspirationTypes.AspirationTypeName,
                           RearTiresTypeName = x.FkRearTiresTypes.RearTiresTypeName,
                           FrontTiresTypeName = x.FkFrontTiresTypes.FrontTiresTypeName,
                           SeparationSystemName = x.FkSeparationSystems.SeparationSystemName,
                           HitchSystemName = x.FkHitchSystems.HitchSystemName,
                           HitchPinName = x.FkHitchPines.HitchPinName,
                           ChamberDimensionName = x.FkChamberDimensions.ChamberDimensionName,
                           ConfigurationTypeName = x.FkConfigurationTypes.ConfigurationTypeName,
                           ConditioningSystemName = x.FkConditioningSystems.ConditioningSystemName,
                           BladeTypeName = x.FkBladeTypes.BladeTypeName,
                           TurningRadiusTypeName = x.FkTurningRadiusTypes.TurningRadiusTypeName
                       });
        }

        /// <summary>
        /// Gets the product and make base.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="modelId">The model identifier.</param>
        /// <param name="minPrice">The minimum price.</param>
        /// <param name="maxPrice">The maximum price.</param>
        /// <param name="minYear">The minimum year.</param>
        /// <param name="maxYear">The maximum year.</param>
        /// <returns>
        /// List of Product And Make Base
        /// </returns>
        public IEnumerable<ProductAndMakeBase> GetProductAndMakeBase(int makesId, int categoryId, int modelId, int minYear, int maxYear, decimal minPrice, decimal maxPrice)
        {
            minYear = Math.Abs(minYear);
            maxYear = Math.Abs(maxYear);
            minPrice = Math.Abs(minPrice);
            maxPrice = Math.Abs(maxPrice);
            var realMinYear = Math.Min(minYear, maxYear);
            var realMaxYear = Math.Max(minYear, maxYear);
            var realMinPrice = Math.Min(minPrice, maxPrice);
            var realMaxPrice = Math.Max(minPrice, maxPrice);
            var productAndMakeBase = this._db.Products
                       .Include(x => x.FkCategories)
                       .Include(x => x.FkModels)
                       .Include(x => x.FkTransmissions)
                       .AsNoTracking()
                       .Where(x => x.FkMakesId == makesId
                                && x.FkCategoriesId == (categoryId == 0 ? x.FkCategoriesId : categoryId)
                                && x.FkModelsId == (modelId == 0 ? x.FkModelsId : modelId)
                                && x.Year >= (realMinYear == 0 ? x.Year : realMinYear) && x.Year <= (realMaxYear == 0 ? x.Year : realMaxYear)
                                && x.SalesPrice >= (realMinPrice == 0 ? x.SalesPrice : realMinPrice) && x.SalesPrice <= (realMaxPrice == 0 ? x.SalesPrice : realMaxPrice))
                       .Select(x => new ProductAndMakeBase()
                       {
                           AllowShow = x.AllowShow ?? 0,
                           CategoriesId = x.FkCategoriesId ?? 0,
                           CategoryName = x.FkCategories.CategoryName,
                           Cupix360Url = x.Cupix360Url,
                           HorsePower = x.Horsepower ?? 0,
                           IsFeatured = Convert.ToBoolean(x.IsFeatured ?? 0),
                           ModelName = x.FkModels.ModelName,
                           Msrp = x.Msrp ?? 0,
                           Passengers = x.Passengers ?? 0,
                           PassengersDoors = x.PassengerDoors ?? 0,
                           PictureUrl = x.PictureUrl,
                           ProductDescription = x.Description,
                           ProductName = x.ProductName,
                           ProductId = x.ProductsId,
                           QuantityInStock = x.QuantityInStock ?? 0,
                           SalesPrice = x.SalesPrice ?? 0,
                           TransmissionName = x.FkTransmissions.TransmissionName,
                           Year = x.Year,
                           YoutubeUrl = x.YoutubeUrl
                       }).ToList();

            return productAndMakeBase.AsEnumerable();
        }

        /// <summary>
        /// Gets the inventory for product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory For Product
        /// </returns>
        public IEnumerable<InventoryForProduct> GetInventoryForProduct(int productId, int makesId)
        {
            var prodcutsWithInvetory = this._db.InventoryItems
                                               .Include(x => x.FkColors)
                                               .Include(x => x.InventoryItemPictures)
                                               .Include(x => x.FkProducts)
                                               .Include(x => x.FkProducts.FkModels)
                                               .Include(x => x.FkProducts.FkTransmissions)
                                               .Include(x => x.FkWarranties)
                                               .AsNoTracking()
                                               .Where(x => x.FkProductsId == (productId == 0 ? x.FkProductsId : productId)
                                                        && x.FkProducts.FkMakesId == (makesId == 0 ? x.FkProducts.FkMakesId : makesId)
                                                        && (x.FkProducts.QuantityInStock ?? 0) > 0)
                                               .Select(x => new InventoryForProduct()
                                               {
                                                   AllowShow = x.FkProducts.AllowShow ?? 0,
                                                   CategoriesId = x.FkProducts.FkCategoriesId ?? 0,
                                                   CategoryName = x.FkProducts.FkCategories.CategoryName,
                                                   ColorName = x.FkColors.ColorName,
                                                   CoverPictureUrl = x.CoverPictureUrl,
                                                   HorsePower = x.FkProducts.Horsepower ?? 0,
                                                   InventoryItemsId = x.InventoryItemsId,
                                                   InventoryItemPictures = x.InventoryItemPictures.ToList(),
                                                   IsFeatured = Convert.ToBoolean(x.FkProducts.IsFeatured ?? 0),
                                                   ModelName = x.FkProducts.FkModels.ModelName,
                                                   Millage = x.Mileage ?? 0,
                                                   Msrp = x.FkProducts.Msrp ?? 0,
                                                   Passengers = x.FkProducts.Passengers ?? 0,
                                                   PassengersDoors = x.FkProducts.PassengerDoors ?? 0,
                                                   PictureUrl = x.FkProducts.PictureUrl,
                                                   ProductDescription = x.FkProducts.Description,
                                                   ProductName = x.FkProducts.ProductName,
                                                   ProductId = x.FkProducts.ProductsId,
                                                   QuantityInStock = x.FkProducts.QuantityInStock ?? 0,
                                                   QuantityInStockInventory = x.QuantityInStock ?? 0,
                                                   AllowShowInventory = x.AllowShow ?? 0,
                                                   SalesPrice = x.FkProducts.SalesPrice ?? 0,
                                                   SalesPriceInventory = x.SalesPrice ?? 0,
                                                   TransmissionName = x.FkProducts.FkTransmissions.TransmissionName,
                                                   Year = x.FkProducts.Year,
                                                   WarrantyDescription = x.FkWarranties.Description,
                                                   WarrantyName = x.FkWarranties.WarrantyName,
                                                   YoutubeUrl = x.FkProducts.YoutubeUrl,
                                                   TaxesForInventory = this._db.TaxesForInventory
                                                                               .Include(z => z.FkTaxes)
                                                                               .AsNoTracking()
                                                                               .Where(y => y.FkInventoryItemsId == x.InventoryItemsId)
                                                                               .ToList()
                                               }).ToList();

            ////Get Item Sales Price using Taxes for inventory
            foreach (var item in prodcutsWithInvetory)
            {
                if (item.TaxesForInventory.Count > 0)
                {
                    decimal taxesPrice = 0;
                    foreach (var tax in item.TaxesForInventory)
                    {
                        taxesPrice += item.SalesPriceInventory * tax.FkTaxes.TaxPercent / 100;
                    }

                    item.SalesPriceInventory = Math.Floor(item.SalesPriceInventory) + Math.Ceiling(taxesPrice);
                }
            }

            return prodcutsWithInvetory.AsEnumerable();
        }

        /// <summary>
        /// Gets the inventory for product with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory And Product Base With Details
        /// </returns>
        public IEnumerable<InventoryAndProductBaseWithDetails> GetInventoryForProductWithDetails(int productId, int inventoryItemsId, int makesId)
        {
            var inventoryAndProdcutsWithDetails = this._db.InventoryItems
                                                      .Include(x => x.FkProducts)
                                                      .Include(x => x.FkProducts.FkModels)
                                                      .Include(x => x.FkProducts.FkDriveTrains)
                                                      .Include(x => x.FkProducts.FkEngineTypes)
                                                      .Include(x => x.FkProducts.FkEpaClasses)
                                                      .Include(x => x.FkProducts.FkTransmissions)
                                                      .Include(x => x.FkColors)
                                                      .Include(x => x.FkWarranties)
                                                      .Include(x => x.InventoryItemPictures)
                                                      .Include(x => x.FkProducts.FkAspirationTypes)
                                                      .Include(x => x.FkProducts.FkBladeTypes)
                                                      .Include(x => x.FkProducts.FkChamberDimensions)
                                                      .Include(x => x.FkProducts.FkConditioningSystems)
                                                      .Include(x => x.FkProducts.FkConfigurationTypes)
                                                      .Include(x => x.FkProducts.FkFrontTiresTypes)
                                                      .Include(x => x.FkProducts.FkHitchPines)
                                                      .Include(x => x.FkProducts.FkHitchSystems)
                                                      .Include(x => x.FkProducts.FkRearTiresTypes)
                                                      .Include(x => x.FkProducts.FkSeparationSystems)
                                                      .Include(x => x.FkProducts.FkTurningRadiusTypes)
                                                      .AsNoTracking()
                                                      .Where(x => x.FkProducts.ProductsId == (productId == 0 ? x.FkProducts.ProductsId : productId)
                                                               && x.InventoryItemsId == (inventoryItemsId == 0 ? x.InventoryItemsId : inventoryItemsId)
                                                               && x.FkProducts.FkMakesId == (makesId == 0 ? x.FkProducts.FkMakesId : makesId)
                                                               && (x.FkProducts.QuantityInStock ?? 0) > 0)
                                                      .Select(x => new InventoryAndProductBaseWithDetails()
                                                      {
                                                          AllowShow = x.FkProducts.AllowShow ?? 0,
                                                          BaseWeight = x.FkProducts.BaseWeight ?? 0,
                                                          CategoriesId = x.FkProducts.FkCategoriesId ?? 0,
                                                          CategoryName = x.FkProducts.FkCategories.CategoryName,
                                                          ColorName = x.FkColors.ColorName,
                                                          CoverPictureUrl = x.CoverPictureUrl,
                                                          Cupix360Url = x.FkProducts.Cupix360Url,
                                                          DriveTrainName = x.FkProducts.FkDriveTrains.DriveTrainName,
                                                          EngineTypeName = x.FkProducts.FkEngineTypes.EngineTypeName,
                                                          EpaClassName = x.FkProducts.FkEpaClasses.EpaClassName,
                                                          GasMileage = x.FkProducts.GasMileage,
                                                          HorsePower = x.FkProducts.Horsepower ?? 0,
                                                          InventoryItemsId = x.InventoryItemsId,
                                                          IsFeatured = Convert.ToBoolean(x.FkProducts.IsFeatured),
                                                          IsNew = Convert.ToBoolean(x.IsNew),
                                                          InventoryItemPictures = x.InventoryItemPictures.ToList(),
                                                          ModelName = x.FkProducts.FkModels.ModelName,
                                                          Mileage = x.Mileage ?? 0,
                                                          Msrp = x.FkProducts.Msrp ?? 0,
                                                          Passengers = x.FkProducts.Passengers ?? 0,
                                                          PassengersDoors = x.FkProducts.PassengerDoors ?? 0,
                                                          PictureUrl = x.FkProducts.PictureUrl,
                                                          Picture360Url = x.FkProducts.Picture360Url,
                                                          Picture360Quantity = x.FkProducts.Picture360Quantity,
                                                          Cupix360UrlInventory = x.Cupix360Url,
                                                          Picture360QuantityInventory = x.Picture360Quantity,
                                                          Picture360UrlInventory = x.Picture360Url,
                                                          ProductDescription = x.FkProducts.Description,
                                                          ProductName = x.FkProducts.ProductName,
                                                          ProductId = x.FkProducts.ProductsId,
                                                          ProductLongDescription = x.FkProducts.LongDescription,
                                                          QuantityInStock = x.FkProducts.QuantityInStock ?? 0,
                                                          QuantityInStockInventory = x.QuantityInStock ?? 0,
                                                          AllowShowInventory = x.AllowShow ?? 0,
                                                          SalesPrice = x.SalesPrice ?? 0,
                                                          SalesPriceInventory = x.SalesPrice ?? 0,
                                                          Sku = x.FkProducts.Sku,
                                                          TransmissionName = x.FkProducts.FkTransmissions.TransmissionName,
                                                          Trim = x.FkProducts.Trim,
                                                          Upc = x.FkProducts.Upc,
                                                          Vin = x.Vin,
                                                          WarrantyDescription = x.FkWarranties.Description,
                                                          WarrantyName = x.FkWarranties.WarrantyName,
                                                          Year = x.FkProducts.Year,
                                                          YoutubeUrl = x.FkProducts.YoutubeUrl,
                                                          TaxesForInventory = this._db.TaxesForInventory
                                                                               .Include(z => z.FkTaxes)
                                                                               .AsNoTracking()
                                                                               .Where(y => y.FkInventoryItemsId == x.InventoryItemsId)
                                                                               .ToList(),
                                                          ProductDocuments = this._db.ProductDocuments
                                                                               .Include(z => z.FkProductDocumentsCategories)
                                                                               .AsNoTracking()
                                                                               .Where(y => y.FkProductsId == x.FkProductsId)
                                                                               .ToList(),
                                                          ////Massey Fields
                                                          Cylinders = x.FkProducts.Cylinders,
                                                          LoadCapacity = x.FkProducts.LoadCapacity,
                                                          SingleWeight = x.FkProducts.SingleWeight,
                                                          Productivity = x.FkProducts.Productivity,
                                                          BaleLength = x.FkProducts.BaleLength,
                                                          Speed = x.FkProducts.Speed,
                                                          RecommendedPower = x.FkProducts.RecommendedPower,
                                                          WorkingWidth = x.FkProducts.WorkingWidth,
                                                          NumberOfLines = x.FkProducts.NumberOfLines,
                                                          NumberOfDiscs = x.FkProducts.NumberOfDiscs,
                                                          InputRotation = x.FkProducts.InputRotation,
                                                          StandardCover = x.FkProducts.StandardCover,
                                                          CuttingWidth = x.FkProducts.CuttingWidth,
                                                          AspirationTypeName = x.FkProducts.FkAspirationTypes.AspirationTypeName,
                                                          RearTiresTypeName = x.FkProducts.FkRearTiresTypes.RearTiresTypeName,
                                                          FrontTiresTypeName = x.FkProducts.FkFrontTiresTypes.FrontTiresTypeName,
                                                          SeparationSystemName = x.FkProducts.FkSeparationSystems.SeparationSystemName,
                                                          HitchSystemName = x.FkProducts.FkHitchSystems.HitchSystemName,
                                                          HitchPinName = x.FkProducts.FkHitchPines.HitchPinName,
                                                          ChamberDimensionName = x.FkProducts.FkChamberDimensions.ChamberDimensionName,
                                                          ConfigurationTypeName = x.FkProducts.FkConfigurationTypes.ConfigurationTypeName,
                                                          ConditioningSystemName = x.FkProducts.FkConditioningSystems.ConditioningSystemName,
                                                          BladeTypeName = x.FkProducts.FkBladeTypes.BladeTypeName,
                                                          TurningRadiusTypeName = x.FkProducts.FkTurningRadiusTypes.TurningRadiusTypeName
                                                      }).ToList();

            ////Get Item Sales Price using Taxes for inventory
            foreach (var item in inventoryAndProdcutsWithDetails)
            {
                if (item.TaxesForInventory.Count > 0)
                {
                    decimal taxesPrice = 0;
                    foreach (var tax in item.TaxesForInventory)
                    {
                        taxesPrice += item.SalesPriceInventory * tax.FkTaxes.TaxPercent / 100;
                    }

                    item.SalesPriceInventory = Math.Floor(item.SalesPriceInventory) + Math.Ceiling(taxesPrice);
                }
            }

            return inventoryAndProdcutsWithDetails.AsEnumerable();
        }

        /// <summary>
        /// Productses the with maintenance by make.
        /// </summary>
        /// <param name="makesName">Name of the makes.</param>
        /// <returns>
        /// List of products with maintenance
        /// </returns>
        public IEnumerable<Products> ProductsWithEnableMaintenanceByMake(string makesName)
        {
            return this._db.ProductMaintenances
                .Include(x => x.FkProducts)
                .ThenInclude(x => x.FkMakes)
                .Where(x => x.Enable && x.FkProducts.FkMakes.MakeName.Contains(makesName, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.FkProducts)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Check existence by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>boolean control</returns>
        private bool RecordExists(long id)
        {
            return this._db.Products.Any(c => c.ProductsId == id);
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product by Id</returns>
        private Products Get(long id)
        {
            return this._db.Products
                .AsNoTracking()
                .FirstOrDefault(c => c.ProductsId == id);
        }
    }
}
