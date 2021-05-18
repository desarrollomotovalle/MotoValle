// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MazdaController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mazda Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Resources;

    /// <summary>
    ///  Mazda Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class MazdaController : Controller
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The makes identifier
        /// </summary>
        private readonly Makes _make;

        /// <summary>
        /// The categories
        /// </summary>
        private readonly List<Categories> _categories;

        /// <summary>
        /// The exception control
        /// </summary>
        private readonly bool _exceptionControl = false;

        /// <summary>
        /// The records per page default
        /// </summary>
        private readonly int _recordsPerPageDefault;

        /// <summary>
        /// The headquarters view model
        /// </summary>
        private readonly List<HeadquartersViewModel> _headquartersViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="FordController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MazdaController(IConfiguration configuration)
        {
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._recordsPerPageDefault = configuration.GetSection("AppSettings").GetValue<int>("RecordsPerPageDefault");
            this._headquartersViewModel = configuration.GetSection("Headquarters").Get<List<HeadquartersViewModel>>();
            var makesApi = new MakesApi(this._httpClientInstance);
            var categoriesApi = new CategoriesApi(this._httpClientInstance);

            try
            {
                this._make = makesApi.GetRecord("Mazda").Result;
                this._headquartersViewModel = this._headquartersViewModel.Where(x => x.Make == "Mazda").ToList();
                this._categories = categoriesApi.GetAllRecords().Result;
            }
            catch (Exception ex)
            {
                this._make = new Makes();
                this._categories = new List<Categories>();
                this._exceptionControl = true;
            }
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Mazda index view</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //Uncomment when Mazda View has been available again
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            ViewBag.MakesId = this._make.MakesId;
            ViewBag.CategoryList = this._categories;
            ViewBag.MakesName = this._make.MakeName;
            ViewBag.YearList = await CommonHelper.GetYears(this._httpClientInstance, this._make.MakesId);
            //Cargar todos los productos por marca siempre
            var shopViewModel = new ShopViewModel() { ProductAndMakeBases = new List<ProductAndMakeBase>() };
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                var productsAndMakeBaseTotal = await makesApi.GetProductAndMakeBase(this._make.MakesId);
                productsAndMakeBaseTotal = productsAndMakeBaseTotal.Where(x => x.QuantityInStock > 0 && x.AllowShow > 0).ToList();
                shopViewModel.ProductAndMakeBases = productsAndMakeBaseTotal.OrderByDescending(x => x.IsFeatured).Take(this._recordsPerPageDefault).ToList();
                ViewBag.PagesCount = Convert.ToInt32(Math.Ceiling((double)productsAndMakeBaseTotal.Count() / this._recordsPerPageDefault));
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(shopViewModel);
        }

        /// <summary>
        /// Indexes the specified basic search view model.
        /// </summary>
        /// <param name="basicSearchViewModel">The basic search view model.</param>
        /// <returns>Basic search view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(BasicSearchViewModel basicSearchViewModel)
        {
            
            //Uncomment when Mazda View has been available again
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            ViewBag.MakesId = this._make.MakesId;
            ViewBag.MakesName = this._make.MakeName;
            ViewBag.CategoryId = basicSearchViewModel.CategoryId;
            ViewBag.ModelId = basicSearchViewModel.ModelId;
            ViewBag.CategoryList = this._categories;
            ViewBag.IsSearch = true;
            ViewBag.YearList = await CommonHelper.GetYears(this._httpClientInstance, this._make.MakesId);

            var makesApi = new MakesApi(this._httpClientInstance);
            var shopViewModel = new ShopViewModel() { ProductAndMakeBases = new List<ProductAndMakeBase>() };
            var fileredProductsTotal = await makesApi.GetProductAndMakeBase(this._make.MakesId, basicSearchViewModel.CategoryId, basicSearchViewModel.ModelId,
                                                basicSearchViewModel.YearMin, basicSearchViewModel.YearMax, basicSearchViewModel.PriceRangeMin, basicSearchViewModel.PriceRangeMax);
            fileredProductsTotal = fileredProductsTotal.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).ToList();
            fileredProductsTotal = fileredProductsTotal.OrderByDescending(x => x.IsFeatured).ToList();

            if (!ModelState.IsValid)
            {
                shopViewModel.ProductAndMakeBases = fileredProductsTotal.Take(this._recordsPerPageDefault).ToList();
                shopViewModel.BasicSearchViewModel = basicSearchViewModel;
                ViewBag.PagesCount = Convert.ToInt32(Math.Ceiling((double)fileredProductsTotal.Count() / this._recordsPerPageDefault));

                return this.View(nameof(Index), shopViewModel);
            }

            shopViewModel.ProductAndMakeBases = fileredProductsTotal.Skip(basicSearchViewModel.RecordsPerPage * (basicSearchViewModel.PageNumber - 1)).Take(basicSearchViewModel.RecordsPerPage).ToList();
            shopViewModel.BasicSearchViewModel = basicSearchViewModel;
            ViewBag.ModelsForCategory = await makesApi.GetModelsForMakeAndCategory(this._make.MakesId, basicSearchViewModel.CategoryId);
            ViewBag.PagesCount = Convert.ToInt32(Math.Ceiling((double)fileredProductsTotal.Count() / basicSearchViewModel.RecordsPerPage));

            return this.View(nameof(Index), shopViewModel);
        }

        /// <summary>
        /// Used for manually passing without Antiforgery and without having a mixed up Index call
        /// </summary>
        /// <param name="basicSearchViewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Alternate(BasicSearchViewModel basicSearchViewModel)
        {

            //Uncomment when Mazda View has been available again
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            ViewBag.MakesId = this._make.MakesId;
            ViewBag.MakesName = this._make.MakeName;
            ViewBag.CategoryId = basicSearchViewModel.CategoryId;
            ViewBag.ModelId = basicSearchViewModel.ModelId;
            ViewBag.CategoryList = this._categories;
            ViewBag.IsSearch = true;
            ViewBag.YearList = await CommonHelper.GetYears(this._httpClientInstance, this._make.MakesId);

            var makesApi = new MakesApi(this._httpClientInstance);
            var shopViewModel = new ShopViewModel() { ProductAndMakeBases = new List<ProductAndMakeBase>() };
            var fileredProductsTotal = await makesApi.GetProductAndMakeBase(this._make.MakesId, basicSearchViewModel.CategoryId, basicSearchViewModel.ModelId,
                                                basicSearchViewModel.YearMin, basicSearchViewModel.YearMax, basicSearchViewModel.PriceRangeMin, basicSearchViewModel.PriceRangeMax);
            fileredProductsTotal = fileredProductsTotal.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).ToList();
            fileredProductsTotal = fileredProductsTotal.OrderByDescending(x => x.IsFeatured).ToList();

            if (!ModelState.IsValid)
            {
                shopViewModel.ProductAndMakeBases = fileredProductsTotal.Take(this._recordsPerPageDefault).ToList();
                shopViewModel.BasicSearchViewModel = basicSearchViewModel;
                ViewBag.PagesCount = Convert.ToInt32(Math.Ceiling((double)fileredProductsTotal.Count() / this._recordsPerPageDefault));

                return this.View(nameof(Index), shopViewModel);
            }

            shopViewModel.ProductAndMakeBases = fileredProductsTotal.Skip(basicSearchViewModel.RecordsPerPage * (basicSearchViewModel.PageNumber - 1)).Take(basicSearchViewModel.RecordsPerPage).ToList();
            shopViewModel.BasicSearchViewModel = basicSearchViewModel;
            ViewBag.ModelsForCategory = await makesApi.GetModelsForMakeAndCategory(this._make.MakesId, basicSearchViewModel.CategoryId);
            ViewBag.PagesCount = Convert.ToInt32(Math.Ceiling((double)fileredProductsTotal.Count() / basicSearchViewModel.RecordsPerPage));

            return this.View("Index", shopViewModel);
        }

        /// <summary>
        /// Products the inventory.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [HttpGet("[controller]/Product/{productId}/Inventory")]
        public async Task<IActionResult> ProductInventory(int productId)
        {

            //Uncomment when Mazda View has been available again
            var inventoryItems = new List<InventoryForProduct>();
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            ViewBag.MakesId = this._make.MakesId;

            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                inventoryItems = await productsApi.GetInventoryForProduct(productId, this._make.MakesId);
                inventoryItems = inventoryItems.Where(x => x.QuantityInStock > 0 && x.QuantityInStockInventory > 0 && x.AllowShow > 0 && x.AllowShowInventory > 0).ToList();
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(inventoryItems);
        }

        /// <summary>
        /// Products the details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <returns>Details view</returns>
        [HttpGet("[controller]/Product/{productId}/Inventory/{inventoryItemsId}/Details")]
        public async Task<IActionResult> ProductDetails(int productId, int inventoryItemsId)
        {

            //Uncomment when Mazda View has been available again
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress");
            var productDetails = new ProductWithDetails();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                var productsWithDetails = await productsApi.GetInventoryAndProductBaseWithDetails(productId, inventoryItemsId, this._make.MakesId);
                productsWithDetails = productsWithDetails.Where(x => x.QuantityInStock > 0 && x.QuantityInStockInventory > 0 && x.AllowShow > 0 && x.AllowShowInventory > 0).ToList();
                productDetails = productsWithDetails.FirstOrDefault();
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(productDetails);
        }
    }
}