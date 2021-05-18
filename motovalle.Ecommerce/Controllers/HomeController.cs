// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Home Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Home Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [AllowAnonymous]
    public class HomeController : Controller
    {
        #region Ctor
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
        /// The email sender
        /// </summary>
        private readonly IEmailSenderExtended _emailSender;

        /// <summary>
        /// The slider go to
        /// </summary>
        private readonly int _sliderGoTo;

        /// <summary>
        /// The email template
        /// </summary>
        private readonly string _emailTemplate;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="blogRepository">The blog repository.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="host">The host.</param>
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
                              IConfiguration configuration, IEmailSenderExtended emailSender, IWebHostEnvironment host)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._emailSender = emailSender;
            this._sliderGoTo = configuration.GetSection("SlideGoTo").GetValue<int>("PostId");
            this._emailTemplate = $"{host.WebRootPath}{configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateGeneric")}";
            this._configuration = configuration;
        }
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index view</returns>
        public IActionResult Index()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }


        /// <summary>
        /// Privacies this instance.
        /// </summary>
        /// <returns>Privacy View</returns>
        public IActionResult Privacy()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }

        /// <summary>
        /// Legals the information.
        /// </summary>
        /// <returns>View</returns>
        [Route("[controller]/Privacy/politicas-e-información-legal")]
        [HttpGet]
        public IActionResult LegalInformation()
        {
            return this.View();
        }

        /// <summary>
        /// Privacies this instance.
        /// </summary>
        /// <returns>Privacy View</returns>
        public IActionResult TestPage()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }

        /// <summary>
        /// Creates the subscription.
        /// </summary>
        /// <param name="subscriptionViewModel">The subscription view model.</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public async Task<IActionResult> Index(SubscriptionViewModel subscriptionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(new { State = "ModelError", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            try
            {
                var newsletterSubscriptionApi = new NewsletterSubscriptionsApi(this._httpClientInstance);
                var newsletterSubscription = await newsletterSubscriptionApi.GetRecord(subscriptionViewModel.Email);
                if (string.IsNullOrEmpty(newsletterSubscription.Email))
                {
                    var newNewsletterSubscription = new NewsLetterSubscriptions()
                    {
                        Email = subscriptionViewModel.Email,
                        Name = subscriptionViewModel.Name
                    };

                    var newsletterSubscriptionCreated = await newsletterSubscriptionApi.CreateRecord(newNewsletterSubscription);
                    if (newsletterSubscriptionCreated.NewsletterSubscriptionsId > 0)
                    {
                        var emailCopy = string.IsNullOrEmpty(this._configuration.GetSection("NewslettersSubscription").GetValue<string>("EmailToCc")) ? this._configuration.GetSection("NewslettersSubscription").GetValue<string>("EmailTo") : this._configuration.GetSection("NewslettersSubscription").GetValue<string>("EmailToCc");
                        await this._emailSender.SendEmailAsync(email: this._configuration.GetSection("NewslettersSubscription").GetValue<string>("EmailTo"),
                                                               subject: "Nueva suscripción registrada",
                                                               bodyMessage: this.EmailBuilderNewsletterSuscription(newNewsletterSubscription),
                                                               attachmentPaths: null,
                                                               emailsToBcc: null,
                                                               emailsToCc: new List<string>() { emailCopy });

                        return this.Json(new { State = "Ok" });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.Json(new { State = "Error" });
            }

            return this.Json(new { State = "Exists" });
        }

        /// <summary>
        /// Creates the interested customer record.
        /// </summary>
        /// <param name="interestedCustomers">The subscription view model.</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public async Task<IActionResult> InterestedCustomers(InterestedCustomers interestedCustomers)
        {
            if (string.IsNullOrEmpty(interestedCustomers.Email) && string.IsNullOrEmpty(interestedCustomers.PhoneNumber))
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar el Teléfono o el Email de contacto.");
            }

            if (!ModelState.IsValid)
            {
                return this.Json(new { State = "ModelError", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            try
            {
                var interestedCustomersApi = new InterestedCustomersApi(this._httpClientInstance);
                interestedCustomers.CreateOn = DateTime.Now;
                interestedCustomers.Managed = 0;
                var interestedCustomersCreated = await interestedCustomersApi.CreateRecord(interestedCustomers);
                await Task.Delay(500);
                if (interestedCustomersCreated.InterestedCustomersId > 0)
                {
                    var interestedCustomer = await interestedCustomersApi.GetRecord(interestedCustomersCreated.InterestedCustomersId);
                    var emailCopy = string.IsNullOrEmpty(this._configuration.GetSection("InterestedCustomers").GetValue<string>("EmailToCc")) ? this._configuration.GetSection("InterestedCustomers").GetValue<string>("EmailTo") : this._configuration.GetSection("InterestedCustomers").GetValue<string>("EmailToCc");
                    await this._emailSender.SendEmailAsync(email: this._configuration.GetSection("InterestedCustomers").GetValue<string>("EmailTo"),
                                                              subject: "Nuevo cliente interesado registrado",
                                                              bodyMessage: this.EmailBuilderInterestedCustomers(interestedCustomer),
                                                              attachmentPaths: null,
                                                              emailsToBcc: null,
                                                              emailsToCc: new List<string>() { emailCopy });

                    return this.Json(new { State = "Ok" });
                }

            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.Json(new { State = "Error" });
        }

        #region Extention Methods
        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoriesId">The categories identifier.</param>
        /// <returns>
        /// Products for category in Json format
        /// </returns>
        [HttpGet]
        [Route("[controller]/make/{makesId}/category/{categoriesId}")]
        public async Task<IActionResult> GetModelsForMakeAndCategory(int makesId, int categoriesId)
        {
            var modelsForMakeAndCategory = new List<ModelAndCategoryBase>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                modelsForMakeAndCategory = await makesApi.GetModelsForMakeAndCategory(makesId, categoriesId);
                return this.Json(modelsForMakeAndCategory);
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.Json(modelsForMakeAndCategory);
        }

        /// <summary>
        /// Gets the models for make.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// Models for makes in Json format
        /// </returns>
        [HttpGet]
        [Route("[controller]/make/{makesId}/models")]
        public async Task<IActionResult> GetModelsForMake(int makesId)
        {
            var modelsForMakeAndCategory = new List<ModelAndCategoryBase>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                modelsForMakeAndCategory = await makesApi.GetModelsForMake(makesId, 0);
                return this.Json(modelsForMakeAndCategory);
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.Json(modelsForMakeAndCategory);
        }

        /// <summary>
        /// Gets the products for make.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>List of products for Make</returns>
        [HttpGet]
        [Route("[controller]/make/{makesId}/products")]
        public async Task<IActionResult> GetProductsForMake(int makesId)
        {
            var products = new List<Products>();
            try
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                products = await productsApi.GetAllRecords();
                products = products.Where(x => x.FkMakesId == makesId).ToList();
                return this.Json(products);
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.Json(products);
        }

        /// <summary>
        /// Gets the models for make.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// Models for makes in Json format
        /// </returns>
        [HttpGet]
        [Route("[controller]/make/{makesId}/models/{modelsId}/category")]
        public async Task<IActionResult> GetCategoryForModel(long makesId, long modelsId)
        {
            var categories = new Categories();
            try
            {
                var categoriesApi = new CategoriesApi(this._httpClientInstance);
                categories = await categoriesApi.GetForMakeAndModel(makesId, modelsId);
                return this.Json(categories);
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.Json(categories);
        }

        /// <summary>
        /// Recordses the per page.
        /// </summary>
        /// <param name="basicSearchViewModel">The basic search view model.</param>
        /// <returns>
        /// Records taked for make, category and model on Json format
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> RecordsPerPage([FromBody] BasicSearchViewModel basicSearchViewModel)
        {
            var makesApi = new MakesApi(this._httpClientInstance);
            var productsListTotal = await makesApi.GetProductAndMakeBase(basicSearchViewModel.MakeId, basicSearchViewModel.CategoryId, basicSearchViewModel.ModelId,
                                                basicSearchViewModel.YearMin, basicSearchViewModel.YearMax, basicSearchViewModel.PriceRangeMin, basicSearchViewModel.PriceRangeMax);

            productsListTotal = productsListTotal.Where(x => x.QuantityInStock > 0 && x.AllowShow > 0).ToList();
            productsListTotal = basicSearchViewModel.SortBy switch
            {
                0 => productsListTotal.OrderByDescending(x => x.IsFeatured).ToList(),
                1 => productsListTotal.OrderBy(x => x.SalesPrice).ToList(),
                2 => productsListTotal.OrderByDescending(x => x.SalesPrice).ToList(),
                _ => productsListTotal,
            };

            var productsTaked = productsListTotal.Take(basicSearchViewModel.RecordsPerPage).ToList();
            int pagesCount = Convert.ToInt32(Math.Ceiling((double)productsListTotal.Count() / basicSearchViewModel.RecordsPerPage));

            ////////////// To Get The first Inventory record and show it
            var make = await makesApi.GetRecord(basicSearchViewModel.MakeId);
            if (make.MakeName.Contains("carshow", StringComparison.OrdinalIgnoreCase))
            {
                var productInventoryListTaked = new List<InventoryForProduct>();
                foreach (var item in productsTaked)
                {
                    var productsApi = new ProductsApi(this._httpClientInstance);
                    var inventoryItems = await productsApi.GetInventoryForProduct(item.ProductId, make.MakesId);
                    var inventoryItem = inventoryItems.FirstOrDefault();
                    if (inventoryItem != null)
                    {
                        productInventoryListTaked.Add(inventoryItem);
                    }
                }

                return this.Json(new { Items = productInventoryListTaked, PagesCount = pagesCount });
            }
            ///////////////////

            var Result = new { Items = productsTaked, PagesCount = pagesCount };
            return this.Json(Result);
        }

        /// <summary>
        /// Paginations the specified records per page.
        /// </summary>
        /// <param name="basicSearchViewModel">The basic search view model.</param>
        /// <returns>
        /// Records per page selected
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Pagination([FromBody] BasicSearchViewModel basicSearchViewModel)
        {
            var makesApi = new MakesApi(this._httpClientInstance);
            var productsListTotal = await makesApi.GetProductAndMakeBase(basicSearchViewModel.MakeId, basicSearchViewModel.CategoryId, basicSearchViewModel.ModelId,
                                    basicSearchViewModel.YearMin, basicSearchViewModel.YearMax, basicSearchViewModel.PriceRangeMin, basicSearchViewModel.PriceRangeMax);

            productsListTotal = productsListTotal.Where(x => x.QuantityInStock > 0 && x.AllowShow > 0).ToList();
            productsListTotal = basicSearchViewModel.SortBy switch
            {
                0 => productsListTotal.OrderByDescending(x => x.IsFeatured).ToList(),
                1 => productsListTotal.OrderBy(x => x.SalesPrice).ToList(),
                2 => productsListTotal.OrderByDescending(x => x.SalesPrice).ToList(),
                _ => productsListTotal,
            };

            var productsTaked = productsListTotal.Skip(basicSearchViewModel.RecordsPerPage * (basicSearchViewModel.PageNumber - 1)).Take(basicSearchViewModel.RecordsPerPage).ToList();
            int pagesCount = Convert.ToInt32(Math.Ceiling((double)productsListTotal.Count() / basicSearchViewModel.RecordsPerPage));

            ////////////// To Get The first Inventory record and show it
            var make = await makesApi.GetRecord(basicSearchViewModel.MakeId);
            if (make.MakeName.Contains("carshow", StringComparison.OrdinalIgnoreCase))
            {
                var productInventoryListTaked = new List<InventoryForProduct>();
                foreach (var item in productsTaked)
                {
                    var productsApi = new ProductsApi(this._httpClientInstance);
                    var inventoryItems = await productsApi.GetInventoryForProduct(item.ProductId, make.MakesId);
                    var inventoryItem = inventoryItems.FirstOrDefault();
                    if (inventoryItem != null)
                    {
                        productInventoryListTaked.Add(inventoryItem);
                    }
                }

                return this.Json(new { Items = productInventoryListTaked, PagesCount = pagesCount });
            }
            ///////////////////

            var Result = new { Items = productsTaked, PagesCount = pagesCount };
            return this.Json(Result);
        }

        /// <summary>
        /// Gets the hero component.
        /// </summary>
        /// <returns>Hero Component</returns>
        [HttpGet("[controller]/[action]/{makesName}")]
        public IActionResult GetHeroComponent(string makesName)
        {
            return ViewComponent("HeroImages", makesName);
        }

        /// <summary>
        /// Gets the latest post home component.
        /// </summary>
        /// <returns>Latest Post Home Component</returns>
        public IActionResult GetLatestPostHomeComponent()
        {
            return ViewComponent("LatestPostsHome");
        }

        /// <summary>
        /// Emails the builder newsletter suscription.
        /// </summary>
        /// <param name="newsLetterSubscriptions">The news letter subscriptions.</param>
        /// <returns>Body string</returns>
        private string EmailBuilderNewsletterSuscription(NewsLetterSubscriptions newsLetterSubscriptions)
        {
            var emailfullPath = this._emailTemplate;
            var logoUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var content = @$"<div><b>Fecha de registro:</b> {DateTime.Now:dddd, dd MMMM yyyy HH:mm:ss}</div>
                             <div><b>Nombre:</b> {newsLetterSubscriptions.Name}</div>  
                             <div><b>Contacto:</b> {newsLetterSubscriptions.Email} </div>";
            var contentTemplate = new StreamReader(emailfullPath)
                .ReadToEnd()
                .Replace("${logoUrl}", logoUrl)
                .Replace("${firstLabel}", "Hemos registrado una nueva <b>suscripción</b>.")
                .Replace("${content}", content)
                .Replace("${Date}", DateTime.Now.Year.ToString());

            return contentTemplate;
        }

        /// <summary>
        /// Emails the builder interested customers.
        /// </summary>
        /// <param name="interestedCustomers">The interested customers.</param>
        /// <returns>string body content</returns>
        private string EmailBuilderInterestedCustomers(InterestedCustomers interestedCustomers)
        {
            var emailfullPath = this._emailTemplate;
            var makeId = interestedCustomers.FkInventoryItems.FkProducts.FkMakesId;
            var retakeOrOther = makeId == 3 ? "Estoy Interesado en" : "Retoma";
            var logoUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var content = @$"<div><b>Fecha de registro:</b> {interestedCustomers.CreateOn:dddd, dd MMMM yyyy HH:mm:ss}</div>
                             <div><b>Nombre:</b> {interestedCustomers.Name}</div>  
                             <div><b>Correo:</b> {interestedCustomers.Email}</div>
                             <div><b>Teléfono:</b> {interestedCustomers.PhoneNumber}</div>
                             <div><b>Producto:</b> {interestedCustomers.FkInventoryItems.FkProducts.ProductName}</div>
                             <div><b>Color:</b> {interestedCustomers.FkInventoryItems.FkColors.ColorName}</div>
                             <div><b>{retakeOrOther}:</b> {interestedCustomers.Retake}</div>
                             <div><b>Sede de atención:</b> {interestedCustomers.Headquarter}</div>";
            var contentTemplate = new StreamReader(emailfullPath)
                .ReadToEnd()
                .Replace("${logoUrl}", logoUrl)
                .Replace("${firstLabel}", "Hemos registrado un nuevo <b>cliente interesado</b>.")
                .Replace("${content}", content)
                .Replace("${Date}", DateTime.Now.Year.ToString());

            return contentTemplate;
        }
        #endregion

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns>Error View</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.PostsFilter = "General";
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Landing Page Forwarding

        [Route("sobre-motovalle")]
        public IActionResult sobremotovalle()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "Info");
        }

        [Route("/ford/home")]
        public IActionResult fordhome()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "Ford");
        }

        [Route("/mazda/home")]
        public IActionResult mazdahome()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "Mazda");
        }

        [Route("/massey/home")]
        public IActionResult masseyhome()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "MasseyFerguson");
        }

        [Route("/usados/home")]
        public IActionResult usadoshome()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "Carshow");
        }

        [Route("/sobre-motovalle/sedes")]
        public IActionResult Sedes()
        {
            return RedirectToActionPermanent(actionName: "Sedes", controllerName: "Info");
        }

        [Route("/sobre-motovalle/trabaje-con-nosotros")]
        public IActionResult TrabajeConNosotros()
        {
            return RedirectToActionPermanent(actionName: "TrabajeConNosotros", controllerName: "Info");
        }

        [Route("/agencia-de-seguros")]
        public IActionResult AgenciaSeguros()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "agencia-de-seguros" });
        }

        [Route("/contact-center-motovalle")]
        public IActionResult ContactCenterMotovalle()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "contact-center-motovalle" });
        }

        [Route("/politica-y-manejo-de-datos")]
        public IActionResult Politica()
        {
            return RedirectToActionPermanent(actionName: "Privacy", controllerName: "Home");
        }

        [Route("/repuestos-2")]
        public IActionResult Repuestos2()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "repuestos-2" });
        }

        [Route("/mazda/vehiculos")]
        public IActionResult MazdaVehiculos()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "Mazda");
        }

        [Route("/ford/vehiculos")]
        public IActionResult FordVehiculos()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "Ford");
        }

        [Route("/massey/massey-ferguson")]
        public IActionResult Massey()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "MasseyFerguson");
        }

        [Route("/massey/massey-ferguson/filosofia-de-massey-ferguson")]
        public IActionResult FilosofiaMassey()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "massey/massey-ferguson/filosofia-de-massey-ferguson" });
        }

        [Route("/massey/massey-ferguson/valores-de-la-marca")]
        public IActionResult MasseyValoresDeMarca()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "massey/massey-ferguson/valores-de-la-marca" });
        }

        [Route("/massey/cotizacion")]
        public IActionResult MasseyQuote()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "massey/cotizacion" });
        }

        [Route("/massey/sedes")]
        public IActionResult MasseySedes()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "massey/sedes" });
        }

        [Route("/massey/club-de-operadores-massey")]
        public IActionResult MasseyOperatorClub()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "massey/club-de-operadores-massey" });
        }

        [Route("/massey/contactenos")]
        public IActionResult MasseyContactenos()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "massey/contactenos" });
        }

        [Route("/mazda/servicios/agendamiento-de-citas")]
        public IActionResult MazdaCitas()
        {
            return Redirect("https://capnet2.ddns.net:3000/Mazda_Motovalle/csl/cita-servicio/nueva");
        }

        [Route("/massey")]
        public IActionResult MasseyHome()
        {
            return RedirectToActionPermanent(actionName: "Index", controllerName: "MasseyFerguson");
        }

        //mazda/servicios/servicio
        [Route("/mazda/servicios/servicio")]
        public IActionResult ServiciosServicios()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/servicios/servicio" });
        }

        [Route("/mazda/servicios")]
        public IActionResult Servicios()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/servicios" });
        }

        [Route("/mazda/test-drive")]
        public IActionResult TestDrive()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/test-drive" });
        }

        [Route("/mazda/cotizacion")]
        public IActionResult Cotizacion()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/cotizacion" });
        }

        ///mazda/servicios/repuestos-y-accesorios/
        [Route("/mazda/servicios/repuestos-y-accesorios")]
        public IActionResult MazdaRepuestosAccesorios()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/servicios/repuestos-y-accesorios" });
        }
        
        [Route("/mazda/servicios/campanas-de-servicio")]
        public IActionResult MazdaCampanaServicios()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/servicios/campanas-de-servicio" });
        }

        [Route("/mazda/diseno-y-tecnologia")]
        public IActionResult MazdaDisenoTecnologia()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/diseno-y-tecnologia" });
        }

        [Route("/mazda/diseno-y-tecnologia/tecnologia")]
        public IActionResult MazdaTecnologia()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda/diseno-y-tecnologia/tecnologia" });
        }

        [Route("/mazda/blog")]
        public IActionResult MazdaBlog()
        {
            return RedirectToActionPermanent(actionName: "GetCategoryList", controllerName: "Blog", new { category = "Mazda" });
        }

        [Route("/mazda-blog/nueva-mazda-cx-5")]
        public IActionResult MazdaCX5Blog()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/nueva-mazda-cx-5" });
        }
        
        [Route("/mazda-blog/mazda-6")]
        public IActionResult Mazda6Blog()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-6" });
        }

        [Route("/mazda-blog/mazda-3")]
        public IActionResult Mazda3Blog()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-3" });
        }

        [Route("/mazda-blog/noticias")]
        public IActionResult MazdaNoticias()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/noticias" });
        }

        [Route("/mazda-blog/mazda-cx-3-el-vehiculo-mas-economico")]
        public IActionResult MazdaCX3VehiculoEconomico()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-cx-3-el-vehiculo-mas-economico" });
        }

        [Route("/mazda-blog/mazda-celebra-el-50o-aniversario-del")]
        public IActionResult Mazda50anniversario()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-celebra-el-50o-aniversario-del" });
        }

        [Route("/mazda-blog/mazda-la-mas-galardonada")]
        public IActionResult MazdaBlogGalaronada()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-la-mas-galardonada" });
        }

        [Route("/mazda-blog/mazda-mx-5-rf-recibe-el-premio")]
        public IActionResult MazdaBlogMX5Premio()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-mx-5-rf-recibe-el-premio" });
        }

        [Route("/mazda-blog/sistema-de-seguridad-pro-activo-i-activsense")]
        public IActionResult MazdaBlogActvisence()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/sistema-de-seguridad-pro-activo-i-activsense" });
        }

        [Route("/mazda-blog/innovaciones")]
        public IActionResult MazdaBlogInnovaciones()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/innovaciones" });
        }

        [Route("/mazda-blog/skyactiv")]
        public IActionResult MazdaBlogSkyactive()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/skyactiv" });
        }

        [Route("/mazda-blog/mazda-connect")]
        public IActionResult MazdaBlogMazdaConnect()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "mazda-blog/mazda-connect" });
        }

        [Route("/ford/solicita-tu-cotizacion")]
        public IActionResult FordCotizacion()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/solicita-tu-cotizacion" });
        }

        [Route("/ford/servicio-posventa")]
        public IActionResult FordServicioPosventa()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/servicio-posventa" });
        }

        [Route("/ford/servicio-posventa/servicio-integral")]
        public IActionResult FordServicioPosventaServicioIntegral()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/servicio-posventa/servicio-integral" });
        }

        [Route("/ford/servicio-posventa/campana-del-mes")]
        public IActionResult FordServicioPosventaCampanaMes()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/servicio-posventa/campana-del-mes" });
        }

        [Route("/ford/servicio-posventa/garantia")]
        public IActionResult FordServicioPosventaGarantia()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/servicio-posventa/garantia" });
        }

        [Route("/ford/asesoria-en-linea")]
        public IActionResult FordAsesoriaEnLinea()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/asesoria-en-linea" });
        }

        //[Route("/ford/test-drive")]
        //public IActionResult FordTestDrive()
        //{
        //    return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford/test-drive" });
        //}

        [Route("/ford-blog/llega-la-nueva-ford-ecosport-a-colombia")]
        public IActionResult FordBlogNewEcosport()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford-blog/llega-la-nueva-ford-ecosport-a-colombia" });
        }

        [Route("/ford-blog/nueva-ford-edge")]
        public IActionResult FordNewEdge()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford-blog/nueva-ford-edge" });
        }

        [Route("/ford-blog/los-tecnicos-ford-se-desafian-para-ser-los-mejores")]
        public IActionResult FordTecnicosDesafian()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford-blog/los-tecnicos-ford-se-desafian-para-ser-los-mejores" });
        }

        [Route("/ford-blog/nombramiento")]
        public IActionResult FordNombramiento()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "ford-blog/nombramiento" });
        }

        [Route("/usados/compramos-su-vehiculo")]
        public IActionResult UsadosCompramosSuBVehiculo()
        {
            return RedirectToActionPermanent(actionName: "Page", controllerName: "LandingPages", new { page = "usados/compramos-su-vehiculo" });
        }

        [Route("/mazda-catalog/mazda-2")]
        public IActionResult Mazda2Sport()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 6;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 14;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-6")]
        public IActionResult Mazda6()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 5;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 18;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-mx-5")]
        public IActionResult MazdaMX5()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 4;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 22;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }


        [Route("/mazda-catalog/mazda-2-sedan")]
        public IActionResult Mazda2Sedan()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 5;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 15;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-cx-3")]
        public IActionResult MazdaCX3()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 19;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-cx-30")]
        public IActionResult MazdaCX30()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 38;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-3-sport-nueva-generacion")]
        public IActionResult Mazda3Sport()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 6;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 17;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-cx-5")]
        public IActionResult MazdaCX5()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 20;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-3-sedan-nueva-generacion")]
        public IActionResult Mazda3Sedan()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 5;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 16;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/mazda-catalog/mazda-cx-9")]
        public IActionResult MazdaCX9()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 2;
            basicSearchViewModel.ModelId = 21;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Mazda", basicSearchViewModel);
        }

        [Route("/ford-catalog/nuevo-mustang-gt-premium-convertible")]
        public IActionResult FordMustangGTPremium()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 4;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 13;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/ecosport")]
        public IActionResult FordEcosport()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 7;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/explorer")]
        public IActionResult FordExplorer()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 9;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/expedition")]
        public IActionResult FordExpedition()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 10;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/escape")]
        public IActionResult FordEscape()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 8;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/ranger")]
        public IActionResult FordRanger()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 1;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 4;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/raptor")]
        public IActionResult FordRaptor()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 3;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 3;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/nuevo-ford-mustang-shelby-gt-350")]
        public IActionResult FordMustangShelbyGT350()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 3;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 2;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/edge-st")]
        public IActionResult FordEdgeST()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 6;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/fusion-hybrid")]
        public IActionResult FordFusionHybrid()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 5;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 12;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/ford-catalog/escape-hybrid")]
        public IActionResult FordEscapeHybrid()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 2;
            basicSearchViewModel.MakeId = 1;
            basicSearchViewModel.ModelId = 44;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "Ford", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-2625")]
        [Route("/massey-catalog/mf-2635")]
        public IActionResult MasseyMF2600()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 12;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 39;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-290")]
        [Route("/massey-catalog/mf-291")]
        public IActionResult MasseySeries200()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 7;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 40;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-4292")]
        [Route("/massey-catalog/mf-4297")]
        [Route("/massey-catalog/mf-4299")]
        public IActionResult MasseySeries4200()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 7;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 41;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-7150")]
        [Route("/massey-catalog/mf-7180")]
        [Route("/massey-catalog/mf-7618")]
        public IActionResult MasseySeries7000()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 7;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 42;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-5690")]
        public IActionResult Massey5690()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 11;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 32;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-32")]
        public IActionResult Massey32()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 11;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 33;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-2013")]
        public IActionResult Massey2013()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 12;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 34;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-1838")]
        public IActionResult Massey1838()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 12;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 35;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-dm-255-p")]
        public IActionResult MasseyDM255P()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 12;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 37;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-326")]
        public IActionResult MasseyMF326()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 12;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 36;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        [Route("/massey-catalog/mf-42-20-sd")]
        public IActionResult MasseyMF4220SD()
        {
            BasicSearchViewModel basicSearchViewModel = new BasicSearchViewModel();
            basicSearchViewModel.CategoryId = 12;
            basicSearchViewModel.MakeId = 3;
            basicSearchViewModel.ModelId = 131;
            basicSearchViewModel.PageNumber = 1;
            basicSearchViewModel.PriceRangeMax = 300000000;
            basicSearchViewModel.PriceRangeMin = 0;
            basicSearchViewModel.RecordsPerPage = 5;

            return RedirectToActionPermanent(actionName: "Alternate", controllerName: "MasseyFerguson", basicSearchViewModel);
        }

        /// <summary>
        /// Jivoes the chat.
        /// </summary>
        /// <returns>Jivochat View</returns>
        [Route("/[action]")]
        public IActionResult JivoChat()
        {
            return this.View();
        }
        #endregion
    }
}
