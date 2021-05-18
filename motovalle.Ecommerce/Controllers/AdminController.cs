// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Admin Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.Directory;
    using motovalle.Ecommerce.Helpers.ReportHelper;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Models.ViewModels.Admin;
    using motovalle.Ecommerce.Resources;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Admin Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "ADMIN,BLOGADMIN")]
    public class AdminController : Controller
    {
        #region ctor
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The role manager
        /// </summary>
        private readonly RoleManager<ApplicationRole> _roleManager;

        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IWebHostEnvironment _host;

        /// <summary>
        /// The report helper
        /// </summary>
        private readonly IReportHelper _reportHelper;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The headquarters view model
        /// </summary>
        private readonly List<HeadquartersViewModel> _headquartersViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="host">The host.</param>
        /// <param name="reportHelper">The report helper.</param>
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
                              IConfiguration configuration, IWebHostEnvironment host, IReportHelper reportHelper)
        {
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._configuration = configuration;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._host = host;
            this._reportHelper = reportHelper;
            this._headquartersViewModel = configuration.GetSection("Headquarters").Get<List<HeadquartersViewModel>>();
        }
        #endregion

        #region Index
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Admin index view</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
        #endregion

        #region Blog
        /// <summary>
        /// Blogs the admin.
        /// </summary>
        /// <returns>Blog admin view</returns>
        [HttpGet]
        public IActionResult BlogAdmin()
        {
            return this.View();
        }
        #endregion

        #region Ecommerce Parameters
        #region Hero Images
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> HeroImages()
        {
            var heroImages = new List<HeroImages>();
            try
            {
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                heroImages = await heroImagesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(heroImages);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HeroImages/Edit/{id}")]
        public async Task<IActionResult> HeroImagesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var makesApi = new MakesApi(this._httpClientInstance);
            var makes = await makesApi.GetAllRecords();
            var heroImageViewModel = new HeroImagesViewModel();
            try
            {
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                var heroImage = await heroImagesApi.GetRecord(id ?? 0);
                heroImageViewModel.HasButton = heroImage.HasButton;
                heroImageViewModel.HeroImagesId = heroImage.HeroImagesId;
                heroImageViewModel.ImageURL = heroImage.ImageURL;
                heroImageViewModel.Index = heroImage.Index;
                heroImageViewModel.Enable = heroImage.Enable;
                heroImageViewModel.ButtonURL = heroImage.ButtonURL;
                heroImageViewModel.ShowSpan = heroImage.ShowSpan;
                heroImageViewModel.SpanComplementText = heroImage.SpanComplementText;
                heroImageViewModel.SpanText = heroImage.SpanText;
                heroImageViewModel.FkMakesId = heroImage.FkMakesId;
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (heroImageViewModel == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", heroImageViewModel.FkMakesId);
            return this.View(heroImageViewModel);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="heroImages">The hero images.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HeroImages/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeroImagesEdit(long id, [FromForm] HeroImagesViewModel heroImageViewModel)
        {
            if (id != heroImageViewModel.HeroImagesId)
            {
                return this.View("NotFoundAdmin");
            }

            var makesApi = new MakesApi(this._httpClientInstance);
            var makes = await makesApi.GetAllRecords();
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", heroImageViewModel.FkMakesId);
            try
            {
                if (heroImageViewModel.ImageUrlFile?.Length > 0)
                {
                    if (heroImageViewModel.ImageUrlFile.Length > 26214400)
                    {
                        ModelState.AddModelError(string.Empty, "The Image is too large (25 MB Max).");
                        return this.View(heroImageViewModel);
                    }

                    ////Deletes picture
                    if (!string.IsNullOrEmpty(heroImageViewModel.ImageURL))
                    {
                        var fullPathDelete = Path.Combine(this._host.WebRootPath, heroImageViewModel.ImageURL);
                        if (System.IO.File.Exists(fullPathDelete))
                        {
                            System.IO.File.Delete(fullPathDelete);
                        }
                    }

                    var partialPath = Path.Combine("media", "main-slider", "hero-images", heroImageViewModel.ImageUrlFile.FileName);
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await heroImageViewModel.ImageUrlFile.CopyToAsync(fileStream);
                    heroImageViewModel.ImageURL = partialPath;
                }
                else
                {
                    if (string.IsNullOrEmpty(heroImageViewModel.ImageURL))
                    {
                        ModelState.AddModelError(nameof(heroImageViewModel.ImageURL), $"{nameof(heroImageViewModel.ImageURL)} is required.");
                        return this.View(heroImageViewModel);
                    }
                }

                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                await heroImagesApi.UpdateRecord(id, heroImageViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(HeroImagesEdit), heroImageViewModel);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(HeroImages));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Hero Images model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HeroImages/Create")]
        public async Task<IActionResult> HeroImagesCreate()
        {
            var makesApi = new MakesApi(this._httpClientInstance);
            var makes = await makesApi.GetAllRecords();
            var heroImages = new HeroImagesViewModel();
            try
            {
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                var heroImagesResult = await heroImagesApi.GetAllRecords();
                var maxIndex = heroImagesResult.Max(x => x?.Index);
                heroImages = new HeroImagesViewModel()
                {
                    Enable = true,
                    HasButton = false,
                    ShowSpan = false,
                    Index = (maxIndex ?? 0) + 1
                };
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
            return this.View(heroImages);
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="heroImageViewModel">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HeroImages/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeroImagesCreate([FromForm] HeroImagesViewModel heroImageViewModel)
        {
            var heroImages = new HeroImages();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                var makes = await makesApi.GetAllRecords();
                ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
                if (heroImageViewModel.ImageUrlFile != null && heroImageViewModel.ImageUrlFile.Length > 0)
                {
                    if (heroImageViewModel.ImageUrlFile.Length > 26214400)
                    {
                        ModelState.AddModelError(string.Empty, "The Image is too large (25 MB Max).");
                        return this.View(heroImageViewModel);
                    }

                    ////Deletes picture
                    if (!string.IsNullOrEmpty(heroImageViewModel.ImageURL))
                    {
                        var fullPathDelete = Path.Combine(this._host.WebRootPath, heroImageViewModel.ImageURL);
                        if (System.IO.File.Exists(fullPathDelete))
                        {
                            System.IO.File.Delete(fullPathDelete);
                        }
                    }

                    var partialPath = Path.Combine("media", "main-slider", "hero-images", heroImageViewModel.ImageUrlFile.FileName);
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await heroImageViewModel.ImageUrlFile.CopyToAsync(fileStream);
                    heroImageViewModel.ImageURL = partialPath;
                }
                else
                {
                    if (string.IsNullOrEmpty(heroImageViewModel.ImageURL))
                    {
                        ModelState.AddModelError(nameof(heroImageViewModel.ImageURL), $"{nameof(heroImageViewModel.ImageURL)} is required.");
                        return this.View(heroImageViewModel);
                    }
                }

                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                heroImages = await heroImagesApi.CreateRecord(heroImageViewModel);
                
                ////If new index is 1, so desplace rest of indexes in 1 unit
                if (heroImages.HeroImagesId > 0 && heroImages.Index == 1)
                {
                    var alHeroImages = (await heroImagesApi.GetAllRecords()).Where(x => x.HeroImagesId != heroImages.HeroImagesId).ToList();
                    foreach (var item in alHeroImages)
                    {
                        item.Index++;
                        await heroImagesApi.UpdateRecord(item.HeroImagesId, item);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (heroImages.HeroImagesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(heroImageViewModel);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(HeroImages));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HeroImages/Delete/{id}")]
        public async Task<IActionResult> HeroImagesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bodyStyle = new HeroImages();
            try
            {
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                bodyStyle = await heroImagesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bodyStyle == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bodyStyle);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="heroImagesId">The hero images identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HeroImages/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeroImagesDeleteConfirmed(long heroImagesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var heroImagesApi = new HeroImagesApi(this._httpClientInstance);
                var heroImage = await heroImagesApi.GetRecord(heroImagesId);
             
                ////Deletes picture
                if (!string.IsNullOrEmpty(heroImage.ImageURL))
                {
                    var fullPathDelete = Path.Combine(this._host.WebRootPath, heroImage.ImageURL);
                    if (System.IO.File.Exists(fullPathDelete))
                    {
                        System.IO.File.Delete(fullPathDelete);
                    }
                }

                await heroImagesApi.DeleteRecord(heroImagesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(HeroImagesDelete), new { id = heroImagesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(HeroImages));
        }
        #endregion
        #endregion

        #region Main Parameters
        ////Starts main parameters actions
        #region Body Styles
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> BodyStyles()
        {
            var bodyStyles = new List<BodyStyles>();
            try
            {
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                bodyStyles = await bodyStylesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(bodyStyles);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/BodyStyles/Edit/{id}")]
        public async Task<IActionResult> BodyStylesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bodyStyle = new BodyStyles();
            try
            {
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                bodyStyle = await bodyStylesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bodyStyle == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bodyStyle);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/BodyStyles/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BodyStylesEdit(long id, [FromForm] BodyStyles bodyStyle)
        {
            if (id != bodyStyle.BodyStylesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                await bodyStylesApi.UpdateRecord(id, bodyStyle);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(BodyStylesEdit), bodyStyle);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(BodyStyles));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/BodyStyles/Create")]
        public IActionResult BodyStylesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="bodyStyle">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/BodyStyles/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BodyStylesCreate([FromForm] BodyStyles bodyStyle)
        {
            var bodyStyles = new BodyStyles();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                bodyStyles = await bodyStylesApi.CreateRecord(bodyStyle);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bodyStyles.BodyStylesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(bodyStyle);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(BodyStyles));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/BodyStyles/Delete/{id}")]
        public async Task<IActionResult> BodyStylesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bodyStyle = new BodyStyles();
            try
            {
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                bodyStyle = await bodyStylesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bodyStyle == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bodyStyle);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="bodyStylesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/BodyStyles/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BodyStylesDeleteConfirmed(long bodyStylesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                await bodyStylesApi.DeleteRecord(bodyStylesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(BodyStylesDelete), new { id = bodyStylesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(BodyStyles));
        }
        #endregion

        #region Categories
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = new List<Categories>();
            try
            {
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                categories = await categoriesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(categories);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Categories/Edit/{id}")]
        public async Task<IActionResult> CategoriesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var categories = new Categories();
            try
            {
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                categories = await categoriesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (categories == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(categories);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Categories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoriesEdit(long id, [FromForm] Categories category)
        {
            if (id != category.CategoriesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                await categoriesApi.UpdateRecord(id, category);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(CategoriesEdit), category);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Categories));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Categories/Create")]
        public IActionResult CategoriesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="category">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Categories/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoriesCreate([FromForm] Categories category)
        {
            var categories = new Categories();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                categories = await categoriesApi.CreateRecord(category);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (categories.CategoriesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(category);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Categories));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Categories/Delete/{id}")]
        public async Task<IActionResult> CategoriesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var category = new Categories();
            try
            {
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                category = await categoriesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (category == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(category);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="categoriesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Categories/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoriesDeleteConfirmed(long categoriesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                await categoriesApi.DeleteRecord(categoriesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(CategoriesDelete), new { id = categoriesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Categories));
        }
        #endregion

        #region Colors
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Colors()
        {
            var colors = new List<Colors>();
            try
            {
                var categoriesApi = new ColorsApi(this._httpClientInstance);
                colors = await categoriesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(colors);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Colors/Edit/{id}")]
        public async Task<IActionResult> ColorsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var colors = new Colors();
            var makes = new List<Makes>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                var colorsApi = new ColorsApi(this._httpClientInstance);
                colors = await colorsApi.GetRecord(id ?? 0);
                makes = await makesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (colors == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", colors.FkMakesId);
            return this.View(colors);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Colors/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ColorsEdit(long id, [FromForm] Colors color)
        {
            var makesApi = new MakesApi(this._httpClientInstance);
            var makes = await makesApi.GetAllRecords();
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", color.FkMakesId);
            if (id != color.ColorsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var colorsApi = new ColorsApi(this._httpClientInstance);
                await colorsApi.UpdateRecord(id, color);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ColorsEdit), color);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Colors));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Colors/Create")]
        public async Task<IActionResult> ColorsCreate()
        {
            var makes = new List<Makes>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                makes = await makesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="color">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Colors/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ColorsCreate([FromForm] Colors color)
        {
            var makes = new List<Makes>();
            var makesApi = new MakesApi(this._httpClientInstance);
            makes = await makesApi.GetAllRecords();
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");

            var colors = new Colors();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var colorsApi = new ColorsApi(this._httpClientInstance);
                colors = await colorsApi.CreateRecord(color);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (colors.ColorsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(color);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Colors));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Colors/Delete/{id}")]
        public async Task<IActionResult> ColorsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var color = new Colors();
            try
            {
                var colorsApi = new ColorsApi(this._httpClientInstance);
                color = await colorsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (color == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(color);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="colorsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Colors/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ColorsDeleteConfirmed(long colorsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var colorsApi = new ColorsApi(this._httpClientInstance);
                await colorsApi.DeleteRecord(colorsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ColorsDelete), new { id = colorsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Colors));
        }
        #endregion

        #region DriveTrains
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> DriveTrains()
        {
            var driveTrains = new List<DriveTrains>();
            try
            {
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                driveTrains = await driveTrainsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(driveTrains);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/DriveTrains/Edit/{id}")]
        public async Task<IActionResult> DriveTrainsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bodyStyle = new DriveTrains();
            try
            {
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                bodyStyle = await driveTrainsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bodyStyle == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bodyStyle);
        }


        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="driveTrains">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/DriveTrains/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DriveTrainsEdit(long id, [FromForm] DriveTrains driveTrains)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(DriveTrainsEdit), driveTrains);
            }

            if (id != driveTrains.DriveTrainsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                await driveTrainsApi.UpdateRecord(id, driveTrains);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(DriveTrainsEdit), driveTrains);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(DriveTrains));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/DriveTrains/Create")]
        public IActionResult DriveTrainsCreate()
        {
            return this.View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/DriveTrains/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DriveTrainsCreate([FromForm] DriveTrains driveTrain)
        {
            var driveTrains = new DriveTrains();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                driveTrains = await driveTrainsApi.CreateRecord(driveTrains);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (driveTrains.DriveTrainsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(driveTrain);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(DriveTrains));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/DriveTrains/Delete/{id}")]
        public async Task<IActionResult> DriveTrainsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bodyStyle = new DriveTrains();
            try
            {
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                bodyStyle = await driveTrainsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bodyStyle == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bodyStyle);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="driveTrainsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/DriveTrains/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DriveTrainsDeleteConfirmed(long driveTrainsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                await driveTrainsApi.DeleteRecord(driveTrainsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(DriveTrainsDelete), new { id = driveTrainsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(DriveTrains));
        }
        #endregion

        #region EngineTypes
        /// <summary>
        /// Engines the types.
        /// </summary>
        /// <returns>Engine types view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> EngineTypes()
        {
            var engineTypes = new List<EngineTypes>();
            try
            {
                var driveTrainsApi = new EngineTypesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/EngineTypes/Edit/{id}")]
        public async Task<IActionResult> EngineTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var engineTypes = new EngineTypes();
            try
            {
                var driveTrainsApi = new EngineTypesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineTypes == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="driveTrains">The body styles.</param>
        /// <returns>Save changes for driveTrain</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/EngineTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EngineTypesEdit(long id, [FromForm] EngineTypes engineType)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(EngineTypesEdit), engineType);
            }

            if (id != engineType.EngineTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new EngineTypesApi(this._httpClientInstance);
                await driveTrainsApi.UpdateRecord(id, engineType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(EngineTypesEdit), engineType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(EngineTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/EngineTypes/Create")]
        public IActionResult EngineTypesCreate()
        {
            return this.View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/EngineTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EngineTypesCreate([FromForm] EngineTypes engineType)
        {
            var engineTypes = new EngineTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new EngineTypesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.CreateRecord(engineType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineTypes.EngineTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(engineType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(EngineTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/EngineTypes/Delete/{id}")]
        public async Task<IActionResult> EngineTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var engineType = new EngineTypes();
            try
            {
                var driveTrainsApi = new EngineTypesApi(this._httpClientInstance);
                engineType = await driveTrainsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(engineType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="engineTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/EngineTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EngineTypesDeleteConfirmed(long engineTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new EngineTypesApi(this._httpClientInstance);
                await driveTrainsApi.DeleteRecord(engineTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(EngineTypesDelete), new { id = engineTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(EngineTypes));
        }
        #endregion

        #region EPA Classes
        /// <summary>
        /// Engines the types.
        /// </summary>
        /// <returns>Engine types view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> EpaClasses()
        {
            var engineTypes = new List<EpaClasses>();
            try
            {
                var driveTrainsApi = new EpaClassesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/EpaClasses/Edit/{id}")]
        public async Task<IActionResult> EpaClassesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var engineTypes = new EpaClasses();
            try
            {
                var driveTrainsApi = new EpaClassesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineTypes == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="driveTrains">The body styles.</param>
        /// <returns>Save changes for driveTrain</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/EpaClasses/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EpaClassesEdit(long id, [FromForm] EpaClasses epaClasses)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(EpaClassesEdit), epaClasses);
            }

            if (id != epaClasses.EpaClassesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new EpaClassesApi(this._httpClientInstance);
                await driveTrainsApi.UpdateRecord(id, epaClasses);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(EpaClassesEdit), epaClasses);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(EpaClasses));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/EpaClasses/Create")]
        public IActionResult EpaClassesCreate()
        {
            return this.View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/EpaClasses/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EpaClassesCreate([FromForm] EpaClasses epaClass)
        {
            var epaClasses = new EpaClasses();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var epaClassesApi = new EpaClassesApi(this._httpClientInstance);
                epaClasses = await epaClassesApi.CreateRecord(epaClass);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (epaClasses.EpaClassesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(epaClass);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(EpaClasses));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/EpaClasses/Delete/{id}")]
        public async Task<IActionResult> EpaClassesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var engineType = new EpaClasses();
            try
            {
                var driveTrainsApi = new EpaClassesApi(this._httpClientInstance);
                engineType = await driveTrainsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(engineType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="epaClassesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/EpaClasses/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EpaClassesDeleteConfirmed(long epaClassesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new EpaClassesApi(this._httpClientInstance);
                await driveTrainsApi.DeleteRecord(epaClassesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(EpaClassesDelete), new { id = epaClassesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(EpaClasses));
        }
        #endregion

        #region Makes
        /// <summary>
        /// Engines the types.
        /// </summary>
        /// <returns>Engine types view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Makes()
        {
            var engineTypes = new List<Makes>();
            try
            {
                var driveTrainsApi = new MakesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Makes/Edit/{id}")]
        public async Task<IActionResult> MakesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var engineTypes = new Makes();
            try
            {
                var driveTrainsApi = new MakesApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineTypes == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="driveTrains">The body styles.</param>
        /// <returns>Save changes for driveTrain</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Makes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakesEdit(long id, [FromForm] Makes makes)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(MakesEdit), makes);
            }

            if (id != makes.MakesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var makesApi = new MakesApi(this._httpClientInstance);
                await makesApi.UpdateRecord(id, makes);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(MakesEdit), makes);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Makes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Makes/Create")]
        public IActionResult MakesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Makeses the create.
        /// </summary>
        /// <param name="make">The make.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Makes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakesCreate([FromForm] Makes make)
        {
            var makes = new Makes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var makesApi = new MakesApi(this._httpClientInstance);
                makes = await makesApi.CreateRecord(make);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (makes.MakesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(make);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Makes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Makes/Delete/{id}")]
        public async Task<IActionResult> MakesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var engineType = new Makes();
            try
            {
                var driveTrainsApi = new MakesApi(this._httpClientInstance);
                engineType = await driveTrainsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (engineType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(engineType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="makesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Makes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakesDeleteConfirmed(long makesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new MakesApi(this._httpClientInstance);
                await driveTrainsApi.DeleteRecord(makesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(MakesDelete), new { id = makesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Makes));
        }
        #endregion

        #region Models
        /// <summary>
        /// Engines the types.
        /// </summary>
        /// <returns>Engine types view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Models()
        {
            var engineTypes = new List<Models>();
            try
            {
                var driveTrainsApi = new ModelsApi(this._httpClientInstance);
                engineTypes = await driveTrainsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(engineTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Models/Edit/{id}")]
        public async Task<IActionResult> ModelsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var models = new Models();
            var categories = new List<Categories>();
            var makes = new List<Makes>();
            try
            {
                var modelsApi = new ModelsApi(this._httpClientInstance);
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                var makesApi = new MakesApi(this._httpClientInstance);
                models = await modelsApi.GetRecord(id ?? 0);
                categories = await categoriesApi.GetAllRecords();
                makes = await makesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (models == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName", models.FkCategoriesId);
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", models.FkMakesId);
            return this.View(models);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="driveTrains">The body styles.</param>
        /// <returns>Save changes for driveTrain</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Models/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModelsEdit(long id, [FromForm] Models models)
        {
            if (id != models.ModelsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var modelsApi = new ModelsApi(this._httpClientInstance);
                await modelsApi.UpdateRecord(id, models);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ModelsEdit), models);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Models));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Models/Create")]
        public async Task<IActionResult> ModelsCreate()
        {
            var categories = new List<Categories>();
            var makes = new List<Makes>();
            try
            {
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                var makesApi = new MakesApi(this._httpClientInstance);
                categories = await categoriesApi.GetAllRecords();
                makes = await makesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName");
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
            return this.View();
        }

        /// <summary>
        /// Modelses the create.
        /// </summary>
        /// <param name="make">The make.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Models/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModelsCreate([FromForm] Models make)
        {
            var makes = new Models();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var makesApi = new ModelsApi(this._httpClientInstance);
                makes = await makesApi.CreateRecord(make);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (makes.ModelsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(make);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Models));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Models/Delete/{id}")]
        public async Task<IActionResult> ModelsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var models = new Models();
            var categories = new List<Categories>();
            var makes = new List<Makes>();
            try
            {
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                var makesApi = new MakesApi(this._httpClientInstance);
                categories = await categoriesApi.GetAllRecords();
                makes = await makesApi.GetAllRecords();
                var modelsApi = new ModelsApi(this._httpClientInstance);
                models = await modelsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (models == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName");
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
            return this.View(models);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="modelsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Models/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModelsDeleteConfirmed(long modelsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var driveTrainsApi = new ModelsApi(this._httpClientInstance);
                await driveTrainsApi.DeleteRecord(modelsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ModelsDelete), new { id = modelsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Models));
        }
        #endregion

        #region Transmissions
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Transmissions()
        {
            var transmissions = new List<Transmissions>();
            try
            {
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                transmissions = await transmissionsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(transmissions);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Transmissions/Edit/{id}")]
        public async Task<IActionResult> TransmissionsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var transmissions = new Transmissions();
            try
            {
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                transmissions = await transmissionsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (transmissions == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(transmissions);
        }


        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Transmissions/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransmissionsEdit(long id, [FromForm] Transmissions transmission)
        {
            if (id != transmission.TransmissionsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                await transmissionsApi.UpdateRecord(id, transmission);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(TransmissionsEdit), transmission);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Transmissions));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Transmissions/Create")]
        public IActionResult TransmissionsCreate()
        {
            return this.View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Transmissions/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransmissionsCreate([FromForm] Transmissions transmission)
        {
            var transmissions = new Transmissions();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                transmissions = await transmissionsApi.CreateRecord(transmission);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (transmissions.TransmissionsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(transmission);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Transmissions));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Transmissions/Delete/{id}")]
        public async Task<IActionResult> TransmissionsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var transmissions = new Transmissions();
            try
            {
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                transmissions = await transmissionsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (transmissions == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(transmissions);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="transmissionsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Transmissions/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransmissionsDeleteConfirmed(long transmissionsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                await transmissionsApi.DeleteRecord(transmissionsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(TransmissionsDelete), new { id = transmissionsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Transmissions));
        }
        #endregion

        #region Warranties
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Warranties()
        {
            var warranties = new List<Warranties>();
            try
            {
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                warranties = await warrantiesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(warranties);
        }

        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Warranties/More/{id}")]
        public async Task<IActionResult> WarrantiesMore(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var warranties = new Warranties();
            try
            {
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                warranties = await warrantiesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(warranties);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Warranties/Edit/{id}")]
        public async Task<IActionResult> WarrantiesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            this.SetCustomFonts();
            var warranties = new Warranties();
            try
            {
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                warranties = await warrantiesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (warranties == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(warranties);
        }


        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Warranties/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WarrantiesEdit(long id, [FromForm] Warranties warranty)
        {
            this.SetCustomFonts();
            if (!ModelState.IsValid)
            {
                return this.View(nameof(WarrantiesEdit), warranty);
            }

            if (id != warranty.WarrantiesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                await warrantiesApi.UpdateRecord(id, warranty);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(WarrantiesEdit), warranty);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Warranties));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Warranties/Create")]
        public IActionResult WarrantiesCreate()
        {
            this.SetCustomFonts();
            return this.View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Warranties/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WarrantiesCreate([FromForm] Warranties warranty)
        {
            this.SetCustomFonts();
            var warranties = new Warranties();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                warranties = await warrantiesApi.CreateRecord(warranty);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (warranties.WarrantiesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(nameof(WarrantiesCreate), warranty);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Warranties));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Warranties/Delete/{id}")]
        public async Task<IActionResult> WarrantiesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var warranties = new Warranties();
            try
            {
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                warranties = await warrantiesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (warranties == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(warranties);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="transmissionsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Warranties/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WarrantiesDeleteConfirmed(long warrantiesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                await warrantiesApi.DeleteRecord(warrantiesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(WarrantiesDelete), new { id = warrantiesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Warranties));
        }
        #endregion

        #region Taxes
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Taxes()
        {
            var taxes = new List<Taxes>();
            try
            {
                var taxesApi = new TaxesApi(this._httpClientInstance);
                taxes = await taxesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(taxes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Taxes/Edit/{id}")]
        public async Task<IActionResult> TaxesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var tax = new Taxes();
            try
            {
                var taxesApi = new TaxesApi(this._httpClientInstance);
                tax = await taxesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (tax == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(tax);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="taxes">The body styles.</param>
        /// <returns>Save changes for tax</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Taxes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxesEdit(long id, [FromForm] Taxes tax)
        {
            if (id != tax.TaxesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var taxesApi = new TaxesApi(this._httpClientInstance);
                await taxesApi.UpdateRecord(id, tax);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(TaxesEdit), tax);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Taxes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Taxes/Create")]
        public IActionResult TaxesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="tax">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Taxes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxesCreate([FromForm] Taxes tax)
        {
            var taxes = new Taxes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var taxesApi = new TaxesApi(this._httpClientInstance);
                taxes = await taxesApi.CreateRecord(tax);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (taxes.TaxesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(tax);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Taxes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Taxes/Delete/{id}")]
        public async Task<IActionResult> TaxesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var tax = new Taxes();
            try
            {
                var taxesApi = new TaxesApi(this._httpClientInstance);
                tax = await taxesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (tax == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(tax);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="taxesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Taxes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxesDeleteConfirmed(long taxesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var taxesApi = new TaxesApi(this._httpClientInstance);
                await taxesApi.DeleteRecord(taxesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(TaxesDelete), new { id = taxesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Taxes));
        }
        #endregion

        #region Product Documents Categories
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ProductDocumentsCategories()
        {
            var productDocumentsCategories = new List<ProductDocumentsCategories>();
            try
            {
                var productDocumentsCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                productDocumentsCategories = await productDocumentsCategoriesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(productDocumentsCategories);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductDocumentsCategories/Edit/{id}")]
        public async Task<IActionResult> ProductDocumentsCategoriesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var tax = new ProductDocumentsCategories();
            try
            {
                var productDocumentsCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                tax = await productDocumentsCategoriesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (tax == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(tax);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="productDocumentsCategories">The body styles.</param>
        /// <returns>Save changes for tax</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductDocumentsCategories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDocumentsCategoriesEdit(long id, [FromForm] ProductDocumentsCategories productDocumentsCategories)
        {
            if (id != productDocumentsCategories.ProductDocumentsCategoriesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                await productDocumentsCategoriesApi.UpdateRecord(id, productDocumentsCategories);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ProductDocumentsCategoriesEdit), productDocumentsCategories);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(ProductDocumentsCategories));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductDocumentsCategories/Create")]
        public IActionResult ProductDocumentsCategoriesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="productDocumentsCategories">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductDocumentsCategories/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDocumentsCategoriesCreate([FromForm] ProductDocumentsCategories productDocumentsCategories)
        {
            var productDocumentsCategoriesCreated = new ProductDocumentsCategories();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                productDocumentsCategoriesCreated = await productDocumentsCategoriesApi.CreateRecord(productDocumentsCategories);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (productDocumentsCategoriesCreated.ProductDocumentsCategoriesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(productDocumentsCategories);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(ProductDocumentsCategories));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductDocumentsCategories/Delete/{id}")]
        public async Task<IActionResult> ProductDocumentsCategoriesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var tax = new ProductDocumentsCategories();
            try
            {
                var productDocumentsCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                tax = await productDocumentsCategoriesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (tax == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(tax);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="productDocumentsCategoriesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductDocumentsCategories/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDocumentsCategoriesDeleteConfirmed(long productDocumentsCategoriesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                await productDocumentsCategoriesApi.DeleteRecord(productDocumentsCategoriesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ProductDocumentsCategoriesDelete), new { id = productDocumentsCategoriesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(ProductDocumentsCategories));
        }
        #endregion

        #region Massey Specifics
        #region Aspiration Types
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> AspirationTypes()
        {
            var aspirationTypes = new List<AspirationTypes>();
            try
            {
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                aspirationTypes = await aspirationTypesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(aspirationTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/AspirationTypes/Edit/{id}")]
        public async Task<IActionResult> AspirationTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var aspirationType = new AspirationTypes();
            try
            {
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                aspirationType = await aspirationTypesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (aspirationType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(aspirationType);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="aspirationTypes">The body styles.</param>
        /// <returns>Save changes for aspirationType</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/AspirationTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AspirationTypesEdit(long id, [FromForm] AspirationTypes aspirationType)
        {
            if (id != aspirationType.AspirationTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                await aspirationTypesApi.UpdateRecord(id, aspirationType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(AspirationTypesEdit), aspirationType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(AspirationTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/AspirationTypes/Create")]
        public IActionResult AspirationTypesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="aspirationType">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/AspirationTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AspirationTypesCreate([FromForm] AspirationTypes aspirationType)
        {
            var aspirationTypes = new AspirationTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                aspirationTypes = await aspirationTypesApi.CreateRecord(aspirationType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (aspirationTypes.AspirationTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(aspirationType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(AspirationTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/AspirationTypes/Delete/{id}")]
        public async Task<IActionResult> AspirationTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var aspirationType = new AspirationTypes();
            try
            {
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                aspirationType = await aspirationTypesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (aspirationType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(aspirationType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="aspirationTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/AspirationTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AspirationTypesDeleteConfirmed(long aspirationTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                await aspirationTypesApi.DeleteRecord(aspirationTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(AspirationTypesDelete), new { id = aspirationTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(AspirationTypes));
        }
        #endregion 

        #region Rear Tires Types
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> RearTiresTypes()
        {
            var rearTiresTypes = new List<RearTiresTypes>();
            try
            {
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                rearTiresTypes = await rearTiresTypesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(rearTiresTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/RearTiresTypes/Edit/{id}")]
        public async Task<IActionResult> RearTiresTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var rearTiresType = new RearTiresTypes();
            try
            {
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                rearTiresType = await rearTiresTypesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (rearTiresType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(rearTiresType);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="rearTiresTypes">The body styles.</param>
        /// <returns>Save changes for rearTiresType</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/RearTiresTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RearTiresTypesEdit(long id, [FromForm] RearTiresTypes rearTiresType)
        {
            if (id != rearTiresType.RearTiresTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                await rearTiresTypesApi.UpdateRecord(id, rearTiresType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(RearTiresTypesEdit), rearTiresType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(RearTiresTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/RearTiresTypes/Create")]
        public IActionResult RearTiresTypesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="rearTiresType">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/RearTiresTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RearTiresTypesCreate([FromForm] RearTiresTypes rearTiresType)
        {
            var rearTiresTypes = new RearTiresTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                rearTiresTypes = await rearTiresTypesApi.CreateRecord(rearTiresType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (rearTiresTypes.RearTiresTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(rearTiresType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(RearTiresTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/RearTiresTypes/Delete/{id}")]
        public async Task<IActionResult> RearTiresTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var rearTiresType = new RearTiresTypes();
            try
            {
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                rearTiresType = await rearTiresTypesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (rearTiresType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(rearTiresType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="rearTiresTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/RearTiresTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RearTiresTypesDeleteConfirmed(long rearTiresTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                await rearTiresTypesApi.DeleteRecord(rearTiresTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(RearTiresTypesDelete), new { id = rearTiresTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(RearTiresTypes));
        }
        #endregion

        #region Front Tires Types
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> FrontTiresTypes()
        {
            var frontTiresTypes = new List<FrontTiresTypes>();
            try
            {
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                frontTiresTypes = await frontTiresTypesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(frontTiresTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/FrontTiresTypes/Edit/{id}")]
        public async Task<IActionResult> FrontTiresTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var frontTiresType = new FrontTiresTypes();
            try
            {
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                frontTiresType = await frontTiresTypesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (frontTiresType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(frontTiresType);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="frontTiresTypes">The body styles.</param>
        /// <returns>Save changes for frontTiresType</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/FrontTiresTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FrontTiresTypesEdit(long id, [FromForm] FrontTiresTypes frontTiresType)
        {
            if (id != frontTiresType.FrontTiresTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                await frontTiresTypesApi.UpdateRecord(id, frontTiresType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(FrontTiresTypesEdit), frontTiresType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(FrontTiresTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/FrontTiresTypes/Create")]
        public IActionResult FrontTiresTypesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="frontTiresType">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/FrontTiresTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FrontTiresTypesCreate([FromForm] FrontTiresTypes frontTiresType)
        {
            var frontTiresTypes = new FrontTiresTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                frontTiresTypes = await frontTiresTypesApi.CreateRecord(frontTiresType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (frontTiresTypes.FrontTiresTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(frontTiresType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(FrontTiresTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/FrontTiresTypes/Delete/{id}")]
        public async Task<IActionResult> FrontTiresTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var frontTiresType = new FrontTiresTypes();
            try
            {
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                frontTiresType = await frontTiresTypesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (frontTiresType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(frontTiresType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="frontTiresTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/FrontTiresTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FrontTiresTypesDeleteConfirmed(long frontTiresTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                await frontTiresTypesApi.DeleteRecord(frontTiresTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(FrontTiresTypesDelete), new { id = frontTiresTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(FrontTiresTypes));
        }
        #endregion

        #region Separation Systems
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> SeparationSystems()
        {
            var separationSystems = new List<SeparationSystems>();
            try
            {
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                separationSystems = await separationSystemsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(separationSystems);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/SeparationSystems/Edit/{id}")]
        public async Task<IActionResult> SeparationSystemsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var separationSystem = new SeparationSystems();
            try
            {
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                separationSystem = await separationSystemsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (separationSystem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(separationSystem);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="separationSystems">The body styles.</param>
        /// <returns>Save changes for separationSystem</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/SeparationSystems/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeparationSystemsEdit(long id, [FromForm] SeparationSystems separationSystem)
        {
            if (id != separationSystem.SeparationSystemsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                await separationSystemsApi.UpdateRecord(id, separationSystem);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(SeparationSystemsEdit), separationSystem);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(SeparationSystems));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/SeparationSystems/Create")]
        public IActionResult SeparationSystemsCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="separationSystem">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/SeparationSystems/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeparationSystemsCreate([FromForm] SeparationSystems separationSystem)
        {
            var separationSystems = new SeparationSystems();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                separationSystems = await separationSystemsApi.CreateRecord(separationSystem);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (separationSystems.SeparationSystemsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(separationSystem);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(SeparationSystems));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/SeparationSystems/Delete/{id}")]
        public async Task<IActionResult> SeparationSystemsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var separationSystem = new SeparationSystems();
            try
            {
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                separationSystem = await separationSystemsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (separationSystem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(separationSystem);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="separationSystemsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/SeparationSystems/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeparationSystemsDeleteConfirmed(long separationSystemsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                await separationSystemsApi.DeleteRecord(separationSystemsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(SeparationSystemsDelete), new { id = separationSystemsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(SeparationSystems));
        }
        #endregion

        #region Hitch Systems
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> HitchSystems()
        {
            var hitchSystems = new List<HitchSystems>();
            try
            {
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                hitchSystems = await hitchSystemsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(hitchSystems);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HitchSystems/Edit/{id}")]
        public async Task<IActionResult> HitchSystemsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var hitchSystem = new HitchSystems();
            try
            {
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                hitchSystem = await hitchSystemsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (hitchSystem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(hitchSystem);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="hitchSystems">The body styles.</param>
        /// <returns>Save changes for hitchSystem</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HitchSystems/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HitchSystemsEdit(long id, [FromForm] HitchSystems hitchSystem)
        {
            if (id != hitchSystem.HitchSystemsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                await hitchSystemsApi.UpdateRecord(id, hitchSystem);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(HitchSystemsEdit), hitchSystem);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(HitchSystems));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HitchSystems/Create")]
        public IActionResult HitchSystemsCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="hitchSystem">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HitchSystems/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HitchSystemsCreate([FromForm] HitchSystems hitchSystem)
        {
            var hitchSystems = new HitchSystems();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                hitchSystems = await hitchSystemsApi.CreateRecord(hitchSystem);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (hitchSystems.HitchSystemsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(hitchSystem);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(HitchSystems));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HitchSystems/Delete/{id}")]
        public async Task<IActionResult> HitchSystemsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var hitchSystem = new HitchSystems();
            try
            {
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                hitchSystem = await hitchSystemsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (hitchSystem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(hitchSystem);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="hitchSystemsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HitchSystems/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HitchSystemsDeleteConfirmed(long hitchSystemsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                await hitchSystemsApi.DeleteRecord(hitchSystemsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(HitchSystemsDelete), new { id = hitchSystemsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(HitchSystems));
        }
        #endregion

        #region Hitch Pines
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> HitchPines()
        {
            var hitchPines = new List<HitchPines>();
            try
            {
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                hitchPines = await hitchPinesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(hitchPines);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HitchPines/Edit/{id}")]
        public async Task<IActionResult> HitchPinesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var hitchPine = new HitchPines();
            try
            {
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                hitchPine = await hitchPinesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (hitchPine == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(hitchPine);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="hitchPines">The body styles.</param>
        /// <returns>Save changes for hitchPine</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HitchPines/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HitchPinesEdit(long id, [FromForm] HitchPines hitchPine)
        {
            if (id != hitchPine.HitchPinesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                await hitchPinesApi.UpdateRecord(id, hitchPine);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(HitchPinesEdit), hitchPine);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(HitchPines));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HitchPines/Create")]
        public IActionResult HitchPinesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="hitchPine">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HitchPines/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HitchPinesCreate([FromForm] HitchPines hitchPine)
        {
            var hitchPines = new HitchPines();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                hitchPines = await hitchPinesApi.CreateRecord(hitchPine);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (hitchPines.HitchPinesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(hitchPine);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(HitchPines));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/HitchPines/Delete/{id}")]
        public async Task<IActionResult> HitchPinesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var hitchPine = new HitchPines();
            try
            {
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                hitchPine = await hitchPinesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (hitchPine == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(hitchPine);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="hitchPinesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/HitchPines/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HitchPinesDeleteConfirmed(long hitchPinesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                await hitchPinesApi.DeleteRecord(hitchPinesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(HitchPinesDelete), new { id = hitchPinesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(HitchPines));
        }
        #endregion

        #region Chamber Dimensions
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ChamberDimensions()
        {
            var chamberDimensions = new List<ChamberDimensions>();
            try
            {
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                chamberDimensions = await chamberDimensionsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(chamberDimensions);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ChamberDimensions/Edit/{id}")]
        public async Task<IActionResult> ChamberDimensionsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var chamberDimension = new ChamberDimensions();
            try
            {
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                chamberDimension = await chamberDimensionsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (chamberDimension == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(chamberDimension);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="chamberDimensions">The body styles.</param>
        /// <returns>Save changes for chamberDimension</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ChamberDimensions/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChamberDimensionsEdit(long id, [FromForm] ChamberDimensions chamberDimension)
        {
            if (id != chamberDimension.ChamberDimensionsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                await chamberDimensionsApi.UpdateRecord(id, chamberDimension);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ChamberDimensionsEdit), chamberDimension);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(ChamberDimensions));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ChamberDimensions/Create")]
        public IActionResult ChamberDimensionsCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="chamberDimension">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ChamberDimensions/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChamberDimensionsCreate([FromForm] ChamberDimensions chamberDimension)
        {
            var chamberDimensions = new ChamberDimensions();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                chamberDimensions = await chamberDimensionsApi.CreateRecord(chamberDimension);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (chamberDimensions.ChamberDimensionsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(chamberDimension);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(ChamberDimensions));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ChamberDimensions/Delete/{id}")]
        public async Task<IActionResult> ChamberDimensionsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var chamberDimension = new ChamberDimensions();
            try
            {
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                chamberDimension = await chamberDimensionsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (chamberDimension == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(chamberDimension);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="chamberDimensionsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ChamberDimensions/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChamberDimensionsDeleteConfirmed(long chamberDimensionsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                await chamberDimensionsApi.DeleteRecord(chamberDimensionsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ChamberDimensionsDelete), new { id = chamberDimensionsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(ChamberDimensions));
        }
        #endregion

        #region Configuration Types
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ConfigurationTypes()
        {
            var configurationTypes = new List<ConfigurationTypes>();
            try
            {
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                configurationTypes = await configurationTypesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(configurationTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ConfigurationTypes/Edit/{id}")]
        public async Task<IActionResult> ConfigurationTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var configurationType = new ConfigurationTypes();
            try
            {
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                configurationType = await configurationTypesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (configurationType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(configurationType);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="configurationTypes">The body styles.</param>
        /// <returns>Save changes for configurationType</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ConfigurationTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigurationTypesEdit(long id, [FromForm] ConfigurationTypes configurationType)
        {
            if (id != configurationType.ConfigurationTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                await configurationTypesApi.UpdateRecord(id, configurationType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ConfigurationTypesEdit), configurationType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(ConfigurationTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ConfigurationTypes/Create")]
        public IActionResult ConfigurationTypesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="configurationType">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ConfigurationTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigurationTypesCreate([FromForm] ConfigurationTypes configurationType)
        {
            var configurationTypes = new ConfigurationTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                configurationTypes = await configurationTypesApi.CreateRecord(configurationType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (configurationTypes.ConfigurationTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(configurationType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(ConfigurationTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ConfigurationTypes/Delete/{id}")]
        public async Task<IActionResult> ConfigurationTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var configurationType = new ConfigurationTypes();
            try
            {
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                configurationType = await configurationTypesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (configurationType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(configurationType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="configurationTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ConfigurationTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfigurationTypesDeleteConfirmed(long configurationTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                await configurationTypesApi.DeleteRecord(configurationTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ConfigurationTypesDelete), new { id = configurationTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(ConfigurationTypes));
        }
        #endregion

        #region Conditioning Systems
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ConditioningSystems()
        {
            var conditioningSystems = new List<ConditioningSystems>();
            try
            {
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                conditioningSystems = await conditioningSystemsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(conditioningSystems);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ConditioningSystems/Edit/{id}")]
        public async Task<IActionResult> ConditioningSystemsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var conditioningSystem = new ConditioningSystems();
            try
            {
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                conditioningSystem = await conditioningSystemsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (conditioningSystem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(conditioningSystem);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="conditioningSystems">The body styles.</param>
        /// <returns>Save changes for conditioningSystem</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ConditioningSystems/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConditioningSystemsEdit(long id, [FromForm] ConditioningSystems conditioningSystem)
        {
            if (id != conditioningSystem.ConditioningSystemsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                await conditioningSystemsApi.UpdateRecord(id, conditioningSystem);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ConditioningSystemsEdit), conditioningSystem);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(ConditioningSystems));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ConditioningSystems/Create")]
        public IActionResult ConditioningSystemsCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="conditioningSystem">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ConditioningSystems/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConditioningSystemsCreate([FromForm] ConditioningSystems conditioningSystem)
        {
            var conditioningSystems = new ConditioningSystems();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                conditioningSystems = await conditioningSystemsApi.CreateRecord(conditioningSystem);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (conditioningSystems.ConditioningSystemsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(conditioningSystem);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(ConditioningSystems));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ConditioningSystems/Delete/{id}")]
        public async Task<IActionResult> ConditioningSystemsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var conditioningSystem = new ConditioningSystems();
            try
            {
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                conditioningSystem = await conditioningSystemsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (conditioningSystem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(conditioningSystem);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="conditioningSystemsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ConditioningSystems/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConditioningSystemsDeleteConfirmed(long conditioningSystemsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                await conditioningSystemsApi.DeleteRecord(conditioningSystemsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ConditioningSystemsDelete), new { id = conditioningSystemsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(ConditioningSystems));
        }
        #endregion

        #region Blade Types
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> BladeTypes()
        {
            var bladeTypes = new List<BladeTypes>();
            try
            {
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                bladeTypes = await bladeTypesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(bladeTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/BladeTypes/Edit/{id}")]
        public async Task<IActionResult> BladeTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bladeType = new BladeTypes();
            try
            {
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                bladeType = await bladeTypesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bladeType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bladeType);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bladeTypes">The body styles.</param>
        /// <returns>Save changes for bladeType</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/BladeTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BladeTypesEdit(long id, [FromForm] BladeTypes bladeType)
        {
            if (id != bladeType.BladeTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                await bladeTypesApi.UpdateRecord(id, bladeType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(BladeTypesEdit), bladeType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(BladeTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/BladeTypes/Create")]
        public IActionResult BladeTypesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="bladeType">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/BladeTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BladeTypesCreate([FromForm] BladeTypes bladeType)
        {
            var bladeTypes = new BladeTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                bladeTypes = await bladeTypesApi.CreateRecord(bladeType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bladeTypes.BladeTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(bladeType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(BladeTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/BladeTypes/Delete/{id}")]
        public async Task<IActionResult> BladeTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var bladeType = new BladeTypes();
            try
            {
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                bladeType = await bladeTypesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (bladeType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(bladeType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="bladeTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/BladeTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BladeTypesDeleteConfirmed(long bladeTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                await bladeTypesApi.DeleteRecord(bladeTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(BladeTypesDelete), new { id = bladeTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(BladeTypes));
        }
        #endregion

        #region Turning Radius Types
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> TurningRadiusTypes()
        {
            var turningRadiusTypes = new List<TurningRadiusTypes>();
            try
            {
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);
                turningRadiusTypes = await turningRadiusTypesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(turningRadiusTypes);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/TurningRadiusTypes/Edit/{id}")]
        public async Task<IActionResult> TurningRadiusTypesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var turningRadiusType = new TurningRadiusTypes();
            try
            {
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);
                turningRadiusType = await turningRadiusTypesApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (turningRadiusType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(turningRadiusType);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="turningRadiusTypes">The body styles.</param>
        /// <returns>Save changes for turningRadiusType</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/TurningRadiusTypes/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TurningRadiusTypesEdit(long id, [FromForm] TurningRadiusTypes turningRadiusType)
        {
            if (id != turningRadiusType.TurningRadiusTypesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);
                await turningRadiusTypesApi.UpdateRecord(id, turningRadiusType);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(TurningRadiusTypesEdit), turningRadiusType);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(TurningRadiusTypes));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/TurningRadiusTypes/Create")]
        public IActionResult TurningRadiusTypesCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="turningRadiusType">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/TurningRadiusTypes/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TurningRadiusTypesCreate([FromForm] TurningRadiusTypes turningRadiusType)
        {
            var turningRadiusTypes = new TurningRadiusTypes();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);
                turningRadiusTypes = await turningRadiusTypesApi.CreateRecord(turningRadiusType);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (turningRadiusTypes.TurningRadiusTypesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(turningRadiusType);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(TurningRadiusTypes));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/TurningRadiusTypes/Delete/{id}")]
        public async Task<IActionResult> TurningRadiusTypesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var turningRadiusType = new TurningRadiusTypes();
            try
            {
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);
                turningRadiusType = await turningRadiusTypesApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (turningRadiusType == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(turningRadiusType);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="turningRadiusTypesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/TurningRadiusTypes/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TurningRadiusTypesDeleteConfirmed(long turningRadiusTypesId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);
                await turningRadiusTypesApi.DeleteRecord(turningRadiusTypesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(TurningRadiusTypesDelete), new { id = turningRadiusTypesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(TurningRadiusTypes));
        }
        #endregion
        #endregion

        #endregion Main Parameters

        #region Products Section
        #region Products
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = new List<Products>();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                products = await productsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(products);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="products">The body styles.</param>
        /// <returns>Save changes for product</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Products/More/{id}")]
        public async Task<IActionResult> ProductsMore(long id)
        {
            var products = new Products();
            var documentsForProducts = new List<ProductDocuments>();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                var productDocumentsApi = new ProductDocumentsApi(this._httpClientInstance);
                products = await productsApi.GetRecord(id);
                documentsForProducts = await productDocumentsApi.GetRecordsForProduct(id);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkProductDocuments = documentsForProducts;
            return this.View(products);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Products/Edit/{id}")]
        public async Task<IActionResult> ProductsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }
            this.SetCustomFonts();
            var bodyStyles = new List<BodyStyles>();
            var categories = new List<Categories>();
            var driveTrains = new List<DriveTrains>();
            var engineTypes = new List<EngineTypes>();
            var epaClasses = new List<EpaClasses>();
            var makes = new List<Makes>();
            var models = new List<Models>();
            var transmissions = new List<Transmissions>();
            var products = new Products();
            var productsViewModel = new ProductsViewModel();

            ////Massey
            var aspirationTypes = new List<AspirationTypes>();
            var rearTiresTypes = new List<RearTiresTypes>();
            var frontTiresTypes = new List<FrontTiresTypes>();
            var separationSystems = new List<SeparationSystems>();
            var hitchSystems = new List<HitchSystems>();
            var hitchPines = new List<HitchPines>();
            var chamberDimensions = new List<ChamberDimensions>();
            var configurationTypes = new List<ConfigurationTypes>();
            var conditioningSystems = new List<ConditioningSystems>();
            var bladeTypes = new List<BladeTypes>();
            var turningRadiusTypes = new List<TurningRadiusTypes>();
            try
            {
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                var engineTypesApi = new EngineTypesApi(this._httpClientInstance);
                var epaClassesApi = new EpaClassesApi(this._httpClientInstance);
                var makesApi = new MakesApi(this._httpClientInstance);
                var modelsApi = new ModelsApi(this._httpClientInstance);
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);
                var productsApi = new ProductsApi(this._httpClientInstance);

                ////Massey
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);

                products = await productsApi.GetRecord(id ?? 0);
                bodyStyles = await bodyStylesApi.GetAllRecords();
                categories = await categoriesApi.GetAllRecords();
                driveTrains = await driveTrainsApi.GetAllRecords();
                engineTypes = await engineTypesApi.GetAllRecords();
                epaClasses = await epaClassesApi.GetAllRecords();
                makes = await makesApi.GetAllRecords();
                transmissions = await transmissionsApi.GetAllRecords();
                var make = makes.FirstOrDefault(x => x.MakesId == products.FkMakesId);
                models = await modelsApi.GetAllRecords();
                models = models.Where(x => x.FkMakesId == make.MakesId).ToList();

                ////Massey
                aspirationTypes = await aspirationTypesApi.GetAllRecords();
                rearTiresTypes = await rearTiresTypesApi.GetAllRecords();
                frontTiresTypes = await frontTiresTypesApi.GetAllRecords();
                separationSystems = await separationSystemsApi.GetAllRecords();
                hitchSystems = await hitchSystemsApi.GetAllRecords();
                hitchPines = await hitchPinesApi.GetAllRecords();
                chamberDimensions = await chamberDimensionsApi.GetAllRecords();
                configurationTypes = await configurationTypesApi.GetAllRecords();
                conditioningSystems = await conditioningSystemsApi.GetAllRecords();
                bladeTypes = await bladeTypesApi.GetAllRecords();
                turningRadiusTypes = await turningRadiusTypesApi.GetAllRecords();

                productsViewModel.ProductsId = products.ProductsId;
                productsViewModel.Cost = products.Cost;
                productsViewModel.Description = products.Description;
                productsViewModel.FkCategoriesId = products.FkCategoriesId;
                productsViewModel.IsFeatured = products.IsFeatured;
                productsViewModel.LongDescription = products.LongDescription;
                productsViewModel.QuantityInStock = products.QuantityInStock;
                productsViewModel.SalesPrice = products.SalesPrice;
                productsViewModel.Sku = products.Sku;
                productsViewModel.Upc = products.Upc;
                productsViewModel.Cupix360Url = products.Cupix360Url;
                productsViewModel.BaseWeight = products.BaseWeight;
                productsViewModel.FkBodyStylesId = products.FkBodyStylesId;
                productsViewModel.FkDriveTrainsId = products.FkDriveTrainsId;
                productsViewModel.FkEngineTypesId = products.FkEngineTypesId;
                productsViewModel.FkEpaClassesId = products.FkEpaClassesId;
                productsViewModel.FkMakesId = products.FkMakesId;
                productsViewModel.FkModelsId = products.FkModelsId;
                productsViewModel.FkTransmissionsId = products.FkTransmissionsId;
                productsViewModel.GasMileage = products.GasMileage;
                productsViewModel.Horsepower = products.Horsepower;
                productsViewModel.Msrp = products.Msrp;
                productsViewModel.PassengerDoors = products.PassengerDoors;
                productsViewModel.Passengers = products.Passengers;
                productsViewModel.Picture360Quantity = products.Picture360Quantity;
                productsViewModel.Picture360Url = products.Picture360Url;
                productsViewModel.ProductName = products.ProductName;
                productsViewModel.QuantityInStock = products.QuantityInStock;
                productsViewModel.SalesPrice = products.SalesPrice;
                productsViewModel.Trim = products.Trim;
                productsViewModel.Year = products.Year;
                productsViewModel.YoutubeUrl = products.YoutubeUrl;
                productsViewModel.AllowShow = products.AllowShow;
                productsViewModel.PictureUrl = products.PictureUrl;

                ////Massey
                productsViewModel.Cylinders = products.Cylinders;
                productsViewModel.LoadCapacity = products.LoadCapacity;
                productsViewModel.SingleWeight = products.SingleWeight;
                productsViewModel.Productivity = products.Productivity;
                productsViewModel.BaleLength = products.BaleLength;
                productsViewModel.Speed = products.Speed;
                productsViewModel.RecommendedPower = products.RecommendedPower;
                productsViewModel.WorkingWidth = products.WorkingWidth;
                productsViewModel.NumberOfLines = products.NumberOfLines;
                productsViewModel.NumberOfDiscs = products.NumberOfDiscs;
                productsViewModel.InputRotation = products.InputRotation;
                productsViewModel.StandardCover = products.StandardCover;
                productsViewModel.CuttingWidth = products.CuttingWidth;
                productsViewModel.FkAspirationTypesId = products.FkAspirationTypesId;
                productsViewModel.FkRearTiresTypesId = products.FkRearTiresTypesId;
                productsViewModel.FkFrontTiresTypesId = products.FkFrontTiresTypesId;
                productsViewModel.FkSeparationSystemsId = products.FkSeparationSystemsId;
                productsViewModel.FkHitchSystemsId = products.FkHitchSystemsId;
                productsViewModel.FkHitchPinesId = products.FkHitchPinesId;
                productsViewModel.FkChamberDimensionsId = products.FkChamberDimensionsId;
                productsViewModel.FkConfigurationTypesId = products.FkConfigurationTypesId;
                productsViewModel.FkConditioningSystemsId = products.FkConditioningSystemsId;
                productsViewModel.FkBladesTypesId = products.FkBladesTypesId;
                productsViewModel.FkTurningRadiusTypesId = products.FkTurningRadiusTypesId;
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (products == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkBodyStylesId = new SelectList(bodyStyles, "BodyStylesId", "BodyStyleName", products.FkBodyStylesId);
            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName", products.FkCategoriesId);
            ViewBag.FkDriveTrainsId = new SelectList(driveTrains, "DriveTrainsId", "DriveTrainName", products.FkDriveTrainsId);
            ViewBag.FkEngineTypesId = new SelectList(engineTypes, "EngineTypesId", "EngineTypeName", products.FkEngineTypesId);
            ViewBag.FkEpaClassesId = new SelectList(epaClasses, "EpaClassesId", "EpaClassName", products.FkEpaClassesId);
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", products.FkMakesId);
            ViewBag.FkModelsId = new SelectList(models, "ModelsId", "ModelName", products.FkModelsId);
            ViewBag.FkTransmissionsId = new SelectList(transmissions, "TransmissionsId", "TransmissionName", products.FkTransmissionsId);

            ////Massey
            ViewBag.FkAspirationTypesId = new SelectList(aspirationTypes, "AspirationTypesId", "AspirationTypeName", products.FkAspirationTypesId);
            ViewBag.FkRearTiresTypesId = new SelectList(rearTiresTypes, "RearTiresTypesId", "RearTiresTypeName", products.FkRearTiresTypesId);
            ViewBag.FkFrontTiresTypesId = new SelectList(frontTiresTypes, "FrontTiresTypesId", "FrontTiresTypeName", products.FkFrontTiresTypesId);
            ViewBag.FkSeparationSystemsId = new SelectList(separationSystems, "SeparationSystemsId", "SeparationSystemName", products.FkSeparationSystemsId);
            ViewBag.FkHitchSystemsId = new SelectList(hitchSystems, "HitchSystemsId", "HitchSystemName", products.FkHitchSystemsId);
            ViewBag.FkHitchPinesId = new SelectList(hitchPines, "HitchPinesId", "HitchPinName", products.FkHitchPinesId);
            ViewBag.FkChamberDimensionsId = new SelectList(chamberDimensions, "ChamberDimensionsId", "ChamberDimensionName", products.FkChamberDimensionsId);
            ViewBag.FkConfigurationTypesId = new SelectList(configurationTypes, "ConfigurationTypesId", "ConfigurationTypeName", products.FkConfigurationTypesId);
            ViewBag.FkConditioningSystemsId = new SelectList(conditioningSystems, "ConditioningSystemsId", "ConditioningSystemName", products.FkConditioningSystemsId);
            ViewBag.FkBladesTypesId = new SelectList(bladeTypes, "BladeTypesId", "BladeTypeName", products.FkBladesTypesId);
            ViewBag.FkTurningRadiusTypesId = new SelectList(turningRadiusTypes, "TurningRadiusTypesId", "TurningRadiusTypeName", products.FkTurningRadiusTypesId);

            return this.View(productsViewModel);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="products">The body styles.</param>
        /// <returns>Save changes for product</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Products/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductsEdit(long id, [FromForm] ProductsViewModel productViewModel)
        {
            this.SetCustomFonts();
            var bodyStyles = new List<BodyStyles>();
            var categories = new List<Categories>();
            var driveTrains = new List<DriveTrains>();
            var engineTypes = new List<EngineTypes>();
            var epaClasses = new List<EpaClasses>();
            var makes = new List<Makes>();
            var models = new List<Models>();
            var transmissions = new List<Transmissions>();
            var products = new Products();

            ////Massey
            var aspirationTypes = new List<AspirationTypes>();
            var rearTiresTypes = new List<RearTiresTypes>();
            var frontTiresTypes = new List<FrontTiresTypes>();
            var separationSystems = new List<SeparationSystems>();
            var hitchSystems = new List<HitchSystems>();
            var hitchPines = new List<HitchPines>();
            var chamberDimensions = new List<ChamberDimensions>();
            var configurationTypes = new List<ConfigurationTypes>();
            var conditioningSystems = new List<ConditioningSystems>();
            var bladeTypes = new List<BladeTypes>();
            var turningRadiusTypes = new List<TurningRadiusTypes>();

            var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
            var categoriesApi = new CategoriesApi(this._httpClientInstance);
            var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
            var engineTypesApi = new EngineTypesApi(this._httpClientInstance);
            var epaClassesApi = new EpaClassesApi(this._httpClientInstance);
            var makesApi = new MakesApi(this._httpClientInstance);
            var modelsApi = new ModelsApi(this._httpClientInstance);
            var transmissionsApi = new TransmissionsApi(this._httpClientInstance);

            ////Massey
            var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
            var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
            var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
            var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
            var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
            var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
            var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
            var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
            var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
            var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
            var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);

            this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
            var productsApi = new ProductsApi(this._httpClientInstance);
            bodyStyles = await bodyStylesApi.GetAllRecords();
            categories = await categoriesApi.GetAllRecords();
            driveTrains = await driveTrainsApi.GetAllRecords();
            engineTypes = await engineTypesApi.GetAllRecords();
            epaClasses = await epaClassesApi.GetAllRecords();
            makes = await makesApi.GetAllRecords();
            transmissions = await transmissionsApi.GetAllRecords();
            var make = makes.FirstOrDefault(x => x.MakesId == productViewModel.FkMakesId);
            models = await modelsApi.GetAllRecords();
            models = models.Where(x => x.FkMakesId == make.MakesId).ToList();

            ////Massey
            aspirationTypes = await aspirationTypesApi.GetAllRecords();
            rearTiresTypes = await rearTiresTypesApi.GetAllRecords();
            frontTiresTypes = await frontTiresTypesApi.GetAllRecords();
            separationSystems = await separationSystemsApi.GetAllRecords();
            hitchSystems = await hitchSystemsApi.GetAllRecords();
            hitchPines = await hitchPinesApi.GetAllRecords();
            chamberDimensions = await chamberDimensionsApi.GetAllRecords();
            configurationTypes = await configurationTypesApi.GetAllRecords();
            conditioningSystems = await conditioningSystemsApi.GetAllRecords();
            bladeTypes = await bladeTypesApi.GetAllRecords();
            turningRadiusTypes = await turningRadiusTypesApi.GetAllRecords();


            ViewBag.FkBodyStylesId = new SelectList(bodyStyles, "BodyStylesId", "BodyStyleName", productViewModel.FkBodyStylesId);
            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName", productViewModel.FkCategoriesId);
            ViewBag.FkDriveTrainsId = new SelectList(driveTrains, "DriveTrainsId", "DriveTrainName", productViewModel.FkDriveTrainsId);
            ViewBag.FkEngineTypesId = new SelectList(engineTypes, "EngineTypesId", "EngineTypeName", productViewModel.FkEngineTypesId);
            ViewBag.FkEpaClassesId = new SelectList(epaClasses, "EpaClassesId", "EpaClassName", productViewModel.FkEpaClassesId);
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", productViewModel.FkMakesId);
            ViewBag.FkModelsId = new SelectList(models, "ModelsId", "ModelName", productViewModel.FkModelsId);
            ViewBag.FkTransmissionsId = new SelectList(transmissions, "TransmissionsId", "TransmissionName", productViewModel.FkTransmissionsId);

            ////Massey
            ViewBag.FkAspirationTypesId = new SelectList(aspirationTypes, "AspirationTypesId", "AspirationTypeName", productViewModel.FkAspirationTypesId);
            ViewBag.FkRearTiresTypesId = new SelectList(rearTiresTypes, "RearTiresTypesId", "RearTiresTypeName", productViewModel.FkRearTiresTypesId);
            ViewBag.FkFrontTiresTypesId = new SelectList(frontTiresTypes, "FrontTiresTypesId", "FrontTiresTypeName", productViewModel.FkFrontTiresTypesId);
            ViewBag.FkSeparationSystemsId = new SelectList(separationSystems, "SeparationSystemsId", "SeparationSystemName", productViewModel.FkSeparationSystemsId);
            ViewBag.FkHitchSystemsId = new SelectList(hitchSystems, "HitchSystemsId", "HitchSystemName", productViewModel.FkHitchSystemsId);
            ViewBag.FkHitchPinesId = new SelectList(hitchPines, "HitchPinesId", "HitchPinName", productViewModel.FkHitchPinesId);
            ViewBag.FkChamberDimensionsId = new SelectList(chamberDimensions, "ChamberDimensionsId", "ChamberDimensionName", productViewModel.FkChamberDimensionsId);
            ViewBag.FkConfigurationTypesId = new SelectList(configurationTypes, "ConfigurationTypesId", "ConfigurationTypeName", productViewModel.FkConfigurationTypesId);
            ViewBag.FkConditioningSystemsId = new SelectList(conditioningSystems, "ConditioningSystemsId", "ConditioningSystemName", productViewModel.FkConditioningSystemsId);
            ViewBag.FkBladesTypesId = new SelectList(bladeTypes, "BladeTypesId", "BladeTypeName", productViewModel.FkBladesTypesId);
            ViewBag.FkTurningRadiusTypesId = new SelectList(turningRadiusTypes, "TurningRadiusTypesId", "TurningRadiusTypeName", productViewModel.FkTurningRadiusTypesId);

            if (!ModelState.IsValid)
            {
                return this.View(nameof(ProductsEdit), productViewModel);
            }

            if (id != productViewModel.ProductsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                var product = new Products
                {
                    Cost = productViewModel.Cost ?? 0,
                    Description = productViewModel.Description,
                    FkCategoriesId = productViewModel.FkCategoriesId,
                    IsFeatured = productViewModel.IsFeatured,
                    LongDescription = productViewModel.LongDescription,
                    QuantityInStock = productViewModel.QuantityInStock,
                    SalesPrice = productViewModel.SalesPrice ?? 0,
                    Sku = productViewModel.Sku,
                    Upc = productViewModel.Upc,
                    Cupix360Url = productViewModel.Cupix360Url,
                    BaseWeight = productViewModel.BaseWeight,
                    FkBodyStylesId = productViewModel.FkBodyStylesId,
                    FkDriveTrainsId = productViewModel.FkDriveTrainsId,
                    FkEngineTypesId = productViewModel.FkEngineTypesId,
                    FkEpaClassesId = productViewModel.FkEpaClassesId,
                    FkMakesId = productViewModel.FkMakesId,
                    FkModelsId = productViewModel.FkModelsId,
                    FkTransmissionsId = productViewModel.FkTransmissionsId,
                    Horsepower = productViewModel.Horsepower,
                    Msrp = productViewModel.Msrp ?? 0,
                    PassengerDoors = productViewModel.PassengerDoors,
                    Passengers = productViewModel.Passengers,
                    Picture360Quantity = productViewModel.Picture360Quantity,
                    Picture360Url = productViewModel.Picture360Url,
                    ProductName = productViewModel.ProductName,
                    Trim = productViewModel.Trim,
                    Year = productViewModel.Year,
                    YoutubeUrl = productViewModel.YoutubeUrl,
                    AllowShow = productViewModel.AllowShow,
                    PictureUrl = productViewModel.PictureUrl,
                    GasMileage = productViewModel.GasMileage,
                    ProductsId = productViewModel.ProductsId,
                    ////Massey
                    Cylinders = productViewModel.Cylinders,
                    LoadCapacity = productViewModel.LoadCapacity,
                    SingleWeight = productViewModel.SingleWeight,
                    Productivity = productViewModel.Productivity,
                    BaleLength = productViewModel.BaleLength,
                    Speed = productViewModel.Speed,
                    RecommendedPower = productViewModel.RecommendedPower,
                    WorkingWidth = productViewModel.WorkingWidth,
                    NumberOfLines = productViewModel.NumberOfLines,
                    NumberOfDiscs = productViewModel.NumberOfDiscs,
                    InputRotation = productViewModel.InputRotation,
                    StandardCover = productViewModel.StandardCover,
                    CuttingWidth = productViewModel.CuttingWidth,
                    FkAspirationTypesId = productViewModel.FkAspirationTypesId,
                    FkRearTiresTypesId = productViewModel.FkRearTiresTypesId,
                    FkFrontTiresTypesId = productViewModel.FkFrontTiresTypesId,
                    FkSeparationSystemsId = productViewModel.FkSeparationSystemsId,
                    FkHitchSystemsId = productViewModel.FkHitchSystemsId,
                    FkHitchPinesId = productViewModel.FkHitchPinesId,
                    FkChamberDimensionsId = productViewModel.FkChamberDimensionsId,
                    FkConfigurationTypesId = productViewModel.FkConfigurationTypesId,
                    FkConditioningSystemsId = productViewModel.FkConditioningSystemsId,
                    FkBladesTypesId = productViewModel.FkBladesTypesId,
                    FkTurningRadiusTypesId = productViewModel.FkTurningRadiusTypesId
                };

                var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
                var makeName = makes.FirstOrDefault(x => x.MakesId == productViewModel.FkMakesId).MakeName.ToLower().Replace(" ", "_");
                var modelName = models.FirstOrDefault(x => x.ModelsId == productViewModel.FkModelsId).ModelName.ToLower().Replace(" ", "_");
                var productName = productViewModel.ProductName.ToLower().Trim().Replace(" ", "_");
                productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);

                if (productViewModel.CoverPicture != null && productViewModel.CoverPicture.Length > 0)
                {
                    ////Deletes picture
                    if (!string.IsNullOrEmpty(product.PictureUrl))
                    {
                        var fullPathDelete = Path.Combine(this._host.WebRootPath, product.PictureUrl);
                        if (System.IO.File.Exists(fullPathDelete))
                        {
                            System.IO.File.Delete(fullPathDelete);
                        }
                    }

                    var subDirectory = new List<string> { makeName, "products", "pictures", modelName, productName, productViewModel.Year.ToString() };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    subDirectory.Add(productViewModel.CoverPicture.FileName);
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    product.PictureUrl = partialPath;

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await productViewModel.CoverPicture.CopyToAsync(fileStream);
                }

                if (productViewModel.Pictures360 != null && productViewModel.Pictures360.Count > 0)
                {
                    ////Deletes 360 pictures
                    if (!string.IsNullOrEmpty(product.Picture360Url))
                    {
                        var fullPathDelete = Path.Combine(this._host.WebRootPath, product.Picture360Url);
                        if (Directory.Exists(fullPathDelete))
                        {
                            var files = Directory.GetFiles(fullPathDelete);
                            foreach (var item in files)
                            {
                                System.IO.File.Delete(Path.Combine(fullPathDelete, item));
                            }
                        }
                    }

                    var subDirectory = new List<string> { makeName, "products", "360_pictures", productViewModel.Year.ToString(), $"{modelName}360" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    productViewModel.Picture360Quantity = productViewModel.Picture360Quantity != productViewModel.Pictures360.Count ? productViewModel.Pictures360.Count : productViewModel.Picture360Quantity;
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath360 = Path.Combine(this._host.WebRootPath, partialPath);
                    product.Picture360Url = partialPath;
                    if (!System.IO.File.Exists(fullPath360))
                    {
                        foreach (var item in productViewModel.Pictures360)
                        {
                            using var fileStream = new FileStream(Path.Combine(fullPath360, item.FileName), FileMode.Create);
                            await item.CopyToAsync(fileStream);
                        }
                    }
                }

                await productsApi.UpdateRecord(id, product);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ProductsEdit), productViewModel);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Products));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Products/Create")]
        public async Task<IActionResult> ProductsCreate()
        {
            this.SetCustomFonts();
            var bodyStyles = new List<BodyStyles>();
            var categories = new List<Categories>();
            var driveTrains = new List<DriveTrains>();
            var engineTypes = new List<EngineTypes>();
            var epaClasses = new List<EpaClasses>();
            var makes = new List<Makes>();
            var models = new List<Models>();
            var transmissions = new List<Transmissions>();

            ////Massey
            var aspirationTypes = new List<AspirationTypes>();
            var rearTiresTypes = new List<RearTiresTypes>();
            var frontTiresTypes = new List<FrontTiresTypes>();
            var separationSystems = new List<SeparationSystems>();
            var hitchSystems = new List<HitchSystems>();
            var hitchPines = new List<HitchPines>();
            var chamberDimensions = new List<ChamberDimensions>();
            var configurationTypes = new List<ConfigurationTypes>();
            var conditioningSystems = new List<ConditioningSystems>();
            var bladeTypes = new List<BladeTypes>();
            var turningRadiusTypes = new List<TurningRadiusTypes>();

            try
            {
                var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
                var engineTypesApi = new EngineTypesApi(this._httpClientInstance);
                var epaClassesApi = new EpaClassesApi(this._httpClientInstance);
                var makesApi = new MakesApi(this._httpClientInstance);
                var modelsApi = new ModelsApi(this._httpClientInstance);
                var transmissionsApi = new TransmissionsApi(this._httpClientInstance);

                ////Massey
                var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
                var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
                var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
                var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
                var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
                var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
                var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
                var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
                var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
                var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
                var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);

                bodyStyles = await bodyStylesApi.GetAllRecords();
                categories = await categoriesApi.GetAllRecords();
                driveTrains = await driveTrainsApi.GetAllRecords();
                engineTypes = await engineTypesApi.GetAllRecords();
                epaClasses = await epaClassesApi.GetAllRecords();
                makes = await makesApi.GetAllRecords();

                ////Massey
                aspirationTypes = await aspirationTypesApi.GetAllRecords();
                rearTiresTypes = await rearTiresTypesApi.GetAllRecords();
                frontTiresTypes = await frontTiresTypesApi.GetAllRecords();
                separationSystems = await separationSystemsApi.GetAllRecords();
                hitchSystems = await hitchSystemsApi.GetAllRecords();
                hitchPines = await hitchPinesApi.GetAllRecords();
                chamberDimensions = await chamberDimensionsApi.GetAllRecords();
                configurationTypes = await configurationTypesApi.GetAllRecords();
                conditioningSystems = await conditioningSystemsApi.GetAllRecords();
                bladeTypes = await bladeTypesApi.GetAllRecords();
                turningRadiusTypes = await turningRadiusTypesApi.GetAllRecords();

                var makeDefault = makes.FirstOrDefault();
                models = await modelsApi.GetAllRecords();
                models = models.Where(x => x.FkMakesId == makeDefault.MakesId).ToList();
                var model = models.FirstOrDefault();
                transmissions = await transmissionsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkBodyStylesId = new SelectList(bodyStyles, "BodyStylesId", "BodyStyleName");
            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName");
            ViewBag.FkDriveTrainsId = new SelectList(driveTrains, "DriveTrainsId", "DriveTrainName");
            ViewBag.FkEngineTypesId = new SelectList(engineTypes, "EngineTypesId", "EngineTypeName");
            ViewBag.FkEpaClassesId = new SelectList(epaClasses, "EpaClassesId", "EpaClassName");
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
            ViewBag.FkModelsId = new SelectList(models, "ModelsId", "ModelName");
            ViewBag.FkTransmissionsId = new SelectList(transmissions, "TransmissionsId", "TransmissionName");

            ////Massey
            ViewBag.FkAspirationTypesId = new SelectList(aspirationTypes, "AspirationTypesId", "AspirationTypeName");
            ViewBag.FkRearTiresTypesId = new SelectList(rearTiresTypes, "RearTiresTypesId", "RearTiresTypeName");
            ViewBag.FkFrontTiresTypesId = new SelectList(frontTiresTypes, "FrontTiresTypesId", "FrontTiresTypeName");
            ViewBag.FkSeparationSystemsId = new SelectList(separationSystems, "SeparationSystemsId", "SeparationSystemName");
            ViewBag.FkHitchSystemsId = new SelectList(hitchSystems, "HitchSystemsId", "HitchSystemName");
            ViewBag.FkHitchPinesId = new SelectList(hitchPines, "HitchPinesId", "HitchPinName");
            ViewBag.FkChamberDimensionsId = new SelectList(chamberDimensions, "ChamberDimensionsId", "ChamberDimensionName");
            ViewBag.FkConfigurationTypesId = new SelectList(configurationTypes, "ConfigurationTypesId", "ConfigurationTypeName");
            ViewBag.FkConditioningSystemsId = new SelectList(conditioningSystems, "ConditioningSystemsId", "ConditioningSystemName");
            ViewBag.FkBladesTypesId = new SelectList(bladeTypes, "BladeTypesId", "BladeTypeName");
            ViewBag.FkTurningRadiusTypesId = new SelectList(turningRadiusTypes, "TurningRadiusTypesId", "TurningRadiusTypeName");

            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="productViewModel">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Products/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductsCreate([FromForm] ProductsViewModel productViewModel)
        {
            this.SetCustomFonts();
            var bodyStyles = new List<BodyStyles>();
            var categories = new List<Categories>();
            var driveTrains = new List<DriveTrains>();
            var engineTypes = new List<EngineTypes>();
            var epaClasses = new List<EpaClasses>();
            var makes = new List<Makes>();
            var models = new List<Models>();
            var transmissions = new List<Transmissions>();

            ////Massey
            var aspirationTypes = new List<AspirationTypes>();
            var rearTiresTypes = new List<RearTiresTypes>();
            var frontTiresTypes = new List<FrontTiresTypes>();
            var separationSystems = new List<SeparationSystems>();
            var hitchSystems = new List<HitchSystems>();
            var hitchPines = new List<HitchPines>();
            var chamberDimensions = new List<ChamberDimensions>();
            var configurationTypes = new List<ConfigurationTypes>();
            var conditioningSystems = new List<ConditioningSystems>();
            var bladeTypes = new List<BladeTypes>();
            var turningRadiusTypes = new List<TurningRadiusTypes>();

            var bodyStylesApi = new BodyStylesApi(this._httpClientInstance);
            var categoriesApi = new CategoriesApi(this._httpClientInstance);
            var driveTrainsApi = new DriveTrainsApi(this._httpClientInstance);
            var engineTypesApi = new EngineTypesApi(this._httpClientInstance);
            var epaClassesApi = new EpaClassesApi(this._httpClientInstance);
            var makesApi = new MakesApi(this._httpClientInstance);
            var modelsApi = new ModelsApi(this._httpClientInstance);
            var transmissionsApi = new TransmissionsApi(this._httpClientInstance);

            ////Massey
            var aspirationTypesApi = new AspirationTypesApi(this._httpClientInstance);
            var rearTiresTypesApi = new RearTiresTypesApi(this._httpClientInstance);
            var frontTiresTypesApi = new FrontTiresTypesApi(this._httpClientInstance);
            var separationSystemsApi = new SeparationSystemsApi(this._httpClientInstance);
            var hitchSystemsApi = new HitchSystemsApi(this._httpClientInstance);
            var hitchPinesApi = new HitchPinesApi(this._httpClientInstance);
            var chamberDimensionsApi = new ChamberDimensionsApi(this._httpClientInstance);
            var configurationTypesApi = new ConfigurationTypesApi(this._httpClientInstance);
            var conditioningSystemsApi = new ConditioningSystemsApi(this._httpClientInstance);
            var bladeTypesApi = new BladeTypesApi(this._httpClientInstance);
            var turningRadiusTypesApi = new TurningRadiusTypesApi(this._httpClientInstance);

            bodyStyles = await bodyStylesApi.GetAllRecords();
            categories = await categoriesApi.GetAllRecords();
            driveTrains = await driveTrainsApi.GetAllRecords();
            engineTypes = await engineTypesApi.GetAllRecords();
            epaClasses = await epaClassesApi.GetAllRecords();
            makes = await makesApi.GetAllRecords();
            transmissions = await transmissionsApi.GetAllRecords();

            ////Massey
            aspirationTypes = await aspirationTypesApi.GetAllRecords();
            rearTiresTypes = await rearTiresTypesApi.GetAllRecords();
            frontTiresTypes = await frontTiresTypesApi.GetAllRecords();
            separationSystems = await separationSystemsApi.GetAllRecords();
            hitchSystems = await hitchSystemsApi.GetAllRecords();
            hitchPines = await hitchPinesApi.GetAllRecords();
            chamberDimensions = await chamberDimensionsApi.GetAllRecords();
            configurationTypes = await configurationTypesApi.GetAllRecords();
            conditioningSystems = await conditioningSystemsApi.GetAllRecords();
            bladeTypes = await bladeTypesApi.GetAllRecords();
            turningRadiusTypes = await turningRadiusTypesApi.GetAllRecords();

            var makeDefault = makes.FirstOrDefault();
            models = await modelsApi.GetAllRecords();
            models = models.Where(x => x.FkMakesId == (productViewModel.FkMakesId == 0 ? makeDefault.MakesId : productViewModel.FkMakesId)).ToList();

            ViewBag.FkBodyStylesId = new SelectList(bodyStyles, "BodyStylesId", "BodyStyleName", productViewModel.FkBodyStylesId);
            ViewBag.FkCategoriesId = new SelectList(categories, "CategoriesId", "CategoryName", productViewModel.FkCategoriesId);
            ViewBag.FkDriveTrainsId = new SelectList(driveTrains, "DriveTrainsId", "DriveTrainName", productViewModel.FkDriveTrainsId);
            ViewBag.FkEngineTypesId = new SelectList(engineTypes, "EngineTypesId", "EngineTypeName", productViewModel.FkEngineTypesId);
            ViewBag.FkEpaClassesId = new SelectList(epaClasses, "EpaClassesId", "EpaClassName", productViewModel.FkEpaClassesId);
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", productViewModel.FkMakesId);
            ViewBag.FkModelsId = new SelectList(models, "ModelsId", "ModelName", productViewModel.FkModelsId);
            ViewBag.FkTransmissionsId = new SelectList(transmissions, "TransmissionsId", "TransmissionName", productViewModel.FkTransmissionsId);

            ////Massey
            ViewBag.FkAspirationTypesId = new SelectList(aspirationTypes, "AspirationTypesId", "AspirationTypeName", productViewModel.FkAspirationTypesId);
            ViewBag.FkRearTiresTypesId = new SelectList(rearTiresTypes, "RearTiresTypesId", "RearTiresTypeName", productViewModel.FkRearTiresTypesId);
            ViewBag.FkFrontTiresTypesId = new SelectList(frontTiresTypes, "FrontTiresTypesId", "FrontTiresTypeName", productViewModel.FkFrontTiresTypesId);
            ViewBag.FkSeparationSystemsId = new SelectList(separationSystems, "SeparationSystemsId", "SeparationSystemName", productViewModel.FkSeparationSystemsId);
            ViewBag.FkHitchSystemsId = new SelectList(hitchSystems, "HitchSystemsId", "HitchSystemName", productViewModel.FkHitchSystemsId);
            ViewBag.FkHitchPinesId = new SelectList(hitchPines, "HitchPinesId", "HitchPinName", productViewModel.FkHitchPinesId);
            ViewBag.FkChamberDimensionsId = new SelectList(chamberDimensions, "ChamberDimensionsId", "ChamberDimensionName", productViewModel.FkChamberDimensionsId);
            ViewBag.FkConfigurationTypesId = new SelectList(configurationTypes, "ConfigurationTypesId", "ConfigurationTypeName", productViewModel.FkConfigurationTypesId);
            ViewBag.FkConditioningSystemsId = new SelectList(conditioningSystems, "ConditioningSystemsId", "ConditioningSystemName", productViewModel.FkConditioningSystemsId);
            ViewBag.FkBladesTypesId = new SelectList(bladeTypes, "BladeTypesId", "BladeTypeName", productViewModel.FkBladesTypesId);
            ViewBag.FkTurningRadiusTypesId = new SelectList(turningRadiusTypes, "TurningRadiusTypesId", "TurningRadiusTypeName", productViewModel.FkTurningRadiusTypesId);

            ModelState["ProductsId"].ValidationState = ModelValidationState.Valid;
            ModelState["ProductsId"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return this.View(productViewModel);
            }

            var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
            var makeName = makes.FirstOrDefault(x => x.MakesId == productViewModel.FkMakesId).MakeName.ToLower().Replace(" ", "_");
            var modelName = models.FirstOrDefault(x => x.ModelsId == productViewModel.FkModelsId).ModelName.ToLower().Replace(" ", "_");
            var productName = productViewModel.ProductName.ToLower().Trim().Replace(" ", "_");
            productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);
            if (productViewModel.CoverPicture != null && productViewModel.CoverPicture.Length > 0)
            {
                var subDirectory = new List<string> { makeName, "products", "pictures", modelName, productName, productViewModel.Year.ToString() };
                subDirectoryHelper.CreateSubDirectory(subDirectory);
                subDirectory.Add(productViewModel.CoverPicture.FileName);
                var partialPath = Path.Combine(subDirectory.ToArray());
                var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                productViewModel.PictureUrl = partialPath;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                using var fileStream = new FileStream(fullPath, FileMode.Create);
                await productViewModel.CoverPicture.CopyToAsync(fileStream);
            }

            if (productViewModel.Pictures360 != null && productViewModel.Pictures360.Count > 0)
            {
                if (productViewModel.Pictures360.Sum(x => x.Length) > 26214400)
                {
                    ModelState.AddModelError(string.Empty, "The 360 files are too large (25 MB Max).");
                    return this.View(productViewModel);
                }

                var subDirectory = new List<string> { makeName, "products", "360_pictures", productViewModel.Year.ToString(), $"{modelName}360" };
                subDirectoryHelper.CreateSubDirectory(subDirectory);
                productViewModel.Picture360Quantity = productViewModel.Picture360Quantity != productViewModel.Pictures360.Count ? productViewModel.Pictures360.Count : productViewModel.Picture360Quantity;
                var partialPath = Path.Combine(subDirectory.ToArray());
                var fullPath360 = Path.Combine(this._host.WebRootPath, partialPath);
                productViewModel.Picture360Url = partialPath;
                ////Deletes all files and put new files
                if (Directory.Exists(fullPath360))
                {
                    var files = Directory.GetFiles(fullPath360);
                    foreach (var item in files)
                    {
                        System.IO.File.Delete(Path.Combine(fullPath360, item));
                    }
                }

                foreach (var item in productViewModel.Pictures360)
                {
                    using var fileStream = new FileStream(Path.Combine(fullPath360, item.FileName), FileMode.Create);
                    await item.CopyToAsync(fileStream);
                }
            }

            var products = new Products();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productsApi = new ProductsApi(this._httpClientInstance);
                products.Cost = productViewModel.Cost ?? 0;
                products.Description = productViewModel.Description;
                products.FkCategoriesId = productViewModel.FkCategoriesId;
                products.IsFeatured = productViewModel.IsFeatured;
                products.LongDescription = productViewModel.LongDescription;
                products.QuantityInStock = productViewModel.QuantityInStock;
                products.SalesPrice = productViewModel.SalesPrice ?? 0;
                products.Sku = productViewModel.Sku;
                products.Upc = productViewModel.Upc;
                products.Cupix360Url = productViewModel.Cupix360Url;
                products.BaseWeight = productViewModel.BaseWeight;
                products.FkBodyStylesId = productViewModel.FkBodyStylesId;
                products.FkDriveTrainsId = productViewModel.FkDriveTrainsId;
                products.FkEngineTypesId = productViewModel.FkEngineTypesId;
                products.FkEpaClassesId = productViewModel.FkEpaClassesId;
                products.FkMakesId = productViewModel.FkMakesId;
                products.FkModelsId = productViewModel.FkModelsId;
                products.FkTransmissionsId = productViewModel.FkTransmissionsId;
                products.Horsepower = productViewModel.Horsepower;
                products.Msrp = productViewModel.Msrp ?? 0;
                products.PassengerDoors = productViewModel.PassengerDoors;
                products.Passengers = productViewModel.Passengers;
                products.Picture360Quantity = productViewModel.Picture360Quantity;
                products.Picture360Url = productViewModel.Picture360Url;
                products.ProductName = productViewModel.ProductName;
                products.Trim = productViewModel.Trim;
                products.Year = productViewModel.Year;
                products.YoutubeUrl = productViewModel.YoutubeUrl;
                products.AllowShow = productViewModel.AllowShow;
                products.PictureUrl = productViewModel.PictureUrl;

                ////Massey
                products.Cylinders = productViewModel.Cylinders;
                products.LoadCapacity = productViewModel.LoadCapacity;
                products.SingleWeight = productViewModel.SingleWeight;
                products.Productivity = productViewModel.Productivity;
                products.BaleLength = productViewModel.BaleLength;
                products.Speed = productViewModel.Speed;
                products.RecommendedPower = productViewModel.RecommendedPower;
                products.WorkingWidth = productViewModel.WorkingWidth;
                products.NumberOfLines = productViewModel.NumberOfLines;
                products.NumberOfDiscs = productViewModel.NumberOfDiscs;
                products.InputRotation = productViewModel.InputRotation;
                products.StandardCover = productViewModel.StandardCover;
                products.CuttingWidth = productViewModel.CuttingWidth;
                products.FkAspirationTypesId = productViewModel.FkAspirationTypesId;
                products.FkRearTiresTypesId = productViewModel.FkRearTiresTypesId;
                products.FkFrontTiresTypesId = productViewModel.FkFrontTiresTypesId;
                products.FkSeparationSystemsId = productViewModel.FkSeparationSystemsId;
                products.FkHitchSystemsId = productViewModel.FkHitchSystemsId;
                products.FkHitchPinesId = productViewModel.FkHitchPinesId;
                products.FkChamberDimensionsId = productViewModel.FkChamberDimensionsId;
                products.FkConfigurationTypesId = productViewModel.FkConfigurationTypesId;
                products.FkConditioningSystemsId = productViewModel.FkConditioningSystemsId;
                products.FkBladesTypesId = productViewModel.FkBladesTypesId;
                products.FkTurningRadiusTypesId = productViewModel.FkTurningRadiusTypesId;

                ////Creates new product
                products = await productsApi.CreateRecord(productViewModel);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (products.ProductsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(productViewModel);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(Products));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Products/Delete/{id}")]
        public async Task<IActionResult> ProductsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var product = new Products();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                product = await productsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (product == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(product);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="productsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Products/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductsDeleteConfirmed(long productsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productsApi = new ProductsApi(this._httpClientInstance);
                var product = await productsApi.GetRecord(productsId);

                if (product.InventoryItems.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado a un Inventario. Si desea que los usuarios no tengan acceso a este producto, favor inhabilite este registro cambiando el campo AllowShow.";
                    return RedirectToAction(nameof(ProductsDelete), new { id = productsId });
                }

                if (product.OrderDetails.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado a detalles de ordenes. Si desea que los usuarios no tengan acceso a este producto, favor inhabilite este registro cambiando el campo AllowShow.";
                    return RedirectToAction(nameof(ProductsDelete), new { id = productsId });
                }

                if (product.ShoppingCartRecords.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado al carrito de algún usuario. Si desea que los usuarios no tengan acceso a este producto, favor inhabilite este registro cambiando el campo AllowShow.";
                    return RedirectToAction(nameof(ProductsDelete), new { id = productsId });
                }

                if (product.ProductDocuments.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado a Documentos por producto. Si desea que los usuarios no tengan acceso a este producto, favor inhabilite este registro cambiando el campo AllowShow.";
                    return RedirectToAction(nameof(ProductsDelete), new { id = productsId });
                }

                if (!string.IsNullOrEmpty(product.PictureUrl))
                {
                    var fullPath = Path.Combine(this._host.WebRootPath, product.PictureUrl);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                if (!string.IsNullOrEmpty(product.Picture360Url))
                {
                    var fullPath = Path.Combine(this._host.WebRootPath, product.Picture360Url);
                    if (Directory.Exists(fullPath))
                    {
                        var files = Directory.GetFiles(fullPath);
                        foreach (var item in files)
                        {
                            System.IO.File.Delete(Path.Combine(fullPath, item));
                        }
                    }
                }

                await productsApi.DeleteRecord(productsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return RedirectToAction(nameof(ProductsDelete), new { id = productsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Products));
        }
        #endregion

        #region Product Documents
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/[action]/{productsId}")]
        public async Task<IActionResult> ProductDocuments(long? productsId)
        {
            if (productsId == null)
            {
                return this.View("NotFoundAdmin");
            }

            var productDocuments = new List<ProductDocuments>();
            var product = new Products();
            try
            {
                var productDocumentsApi = new ProductDocumentsApi(this._httpClientInstance);
                var productsApi = new ProductsApi(this._httpClientInstance);
                productDocuments = await productDocumentsApi.GetRecordsForProduct(productsId ?? 0);
                product = await productsApi.GetRecord(productsId ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkProducts = product;
            return this.View(productDocuments);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductDocuments/Edit/{productDocumentsId}")]
        public async Task<IActionResult> ProductDocumentsEdit(long? productDocumentsId)
        {
            if (productDocumentsId == null)
            {
                return this.View("NotFoundAdmin");
            }

            var productDocuments = new ProductDocuments();
            var productDocumentCategories = new List<ProductDocumentsCategories>();
            try
            {
                var productDocumentsApi = new ProductDocumentsApi(this._httpClientInstance);
                var productDocumentCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                productDocuments = await productDocumentsApi.GetRecord(productDocumentsId ?? 0);
                productDocumentCategories = await productDocumentCategoriesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (productDocuments == null)
            {
                return this.View("NotFoundAdmin");
            }

            var invetoryItemsViewModel = new ProductDocumentsViewModel()
            {
                FileName = productDocuments.FileName,
                FilePath = productDocuments.FilePath,
                FkProductDocumentsCategoriesId = productDocuments.FkProductDocumentsCategoriesId,
                FkProductsId = productDocuments.FkProductsId,
                ProductDocumentsId = productDocuments.ProductDocumentsId
            };

            ViewBag.FkProductDocumentsCategories = new SelectList(productDocumentCategories, "ProductDocumentsCategoriesId", "CategoryName", productDocuments.FkProductDocumentsCategoriesId);
            return this.View(invetoryItemsViewModel);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductDocuments/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDocumentsEdit(long id, [FromForm] ProductDocumentsViewModel productDocumentsViewModel)
        {

            if (id != productDocumentsViewModel.ProductDocumentsId)
            {
                return this.View("NotFoundAdmin");
            }

            var productDocumentCategories = new List<ProductDocumentsCategories>();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsApi = new ProductDocumentsApi(this._httpClientInstance);

                if (productDocumentsViewModel.File != null && productDocumentsViewModel.File.Length > 0)
                {
                    if (!string.IsNullOrEmpty(productDocumentsViewModel.FilePath) && System.IO.File.Exists(Path.Combine(this._host.WebRootPath, productDocumentsViewModel.FilePath)))
                    {
                        System.IO.File.Delete(Path.Combine(this._host.WebRootPath, productDocumentsViewModel.FilePath));
                    }

                    var models = new List<Models>();
                    var products = new List<Products>();
                    var makes = new List<Makes>();
                    var modelsApi = new ModelsApi(this._httpClientInstance);
                    var productsApi = new ProductsApi(this._httpClientInstance);
                    var makesApi = new MakesApi(this._httpClientInstance);
                    var productDocumentCategoriesApi = new ProductDocumentsCategoriesApi(this._httpClientInstance);

                    models = await modelsApi.GetAllRecords();
                    products = await productsApi.GetAllRecords();
                    makes = await makesApi.GetAllRecords();
                    productDocumentCategories = await productDocumentCategoriesApi.GetAllRecords();

                    var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
                    var product = products.FirstOrDefault(x => x.ProductsId == productDocumentsViewModel.FkProductsId);
                    var makeName = makes.FirstOrDefault(x => x.MakesId == product.FkMakesId).MakeName.ToLower().Replace(" ", "_");
                    var modelName = models.FirstOrDefault(x => x.ModelsId == product.FkModelsId).ModelName.ToLower().Replace(" ", "_");
                    var productName = product.ProductName.ToLower().Trim().Replace(" ", "_");
                    productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);

                    var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, "documents" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    subDirectory.Add(productDocumentsViewModel.File.FileName);
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    productDocumentsViewModel.FilePath = partialPath;
                    if (!System.IO.File.Exists(fullPath))
                    {
                        using var fileStream = new FileStream(fullPath, FileMode.Create);
                        await productDocumentsViewModel.File.CopyToAsync(fileStream);
                    }
                }

                var productDocumentsId = new ProductDocuments()
                {
                    ProductDocumentsId = productDocumentsViewModel.ProductDocumentsId,
                    FileName = productDocumentsViewModel.FileName,
                    FilePath = productDocumentsViewModel.FilePath,
                    FkProductsId = productDocumentsViewModel.FkProductsId,
                    FkProductDocumentsCategoriesId = productDocumentsViewModel.FkProductDocumentsCategoriesId
                };

                await productDocumentsApi.UpdateRecord(id, productDocumentsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                ViewBag.FkProductDocumentsCategories = new SelectList(productDocumentCategories, "ProductDocumentsCategoriesId", "CategoryName", productDocumentsViewModel.FkProductDocumentsCategoriesId);
                return this.View(nameof(ProductDocumentsEdit));
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(ProductDocuments), new { productsId = productDocumentsViewModel.FkProductsId });
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductDocuments/Create/{productsId}")]
        public async Task<IActionResult> ProductDocumentsCreate(long? productsId)
        {
            if (productsId == null)
            {
                return this.View("NotFoundAdmin");
            }

            var product = new Products();
            var productDocumentCategories = new List<ProductDocumentsCategories>();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                var productDocumentsCategoriesapi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                product = await productsApi.GetRecord(productsId ?? 0);
                productDocumentCategories = await productDocumentsCategoriesapi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.FkProductDocumentsCategories = new SelectList(productDocumentCategories, "ProductDocumentsCategoriesId", "CategoryName");
                ViewBag.FkProducts = product;
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View();
            }

            ViewBag.FkProductDocumentsCategories = new SelectList(productDocumentCategories, "ProductDocumentsCategoriesId", "CategoryName");
            ViewBag.FkProducts = product;
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="productDocumentsViewModel">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductDocuments/Create/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDocumentsCreate(long id, [FromForm] ProductDocumentsViewModel productDocumentsViewModel)
        {
            var productDocuments = new ProductDocuments();
            var productDocumentCategories = new List<ProductDocumentsCategories>();
            var productsApi = new ProductsApi(this._httpClientInstance);
            var products = await productsApi.GetRecord(productDocumentsViewModel.FkProductsId ?? 0);
            try
            {
                var productDocumentsCategoriesapi = new ProductDocumentsCategoriesApi(this._httpClientInstance);
                productDocumentCategories = await productDocumentsCategoriesapi.GetAllRecords();
                if (productDocumentsViewModel.File != null && productDocumentsViewModel.File.Length > 0)
                {
                    var models = new List<Models>();
                    var makes = new List<Makes>();

                    var modelsApi = new ModelsApi(this._httpClientInstance);
                    var makesApi = new MakesApi(this._httpClientInstance);

                    models = await modelsApi.GetAllRecords();
                    products = await productsApi.GetRecord(productDocumentsViewModel.FkProductsId ?? 0);
                    makes = await makesApi.GetAllRecords();

                    var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
                    var makeName = makes.FirstOrDefault(x => x.MakesId == products.FkMakesId).MakeName.ToLower().Replace(" ", "_");
                    var modelName = models.FirstOrDefault(x => x.ModelsId == products.FkModelsId).ModelName.ToLower().Replace(" ", "_");
                    var productName = products.ProductName.ToLower().Trim().Replace(" ", "_");
                    productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);

                    var subDirectory = new List<string> { makeName, "models", modelName, products.Year.ToString(), productName, "documents" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    subDirectory.Add(productDocumentsViewModel.File.FileName);
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    productDocumentsViewModel.FilePath = partialPath;

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await productDocumentsViewModel.File.CopyToAsync(fileStream);
                }
                else
                {
                    if (string.IsNullOrEmpty(productDocumentsViewModel.FilePath))
                    {
                        ModelState.AddModelError("FilePath", "File Path is mandatory");
                        ViewBag.FkProductDocumentsCategories = new SelectList(productDocumentCategories, "ProductDocumentsCategoriesId", "CategoryName", productDocumentsViewModel.FkProductDocumentsCategoriesId);
                        ViewBag.FkProducts = products;
                        return this.View(productDocumentsViewModel);
                    }
                }

                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsIdApi = new ProductDocumentsApi(this._httpClientInstance);
                var productDocumentsId = new ProductDocuments()
                {
                    FileName = productDocumentsViewModel.FileName,
                    FilePath = productDocumentsViewModel.FilePath,
                    FkProductDocumentsCategoriesId = productDocumentsViewModel.FkProductDocumentsCategoriesId,
                    FkProductsId = productDocumentsViewModel.FkProductsId
                };

                productDocuments = await productDocumentsIdApi.CreateRecord(productDocumentsId);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (productDocuments.ProductDocumentsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                ViewBag.FkProductDocumentsCategories = new SelectList(productDocumentCategories, "ProductDocumentsCategoriesId", "CategoryName", productDocumentsViewModel.FkProductDocumentsCategoriesId);
                ViewBag.FkProducts = products;
                return this.View(productDocumentsViewModel);
            }

            return this.RedirectToAction(nameof(ProductDocuments), new { productsId = productDocumentsViewModel.FkProductsId });
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductDocuments/Delete/{id}")]
        public async Task<IActionResult> ProductDocumentsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var productDocuments = new ProductDocuments();
            try
            {
                var productDocumentsApi = new ProductDocumentsApi(this._httpClientInstance);
                productDocuments = await productDocumentsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (productDocuments == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(productDocuments);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="bodyStylesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductDocuments/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDocumentsDeleteConfirmed(long productDocumentsId)
        {
            var productDocuments = new ProductDocuments();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsApi = new ProductDocumentsApi(this._httpClientInstance);
                productDocuments = await productDocumentsApi.GetRecord(productDocumentsId);
                if (!string.IsNullOrEmpty(productDocuments.FilePath) && System.IO.File.Exists(Path.Combine(this._host.WebRootPath, productDocuments.FilePath)))
                {
                    System.IO.File.Delete(Path.Combine(this._host.WebRootPath, productDocuments.FilePath));
                }

                await productDocumentsApi.DeleteRecord(productDocumentsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ProductDocumentsDelete), new { id = productDocuments });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(ProductDocuments), new { productsId = productDocuments.FkProductsId });
        }
        #endregion

        #region Product Maintenances
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/[action]/{productsId}")]
        public async Task<IActionResult> ProductMaintenances(int productsId)
        {
            var productMaintenances = new List<ProductMaintenances>();
            var product = new Products();
            try
            {
                var productDocumentsApi = new ProductMaintenancesApi(this._httpClientInstance);
                var productsApi = new ProductsApi(this._httpClientInstance);
                productMaintenances = await productDocumentsApi.GetRecordsByProduct(productsId, false);
                product = await productsApi.GetRecord(productsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.FkProducts = product;
            return this.View(productMaintenances);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductMaintenances/Edit/{id}")]
        public async Task<IActionResult> ProductMaintenancesEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                var productMaintenancesApi = new ProductMaintenancesApi(this._httpClientInstance);
                var productMaintenance = await productMaintenancesApi.GetRecord(id ?? 0);
                this.SetCustomFonts();
                return this.View(productMaintenance);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View("NotFoundAdmin");
            }
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductMaintenances/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductMaintenancesEdit(long id, [FromForm] ProductMaintenances productMaintenances)
        {
            if (id != productMaintenances.ProductMaintenancesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productMaintenancesApi = new ProductMaintenancesApi(this._httpClientInstance);
                await productMaintenancesApi.UpdateRecord(id, productMaintenances);
            }
            catch (Exception ex)
            {
                this.SetCustomFonts();
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ProductMaintenancesEdit));
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(ProductMaintenances), new { productsId = productMaintenances.FkProductsId });
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductMaintenances/Create/{productsId}")]
        public async Task<IActionResult> ProductMaintenancesCreate(long? productsId)
        {
            if (productsId == null)
            {
                return this.View("NotFoundAdmin");
            }

            var product = new Products();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                product = await productsApi.GetRecord(productsId ?? 0);
                ViewBag.FkProducts = product;
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                ViewBag.FkProducts = product;
                return this.View();
            }

            this.SetCustomFonts();
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="productMaintenances">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductMaintenances/Create/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductMaintenancesCreate(long id, [FromForm] ProductMaintenances productMaintenances)
        {
            var newProductMaintenances = new ProductMaintenances();
            var productsApi = new ProductsApi(this._httpClientInstance);
            var product = await productsApi.GetRecord(id);
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productMaintenancesApi = new ProductMaintenancesApi(this._httpClientInstance);
                newProductMaintenances = await productMaintenancesApi.CreateRecord(productMaintenances);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (newProductMaintenances.ProductMaintenancesId <= 0)
            {
                this.SetCustomFonts();
                ViewBag.FkProducts = product;
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(productMaintenances);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(ProductMaintenances), new { productsId = productMaintenances.FkProductsId });
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/ProductMaintenances/Delete/{id}")]
        public async Task<IActionResult> ProductMaintenancesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var productDocuments = new ProductMaintenances();
            try
            {
                var productDocumentsApi = new ProductMaintenancesApi(this._httpClientInstance);
                productDocuments = await productDocumentsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (productDocuments == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(productDocuments);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="productMaintenancesId">The product maintenances identifier.</param>
        /// <returns>
        /// Delete result
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/ProductMaintenances/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductMaintenancesDeleteConfirmed(long productMaintenancesId)
        {
            var productMaintenancesApi = new ProductMaintenancesApi(this._httpClientInstance);
            var productMaintenances = await productMaintenancesApi.GetRecord(productMaintenancesId);
            ViewBag.FkProducts = productMaintenances.FkProducts;

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var productDocumentsApi = new ProductMaintenancesApi(this._httpClientInstance);
                await productDocumentsApi.DeleteRecord(productMaintenancesId);

            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(ProductMaintenancesDelete), new { productsId = productMaintenances.FkProductsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(ProductMaintenances), new { productsId = productMaintenances.FkProductsId });
        }
        #endregion 
        #endregion

        #region InventoryItems
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> InventoryItems()
        {
            var inventoryItems = new List<InventoryItems>();
            try
            {
                var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                inventoryItems = await inventoryItemsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(inventoryItems);
        }

        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItems/More/{id}")]
        public async Task<IActionResult> InventoryItemsMore(long id)
        {
            var inventoryItems = new InventoryItems();
            var taxesForInventory = new List<TaxesForInventory>();
            try
            {
                var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                var taxesForInventoryApi = new TaxesForInventoryApi(this._httpClientInstance);
                inventoryItems = await inventoryItemsApi.GetRecord(id);
                taxesForInventory = await taxesForInventoryApi.GetForInventory(id);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.TaxesForInventory = taxesForInventory;
            return this.View(inventoryItems);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItems/Edit/{id}")]
        public async Task<IActionResult> InventoryItemsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var colors = new List<Colors>();
            var products = new List<Products>();
            var warranties = new List<Warranties>();
            var inventoryItems = new InventoryItems();
            var taxesForInventory = new List<TaxesForInventory>();
            var taxes = new List<Taxes>();
            var makesId = 0;
            try
            {
                var colorsApi = new ColorsApi(this._httpClientInstance);
                var productsApi = new ProductsApi(this._httpClientInstance);
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                var modelsApi = new ModelsApi(this._httpClientInstance);
                var taxesForInventoryApi = new TaxesForInventoryApi(this._httpClientInstance);
                var taxesApi = new TaxesApi(this._httpClientInstance);
                inventoryItems = await inventoryItemsApi.GetRecord(id ?? 0);
                colors = await colorsApi.GetAllRecords();
                products = await productsApi.GetAllRecords();
                warranties = await warrantiesApi.GetAllRecords();
                taxesForInventory = await taxesForInventoryApi.GetForInventory(id ?? 0);
                taxes = await taxesApi.GetAllRecords();
                makesId = inventoryItems.FkProducts.FkMakesId;
                colors = colors.Where(x => x.FkMakesId == makesId).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            if (inventoryItems == null)
            {
                return this.View("NotFoundAdmin");
            }

            var inventoryItemViewModel = new InventoryItemsViewModel
            {
                InventoryItemsId = inventoryItems.InventoryItemsId,
                Vin = inventoryItems.Vin,
                FkColorsId = inventoryItems.FkColorsId,
                Cost = inventoryItems.Cost,
                SalesPrice = inventoryItems.SalesPrice,
                Mileage = inventoryItems.Mileage,
                FkWarrantiesId = inventoryItems.FkWarrantiesId,
                FkProductsId = inventoryItems.FkProductsId,
                IsNew = inventoryItems.IsNew,
                IsSold = inventoryItems.IsSold,
                CoverPictureUrl = inventoryItems.CoverPictureUrl,
                AllowShow = inventoryItems.AllowShow,
                Cupix360Url = inventoryItems.Cupix360Url,
                Picture360Url = inventoryItems.Picture360Url,
                Picture360Quantity = inventoryItems.Picture360Quantity,
                SelectedTaxes = taxesForInventory.Select(x => x.FkTaxesId.ToString()).ToList(),
                QuantityInStock = inventoryItems.QuantityInStock
            };

            ViewBag.FkColorsId = new SelectList(colors, "ColorsId", "ColorName", inventoryItemViewModel.FkColorsId);
            ViewBag.FkProductsId = new SelectList(products, "ProductsId", "ProductName", inventoryItemViewModel.FkProductsId);
            ViewBag.FkWarrantiesId = new SelectList(warranties, "WarrantiesId", "WarrantyName", inventoryItemViewModel.FkWarrantiesId);
            ViewBag.Taxes = new MultiSelectList(taxes, "TaxesId", "TaxName", inventoryItemViewModel.SelectedTaxes);

            return this.View(inventoryItemViewModel);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="inventoryItemViewModel">The inventory item view model.</param>
        /// <returns>
        /// Save changes for bodyStyle
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InventoryItems/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InventoryItemsEdit(long id, [FromForm] InventoryItemsViewModel inventoryItemViewModel)
        {
            var colors = new List<Colors>();
            var products = new List<Products>();
            var warranties = new List<Warranties>();
            var makes = new List<Makes>();
            var models = new List<Models>();
            var taxesForInventory = new List<TaxesForInventory>();
            var taxes = new List<Taxes>();
            this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
            var taxesForInventoryApi = new TaxesForInventoryApi(this._httpClientInstance);
            var taxesApi = new TaxesApi(this._httpClientInstance);
            var colorsApi = new ColorsApi(this._httpClientInstance);
            var productsApi = new ProductsApi(this._httpClientInstance);
            var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
            var makesApi = new MakesApi(this._httpClientInstance);
            var modelsApi = new ModelsApi(this._httpClientInstance);
            colors = await colorsApi.GetAllRecords();
            products = await productsApi.GetAllRecords();
            warranties = await warrantiesApi.GetAllRecords();
            var product = products.FirstOrDefault(x => x.ProductsId == inventoryItemViewModel.FkProductsId);
            makes = await makesApi.GetAllRecords();
            colors = colors.Where(x => x.FkMakesId == product.FkMakesId).ToList();
            models = await modelsApi.GetAllRecords();
            var make = makes.FirstOrDefault(x => x.MakesId == product.FkMakesId);
            taxes = await taxesApi.GetAllRecords();

            ViewBag.Taxes = new MultiSelectList(taxes, "TaxesId", "TaxName", inventoryItemViewModel.SelectedTaxes);
            ViewBag.FkColorsId = new SelectList(colors, "ColorsId", "ColorName", inventoryItemViewModel.FkColorsId);
            ViewBag.FkProductsId = new SelectList(products, "ProductsId", "ProductName", inventoryItemViewModel.FkProductsId);
            ViewBag.FkWarrantiesId = new SelectList(warranties, "WarrantiesId", "WarrantyName", inventoryItemViewModel.FkWarrantiesId);

            if (id != inventoryItemViewModel.InventoryItemsId)
            {
                return this.View("NotFoundAdmin");
            }

            /*try
            {*/
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
                var makeName = make.MakeName.ToLower().Replace(" ", "_");
                var modelName = models.FirstOrDefault(x => x.ModelsId == product.FkModelsId).ModelName.ToLower().Replace(" ", "_");
                var productName = product.ProductName.ToLower().Trim().Replace(" ", "_");
                var colorName = colors.FirstOrDefault(x => x.ColorsId == inventoryItemViewModel.FkColorsId).ColorName.ToLower().Replace(" ", "_");
                productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);
                taxesForInventory = await taxesForInventoryApi.GetForInventory(id);

                if (inventoryItemViewModel.CoverPictureFile != null && inventoryItemViewModel.CoverPictureFile.Length > 0)
                {
                    if (!string.IsNullOrEmpty(inventoryItemViewModel.CoverPictureUrl))
                    {
                        System.IO.File.Delete(Path.Combine(this._host.WebRootPath, inventoryItemViewModel.CoverPictureUrl));
                    }

                    var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, colorName, "cover" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    subDirectory.Add(inventoryItemViewModel.CoverPictureFile.FileName);
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    inventoryItemViewModel.CoverPictureUrl = partialPath;

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await inventoryItemViewModel.CoverPictureFile.CopyToAsync(fileStream);
                }

                if (inventoryItemViewModel.Pictures360File != null && inventoryItemViewModel.Pictures360File.Count > 0)
                {
                    if (!string.IsNullOrEmpty(inventoryItemViewModel.Picture360Url))
                    {
                        var fullPath = Path.Combine(this._host.WebRootPath, inventoryItemViewModel.Picture360Url);
                        if (Directory.Exists(fullPath))
                        {
                            var files = Directory.GetFiles(fullPath);
                            foreach (var item in files)
                            {
                                System.IO.File.Delete(Path.Combine(fullPath, item));
                            }
                        }
                    }

                    var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, colorName, "360" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    inventoryItemViewModel.Picture360Quantity = inventoryItemViewModel.Picture360Quantity != inventoryItemViewModel.Pictures360File.Count ? inventoryItemViewModel.Pictures360File.Count : inventoryItemViewModel.Picture360Quantity;
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath360 = Path.Combine(this._host.WebRootPath, partialPath);
                    inventoryItemViewModel.Picture360Url = partialPath;
                    if (!System.IO.File.Exists(fullPath360))
                    {
                        foreach (var item in inventoryItemViewModel.Pictures360File)
                        {
                            using var fileStream = new FileStream(Path.Combine(fullPath360, item.FileName), FileMode.Create);
                            await item.CopyToAsync(fileStream);
                        }
                    }
                }

                var inventoryItems = new InventoryItems()
                {
                    InventoryItemsId = inventoryItemViewModel.InventoryItemsId,
                    Vin = inventoryItemViewModel.Vin,
                    FkColorsId = inventoryItemViewModel.FkColorsId,
                    Cost = inventoryItemViewModel.Cost ?? 0,
                    SalesPrice = inventoryItemViewModel.SalesPrice ?? 0,
                    Mileage = inventoryItemViewModel.Mileage,
                    FkWarrantiesId = inventoryItemViewModel.FkWarrantiesId,
                    IsNew = inventoryItemViewModel.IsNew,
                    IsSold = inventoryItemViewModel.IsSold,
                    CoverPictureUrl = inventoryItemViewModel.CoverPictureUrl,
                    AllowShow = inventoryItemViewModel.AllowShow,
                    Cupix360Url = inventoryItemViewModel.Cupix360Url,
                    Picture360Url = inventoryItemViewModel.Picture360Url,
                    Picture360Quantity = inventoryItemViewModel.Picture360Quantity,
                    FkProductsId = inventoryItemViewModel.FkProductsId,
                    QuantityInStock = inventoryItemViewModel.QuantityInStock
                };


                await inventoryItemsApi.UpdateRecord(id, inventoryItems);

                ////Delete old Taxes for inventory
                if (taxesForInventory.Count > 0)
                {
                    foreach (var item in taxesForInventory)
                    {
                        await taxesForInventoryApi.DeleteRecord(item.TaxesForInventoryId);
                    }
                }

                ////Set Selected Taxes
                if (inventoryItemViewModel.SelectedTaxes != null && inventoryItemViewModel.SelectedTaxes.Count > 0)
                {
                    var taxesForInventoryNew = new List<TaxesForInventory>();
                    foreach (var item in inventoryItemViewModel.SelectedTaxes)
                    {
                        taxesForInventoryNew.Add(new TaxesForInventory()
                        {
                            FkInventoryItemsId = inventoryItems.InventoryItemsId,
                            FkTaxesId = int.Parse(item)
                        });
                    }

                    var taxesForInventoryCreated = await taxesForInventoryApi.CreateRecords(taxesForInventoryNew);
                }
            /*}
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(InventoryItemsEdit), inventoryItemViewModel);
            }*/

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(InventoryItems));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItems/Create")]
        public async Task<IActionResult> InventoryItemsCreate()
        {
            var colors = new List<Colors>();
            var products = new List<Products>();
            var warranties = new List<Warranties>();
            var makes = new List<Makes>();
            var taxes = new List<Taxes>();
            try
            {
                var colorsApi = new ColorsApi(this._httpClientInstance);
                var productsApi = new ProductsApi(this._httpClientInstance);
                var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
                var makesApi = new MakesApi(this._httpClientInstance);
                var taxesApi = new TaxesApi(this._httpClientInstance);
                colors = await colorsApi.GetAllRecords();
                products = await productsApi.GetAllRecords();
                warranties = await warrantiesApi.GetAllRecords();
                makes = await makesApi.GetAllRecords();
                taxes = await taxesApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            var make = makes.FirstOrDefault().MakesId;
            colors = colors.Where(x => x.FkMakesId == make).ToList();
            products = products.Where(x => x.FkMakesId == make).ToList();
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName");
            ViewBag.FkColorsId = new SelectList(colors, "ColorsId", "ColorName");
            ViewBag.FkProductsId = new SelectList(products, "ProductsId", "ProductName");
            ViewBag.FkWarrantiesId = new SelectList(warranties, "WarrantiesId", "WarrantyName");
            ViewBag.Taxes = new MultiSelectList(taxes, "TaxesId", "TaxName");
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="inventoryItemViewModel">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InventoryItems/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InventoryItemsCreate([FromForm] InventoryItemsViewModel inventoryItemViewModel)
        {
            var colors = new List<Colors>();
            var products = new List<Products>();
            var warranties = new List<Warranties>();
            var makes = new List<Makes>();
            var models = new List<Models>();
            var taxes = new List<Taxes>();
            var colorsApi = new ColorsApi(this._httpClientInstance);
            var productsApi = new ProductsApi(this._httpClientInstance);
            var warrantiesApi = new WarrantiesApi(this._httpClientInstance);
            var makesApi = new MakesApi(this._httpClientInstance);
            var modelsApi = new ModelsApi(this._httpClientInstance);
            var taxesApi = new TaxesApi(this._httpClientInstance);
            colors = await colorsApi.GetAllRecords();
            products = await productsApi.GetAllRecords();
            warranties = await warrantiesApi.GetAllRecords();
            makes = await makesApi.GetAllRecords();
            taxes = await taxesApi.GetAllRecords();
            ViewBag.FkColorsId = new SelectList(colors, "ColorsId", "ColorName", inventoryItemViewModel.FkColorsId);
            ViewBag.FkProductsId = new SelectList(products, "ProductsId", "ProductName", inventoryItemViewModel.FkProductsId);
            ViewBag.FkWarrantiesId = new SelectList(warranties, "WarrantiesId", "WarrantyName", inventoryItemViewModel.FkWarrantiesId);
            ViewBag.FkMakesId = new SelectList(makes, "MakesId", "MakeName", inventoryItemViewModel.FkProducts.FkMakesId);
            ViewBag.Taxes = new MultiSelectList(taxes, "TaxesId", "TaxName", inventoryItemViewModel.SelectedTaxes);

            models = await modelsApi.GetAllRecords();
            var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
            var product = products.FirstOrDefault(x => x.ProductsId == inventoryItemViewModel.FkProductsId);
            var makeName = makes.FirstOrDefault(x => x.MakesId == product.FkMakesId).MakeName.ToLower().Replace(" ", "_");
            var modelName = models.FirstOrDefault(x => x.ModelsId == product.FkModelsId).ModelName.ToLower().Replace(" ", "_");
            var productName = product.ProductName.ToLower().Trim().Replace(" ", "_");
            var colorName = colors.FirstOrDefault(x => x.ColorsId == inventoryItemViewModel.FkColorsId).ColorName.ToLower().Replace(" ", "_");
            productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);
            if (inventoryItemViewModel.CoverPictureFile != null && inventoryItemViewModel.CoverPictureFile.Length > 0)
            {
                var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, colorName, "cover" };
                subDirectoryHelper.CreateSubDirectory(subDirectory);
                subDirectory.Add(inventoryItemViewModel.CoverPictureFile.FileName);
                var partialPath = Path.Combine(subDirectory.ToArray());
                var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                inventoryItemViewModel.CoverPictureUrl = partialPath;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                using var fileStream = new FileStream(fullPath, FileMode.Create);
                await inventoryItemViewModel.CoverPictureFile.CopyToAsync(fileStream);
            }

            if (inventoryItemViewModel.Pictures360File != null && inventoryItemViewModel.Pictures360File.Count > 0)
            {
                var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, colorName, "360" };
                subDirectoryHelper.CreateSubDirectory(subDirectory);
                inventoryItemViewModel.Picture360Quantity = inventoryItemViewModel.Picture360Quantity != inventoryItemViewModel.Pictures360File.Count ? inventoryItemViewModel.Pictures360File.Count : inventoryItemViewModel.Picture360Quantity;
                var partialPath = Path.Combine(subDirectory.ToArray());
                var fullPath360 = Path.Combine(this._host.WebRootPath, partialPath);
                inventoryItemViewModel.Picture360Url = partialPath;

                ////Deletes all files and put new files
                if (Directory.Exists(fullPath360))
                {
                    var files = Directory.GetFiles(fullPath360);
                    foreach (var item in files)
                    {
                        System.IO.File.Delete(Path.Combine(fullPath360, item));
                    }
                }

                foreach (var item in inventoryItemViewModel.Pictures360File)
                {
                    using var fileStream = new FileStream(Path.Combine(fullPath360, item.FileName), FileMode.Create);
                    await item.CopyToAsync(fileStream);
                }
            }

            var inventoryItems = new InventoryItems();
            try
            {
                inventoryItems.FkProductsId = inventoryItemViewModel.FkProductsId;
                inventoryItems.Vin = inventoryItemViewModel.Vin;
                inventoryItems.FkColorsId = inventoryItemViewModel.FkColorsId;
                inventoryItems.Cost = inventoryItemViewModel.Cost ?? 0;
                inventoryItems.SalesPrice = inventoryItemViewModel.SalesPrice ?? 0;
                inventoryItems.Mileage = inventoryItemViewModel.Mileage;
                inventoryItems.FkWarrantiesId = inventoryItemViewModel.FkWarrantiesId;
                inventoryItems.IsNew = inventoryItemViewModel.IsNew;
                inventoryItems.IsSold = inventoryItemViewModel.IsSold;
                inventoryItems.CoverPictureUrl = inventoryItemViewModel.CoverPictureUrl;
                inventoryItems.AllowShow = inventoryItemViewModel.AllowShow;
                inventoryItems.Cupix360Url = inventoryItemViewModel.Cupix360Url;
                inventoryItems.Picture360Url = inventoryItemViewModel.Picture360Url;
                inventoryItems.Picture360Quantity = inventoryItemViewModel.Picture360Quantity;
                inventoryItems.QuantityInStock = inventoryItemViewModel.QuantityInStock;

                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                inventoryItems = await inventoryItemsApi.CreateRecord(inventoryItems);
                await Task.Delay(500);

                ////Set selected Taxes
                if (inventoryItemViewModel.SelectedTaxes != null && inventoryItemViewModel.SelectedTaxes.Count > 0)
                {
                    this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                    var taxesForInventoryApi = new TaxesForInventoryApi(this._httpClientInstance);
                    var taxesForInventory = new List<TaxesForInventory>();
                    foreach (var item in inventoryItemViewModel.SelectedTaxes)
                    {
                        taxesForInventory.Add(new TaxesForInventory()
                        {
                            FkInventoryItemsId = inventoryItems.InventoryItemsId,
                            FkTaxesId = int.Parse(item)
                        });
                    }

                    var taxesForInventoryCreated = await taxesForInventoryApi.CreateRecords(taxesForInventory);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (inventoryItems.InventoryItemsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(inventoryItemViewModel);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(InventoryItems));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItems/Delete/{id}")]
        public async Task<IActionResult> InventoryItemsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var inventoryItem = new InventoryItems();
            var taxesForInventory = new List<TaxesForInventory>();
            try
            {
                var inventoryItemApi = new InventoryItemsApi(this._httpClientInstance);
                inventoryItem = await inventoryItemApi.GetRecord(id ?? 0);
                var taxesForInventoryApi = new TaxesForInventoryApi(this._httpClientInstance);
                taxesForInventory = await taxesForInventoryApi.GetForInventory(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (inventoryItem == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.TaxesForInventory = taxesForInventory;
            return this.View(inventoryItem);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="bodyStylesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InventoryItems/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InventoryItemsDeleteConfirmed(long inventoryItemsId)
        {
            var inventoryItemPictures = new List<InventoryItemPictures>();
            var taxesForInventory = new List<TaxesForInventory>();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                var inventoryyItemPicturesApi = new InventoryItemPicturesApi(this._httpClientInstance);
                var taxesForInventoryApi = new TaxesForInventoryApi(this._httpClientInstance);
                var inventoryItems = await inventoryItemsApi.GetRecord(inventoryItemsId);
                taxesForInventory = await taxesForInventoryApi.GetForInventory(inventoryItemsId);
                inventoryItemPictures = await inventoryyItemPicturesApi.GetForInventory(inventoryItemsId);

                if (inventoryItems.OrderDetails.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado a detalles de ordenes. Si desea que los usuarios no tengan acceso a este inventario, favor inhabilite este registro cambiando el campo AllowShow.";
                    return this.RedirectToAction(nameof(InventoryItemsDelete), new { id = inventoryItemsId });
                }

                if (inventoryItems.ShoppingCartRecords.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado al carrito de compras. Si desea que los usuarios no tengan acceso a este inventario, favor inhabilite este registro cambiando el campo AllowShow.";
                    return this.RedirectToAction(nameof(InventoryItemsDelete), new { id = inventoryItemsId });
                }

                if (inventoryItems.InterestedCustomers.Count > 0)
                {
                    TempData["Info"] = "No se puede eliminar registro ya que está ligado a la lista de clientes interesados. Si desea que los usuarios no tengan acceso a este inventario, favor inhabilite este registro cambiando el campo AllowShow.";
                    return this.RedirectToAction(nameof(InventoryItemsDelete), new { id = inventoryItemsId });
                }

                if (!string.IsNullOrEmpty(inventoryItems.CoverPictureUrl))
                {
                    var fullPath = Path.Combine(this._host.WebRootPath, inventoryItems.CoverPictureUrl);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                if (!string.IsNullOrEmpty(inventoryItems.Picture360Url))
                {
                    var fullPath = Path.Combine(this._host.WebRootPath, inventoryItems.Picture360Url);
                    if (Directory.Exists(fullPath))
                    {
                        var files = Directory.GetFiles(fullPath);
                        foreach (var item in files)
                        {
                            System.IO.File.Delete(Path.Combine(fullPath, item));
                        }
                    }
                }

                if (inventoryItemPictures.Count > 0)
                {
                    foreach (var item in inventoryItemPictures)
                    {
                        var fullPath = Path.Combine(this._host.WebRootPath, item.PictureUrl);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(Path.Combine(fullPath));
                        }

                        await inventoryyItemPicturesApi.DeleteRecord(item.InventoryItemPicturesId);
                    }
                }

                if (taxesForInventory.Count > 0)
                {
                    foreach (var item in taxesForInventory)
                    {
                        await taxesForInventoryApi.DeleteRecord(item.TaxesForInventoryId);
                    }
                }

                await inventoryItemsApi.DeleteRecord(inventoryItemsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.RedirectToAction(nameof(InventoryItemsDelete), new { id = inventoryItemsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(InventoryItems));
        }
        #endregion

        #region InventoryItemPictures
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/[action]/{inventoryItemId}")]
        public async Task<IActionResult> InventoryItemPictures(long? inventoryItemId)
        {
            if (inventoryItemId == null)
            {
                return this.View("NotFoundAdmin");
            }

            var inventoryItems = new List<InventoryItemPictures>();
            try
            {
                var inventoryItemsApi = new InventoryItemPicturesApi(this._httpClientInstance);
                inventoryItems = await inventoryItemsApi.GetForInventory(inventoryItemId ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.InventoryItemId = inventoryItemId;
            return this.View(inventoryItems);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItemPictures/Edit/{inventoryItemPicturesId}")]
        public async Task<IActionResult> InventoryItemPicturesEdit(long? inventoryItemPicturesId)
        {
            if (inventoryItemPicturesId == null)
            {
                return this.View("NotFoundAdmin");
            }

            var inventoryItems = new InventoryItemPictures();
            try
            {
                var inventoryItemsApi = new InventoryItemPicturesApi(this._httpClientInstance);
                inventoryItems = await inventoryItemsApi.GetRecord(inventoryItemPicturesId ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (inventoryItems == null)
            {
                return this.View("NotFoundAdmin");
            }

            var invetoryItemsViewModel = new InventoryItemPicturesViewModel()
            {
                AlternateText = inventoryItems.AlternateText,
                FkInventoryItemsId = inventoryItems.FkInventoryItemsId,
                InventoryItemPicturesId = inventoryItems.InventoryItemPicturesId,
                PictureHeight = inventoryItems.PictureHeight,
                PictureName = inventoryItems.PictureName,
                PictureUrl = inventoryItems.PictureUrl,
                PictureWidth = inventoryItems.PictureWidth
            };

            return this.View(invetoryItemsViewModel);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="bodyStyles">The body styles.</param>
        /// <returns>Save changes for bodyStyle</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InventoryItemPictures/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InventoryItemPicturesEdit(long id, [FromForm] InventoryItemPicturesViewModel inventoryItemPictureViewModel)
        {

            if (id != inventoryItemPictureViewModel.InventoryItemPicturesId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var inventoryItemPictureApi = new InventoryItemPicturesApi(this._httpClientInstance);
                if (inventoryItemPictureViewModel.PictureFile != null && inventoryItemPictureViewModel.PictureFile.Length > 0)
                {
                    if (!string.IsNullOrEmpty(inventoryItemPictureViewModel.PictureUrl))
                    {
                        System.IO.File.Delete(Path.Combine(this._host.WebRootPath, inventoryItemPictureViewModel.PictureUrl));
                    }

                    var models = new List<Models>();
                    var products = new List<Products>();
                    var makes = new List<Makes>();
                    var colors = new List<Colors>();
                    var inventoryItem = new InventoryItems();
                    var modelsApi = new ModelsApi(this._httpClientInstance);
                    var productsApi = new ProductsApi(this._httpClientInstance);
                    var makesApi = new MakesApi(this._httpClientInstance);
                    var colorsApi = new ColorsApi(this._httpClientInstance);
                    var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                    inventoryItem = await inventoryItemsApi.GetRecord(inventoryItemPictureViewModel.FkInventoryItemsId);

                    models = await modelsApi.GetAllRecords();
                    products = await productsApi.GetAllRecords();
                    makes = await makesApi.GetAllRecords();
                    colors = await colorsApi.GetAllRecords();

                    var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
                    var product = products.FirstOrDefault(x => x.ProductsId == inventoryItem.FkProductsId);
                    var makeName = makes.FirstOrDefault(x => x.MakesId == product.FkMakesId).MakeName.ToLower().Replace(" ", "_");
                    var modelName = models.FirstOrDefault(x => x.ModelsId == product.FkModelsId).ModelName.ToLower().Replace(" ", "_");
                    var productName = product.ProductName.ToLower().Trim().Replace(" ", "_");
                    var colorName = colors.FirstOrDefault(x => x.ColorsId == inventoryItem.FkColorsId).ColorName.ToLower().Replace(" ", "_");
                    productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);

                    var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, colorName, "pictures" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    subDirectory.Add(inventoryItemPictureViewModel.PictureFile.FileName);
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    inventoryItemPictureViewModel.PictureUrl = partialPath;
                    if (!System.IO.File.Exists(fullPath))
                    {
                        using var fileStream = new FileStream(fullPath, FileMode.Create);
                        await inventoryItemPictureViewModel.PictureFile.CopyToAsync(fileStream);
                    }
                }

                var inventoryItemPictures = new InventoryItemPictures()
                {
                    InventoryItemPicturesId = inventoryItemPictureViewModel.InventoryItemPicturesId,
                    FkInventoryItemsId = inventoryItemPictureViewModel.FkInventoryItemsId,
                    PictureName = inventoryItemPictureViewModel.PictureName,
                    PictureHeight = inventoryItemPictureViewModel.PictureHeight,
                    PictureWidth = inventoryItemPictureViewModel.PictureWidth,
                    PictureUrl = inventoryItemPictureViewModel.PictureUrl,
                    AlternateText = inventoryItemPictureViewModel.AlternateText
                };

                await inventoryItemPictureApi.UpdateRecord(id, inventoryItemPictures);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(InventoryItemPicturesEdit));
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(InventoryItemPictures), new { inventoryItemId = inventoryItemPictureViewModel.FkInventoryItemsId });
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItemPictures/Create/{inventoryItemId}")]
        public IActionResult InventoryItemPicturesCreate(long? inventoryItemId)
        {
            if (inventoryItemId == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkInventoryItemsId = inventoryItemId;
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="inventoryItemPictureViewModel">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InventoryItemPictures/Create/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InventoryItemPicturesCreate(long id, [FromForm] InventoryItemPicturesViewModel inventoryItemPictureViewModel)
        {
            var inventoryItems = new InventoryItemPictures();
            try
            {
                if (inventoryItemPictureViewModel.PictureFile != null && inventoryItemPictureViewModel.PictureFile.Length > 0)
                {
                    var models = new List<Models>();
                    var products = new List<Products>();
                    var makes = new List<Makes>();
                    var colors = new List<Colors>();
                    var inventoryItem = new InventoryItems();
                    var modelsApi = new ModelsApi(this._httpClientInstance);
                    var productsApi = new ProductsApi(this._httpClientInstance);
                    var makesApi = new MakesApi(this._httpClientInstance);
                    var colorsApi = new ColorsApi(this._httpClientInstance);
                    var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
                    inventoryItem = await inventoryItemsApi.GetRecord(inventoryItemPictureViewModel.FkInventoryItemsId);

                    models = await modelsApi.GetAllRecords();
                    products = await productsApi.GetAllRecords();
                    makes = await makesApi.GetAllRecords();
                    colors = await colorsApi.GetAllRecords();

                    var subDirectoryHelper = new DirectoryHelper(this._host.WebRootPath);
                    var product = products.FirstOrDefault(x => x.ProductsId == inventoryItem.FkProductsId);
                    var makeName = makes.FirstOrDefault(x => x.MakesId == product.FkMakesId).MakeName.ToLower().Replace(" ", "_");
                    var modelName = models.FirstOrDefault(x => x.ModelsId == product.FkModelsId).ModelName.ToLower().Replace(" ", "_");
                    var productName = product.ProductName.ToLower().Trim().Replace(" ", "_");
                    var colorName = colors.FirstOrDefault(x => x.ColorsId == inventoryItem.FkColorsId).ColorName.ToLower().Replace(" ", "_");
                    productName = Regex.Replace(productName, @"[.!@$%^&*+()\-/]+", "", RegexOptions.Compiled);

                    var subDirectory = new List<string> { makeName, "models", modelName, product.Year.ToString(), productName, colorName, "pictures" };
                    subDirectoryHelper.CreateSubDirectory(subDirectory);
                    subDirectory.Add(inventoryItemPictureViewModel.PictureFile.FileName);
                    var partialPath = Path.Combine(subDirectory.ToArray());
                    var fullPath = Path.Combine(this._host.WebRootPath, partialPath);
                    inventoryItemPictureViewModel.PictureUrl = partialPath;

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await inventoryItemPictureViewModel.PictureFile.CopyToAsync(fileStream);
                }

                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var inventoryItemPicturesApi = new InventoryItemPicturesApi(this._httpClientInstance);
                var inventoryItemPictures = new InventoryItemPictures()
                {
                    FkInventoryItemsId = inventoryItemPictureViewModel.FkInventoryItemsId,
                    PictureName = inventoryItemPictureViewModel.PictureName,
                    PictureHeight = inventoryItemPictureViewModel.PictureHeight,
                    PictureWidth = inventoryItemPictureViewModel.PictureWidth,
                    PictureUrl = inventoryItemPictureViewModel.PictureUrl,
                    AlternateText = inventoryItemPictureViewModel.AlternateText
                };

                inventoryItems = await inventoryItemPicturesApi.CreateRecord(inventoryItemPictures);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (inventoryItems.InventoryItemPicturesId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                ViewBag.FkInventoryItemsId = inventoryItemPictureViewModel.FkInventoryItemsId;
                return this.View(inventoryItemPictureViewModel);
            }

            return this.RedirectToAction(nameof(InventoryItemPictures), new { inventoryItemId = inventoryItemPictureViewModel.FkInventoryItemsId });
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InventoryItemPictures/Delete/{id}")]
        public async Task<IActionResult> InventoryItemPicturesDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var inventoryItem = new InventoryItemPictures();
            try
            {
                var inventoryItemApi = new InventoryItemPicturesApi(this._httpClientInstance);
                inventoryItem = await inventoryItemApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (inventoryItem == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(inventoryItem);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="bodyStylesId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InventoryItemPictures/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InventoryItemPicturesDeleteConfirmed(long inventoryItemPicturesId)
        {
            var inventoryItemPictures = new InventoryItemPictures();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var inventoryItemsPicturesApi = new InventoryItemPicturesApi(this._httpClientInstance);
                inventoryItemPictures = await inventoryItemsPicturesApi.GetRecord(inventoryItemPicturesId);
                if (!string.IsNullOrEmpty(inventoryItemPictures.PictureUrl))
                {
                    var imagePath = Path.Combine(this._host.WebRootPath, inventoryItemPictures.PictureUrl);
                    System.IO.File.Delete(imagePath);
                }

                await inventoryItemsPicturesApi.DeleteRecord(inventoryItemPicturesId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.RedirectToAction(nameof(InventoryItemPicturesDelete), new { id = inventoryItemPicturesId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(InventoryItemPictures), new { inventoryItemId = inventoryItemPictures.FkInventoryItemsId });
        }
        #endregion

        #region Orders / Shopping Cart / Funding Request
        //// Orders parameters
        #region Orders
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var newsLetterSubscriptions = new List<Orders>();
            try
            {
                var newsletterSubscriptionsApi = new OrdersApi(this._httpClientInstance);
                newsLetterSubscriptions = await newsletterSubscriptionsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(newsLetterSubscriptions);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Orders/Edit/{id}")]
        public async Task<IActionResult> OrdersEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var customers = new List<Customers>();
            var orders = new Orders();
            try
            {
                var newsletterSubscriptionsApi = new OrdersApi(this._httpClientInstance);
                var customersApi = new CustomerApi(this._httpClientInstance);
                orders = await newsletterSubscriptionsApi.GetRecord(id ?? 0);
                customers = await customersApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (orders == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.FkCustomersId = new SelectList(customers, "CustomersId", "FullName", orders.FkCustomersId);
            return this.View(orders);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="order">The body styles.</param>
        /// <returns>Save changes for product</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Orders/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdersEdit(long id, [FromForm] Orders order)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(OrdersEdit), order);
            }

            if (id != order.OrdersId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var orderApi = new OrdersApi(this._httpClientInstance);
                await orderApi.UpdateRecord(id, order);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(OrdersEdit), order);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(Orders));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/Orders/Delete/{id}")]
        public async Task<IActionResult> OrdersDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var orders = new Orders();
            try
            {
                var ordersApi = new OrdersApi(this._httpClientInstance);
                orders = await ordersApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (orders == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(orders);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="ordersId">The newsletter subscriptions identifier identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/Orders/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdersDeleteConfirmed(long ordersId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                var ordersApi = new OrdersApi(this._httpClientInstance);
                await orderDetailsApi.DeleteRecordsForOrder(ordersId);
                await ordersApi.DeleteRecord(ordersId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(OrdersDelete), new { id = ordersId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Orders));
        }
        #endregion

        #region Orders Details
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/[action]/{id}")]
        public async Task<IActionResult> OrdersDetails(long id)
        {
            var orderDetails = new OrderWithDetailsAndProductInfo();
            try
            {
                var order = new Orders();
                var orderApi = new OrdersApi(this._httpClientInstance);
                var orderDetailsApi = new OrdersApi(this._httpClientInstance);
                order = await orderApi.GetRecord(id);
                orderDetails = await orderDetailsApi.GetOrderWithDetailsWithProductInfo(id, order.FkCustomersId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(orderDetails);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/OrdersDetails/Edit/{id}")]
        public async Task<IActionResult> OrdersDetailsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var orderDetails = new OrderDetails();
            try
            {
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                orderDetails = await orderDetailsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (orderDetails == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(orderDetails);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="fundingRequests">Thefunding requests.</param>
        /// <returns>Save changes for fundingRequest</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/OrdersDetails/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdersDetailsEdit(long id, [FromForm] OrderDetails orderDetails)
        {
            if (id != orderDetails.OrderDetailsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                await orderDetailsApi.UpdateRecord(id, orderDetails);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(FundingRequestsEdit), orderDetails);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(OrdersDetails), new { id = orderDetails.FkOrdersId });
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/OrdersDetails/Delete/{id}")]
        public async Task<IActionResult> OrderDetailsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var orderDetails = new OrderDetails();
            try
            {
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                orderDetails = await orderDetailsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (orderDetails == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(orderDetails);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="orderDetailsId">The order details identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/OrdersDetails/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdersDetailsDeleteConfirmed(long orderDetailsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                await orderDetailsApi.DeleteRecord(orderDetailsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(OrderDetailsDelete), new { id = orderDetailsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(Orders));
        }
        #endregion

        #region Shopping Cart Records
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> ShoppingCartRecords()
        {
            var shoppingCartRecords = new List<ShoppingCartRecords>();
            try
            {
                var shoppingCartRecordsApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                shoppingCartRecords = await shoppingCartRecordsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(shoppingCartRecords);
        }
        #endregion

        #region Funding Requests
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> FundingRequests(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var fundingRequests = new FundingRequests();
            try
            {
                var fundingRequestsApi = new FundingRequestsApi(this._httpClientInstance);
                fundingRequests = await fundingRequestsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(fundingRequests);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/FundingRequests/Edit/{id}")]
        public async Task<IActionResult> FundingRequestsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var fundingRequest = new FundingRequests();
            try
            {
                var fundingRequestsApi = new FundingRequestsApi(this._httpClientInstance);
                fundingRequest = await fundingRequestsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (fundingRequest == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(fundingRequest);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="fundingRequests">Thefunding requests.</param>
        /// <returns>Save changes for fundingRequest</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/FundingRequests/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundingRequestsEdit(long id, [FromForm] FundingRequests fundingRequest)
        {
            if (id != fundingRequest.FundingRequestId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                fundingRequest.UpdateDate = DateTime.Now;
                var fundingRequestsApi = new FundingRequestsApi(this._httpClientInstance);
                await fundingRequestsApi.UpdateRecord(id, fundingRequest);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(FundingRequestsEdit), fundingRequest);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(FundingRequests), new { id = id });
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/FundingRequests/Create")]
        public IActionResult FundingRequestsCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="fundingRequest">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/FundingRequests/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundingRequestsCreate([FromForm] FundingRequests fundingRequest)
        {
            var fundingRequests = new FundingRequests();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var fundingRequestsApi = new FundingRequestsApi(this._httpClientInstance);
                fundingRequests = await fundingRequestsApi.CreateRecord(fundingRequest);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (fundingRequests.FundingRequestId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(fundingRequest);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(FundingRequests));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/FundingRequests/Delete/{id}")]
        public async Task<IActionResult> FundingRequestsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var fundingRequest = new FundingRequests();
            try
            {
                var fundingRequestsApi = new FundingRequestsApi(this._httpClientInstance);
                fundingRequest = await fundingRequestsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (fundingRequest == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(fundingRequest);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="fundingRequestsId">Thefunding requests identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/FundingRequests/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FundingRequestsDeleteConfirmed(long fundingRequestsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var fundingRequestsApi = new FundingRequestsApi(this._httpClientInstance);
                await fundingRequestsApi.DeleteRecord(fundingRequestsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(FundingRequestsDelete), new { id = fundingRequestsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(FundingRequests));
        }
        #endregion

        #endregion

        #region Interested Customers
        /// <summary>
        /// Interesteds the customers.
        /// </summary>
        /// <returns>Interested customers View</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> InterestedCustomers()
        {
            var interestedCustomers = new List<InterestedCustomers>();
            try
            {
                var interestedCustomersApi = new InterestedCustomersApi(this._httpClientInstance);
                interestedCustomers = await interestedCustomersApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(interestedCustomers);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InterestedCustomers/Edit/{id}")]
        public async Task<IActionResult> InterestedCustomersEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var interestedCustomer = new InterestedCustomers();
            try
            {
                var interestedCustomersApi = new InterestedCustomersApi(this._httpClientInstance);
                interestedCustomer = await interestedCustomersApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (interestedCustomer == null)
            {
                return this.View("NotFoundAdmin");
            }

            ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress", interestedCustomer.Headquarter);
            return this.View(interestedCustomer);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="interestedCustomers">The body styles.</param>
        /// <returns>Save changes for interestedCustomer</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InterestedCustomers/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InterestedCustomersEdit(long id, [FromForm] InterestedCustomers interestedCustomer)
        {
            if (id != interestedCustomer.InterestedCustomersId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                if ((interestedCustomer.Managed ?? 0) == 1)
                {
                    interestedCustomer.ManagedOn = DateTime.Now;
                }

                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var interestedCustomersApi = new InterestedCustomersApi(this._httpClientInstance);
                await interestedCustomersApi.UpdateRecord(id, interestedCustomer);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(InterestedCustomersEdit), interestedCustomer);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(InterestedCustomers));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/InterestedCustomers/Delete/{id}")]
        public async Task<IActionResult> InterestedCustomersDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var interestedCustomer = new InterestedCustomers();
            try
            {
                var interestedCustomersApi = new InterestedCustomersApi(this._httpClientInstance);
                interestedCustomer = await interestedCustomersApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (interestedCustomer == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(interestedCustomer);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="interestedCustomersId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/InterestedCustomers/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InterestedCustomersDeleteConfirmed(long interestedCustomersId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var interestedCustomersApi = new InterestedCustomersApi(this._httpClientInstance);
                await interestedCustomersApi.DeleteRecord(interestedCustomersId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(InterestedCustomersDelete), new { id = interestedCustomersId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(InterestedCustomers));
        }
        #endregion

        #region Campaign Leads
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> CampaignLeads()
        {
            var campaignLeads = new List<CampaignLeads>();
            try
            {
                var campaignLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
                campaignLeads = await campaignLeadsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(campaignLeads);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/CampaignLeads/Edit/{id}")]
        public async Task<IActionResult> CampaignLeadsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var campaignLead = new CampaignLeads();
            try
            {
                var campaignLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
                campaignLead = await campaignLeadsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (campaignLead == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(campaignLead);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="campaignLeads">The body styles.</param>
        /// <returns>Save changes for campaignLead</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/CampaignLeads/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CampaignLeadsEdit(long id, [FromForm] CampaignLeads campaignLead)
        {
            if (id != campaignLead.CampaignLeadsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var campaignLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
                await campaignLeadsApi.UpdateRecord(id, campaignLead);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(CampaignLeadsEdit), campaignLead);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(CampaignLeads));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/CampaignLeads/Create")]
        public IActionResult CampaignLeadsCreate()
        {
            return this.View();
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <param name="campaignLead">The body style.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/CampaignLeads/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CampaignLeadsCreate([FromForm] CampaignLeads campaignLead)
        {
            var campaignLeads = new CampaignLeads();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var campaignLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
                campaignLeads = await campaignLeadsApi.CreateRecord(campaignLead);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (campaignLeads.CampaignLeadsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(campaignLead);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(CampaignLeads));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/CampaignLeads/Delete/{id}")]
        public async Task<IActionResult> CampaignLeadsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var campaignLead = new CampaignLeads();
            try
            {
                var campaignLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
                campaignLead = await campaignLeadsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (campaignLead == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(campaignLead);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="campaignLeadsId">The body styles identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/CampaignLeads/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CampaignLeadsDeleteConfirmed(long campaignLeadsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var campaignLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
                await campaignLeadsApi.DeleteRecord(campaignLeadsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(CampaignLeadsDelete), new { id = campaignLeadsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(CampaignLeads));
        }
        #endregion

        #region Newsletters Subscriptions
        /// <summary>
        /// Bodies the styles.
        /// </summary>
        /// <returns>Body styles view list</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> NewsletterSubscriptions()
        {
            var newsLetterSubscriptions = new List<NewsLetterSubscriptions>();
            try
            {
                var newsletterSubscriptionsApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                newsLetterSubscriptions = await newsletterSubscriptionsApi.GetAllRecords();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(newsLetterSubscriptions);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Body styles Edit view</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/NewsletterSubscriptions/Edit/{id}")]
        public async Task<IActionResult> NewsletterSubscriptionsEdit(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var newsLetterSubscriptions = new NewsLetterSubscriptions();
            try
            {
                var newsletterSubscriptionsApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                newsLetterSubscriptions = await newsletterSubscriptionsApi.GetRecord(id ?? 0);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (newsLetterSubscriptions == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(newsLetterSubscriptions);
        }

        /// <summary>
        /// Bodies the styles edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="newsLetterSubscription">The news letter subscription.</param>
        /// <returns>
        /// Save changes for newsLetterSubscription
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/NewsletterSubscriptions/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsletterSubscriptionsEdit(long id, [FromForm] NewsLetterSubscriptions newsLetterSubscription)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(NewsletterSubscriptionsEdit), newsLetterSubscription);
            }

            if (id != newsLetterSubscription.NewsletterSubscriptionsId)
            {
                return this.View("NotFoundAdmin");
            }

            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var bodyStylesApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                await bodyStylesApi.UpdateRecord(id, newsLetterSubscription);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(NewsletterSubscriptionsEdit), newsLetterSubscription);
            }

            TempData["EditOk"] = StaticResources.EditOk;
            return this.RedirectToAction(nameof(NewsletterSubscriptions));
        }

        /// <summary>
        /// Bodies the styles create.
        /// </summary>
        /// <returns>Create view for Body Styles model</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/NewsletterSubscriptions/Create")]
        public IActionResult NewsletterSubscriptionsCreate()
        {
            return this.View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/NewsletterSubscriptions/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsletterSubscriptionsCreate([FromForm] NewsLetterSubscriptions newsLetterSubscription)
        {
            var newsLetterSubscriptions = new NewsLetterSubscriptions();
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var newsletterSubscriptionsApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                newsLetterSubscriptions = await newsletterSubscriptionsApi.CreateRecord(newsLetterSubscription);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (newsLetterSubscriptions.NewsletterSubscriptionsId <= 0)
            {
                ModelState.AddModelError(string.Empty, "No se ha podido almacenar el registro. Intenta Nuevamente.");
                return this.View(newsLetterSubscription);
            }

            TempData["CreateOk"] = StaticResources.CreateOk;
            return this.RedirectToAction(nameof(NewsletterSubscriptions));
        }

        /// <summary>
        /// Bodies the styles delete.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete view with Body style Data</returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/NewsletterSubscriptions/Delete/{id}")]
        public async Task<IActionResult> NewsletterSubscriptionsDelete(long? id)
        {
            if (id == null)
            {
                return this.View("NotFoundAdmin");
            }

            var newsLetterSubscriptions = new NewsLetterSubscriptions();
            try
            {
                var newsletterSubscriptionsApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                newsLetterSubscriptions = await newsletterSubscriptionsApi.GetRecord(id ?? 0);
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            if (newsLetterSubscriptions == null)
            {
                return this.View("NotFoundAdmin");
            }

            return this.View(newsLetterSubscriptions);
        }

        /// <summary>
        /// Bodies the styles delete confirmed.
        /// </summary>
        /// <param name="newsletterSubscriptionsId">The newsletter subscriptions identifier identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("[controller]/NewsletterSubscriptions/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsletterSubscriptionsDeleteConfirmed(long newsletterSubscriptionsId)
        {
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var newsletterSubscriptionsApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                await newsletterSubscriptionsApi.DeleteRecord(newsletterSubscriptionsId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(NewsletterSubscriptionsDelete), new { id = newsletterSubscriptionsId });
            }

            TempData["DeleteOk"] = StaticResources.DeleteOk;
            return RedirectToAction(nameof(NewsletterSubscriptions));
        }
        #endregion

        #region SEO
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult SEO()
        {
            return this.View();
        }
        #endregion

        #region User Admin
        /// <summary>
        /// Users the manager.
        /// </summary>
        /// <returns>User manager admin view</returns>
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UserManager()
        {
            var userMangerViewModel = new List<UserManagerViewModel>();
            try
            {
                var users = await this._userManager.Users
                                      .AsNoTracking()
                                      .OrderByDescending(x => x.RegisterDate)
                                      .ToListAsync();

                foreach (var user in users)
                {
                    userMangerViewModel.Add(
                        new UserManagerViewModel()
                        {
                            ApplicationUser = user,
                            Roles = await this._userManager.GetRolesAsync(user)
                        });
                }

                return this.View(userMangerViewModel);
            }
            catch (Exception ex)
            {
                return this.View(userMangerViewModel);
            }

        }

        /// <summary>
        /// Unlocks the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Unlock user result
        /// </returns>
        [HttpGet("[controller]/[action]/{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UnlockoutUser(string userId)
        {
            try
            {
                var user = await this._userManager.FindByIdAsync(userId);
                var result = await this._userManager.ResetAccessFailedCountAsync(user);
                user.LockoutEnd = null;
                await this._userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return this.Ok(new { Status = "Exception", Message = ex?.InnerException?.Message ?? ex.Message });
            }

            return this.Ok(new { Status = "Ok", Message = "Ok" });
        }

        /// <summary>
        /// Roles the manager.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Role manager view</returns>
        [HttpGet("[controller]/[action]/{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RoleManager(string userId)
        {
            var userRolesViewModel = new List<UsersViewModel>();
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await this._userManager.FindByIdAsync(userId);
                var rolesForUser = await this._userManager.GetRolesAsync(user);
                userRolesViewModel = new List<UsersViewModel>()
                {
                    new UsersViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.FullName,
                        Roles = rolesForUser.ToList()
                    }
                };

                return this.View(userRolesViewModel);
            }

            return this.View(userRolesViewModel);
        }

        /// <summary>
        /// Sets the user role.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>User role view</returns>
        [HttpGet("[controller]/[action]/{idUser}/Role/{roleName}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> UserRoleManager(string idUser, string roleName = null)
        {
            var user = await this._userManager.FindByIdAsync(idUser);
            var userRoleViewModel = new UserRolesViewModel();
            ViewBag.IsEdit = false;
            if (!string.IsNullOrEmpty(roleName))
            {
                ViewBag.IsEdit = true;
                var role = this._roleManager.Roles.AsNoTracking().FirstOrDefault(x => x.Name == roleName);
                userRoleViewModel.IdRole = role.Id;
            }

            userRoleViewModel.IdUser = long.Parse(idUser);
            userRoleViewModel.UserName = user.UserName;
            userRoleViewModel.Roles = this._roleManager.Roles.ToList();
            return this.View(userRoleViewModel);
        }

        /// <summary>
        /// Adds the user role.
        /// </summary>
        /// <param name="userRolesViewModel">The user role view model.</param>
        /// <returns>Add user to role</returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> SetUserRole([FromBody] UserRolesViewModel userRolesViewModel)
        {
            var isEdit = true;
            if (userRolesViewModel.IdRole <= 0)
            {
                ModelState.Remove(nameof(UserRolesViewModel.IdRole));
                isEdit = false;
            }

            if (!ModelState.IsValid)
            {
                return this.Ok(new { Status = "ModelError", Message = "Debes seleccionar un role" });
            }

            try
            {
                if (isEdit)
                {
                    ////Delete old role
                    var user = await this._userManager.FindByIdAsync(userRolesViewModel.IdUser.ToString());
                    var oldRole = await this._roleManager.FindByIdAsync(userRolesViewModel.IdRole.ToString());
                    var newRole = await this._roleManager.FindByIdAsync(userRolesViewModel.IdRoleNew.ToString());
                    await this._userManager.RemoveFromRoleAsync(user, oldRole.Name);

                    ////Set new role
                    await this._userManager.AddToRoleAsync(user, newRole.Name);
                }
                else
                {
                    var user = await this._userManager.FindByIdAsync(userRolesViewModel.IdUser.ToString());
                    var newRole = await this._roleManager.FindByIdAsync(userRolesViewModel.IdRoleNew.ToString());
                    ////Set new role
                    await this._userManager.AddToRoleAsync(user, newRole.Name);
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { Status = "Exception", Message = ex?.InnerException?.Message ?? ex.Message });
            }

            return this.Ok(new { Status = "Ok" });
        }
        #endregion

        #region Common
        /// <summary>
        /// Gets the colors for product.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns></returns>
        [HttpGet("[controller]/[action]/{productsId}")]
        public async Task<IActionResult> GetColorsForProduct(long productsId)
        {
            var colors = new List<Colors>();
            try
            {
                var colorsApi = new ColorsApi(this._httpClientInstance);
                colors = await colorsApi.GetForProduct(productsId);
            }
            catch (Exception ex)
            {
                return this.Json(colors);
            }

            return this.Json(colors);
        }

        /// <summary>
        /// Gets the colors for product.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>File</returns>
        [AllowAnonymous]
        [HttpGet("[controller]/[action]/{id}")]
        public async Task<FileResult> DownloadFile(long id)
        {
            try
            {
                var productDocumentApi = new ProductDocumentsApi(this._httpClientInstance);
                var productDocument = await productDocumentApi.GetRecord(id);
                var fullPath = Path.Combine(this._host.WebRootPath, productDocument.FilePath);

                var fileBytes = System.IO.File.ReadAllBytes(fullPath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, productDocument.FileName);
            }
            catch (Exception ex)
            {
                return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, string.Empty);
            }
        }

        /// <summary>
        /// Creates the excel report campaing leads.
        /// </summary>
        /// <param name="report">The report.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Report
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/[action]/{report}/{type}")]
        public async Task<IActionResult> GetReport(string report, string type) 
        {
            try
            {
                switch (type)
                {
                    /////////////////// Excel Reports ///////////////////
                    case "Excel":
                        var fileName = $"TiendaMotovalle_{report}_{DateTime.Now:MMddyyyyHHmmss}.xlsx";
                        byte[] Excelreport;
                        switch (report)
                        {
                            case "CampaingLeads":
                                Excelreport = await this._reportHelper.GetCampaingLeads();
                                return File(Excelreport, "application/unknown", fileName);
                            default:
                                return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, string.Empty);
                        }

                    /////////////////// Other formats Reports ///////////////////
                    default:
                        return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, string.Empty);
                } 
            }
            catch (Exception ex)
            {
                return File(new byte[0], System.Net.Mime.MediaTypeNames.Application.Octet, "Error");
            }
        }

        /// <summary>
        /// Gets the index of the next maintenance.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns></returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("[controller]/[action]/{productsId}")]
        public async Task<IActionResult> GetNextMaintenanceIndex(int productsId)
        {
            var productMaintenancesApi = new ProductMaintenancesApi(this._httpClientInstance);
            var maintenances = await productMaintenancesApi.GetRecordsByProduct(productsId);
            var maxIndex = maintenances.Select(x => x.MaintenanceIndex).DefaultIfEmpty(0).Max() + 1;
            return this.Ok(maxIndex);
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// Token user
        /// </returns>
        private async Task<string> GetUserToken(ClaimsPrincipal userClaim, IConfiguration configuration)
        {
            var user = await this._userManager.GetUserAsync(userClaim);
            var token = await this._userManager.GetAuthenticationTokenAsync(user, configuration.GetSection("JwtSettings").GetValue<string>("Issuer"), configuration.GetSection("JwtSettings").GetValue<string>("Subject"));
            return token;
        }

        /// <summary>
        /// Sets the custom fonts.
        /// </summary>
        private void SetCustomFonts()
        {
            object font1 = new { text = "Segoe UI", value = "Segoe UI" };
            object font2 = new { text = "Arial", value = "Arial,Helvetica,sans-serif" };
            object font3 = new { text = "Courier New", value = "Courier New,Courier,monospace" };
            object font4 = new { text = "Georgia", value = "Georgia,serif" };
            object font5 = new { text = "Impact", value = "Impact,Charcoal,sans-serif" };
            object font6 = new { text = "Calibri Light", value = "CalibriLight" };
            object font7 = new { text = "Ford Font", value = "ford-font" };
            object font8 = new { text = "Mazda Font", value = "mazda-font" };
            object font9 = new { text = "Masey Ferguson Font", value = "massey-font" };
            ViewBag.Fonts = new[] { font1, font2, font3, font4, font5, font6, font7, font8, font9 };
        }
        #endregion
    }
}