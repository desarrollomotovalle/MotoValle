// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrandsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Brands controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Resources;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Brands controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BrandsController : Controller
    {
        #region Ctor
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The makes identifier
        /// </summary>
        private readonly List<Makes> _makes;

        /// <summary>
        /// The categories
        /// </summary>
        private readonly List<Categories> _categories;

        /// <summary>
        /// The exception control
        /// </summary>
        private readonly bool _exceptionControl = false;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IWebHostEnvironment _host;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The headquarters view model
        /// </summary>
        private readonly List<HeadquartersViewModel> _headquartersViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandsController" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="host">The host.</param>
        public BrandsController(IConfiguration configuration, IWebHostEnvironment host)
        {
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            var makesApi = new MakesApi(this._httpClientInstance);
            this._headquartersViewModel = configuration.GetSection("Headquarters").Get<List<HeadquartersViewModel>>();

            try
            {
                this._configuration = configuration;
                this._host = host;
                this._makes = makesApi.GetAllRecords().Result;
                this._headquartersViewModel = this._headquartersViewModel.Where(x => x.Make == "Ford").ToList();
            }
            catch (Exception ex)
            {
                this._makes = new List<Makes>();
                this._categories = new List<Categories>();
                this._exceptionControl = true;
            }
        }
        #endregion

        #region Ford
        /// <summary>
        /// Fords this instance.
        /// </summary>
        /// <returns>Ford brand view</returns>
        public async Task<IActionResult> Ford()
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var makeId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Contains("ford"))?.MakesId ?? 0;
            var productAndMakeBases = new List<ProductAndMakeBase>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                productAndMakeBases = await makesApi.GetProductAndMakeBase(makeId);
                productAndMakeBases = productAndMakeBases.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).OrderBy(x => Guid.NewGuid()).ToList();
                ViewBag.Categories = productAndMakeBases.GroupBy(x => x.CategoriesId).Select(x => x.Select(y => new List<string>() { y.CategoriesId.ToString(), y.CategoryName }).FirstOrDefault()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            ViewBag.Products = new SelectList(productAndMakeBases, "ProductName", "ProductName");
            ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress");
            var fordBrandViewModel = new FordBrandViewModel()
            {
                ProductsAndMakeBase = productAndMakeBases
            };

            return this.View(fordBrandViewModel);
        }

        /// <summary>
        /// Fords the test drive.
        /// </summary>
        /// <returns></returns>
        [HttpPost("[controller]/Ford")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FordTestDrive(TestDriveFordViewModel testDriveFordViewModel)
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var makeId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Contains("ford"))?.MakesId ?? 0;
            var productAndMakeBases = new List<ProductAndMakeBase>();
            var fordBrandViewModel = new FordBrandViewModel()
            {
                TestDriveFordViewModel = testDriveFordViewModel,
                ProductsAndMakeBase = productAndMakeBases
            };

            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                productAndMakeBases = await makesApi.GetProductAndMakeBase(makeId);
                productAndMakeBases = productAndMakeBases.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).OrderBy(x => Guid.NewGuid()).ToList();
                fordBrandViewModel.ProductsAndMakeBase = productAndMakeBases;
                ViewBag.Categories = productAndMakeBases.GroupBy(x => x.CategoriesId).Select(x => x.Select(y => new List<string>() { y.CategoriesId.ToString(), y.CategoryName }).FirstOrDefault()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                ViewBag.Products = new SelectList(productAndMakeBases, "ProductName", "ProductName", testDriveFordViewModel.Product);
                ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress", testDriveFordViewModel.Headquarter);
                return this.View(nameof(Ford), fordBrandViewModel);
            }

            ViewBag.Products = new SelectList(productAndMakeBases, "ProductName", "ProductName", testDriveFordViewModel.Product);
            ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress", testDriveFordViewModel.Headquarter);

            if (testDriveFordViewModel.Date <= DateTime.Now)
            {
                ModelState.AddModelError("Date", "Debe ingresar una fecha de agendamiento mayor a hoy.");
            }

            var dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 30, 0);
            var dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0);
            //if (testDriveFordViewModel.Hour.TimeOfDay < dateFrom.TimeOfDay || testDriveFordViewModel.Hour.TimeOfDay > dateTo.TimeOfDay)
            //{
            //    ModelState.AddModelError("Date", "Debe ingresar una hora de agendamiento entre las 07:30 y 17:30.");
            //}

            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Verifique los datos ingresados";
                return this.View(nameof(Ford), fordBrandViewModel);
            }

            //Send email.
            var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateTestDriveFord")}";
            string emailBody = this.EmailFordTestDriveBodyBuilder(testDriveFordViewModel, emailTemplateUrl);
            var emailTo = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            var emailSender = new EmailSender(this._configuration);

            try
            {
                await emailSender.SendEmailAsync(emailTo, "Test Drive FORD (Solicitud de Agendamiento)", emailBody);
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                return this.View(nameof(Ford), fordBrandViewModel);
            }

            ViewBag.Products = new SelectList(productAndMakeBases, "ProductName", "ProductName");
            ViewBag.Headquarters = new SelectList(this._headquartersViewModel, "Name", "NameWithAddress");
            ViewBag.Error = false;
            fordBrandViewModel.TestDriveFordViewModel = null;
            return this.View(nameof(Ford), fordBrandViewModel);
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
            var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
               .Replace("${logoUrl}", imgUrl)
               .Replace("${footerImg}", testDriveImgUrl)
               .Replace("${firstLabel}", "Un cliente ha realizado una solicitud de <b>Test Drive</b> (FORD)")
               .Replace("${Names}", $"{testDriveFordViewModel.FullName}")
               .Replace("${Email}", $"{testDriveFordViewModel.Email}")
               .Replace("${PhoneNumber}", $"{testDriveFordViewModel.PhoneNumber}")
               .Replace("${Headquarter}", $"{testDriveFordViewModel.Headquarter}")
               .Replace("${Product}", $"{testDriveFordViewModel.Product}")
               .Replace("${Date}", $"{testDriveFordViewModel.Date:dddd, dd MMMM yyyy HH:mm:ss}")
               //.Replace("${Hour}", $"{testDriveFordViewModel.Hour:HH:mm:ss}")
               .Replace("${WorkDate}", date.ToString("dddd, dd MMMM yyyy"))
               .Replace("${Date}", $"{date.Year}");
            return emailTemplate;
        }
        #endregion

        #region Mazda
        /// <summary>
        /// Mazda this instance.
        /// </summary>
        /// <returns>Mazda Brand view</returns>
        public async Task<IActionResult> Mazda()
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var makeId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Contains("mazda"))?.MakesId ?? 0;
            var productAndMakeBases = new List<ProductAndMakeBase>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                productAndMakeBases = await makesApi.GetProductAndMakeBase(makeId);
                productAndMakeBases = productAndMakeBases.Where(x => x.QuantityInStock > 0 && x.AllowShow > 0).OrderBy(x => Guid.NewGuid()).ToList();
                ViewBag.Categories = productAndMakeBases.GroupBy(x => x.CategoriesId).Select(x => x.Select(y => new List<string>() { y.CategoriesId.ToString(), y.CategoryName }).FirstOrDefault()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(productAndMakeBases);
        }
        #endregion

        #region Massey Ferguson
        /// <summary>
        /// Massey the ferguson.
        /// </summary>
        /// <returns>Massey Ferguson view</returns>
        public async Task<IActionResult> MasseyFerguson()
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var makeId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Contains("massey ferguson"))?.MakesId ?? 0;
            var productAndMakeBases = new List<ProductAndMakeBase>();
            var masseyViewModel = new MasseyFergusonBrandViewModel() { ProductsAndMakeBase = productAndMakeBases };

            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                productAndMakeBases = await makesApi.GetProductAndMakeBase(makeId);
                productAndMakeBases = productAndMakeBases.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).OrderBy(x => Guid.NewGuid()).ToList();
                masseyViewModel.ProductsAndMakeBase = productAndMakeBases;
                ViewBag.Categories = productAndMakeBases.GroupBy(x => x.CategoriesId).Select(x => x.Select(y => new List<string>() { y.CategoriesId.ToString(), y.CategoryName }).FirstOrDefault()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(masseyViewModel);
        }

        /// <summary>
        /// Operatorses the club.
        /// </summary>
        /// <param name="operatorsClubViewModel">The operators club view model.</param>
        /// <returns>Massey Ferguson view brand</returns>
        [HttpPost("[controller]/MasseyFerguson")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OperatorsClubMasseyFerguson(OperatorsClubViewModel operatorsClubViewModel)
        {
            ViewBag.Message = this._exceptionControl ? StaticResources.NotAvailable : null;
            var makeId = this._makes.FirstOrDefault(x => x.MakeName.ToLower().Contains("massey ferguson"))?.MakesId ?? 0;
            var masseyViewModel = new MasseyFergusonBrandViewModel();
            var productAndMakeBases = new List<ProductAndMakeBase>();
            try
            {
                var makesApi = new MakesApi(this._httpClientInstance);
                productAndMakeBases = await makesApi.GetProductAndMakeBase(makeId);
                productAndMakeBases = productAndMakeBases.Where(x => x.AllowShow > 0 && x.QuantityInStock > 0).OrderBy(x => Guid.NewGuid()).ToList();
                masseyViewModel.ProductsAndMakeBase = productAndMakeBases;
                ViewBag.Categories = productAndMakeBases.GroupBy(x => x.CategoriesId).Select(x => x.Select(y => new List<string>() { y.CategoriesId.ToString(), y.CategoryName }).FirstOrDefault()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = StaticResources.NotAvailable;
                return this.View(nameof(MasseyFerguson), masseyViewModel);
            }

            var dateControl = DateTime.Now - operatorsClubViewModel.DateOfBirth;
            var yearsControl = Math.Abs(Math.Ceiling(dateControl.TotalDays / 365));
            if (yearsControl < 18 || operatorsClubViewModel.DateOfBirth > DateTime.Now)
            {
                ModelState.AddModelError("DateOfBirth", "Debe ingresar una fecha de nacimiento válida.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Verifique los datos ingresados";
                masseyViewModel.OperatorsClubViewModel = operatorsClubViewModel;
                return this.View(nameof(MasseyFerguson), masseyViewModel);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateOperatorsClubMassey")}";
            var emailBody = this.EmailMasseyOperatorsClubBodyBuilder(operatorsClubViewModel, emailTemplateUrl);
            var emailTo = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            var emailSender = new EmailSender(this._configuration);

            try
            {
                await emailSender.SendEmailAsync(emailTo, "Club de Operadores Massey (Solicitud de Registro)", emailBody);
            }
            catch (Exception ex)
            {
                ViewBag.Error = true;
                return this.View(nameof(MasseyFerguson), masseyViewModel);
            }

            masseyViewModel.OperatorsClubViewModel = null;
            ViewBag.Error = false;
            return this.View(nameof(MasseyFerguson), masseyViewModel);
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
            var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
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
            return emailTemplate;
        }
        #endregion
    }
}