// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagesController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Pages Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.ApiRequest.Services;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Models.ViewModels.Landing;
    using motovalle.Ecommerce.Resources;
    using Newtonsoft.Json;
    using reCAPTCHA.AspNetCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Pages Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [AllowAnonymous]
    public class PagesController : Controller
    {
        #region ctor
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The email template URI
        /// </summary>
        private readonly string _emailTemplateUri;

        /// <summary>
        /// The funding request template
        /// </summary>
        private readonly string _fundingRequestTemplate;

        /// <summary>
        /// The funding request email to
        /// </summary>
        private readonly string _fundingRequestEmailTo;

        /// <summary>
        /// The funding request email to cc
        /// </summary>
        private readonly string _fundingRequestEmailToCc;

        /// <summary>
        /// The funding request email to CC2
        /// </summary>
        private readonly string _fundingRequestEmailToCc2;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IWebHostEnvironment _host;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The services helper
        /// </summary>
        private readonly IServicesHelper _servicesHelper;

        /// <summary>
        /// The recaptcha service
        /// </summary>
        private readonly IRecaptchaService _recaptchaService;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSenderExtended _emailSender;

        /// <summary>
        /// The makes identifier
        /// </summary>
        private readonly List<Makes> _makes;

        /// <summary>
        /// The exception control
        /// </summary>
        private readonly bool _exceptionControl = false;

        /// <summary>
        /// The headquarters view model
        /// </summary>
        private readonly List<HeadquartersViewModel> _headquartersViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesController" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="host">The host.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="servicesHelper">The services helper.</param>
        public PagesController(IConfiguration configuration, IWebHostEnvironment host, UserManager<ApplicationUser> userManager, IServicesHelper servicesHelper, IRecaptchaService recaptchaService, IEmailSenderExtended emailSender)
        {
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._emailTemplateUri = configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateGeneric");
            this._fundingRequestTemplate = configuration.GetSection("WestBankFundingRequest").GetValue<string>("ManualInvoiceTemplate");
            this._fundingRequestEmailTo = configuration.GetSection("WestBankFundingRequest").GetValue<string>("ManualInvoiceEmailTo");
            this._fundingRequestEmailToCc = configuration.GetSection("WestBankFundingRequest").GetValue<string>("ManualInvoiceEmailToCc");
            this._fundingRequestEmailToCc2 = configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            this._configuration = configuration;
            this._host = host;
            this._userManager = userManager;
            this._servicesHelper = servicesHelper;
            this._recaptchaService = recaptchaService;
            this._emailSender = emailSender;
            var makesApi = new MakesApi(this._httpClientInstance);
            try
            {
                this._makes = makesApi.GetAllRecords().Result;
                this._headquartersViewModel = configuration.GetSection("Headquarters").Get<List<HeadquartersViewModel>>().Where(x => x.Make.Contains("Ford", StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                this._makes = new List<Makes>();
                this._exceptionControl = true;
            }
        }
        #endregion

        #region Ford
        #region Interactive
        /// <summary>
        /// Fords the interactivo.
        /// </summary>
        /// <returns>Ford interactivo view</returns>
        public async Task<IActionResult> Ford_Interactivo()
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var inventoryWithCupix = new List<InventoryForProduct>();
            try
            {
                var makesId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Trim().Contains("ford"))?.MakesId ?? 0;
                var productsWithCupix = await this.GetProductsWithCupix(makesId);
                inventoryWithCupix = await this.GetInventoryForProductWithCupix(productsWithCupix);
                inventoryWithCupix = inventoryWithCupix.Where(x => x.AllowShow > 0 && x.AllowShowInventory > 0 && x.QuantityInStock > 0 && x.QuantityInStockInventory > 0).ToList();

            }
            catch (Exception)
            {
                ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            }

            return this.View("Ford/Ford_Interactivo", inventoryWithCupix);
        }

        /// <summary>
        /// Fords the coloring.
        /// </summary>
        /// <returns>Ford coloring view</returns>
        public IActionResult Ford_Coloring()
        {
            return this.View("Ford/Coloring");
        }

        /// <summary>
        /// Tests the drive interactivo ford.
        /// </summary>
        /// <returns>Test drive Interactivo ford</returns>
        public IActionResult Test_Drive_Interactivo_Ford()
        {
            return this.View("Ford/Test_Drive_Interactivo");
        }

        /// <summary>
        /// Fords the en peliculas.
        /// </summary>
        /// <returns>Ford en películas view</returns>
        public IActionResult Ford_en_Peliculas()
        {
            return this.View("Ford/Ford_en_Peliculas");
        }

        /// <summary>
        /// Fords the wallpaper.
        /// </summary>
        /// <returns>Ford wallpaper view</returns>
        public IActionResult Ford_Wallpaper()
        {
            return this.View("Ford/Wallpaper");
        }

        /// <summary>
        /// Fords the serivicio intengral.
        /// </summary>
        /// <returns>Servicio Integral Ford</returns>
        [Route("/Ford/Servicio-integral")]
        public IActionResult FordSerivicioIntengral()
        {
            return this.View("Ford/FordSerivicioIntengral");
        }

        /// <summary>
        /// Fords the test drive.
        /// </summary>
        /// <returns>Test Drive Ford View</returns>
        [Route("/Ford/Test-drive")]
        public async Task<IActionResult> FordTestDrive()
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var makeId = this._makes.Find(x => x.MakeName.Contains("ford", StringComparison.OrdinalIgnoreCase))?.MakesId ?? 0;
            var productAndMakeBases = new List<ProductAndMakeBase>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                productAndMakeBases = await makesApi.GetProductAndMakeBase(makeId);
                productAndMakeBases = productAndMakeBases.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).OrderBy(x => Guid.NewGuid()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.Products = new SelectList(productAndMakeBases, "ProductName", "ProductName");
            ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress");
            return this.View("Ford/FordTestDrive");
        }

        /// <summary>
        /// Fords the test drive.
        /// </summary>
        /// <returns>Control</returns>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FordTestDrive(TestDriveFordViewModel testDriveFordViewModel)
        {
            //Send email.
            var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateTestDriveFord")}";
            string emailBody = this.EmailFordTestDriveBodyBuilder(testDriveFordViewModel, emailTemplateUrl);
            var emailTo = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            var emailToCc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToCcDefault");

            try
            {
                await this._emailSender.SendEmailAsync(emailTo, "Test Drive FORD (Solicitud de Agendamiento)", emailBody, new List<string>(), new List<string>(), new List<string>() { emailToCc } );
                return this.Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Campaign Landing Pages
        #region Ford Campaing
        /// <summary>
        /// Fords campaing.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <returns>Ford CampaingLanding page (view)</returns>
        [HttpGet("[controller]/[action]/{campaing?}")]
        public IActionResult Ford_Campaing(int? campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Campaing";
            return campaing switch
            {
                null => this.View("Ford/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                1 => this.View("Ford/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                2 => this.View("Ford/CampaingHybrids", new ContactViewModel() { CampaingNumber = 2 }),
                3 => this.View("Ford/CampaingMoveCalmly", new ContactViewModel() { CampaingNumber = 3 }),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Fords the campaing.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <returns></returns>
        [HttpGet("[controller]/Ford/{campaing?}")]
        public IActionResult Ford_Campaing(string campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Campaing";
            return campaing?.ToLower() switch
            {
                null => this.View("Ford/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                "plan-financiero-ford" => this.View("Ford/Plan-financiero-ford", new ContactViewModel() { CampaingNumber = 4 }),
                "movamonos-con-tranquilidad" => this.View("Ford/MovamonosConTranquilidad", new ContactViewModel() { CampaingNumber = 5 }),
                "cc-campanario" => this.View("Ford/CC-Campanario", new ContactViewModel() { CampaingNumber = 6 }),
                "hibridos" => this.View("Ford/HibridosSep2020", new ContactViewModel() { CampaingNumber = 7 }),
                "fusion-hibrida" => this.View("Ford/FusionHibridaOct2020", new ContactViewModel() { CampaingNumber = 8 }),
                "fusion" => this.View("Ford/FusionOct2020", new ContactViewModel() { CampaingNumber = 9 }),
                "ford-cali" => this.View("Ford/FordCali", new ContactViewModel() { CampaingNumber = 10 }),
                "ford-bogota" => this.View("Ford/FordBogota", new ContactViewModel() { CampaingNumber = 11 }),
                _ => this.View("NotFound")
            };
        }

        /// <summary>
        /// Fords campaing.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>Ford CampaingLanding page (view)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_Campaing(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Campaing";
            Campaings campaing;
            string page, campaingName, emailTo, emailToCc, emailToBcc;
            switch (contactViewModel.CampaingNumber)
            {
                case null:
                case 1:
                    page = "Ford/Campaing";
                    campaing = Campaings.FordJuly;
                    contactViewModel.CampaingNumber = null;
                    campaingName = "Ford Julio";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("FordCampaingEmailTo");
                    emailToCc = string.Empty;
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("FordCampaingEmailToBcc");
                    break;
                case 2:
                    page = "Ford/CampaingHybrids";
                    campaing = Campaings.FordHybrids;
                    campaingName = "Ford Híbridos";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 3:
                    page = "Ford/CampaingMoveCalmly";
                    campaing = Campaings.FordMoveCalmly;
                    campaingName = "Movámonos con tranquilidad";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 4:
                    page = "Ford/Plan-financiero-ford";
                    campaing = Campaings.FordPlanFinancieroFord;
                    campaingName = "Plan Financiero Ford";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 5:
                    page = "Ford/MovamonosConTranquilidad";
                    campaing = Campaings.FordMovamonosConTranquilidad;
                    campaingName = "Movámonos con tranquilidad";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 6:
                    page = "Ford/CC-Campanario";
                    campaing = Campaings.FordCentroComercialCampanario;
                    campaingName = "Centro Comercial Campanario";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 7:
                    page = "Ford/HibridosSep2020";
                    campaing = Campaings.FordHibridasSeptiembre2020;
                    campaingName = "Ford Híbridos Septiembre";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 8:
                    page = "Ford/FusionHibridaOct2020";
                    campaing = Campaings.FordFusionHibridaOctubre2020;
                    campaingName = "Ford Fusion Híbrida Octubre 2020";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 9:
                    page = "Ford/FusionOct2020";
                    campaing = Campaings.FordFusionOctubre2020;
                    campaingName = "Ford Fusion Octubre 2020";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 10:
                    page = "Ford/FordCali";
                    campaing = Campaings.FordCali;
                    campaingName = "Ford Cali";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                case 11:
                    page = "Ford/FordBogota";
                    campaing = Campaings.FordBogota;
                    campaingName = "Ford Bogotá";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc");
                    emailToBcc = string.Empty;
                    break;
                default:
                    page = "Ford/Campaing";
                    contactViewModel.CampaingNumber = null;
                    campaing = Campaings.FordJuly;
                    campaingName = "Ford Julio";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("FordCampaingEmailTo");
                    emailToCc = string.Empty;
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("FordCampaingEmailToBcc");
                    break;
            }

            if (!ModelState.IsValid)
            {
                return this.View(page, contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, $"{campaingName}");
            var emailSender = new EmailSender(this._configuration);
            var emailsToCc = new List<string>();
            var emailsToBcc = new List<string>();

            if (!string.IsNullOrEmpty(emailToCc))
            {
                emailsToCc = new List<string>
                {
                    emailToCc
                };
            }

            if (!string.IsNullOrEmpty(emailToBcc))
            {
                emailsToBcc = new List<string>
                {
                    emailToBcc
                };
            }

            try
            {
                await Task.WhenAll(
                    emailSender.SendEmailAsync(emailTo, $"{campaingName} Campaign", emailBody, new List<string>(), emailsToBcc, emailsToCc),
                    this.CreateCampaingLeadsRecord(campaing, contactViewModel, emailTo));
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View(page, new ContactViewModel() { CampaingNumber = contactViewModel.CampaingNumber });
            }

            TempData["Error"] = "false";
            return this.View(page, new ContactViewModel() { CampaingNumber = contactViewModel.CampaingNumber });
        }
        #endregion

        #region EcoSport
        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <returns>Ford EcoSrport Landing page (view)</returns>
        [HttpGet("[controller]/[action]/{campaing?}")]
        public IActionResult Ford_EcoSport(int? campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_EcoSport";
            return campaing switch
            {
                null => this.View("Ford/EcoSport", new ContactViewModel() { CampaingNumber = 1 }),
                1 => this.View("Ford/EcoSport", new ContactViewModel() { CampaingNumber = 1 }),
                2 => this.View("Ford/EcoSport2", new ContactViewModel() { CampaingNumber = 2 }),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Ford EcoSrport Landing page (view)
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_EcoSport(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_EcoSport";
            Campaings campaing;
            string page;
            switch (contactViewModel.CampaingNumber)
            {
                case null:
                case 1:
                    page = "Ford/EcoSport";
                    campaing = Campaings.EcoSport;
                    break;
                case 2:
                    page = "Ford/EcoSport";
                    campaing = Campaings.EcoSport2;
                    break;
                default:
                    page = "Ford/EcoSport";
                    campaing = Campaings.EcoSport;
                    break;
            }

            if (!ModelState.IsValid)
            {
                return this.View(page, contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, $"Ford Ecosport {contactViewModel.CampaingNumber?.ToString() ?? string.Empty}");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
            var emailToBcc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await emailSender.SendEmailAsync(emailTo, $"Ford Ecosport Campaing {contactViewModel.CampaingNumber?.ToString() ?? string.Empty}", emailBody, new List<string>(), emailsToBcc);
                var hleads = await this.CreateCampaingLeadsRecord(campaing, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View(page);
            }

            TempData["Error"] = "false";
            return this.View(page);
        }
        #endregion

        #region Escape
        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <returns>Ford EcoSrport Landing page (view)</returns>
        [HttpGet("[controller]/[action]/{campaing?}")]
        public IActionResult Ford_Escape(int? campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Escape";
            return campaing switch
            {
                null => this.View("Ford/Escape", new ContactViewModel() { CampaingNumber = 1 }),
                1 => this.View("Ford/Escape", new ContactViewModel() { CampaingNumber = 1 }),
                2 => this.View("Ford/Escape2", new ContactViewModel() { CampaingNumber = 2 }),
                3 => this.View("Ford/Escape3", new ContactViewModel() { CampaingNumber = 3 }),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Ford EcoSrport Landing page (view)
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_Escape(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Escape";

            Campaings campaing;
            string campaingName;
            string page;
            switch (contactViewModel.CampaingNumber)
            {
                case null:
                case 1:
                    page = "Ford/Escape";
                    campaing = Campaings.Escape;
                    campaingName = contactViewModel.CampaingNumber.ToString();
                    break;
                case 2:
                    page = "Ford/Escape2";
                    campaing = Campaings.Escape2;
                    campaingName = contactViewModel.CampaingNumber.ToString();
                    break;
                case 3:
                    page = "Ford/Escape3";
                    campaing = Campaings.Escape3;
                    campaingName = "Hybrid";
                    break;
                default:
                    page = "Ford/Escape";
                    campaing = Campaings.EcoSport;
                    campaingName = contactViewModel.CampaingNumber.ToString();
                    break;
            }

            if (!ModelState.IsValid)
            {
                return this.View(page, contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, $"Ford Escape {campaingName}");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
            var emailToBcc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await emailSender.SendEmailAsync(emailTo, $"Ford Escape Campaing {campaingName}", emailBody, new List<string>(), emailsToBcc);
                var hleads = await this.CreateCampaingLeadsRecord(campaing, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View(page);
            }

            TempData["Error"] = "false";
            return this.View(page);
        }
        #endregion

        #region Fusion
        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <returns>Ford EcoSrport Landing page (view)</returns>
        public IActionResult Ford_Fusion()
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Fusion";
            return this.View("Ford/Fusion");
        }

        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Ford EcoSrport Landing page (view)
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_Fusion(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Fusion";
            if (!ModelState.IsValid)
            {
                return this.View("Ford/Fusion", contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, "Ford Fusion");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
            var emailToBcc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await emailSender.SendEmailAsync(emailTo, "Ford Fusion Campaing", emailBody, new List<string>(), emailsToBcc);
                var hleads = await this.CreateCampaingLeadsRecord(Campaings.Fusion, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View("Ford/Fusion");
            }

            TempData["Error"] = "false";
            return this.View("Ford/Fusion");
        }
        #endregion 

        #region Ranger
        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <returns>Ford EcoSrport Landing page (view)</returns>
        public IActionResult Ford_Ranger()
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Ranger";
            return this.View("Ford/Ranger");
        }

        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Ford EcoSrport Landing page (view)
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_Ranger(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Ranger";
            if (!ModelState.IsValid)
            {
                return this.View("Ford/Ranger", contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, "Ford Ranger");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
            var emailToBcc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await emailSender.SendEmailAsync(emailTo, "Ford Ranger Campaing", emailBody, new List<string>(), emailsToBcc);
                var hleads = await this.CreateCampaingLeadsRecord(Campaings.Ranger, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View("Ford/Ranger");
            }

            TempData["Error"] = "false";
            return this.View("Ford/Ranger");
        }
        #endregion 

        #region Edge
        /// <summary>
        /// Fords the Edge.
        /// </summary>
        /// <returns>Ford Edge Landing page (view)</returns>
        public IActionResult Ford_Edge()
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Edge";
            return this.View("Ford/Edge");
        }

        /// <summary>
        /// Fords the Edge.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Ford Edge Landing page (view)
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_Edge(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Edge";
            if (!ModelState.IsValid)
            {
                return this.View("Ford/Edge", contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, "Ford Edge");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
            var emailToBcc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await emailSender.SendEmailAsync(emailTo, "Ford Edge Campaing", emailBody, new List<string>(), emailsToBcc);
                var hleads = await this.CreateCampaingLeadsRecord(Campaings.Edge, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View("Ford/Edge");
            }

            TempData["Error"] = "false";
            return this.View("Ford/Edge");
        }
        #endregion 

        #region Explorer
        /// <summary>
        /// Fords the Explorer.
        /// </summary>
        /// <returns>Ford Explorer Landing page (view)</returns>
        public IActionResult Ford_Explorer()
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Explorer";
            return this.View("Ford/Explorer");
        }

        /// <summary>
        /// Fords the Explorer.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Ford Explorer Landing page (view)
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ford_Explorer(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Ford_Explorer";
            if (!ModelState.IsValid)
            {
                return this.View("Ford/Explorer", contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, "Ford Explorer");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
            var emailToBcc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await emailSender.SendEmailAsync(emailTo, "Ford Explorer Campaing", emailBody, new List<string>(), emailsToBcc);
                var hleads = await this.CreateCampaingLeadsRecord(Campaings.Explorer, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View("Ford/Explorer");
            }

            TempData["Error"] = "false";
            return this.View("Ford/Explorer");
        }
        #endregion 
        #endregion
        #endregion

        #region Mazda
        /// <summary>
        /// Mazdas the interactivo.
        /// </summary>
        /// <returns>Mazda interactivo view</returns>
        public async Task<IActionResult> Mazda_Interactivo()
        {
            //Uncomment when Mazda View has been available again
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var inventoryWithCupix = new List<InventoryForProduct>();
            try
            {
                var makesId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Trim().Contains("mazda"))?.MakesId ?? 0;
                var productsWithCupix = await this.GetProductsWithCupix(makesId);
                inventoryWithCupix = await this.GetInventoryForProductWithCupix(productsWithCupix);
                inventoryWithCupix = inventoryWithCupix.Where(x => x.AllowShow > 0 && x.AllowShowInventory > 0 && x.QuantityInStock > 0 && x.QuantityInStockInventory > 0).ToList();
            }
            catch (Exception)
            {
                ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            }

            return this.View("Mazda/Mazda_Interactivo", inventoryWithCupix);
        }

        /// <summary>
        /// Mazdas the servicios.
        /// </summary>
        /// <returns>Mazda Servicios View</returns>
        public IActionResult MazdaServicios()
        {
            return this.View("Mazda/MazdaServicios");
        }

        /// <summary>
        /// Mazdas the servicios home.
        /// </summary>
        /// <returns>>Mazda Servicios Home View</returns>
        public IActionResult MazdaServiciosHome()
        {
            return this.View("Mazda/MazdaServiciosHome");
        }

        /// <summary>
        /// Mazdas the coloring.
        /// </summary>
        /// <returns>Mazda coloring view</returns>
        public IActionResult Mazda_Coloring()
        {

            return this.View("Mazda/Coloring");
        }

        /// <summary>
        /// Tests the drive interactivo mazda.
        /// </summary>
        /// <returns>Test drive interactivo mazda</returns>
        public IActionResult Test_Drive_Interactivo_Mazda()
        {
                return this.View("Mazda/Test_Drive_Interactivo");
        }

        /// <summary>
        /// Mazdas the wallpaper.
        /// </summary>
        /// <returns>Mazda wallpaper</returns>
        public IActionResult Mazda_Wallpaper()
        {
            return this.View("Mazda/Wallpaper");
            
        }

        public IActionResult MazdaRepuestosAccesorios()
        {
            return this.View("Mazda/MazdaRepuestosAccesorios");

        }

        /// <summary>
        /// Espiritus the mazda.
        /// </summary>
        /// <returns>Espititu Mazda</returns>
        [Route("/mazda/espiritu-mazda")]
        public IActionResult EspirituMazda()
        {
            return this.View("Mazda/EspirituMazda");
        }

        /// <summary>
        /// Mazdas the mantenimientos.
        /// </summary>
        /// <returns>Maintenances View</returns>
        [Route("/{makesName}/mantenimientos")]
        public async Task<IActionResult> Mantenimientos(string makesName)
        {
            var productsApi = new ProductsApi(this._httpClientInstance);
            var products = await productsApi.ProductsWithEnableMaintenanceByMake(makesName);
            ViewBag.Products = new SelectList(products, "ProductsId", "ProductName");
            return this.View("Motovalle/Mantenimientos", makesName);
        }

        #region Campaing Landing Pages
        #region Mazda Campaing
        /// <summary>
        /// Fords campaing.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <returns>Mazda Campaign Landing page (view)</returns>
        [HttpGet("[controller]/[action]/{campaing?}")]
        public IActionResult Mazda_Campaing(int? campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingPhone");
            TempData["ExternalLink"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingLink");
            TempData["Controller"] = "Mazda_Campaing";
            return campaing switch
            {
                null => this.View("Mazda/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                1 => this.View("Mazda/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                2 => this.View("Mazda/CampaingSantaFe", new ContactViewModel() { CampaingNumber = 2 }),
                3 => this.View("Mazda/CampaingExperience", new ContactViewModel() { CampaingNumber = 3 }),
                4 => this.View("Mazda/CampaingBogota", new ContactViewModel() { CampaingNumber = 4 }),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Mazdas the campaing.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <returns>Mazda Campaign Landing page (view)</returns>
        [HttpGet("[controller]/Mazda/{campaing?}")]
        public IActionResult Mazda_Campaing(string campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingPhone");
            TempData["ExternalLink"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingLink");
            TempData["Controller"] = "Mazda_Campaing";
            return campaing.ToLower() switch
            {
                null => this.View("Mazda/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                "1" => this.View("Mazda/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                "2" => this.View("Mazda/CampaingSantaFe", new ContactViewModel() { CampaingNumber = 2 }),
                "mv-experience" => this.View("Mazda/CampaingExperience", new ContactViewModel() { CampaingNumber = 3 }),
                "mazda-bogota" => this.View("Mazda/CampaingBogota", new ContactViewModel() { CampaingNumber = 4 }),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Fords campaing.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>Ford CampaingLanding page (view)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Mazda_Campaing(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingPhone");
            TempData["ExternalLink"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingLink");
            TempData["Controller"] = "Mazda_Campaing";
            Campaings campaing;
            string page, campaingName, emailTo, emailToBcc, emailToCc;
            switch (contactViewModel.CampaingNumber)
            {
                case null:
                case 1:
                    page = "Mazda/Campaing";
                    campaing = Campaings.MazdaCDS;
                    campaingName = "Mazda CDS";
                    contactViewModel.CampaingNumber = null;
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaing");
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToBcc");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToCc");
                    break;
                case 2:
                    page = "Mazda/CampaingSantaFe";
                    campaing = Campaings.MazdaSantaFe;
                    campaingName = "Mazda - Santa Fé";
                    contactViewModel.CampaingNumber = 2;
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaing");
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToBcc");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToCc");
                    break;
                case 3:
                    page = "Mazda/CampaingExperience";
                    campaing = Campaings.MazdaExperience;
                    campaingName = "Mazda - Motovalle Experience";
                    contactViewModel.CampaingNumber = 3;
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaing");
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToBcc");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToCc");
                    break;
                case 4:
                    page = "Mazda/CampaingBogota";
                    campaing = Campaings.MazdaBogota;
                    campaingName = "Mazda - Bogotá";
                    contactViewModel.CampaingNumber = 4;
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaing");
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToBcc");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToCc");
                    break;
                default:
                    campaingName = "Mazda CDS";
                    page = "Mazda/Campaing";
                    campaing = Campaings.MazdaCDS;
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaing");
                    emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToBcc");
                    emailToCc = this._configuration.GetSection("CampaingSettings").GetValue<string>("MazdaCDSCampaingToCc");
                    break;
            }

            if (!ModelState.IsValid)
            {
                return this.View(page, contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, $"{campaingName}");
            var emailSender = new EmailSender(this._configuration);

            try
            {
                await Task.WhenAll(
                    emailSender.SendEmailAsync(emailTo, $"{campaingName} Campaing", emailBody, new List<string>(), string.IsNullOrEmpty(emailToBcc) ? null : new List<string>() { emailToBcc }, string.IsNullOrEmpty(emailToCc) ? null : new List<string>() { emailToCc }),
                    this.CreateCampaingLeadsRecord(campaing, contactViewModel, emailTo));
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View(page);
            }

            TempData["Error"] = "false";
            return this.View(page);
        }
        #endregion
        #endregion
        #endregion

        #region Massey Ferguson
        /// <summary>
        /// Masseys the ferguson interactivo.
        /// </summary>
        /// <returns>Massey ferguson interactivo view</returns>
        public async Task<IActionResult> Massey_Ferguson_Interactivo()
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var inventoryWithCupix = new List<InventoryForProduct>();
            try
            {
                var makesId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Trim().Contains("massey ferguson"))?.MakesId ?? 0;
                var productsWithCupix = await this.GetProductsWithCupix(makesId);
                inventoryWithCupix = await this.GetInventoryForProductWithCupix(productsWithCupix);
                inventoryWithCupix = inventoryWithCupix.Where(x => x.AllowShow > 0 && x.AllowShowInventory > 0 && x.QuantityInStock > 0 && x.QuantityInStockInventory > 0).ToList();

            }
            catch (Exception)
            {
                ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            }

            return this.View("MasseyFerguson/Massey_Ferguson_Interactivo", inventoryWithCupix);
        }

        /// <summary>
        /// Masseys the ferguson coloring.
        /// </summary>
        /// <returns>Massey fergusson coloring view</returns>
        public IActionResult Massey_Ferguson_Coloring()
        {
            return this.View("MasseyFerguson/Coloring");
        }

        /// <summary>
        /// Masseys the quienes somos.
        /// </summary>
        /// <returns>Quienes somos View</returns>
        [Route("MasseyFerguson/Quienes-somos")]
        public IActionResult MasseyQuienesSomos()
        {
            return this.View("MasseyFerguson/MasseyQuienesSomos");
        }

        /// <summary>
        /// Masseys the filosofia.
        /// </summary>
        /// <returns>Filosofia massey</returns>
        [Route("MasseyFerguson/Filosofia")]
        public IActionResult MasseyFilosofia()
        {
            return this.View("MasseyFerguson/MasseyFilosofia");
        }

        /// <summary>
        /// Masseys the valores.
        /// </summary>
        /// <returns>Massey Valores</returns>
        [Route("MasseyFerguson/Valores")]
        public IActionResult MasseyValores()
        {
            return this.View("MasseyFerguson/MasseyValores");
        }

        /// <summary>
        /// Masseys the club operadores.
        /// </summary>
        /// <returns>Massey Club Operadores</returns>
        [Route("MasseyFerguson/Club-Operadores")]
        public IActionResult MasseyClubOperadores()
        {
            return this.View("MasseyFerguson/MasseyClubOperadores");
        }

        /// <summary>
        /// Operatorses the club.
        /// </summary>
        /// <param name="operatorsClubViewModel">The operators club view model.</param>
        /// <returns>Massey Ferguson view brand</returns>
        [HttpPost]
        public async Task<IActionResult> MasseyClubOperadores(OperatorsClubViewModel operatorsClubViewModel)
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateOperatorsClubMassey")}";
            var emailBody = this.EmailMasseyOperatorsClubBodyBuilder(operatorsClubViewModel, emailTemplateUrl);
            var emailTo = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            var emailToCc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToCcDefault");
            try
            {
                await this._emailSender.SendEmailAsync(emailTo, "Club de Operadores Massey (Solicitud de Registro)", emailBody, null, null, new List<string>() { emailToCc });
                return this.Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Carshow
        #region Campaigns Landing Pages   
        /// <summary>
        /// Carshows the campaing.
        /// </summary>
        /// <returns>
        /// Ford Carshow Campaing Landing page (view)
        /// </returns>
        public IActionResult Carshow_Campaing()
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Carshow_Campaing";
            return this.View("Carshow/Campaing");
        }

        /// <summary>
        /// Carshows the campaing.
        /// </summary>
        /// <param name="campaignName">Name of the campaign.</param>
        /// <returns>Carshow Landing for Campaign</returns>
        [HttpGet("[controller]/{campaignName}")]
        public IActionResult CarshowCampaing(string campaignName)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Carshow_Campaing";
            return campaignName.ToLower() switch
            {
                "carshow-6-meses-de-gracia" => this.View("Carshow/Carshow-6-meses-de-gracia", new ContactViewModel() { CampaingNumber = 2 }),
                null => this.View("NotFound"),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Fords the eco sport.
        /// </summary>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <returns>
        /// Carshow Landing page
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Carshow_Campaing(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Carshow_Campaing";
            string pageName, campaigngName, emailTo;
            var emailsToCc = new List<string>();
            switch (contactViewModel.CampaingNumber)
            {
                case 2:
                    pageName = "Carshow/Carshow-6-meses-de-gracia";
                    campaigngName = "Carshow 6 meses de gracia";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("Email");
                    emailsToCc = new List<string>() { this._configuration.GetSection("CampaingSettings").GetValue<string>("EmailToCc") };
                    break;
                default:
                    pageName = "Carshow/Campaing";
                    campaigngName = "Campaña Carshow";
                    emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("CarshowEmailTo");
                    emailsToCc = new List<string>() { this._configuration.GetSection("CampaingSettings").GetValue<string>("CarshowEmailToBcc") };
                    break;
            }

            if (!ModelState.IsValid)
            {
                return this.View(pageName, contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, campaigngName);
            var emailSender = new EmailSender(this._configuration);


            try
            {
                await emailSender.SendEmailAsync(emailTo, campaigngName, emailBody, new List<string>(), new List<string>(), emailsToCc);
                var hleads = await this.CreateCampaingLeadsRecord(Campaings.Carshow6MesesDeGracia, contactViewModel, emailTo);
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View(pageName, contactViewModel);
            }

            TempData["Error"] = "false";
            return this.View(pageName, new ContactViewModel() { CampaingNumber = contactViewModel.CampaingNumber });
        }
        #endregion
        #endregion

        #region Motovalle
        #region Interactive
        /// <summary>
        /// Airlifes this instance.
        /// </summary>
        /// <returns>Air life view</returns>
        public IActionResult Airlife()
        {
            ViewBag.PostsFilter = "General";
            return View("Motovalle/Airlife");
        }

        /// <summary>
        /// Donacioneses this instance.
        /// </summary>
        /// <returns>Donaciones view</returns>
        public IActionResult Donaciones()
        {
            ViewBag.PostsFilter = "General";
            return this.View("Motovalle/Donaciones");
        }

        /// <summary>
        /// Aniversarioes this instance.
        /// </summary>
        /// <returns>Aniversario view</returns>
        public IActionResult Aniversario()
        {
            ViewBag.PostsFilter = "General";
            return this.View("Motovalle/Aniversario_65");
        }
        #endregion

        #region Campaings
        #region Limitless Campaing
        /// <summary>
        /// Motovalles the campaing.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <returns>Motovalle Campaing Landing page (view)</returns>
        [HttpGet("[controller]/[action]/{campaing?}")]
        public IActionResult Motovalle_Campaing(int? campaing)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Motovalle_Campaing";
            return campaing switch
            {
                null => this.View("Motovalle/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                1 => this.View("Motovalle/Campaing", new ContactViewModel() { CampaingNumber = 1 }),
                _ => this.View("NotFound"),
            };
        }

        /// <summary>
        /// Motovalles the campaing.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <returns>Motovalle Campaing Landing page (view)</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Motovalle_Campaing(ContactViewModel contactViewModel)
        {
            TempData["Phone"] = this._configuration.GetSection("CampaingSettings").GetValue<string>("Phone");
            TempData["Controller"] = "Motovalle_Campaing";
            Campaings campaing;
            string page;
            switch (contactViewModel.CampaingNumber)
            {
                case null:
                case 1:
                    page = "Motovalle/Campaing";
                    campaing = Campaings.MotovalleLimitless;
                    contactViewModel.CampaingNumber = null;
                    break;
                default:
                    page = "Motovalle/Campaing";
                    campaing = Campaings.MotovalleLimitless;
                    break;
            }

            if (!ModelState.IsValid)
            {
                return this.View(page, contactViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactViewModel, emailTemplateUrl, $"Motovalle Sin Límites {contactViewModel.CampaingNumber?.ToString() ?? string.Empty}");
            var emailTo = this._configuration.GetSection("CampaingSettings").GetValue<string>("FordCampaingEmailTo");
            var emailToBcc = this._configuration.GetSection("CampaingSettings").GetValue<string>("FordCampaingEmailToBcc");
            var emailSender = new EmailSender(this._configuration);

            var emailsToBcc = new List<string>
            {
                emailToBcc
            };

            try
            {
                await Task.WhenAll(
                    emailSender.SendEmailAsync(emailTo, $"Motovalle Limitless {contactViewModel.CampaingNumber?.ToString() ?? string.Empty}", emailBody, new List<string>(), emailsToBcc),
                    this.CreateCampaingLeadsRecord(campaing, contactViewModel, emailTo));
            }
            catch (Exception)
            {
                TempData["Error"] = "true";
                return this.View(page);
            }

            TempData["Error"] = "false";
            return this.View(page);
        }
        #endregion

        #region Credito Ya

        /// <summary>
        /// Solicita the tu credito ya.
        /// </summary>
        /// <returns>Solicita tu credito ya</returns>
        public IActionResult SolicitaTuCreditoYaMazda()
        {
            return this.View();
        }

        /// <summary>
        /// Solicita the tu credito ya.
        /// </summary>
        /// <param name="creditOnlineNowViewModel">The credit online now view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolicitaTuCreditoYaMazda(CreditOnlineNowViewModel creditOnlineNowViewModel)
        {
            var recaptcha = await this._recaptchaService.Validate(this.Request);
            if (!recaptcha.success)
            {
                ModelState.AddModelError(string.Empty, "Hubo un error validando recatpcha. Por favor intente nuevamente.");
            }

            if (!ModelState.IsValid)
            {
                return this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
                {
                    ControlStatus = "ModelError",
                    Message = "Verifique los datos envíados",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage))
                });
            }

            ////Step 1. Create or verify Customer
            var customer = await this.CheckCustomer(creditOnlineNowViewModel);

            ////Step 2. Get Token Auth
            var authResponse = await this._servicesHelper.SantanderBankAuth();
            if (!authResponse.IsSuccess)
            {
                return this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
                {
                    ControlStatus = "Error",
                    Message = $"Error al Autenticarse con el Banco Santander => {authResponse.Message}",
                });
            }

            ////Step 3. Try Get Funding Request
            var token = authResponse.Result.Token;
            var requestBody = CommonHelper.ToSantanderBankFundingRequest(creditOnlineNowViewModel, this._configuration);
            var fundingResponse = await this._servicesHelper.SantanderFundingRequest(requestBody, token);
            var saveBody = JsonConvert.SerializeObject(requestBody);
            var reportId = Guid.NewGuid().ToString();
            if (fundingResponse.IsSuccess)
            {
                ////Step 4. Send Email to Admin
                await this.SendEmailForFundingRequest(creditOnlineNowViewModel, fundingResponse.Message);
                var response = fundingResponse.Result.Semaforo switch
                {
                    SantanderBankSemaforo.ProblemaTecnico => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario se ha generado un <b>PROBLEMA TÉCNICO</b> al ejecutar la solicitud. Por favor comparta este identificador al administrador del sistema: <b>{reportId}</b>. Por favor intente más tarde.", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.Negado => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario su solicitud no cumple con los requisitos y ha sido <b>NEGADA</b>. {fundingResponse.Result.Titulo}", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.PreAprobado => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario su solicitud es <b>PRE-APROBADA</b>, por favor verifique lo siguiente: {fundingResponse.Result.Titulo}", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.AprobadoConDocumentos => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "FundingRequestOk", Message = $"Señor usuario: {fundingResponse.Result.Titulo}.", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.AprobadoSinDocumentos => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "FundingRequestOk", Message = $"Señor usuario: {fundingResponse.Result.Titulo}.", Response = fundingResponse.Result }),
                    _ => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Error", Message = $"Error al realizar la solicitud de crédito, favor comuniquese con el administrador del sistema y compartele este identificador: <b>{reportId}</b>", Response = fundingResponse.Result }),
                };

                ////Step 5. Save Log
                var responseSerialized = JsonConvert.SerializeObject(response);
                CommonHelper.SaveRequestReportOnTempPath(reportId, saveBody, responseSerialized, "Santander Bank");
                return response;
            }

            var responseNoSuccess = new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
            {
                ControlStatus = "Error",
                Message = $"Error al realizar la solicitud de crédito, favor comuniquese con el administrador del sistema y compartele este identificador: <b>{reportId}</b>",
                Response = fundingResponse.Result
            };

            var responseNoSuccessSerialized = JsonConvert.SerializeObject(responseNoSuccess);
            CommonHelper.SaveRequestReportOnTempPath(reportId, saveBody, responseNoSuccessSerialized, "Santander Bank");
            return this.Ok(responseNoSuccess);
        }

        /// <summary>
        /// Solicita the tu credito ya.
        /// </summary>
        /// <returns>Solicita tu credito ya</returns>
        public IActionResult SolicitaTuCreditoYa()
        {
            return this.View();
        }

        /// <summary>
        /// Solicita the tu credito ya.
        /// </summary>
        /// <param name="creditOnlineNowViewModel">The credit online now view model.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolicitaTuCreditoYa(CreditOnlineNowViewModel creditOnlineNowViewModel)
        {
            var recaptcha = await this._recaptchaService.Validate(this.Request);
            if (!recaptcha.success)
            {
                ModelState.AddModelError(string.Empty, "Hubo un error validando recatpcha. Por favor intente nuevamente.");
            }

            if (!ModelState.IsValid)
            {
                return this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
                {
                    ControlStatus = "ModelError",
                    Message = "Verifique los datos envíados",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage))
                });
            }

            ////Step 1. Create or verify Customer
            var customer = await this.CheckCustomer(creditOnlineNowViewModel);

            ////Step 2. Get Token Auth
            var authResponse = await this._servicesHelper.SantanderBankAuth();
            if (!authResponse.IsSuccess)
            {
                return this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
                {
                    ControlStatus = "Error",
                    Message = $"Error al Autenticarse con el Banco Santander => {authResponse.Message}",
                });
            }

            ////Step 3. Try Get Funding Request
            var token = authResponse.Result.Token;
            var requestBody = CommonHelper.ToSantanderBankFundingRequest(creditOnlineNowViewModel, this._configuration);
            var fundingResponse = await this._servicesHelper.SantanderFundingRequest(requestBody, token);
            var saveBody = JsonConvert.SerializeObject(requestBody);
            var reportId = Guid.NewGuid().ToString();
            if (fundingResponse.IsSuccess)
            {
                ////Step 4. Send Email to Admin
                await this.SendEmailForFundingRequest(creditOnlineNowViewModel, fundingResponse.Message);
                var response = fundingResponse.Result.Semaforo switch
                {
                    SantanderBankSemaforo.ProblemaTecnico => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario se ha generado un <b>PROBLEMA TÉCNICO</b> al ejecutar la solicitud. Por favor comparta este identificador al administrador del sistema: <b>{reportId}</b>. Por favor intente más tarde.", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.Negado => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario su solicitud no cumple con los requisitos y ha sido <b>NEGADA</b>. {fundingResponse.Result.Titulo}", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.PreAprobado => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Info", Message = $"Señor usuario su solicitud es <b>PRE-APROBADA</b>, por favor verifique lo siguiente: {fundingResponse.Result.Titulo}", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.AprobadoConDocumentos => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "FundingRequestOk", Message = $"Señor usuario: {fundingResponse.Result.Titulo}.", Response = fundingResponse.Result }),
                    SantanderBankSemaforo.AprobadoSinDocumentos => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "FundingRequestOk", Message = $"Señor usuario: {fundingResponse.Result.Titulo}.", Response = fundingResponse.Result }),
                    _ => this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>() { ControlStatus = "Error", Message = $"Error al realizar la solicitud de crédito, favor comuniquese con el administrador del sistema y compartele este identificador: <b>{reportId}</b>", Response = fundingResponse.Result }),
                };

                ////Step 5. Save Log
                var responseSerialized = JsonConvert.SerializeObject(response);
                CommonHelper.SaveRequestReportOnTempPath(reportId, saveBody, responseSerialized, "Santander Bank");
                return response;
            }

            var responseNoSuccess = new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
            {
                ControlStatus = "Error",
                Message = $"Error al realizar la solicitud de crédito, favor comuniquese con el administrador del sistema y compartele este identificador: <b>{reportId}</b>",
                Response = fundingResponse.Result
            };

            var responseNoSuccessSerialized = JsonConvert.SerializeObject(responseNoSuccess);
            CommonHelper.SaveRequestReportOnTempPath(reportId, saveBody, responseNoSuccessSerialized, "Santander Bank");
            return this.Ok(responseNoSuccess);
        }
        #endregion
        #endregion
        #endregion

        #region Common
        /// <summary>
        /// Maintenanceses the by product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Maintenances Partial View</returns>
        public async Task<IActionResult> MaintenancesByProduct(int id)
        {
            var productMaintenanceApi = new ProductMaintenancesApi(this._httpClientInstance);
            var productMaintenances = await productMaintenanceApi.GetRecordsByProduct(id);
            return this.PartialView("~/Views/Shared/Landing/_MaintenancesByProductPartial.cshtml", productMaintenances);
        }

        /// <summary>
        /// Gets the products with cupix.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>List of products for make with cupix 360 content</returns>
        private async Task<List<ProductAndMakeBase>> GetProductsWithCupix(int makesId)
        {
            var makesApi = new MakesApi(this._httpClientInstance);
            var productsForMake = await makesApi.GetProductAndMakeBase(makesId);
            return productsForMake.Where(x => x.Cupix360Url != null).ToList();
        }

        /// <summary>
        /// Gets the inventory for product with cupix.
        /// </summary>
        /// <param name="productsWithCupix">The products with cupix.</param>
        /// <returns>List of inventory for product (Just one for product)</returns>
        private async Task<List<InventoryForProduct>> GetInventoryForProductWithCupix(List<ProductAndMakeBase> productsWithCupix)
        {
            var productsApi = new ProductsApi(this._httpClientInstance);
            var inventoryForProductList = new List<InventoryForProduct>();
            foreach (var item in productsWithCupix)
            {
                var inventoryItems = await productsApi.GetInventoryForProduct(item.ProductId, 0);
                inventoryForProductList.Add(inventoryItems.FirstOrDefault());
            }

            inventoryForProductList = inventoryForProductList.Where(x => x != null).ToList();
            return inventoryForProductList;
        }

        /// <summary>
        /// Emails the body builder.
        /// </summary>
        /// <param name="contactUs">The contact us.</param>
        /// <param name="emailTemplateUrl">The email template URL.</param>
        /// <param name="camapingName">Name of the camaping.</param>
        /// <returns>
        /// Email body
        /// </returns>
        private string EmailBodyBuilder(ContactViewModel contactUs, string emailTemplateUrl, string camapingName)
        {
            var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var date = DateTime.Now;
            var content = @$"<div>
                                 <b>Fecha:</b> {date:dddd, dd MMMM yyyy}
                             </div>
                             <div>
                                 <b>Tipo de contacto:</b> Campaña {camapingName}
                             </div>
                             <div>
                                 <b>Nombre:</b> {contactUs.FullName}
                             </div>
                             <div>
                                 <b>Correo:</b> {contactUs.Email}
                             </div>
                             <div>
                                 <b>Número telefónico:</b> {contactUs.PhoneNumber}
                             </div>
                             <div>
                                 <b>Ciudad:</b> {contactUs.City}
                             </div>
                             <div>
                                 <b>Mensaje:</b> {contactUs.Message}
                             </div>";

            var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
               .Replace("${logoUrl}", imgUrl)
               .Replace("${firstLabel}", "Un cliente ha ingrasado información por medio del formulario de contacto")
               .Replace("${Date}", $"{date.Year}")
               .Replace("${content}", $"{content}");
            return emailTemplate;
        }

        /// <summary>
        /// Creates the campaing leads record.
        /// </summary>
        /// <param name="campaing">The campaing.</param>
        /// <param name="contactViewModel">The contact view model.</param>
        /// <param name="emailSentTo">The email sent to.</param>
        /// <returns>CampaignLeads Created</returns>
        private async Task<CampaignLeads> CreateCampaingLeadsRecord(Campaings campaing, ContactViewModel contactViewModel, string emailSentTo)
        {
            var hleads = new CampaignLeads()
            {
                Campaign = (int)campaing,
                City = contactViewModel.City,
                CreatedOn = DateTime.Now,
                Email = contactViewModel.Email,
                EmailSentTo = emailSentTo,
                InfoFrom = Request.GetDisplayUrl(),
                Name = contactViewModel.FullName,
                PhoneNumber = contactViewModel.PhoneNumber,
                Remarks = contactViewModel.Message,
                Status = (int)Status.Active
            };

            var campaingLeadsApi = new CampaignLeadsApi(this._httpClientInstance);
            var hleadsCreated = await campaingLeadsApi.CreateRecord(hleads);
            await Task.Delay(500);
            return hleadsCreated;
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// Token user
        /// </returns>
        private async Task<string> GetUserToken()
        {
            var user = await this._userManager.GetUserAsync(User);
            var token = await this._userManager.GetAuthenticationTokenAsync(user, this._configuration.GetSection("JwtSettings").GetValue<string>("Issuer"), this._configuration.GetSection("JwtSettings").GetValue<string>("Subject"));
            return token;
        }

        /// <summary>
        /// Checks the customer.
        /// </summary>
        /// <param name="creditOnlineNowViewModel">The credit online now view model.</param>
        /// <returns>Customer</returns>
        private async Task<Customers> CheckCustomer(CreditOnlineNowViewModel creditOnlineNowViewModel)
        {
            var customersApi = new CustomerApi(this._httpClientInstance);
            var customer = (await customersApi.GetAllRecords()).FirstOrDefault(x => x.EmailAddress == creditOnlineNowViewModel.EmailAddress);
            if (customer != null)
            {
                return customer;
            }

            var newCustomer = new Customers()
            {
                BillToAddress = creditOnlineNowViewModel.Address,
                BillToCity = creditOnlineNowViewModel.City,
                BillToState = creditOnlineNowViewModel.State,
                BillToZipcode = creditOnlineNowViewModel.ZipCode,
                EmailAddress = creditOnlineNowViewModel.EmailAddress,
                FullName = creditOnlineNowViewModel.FullName,
                PhoneNumber = creditOnlineNowViewModel.PhoneNumber,
                ShipToAddress = creditOnlineNowViewModel.Address,
                ShipToCity = creditOnlineNowViewModel.City,
                ShipToState = creditOnlineNowViewModel.State,
                ShipToZipcode = creditOnlineNowViewModel.ZipCode
            };

            newCustomer = await customersApi.CreateRecord(newCustomer);
            return newCustomer;
        }

        /// <summary>
        /// Sends the email for funding request.
        /// </summary>
        /// <param name="creditOnlineNowViewModel">The credit online now view model.</param>
        /// <returns></returns>
        private async Task<bool> SendEmailForFundingRequest(CreditOnlineNowViewModel creditOnlineNowViewModel, string fundingRequestMessage)
        {
            var emailfullPath = $"{this._host.WebRootPath}{this._fundingRequestTemplate}";
            var logoUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var contentTemplate = new StreamReader(emailfullPath)
                .ReadToEnd()
                .Replace("${logoUrl}", logoUrl)
                .Replace("${firstLabel}", @"Se ha realizado una solicitud de financiación para una Orden creada por medio de la tienda Motovalle - Ecommerce. En pocos minutos llegará el soporte de la orden. <br/><br/>
                                            A continuación encontrará toda la información de dicha solicitud de financiación.")
                .Replace("${bankName}", CommonHelper.GetBankName(Banks.SantanderBank))
                .Replace("${bankResponse}", fundingRequestMessage)
                .Replace("${name}", creditOnlineNowViewModel.FullName)
                .Replace("${idType}", creditOnlineNowViewModel.DocType.ToString())
                .Replace("${idNumber}", creditOnlineNowViewModel.DocNumber)
                .Replace("${profession}", string.Empty)
                .Replace("${email}", creditOnlineNowViewModel.EmailAddress)
                .Replace("${phoneNumber}", creditOnlineNowViewModel.PhoneNumber)
                .Replace("${dateOfBirth}", string.Empty)
                .Replace("${cityOfResidence}", creditOnlineNowViewModel.City)
                .Replace("${issueDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                .Replace("${requestId}", string.Empty)
                .Replace("${orderNumber}", string.Empty)
                .Replace("${totalAmount}", creditOnlineNowViewModel.TotalAmount.ToString("C"))
                .Replace("${initialFee}", creditOnlineNowViewModel.InitialFee.ToString("C"))
                .Replace("${installments}", CommonHelper.GetSantanderBankInstallmentsMessage(creditOnlineNowViewModel.Installments))
                .Replace("${economicActivity}", CommonHelper.GetSantanderBankEconomicActivityMessage(creditOnlineNowViewModel.ActividadEconomica))
                .Replace("${independentActivity}", CommonHelper.GetSantanderBankIndependentActivityMessage(creditOnlineNowViewModel.ActividadIndependiente))
                .Replace("${monthlyIncome}", creditOnlineNowViewModel.MonthlyIncome.ToString("C"))
                .Replace("${Date}", DateTime.Now.Year.ToString());

            try
            {
                await this._emailSender.SendEmailAsync(this._fundingRequestEmailTo, "Nueva solicitud de crédito desde Solicita tu crédito ya", contentTemplate, null, new List<string>() { this._fundingRequestEmailToCc, this._fundingRequestEmailToCc2 });
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception($"Error sending emails => {ex.Message}");
            }
        }

        /// <summary>
        /// Emails the ford test drive body builder.
        /// </summary>
        /// <param name="testDriveFordViewModel">The test drive ford view model.</param>
        /// <param name="emailTemplateUrl">The email template URL.</param>
        /// <returns>Ford Test Drive Body Content</returns>
        private string EmailFordTestDriveBodyBuilder(TestDriveFordViewModel testDriveFordViewModel, string emailTemplateUrl)
        {
            var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var testDriveImgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/ford/home/ford_test_drive.jpg";
            var date = DateTime.Now;
            return new StreamReader(emailTemplateUrl).ReadToEnd()
               .Replace("${logoUrl}", imgUrl)
               .Replace("${footerImg}", testDriveImgUrl)
               .Replace("${firstLabel}", "Un cliente ha realizado una solicitud de <b>Test Drive</b> (FORD)")
               .Replace("${Names}", $"{testDriveFordViewModel.FullName}")
               .Replace("${Email}", $"{testDriveFordViewModel.Email}")
               .Replace("${PhoneNumber}", $"{testDriveFordViewModel.PhoneNumber}")
               .Replace("${Headquarter}", $"{testDriveFordViewModel.Headquarter}")
               .Replace("${Product}", $"{testDriveFordViewModel.Product}")
               .Replace("${Date}", $"{testDriveFordViewModel.Date:dddd, dd MMMM yyyy HH:mm:ss}")
               .Replace("${WorkDate}", date.ToString("dddd, dd MMMM yyyy"))
               .Replace("${Date}", $"{date.Year}");
        }

        /// <summary>
        /// Emails the massey operators club body builder.
        /// </summary>
        /// <param name="operatorsClubViewModel">The operators club view model.</param>
        /// <param name="emailTemplateUrl">The email template URL.</param>
        /// <returns>String with body content</returns>
        private string EmailMasseyOperatorsClubBodyBuilder(OperatorsClubViewModel operatorsClubViewModel, string emailTemplateUrl)
        {
            var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var footerImgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/massey_ferguson/home/header-4.png";
            var date = DateTime.Now;
            return new StreamReader(emailTemplateUrl).ReadToEnd()
               .Replace("${logoUrl}", imgUrl)
               .Replace("${footerImg}", footerImgUrl)
               .Replace("${firstLabel}", "Un cliente ha realizado una solicitud de registro al <b>Club de operadores Massey</b>")
               .Replace("${Names}", $"{operatorsClubViewModel.Name}")
               .Replace("${LastName}", $"{operatorsClubViewModel.LastName}")
               .Replace("${PhoneNumber}", $"{operatorsClubViewModel.PhoneNumber}")
               .Replace("${Email}", $"{operatorsClubViewModel.Email}")
               .Replace("${DateOfBirth}", $"{operatorsClubViewModel.DateOfBirth:dddd, dd MMMM yyyy}")
               .Replace("${Serial}", $"{operatorsClubViewModel.Serial}")
               .Replace("${ChasisNumber}", $"{operatorsClubViewModel.ChasisNumber}")
               .Replace("${Ubication}", $"{operatorsClubViewModel.Ubication}")
               .Replace("${WorkDate}", date.ToString("dddd, dd MMMM yyyy"))
               .Replace("${Date}", $"{date.Year}");
        }
        #endregion
    }
}