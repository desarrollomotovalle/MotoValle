// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Info Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Models.ViewModels;
    using reCAPTCHA.AspNetCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Info Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [AllowAnonymous]
    public class InfoController : Controller
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The email template URI
        /// </summary>
        private readonly string _emailTemplateUri;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IWebHostEnvironment _host;

        /// <summary>
        /// The recaptcha service
        /// </summary>
        private readonly IRecaptchaService _recaptchaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoController" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public InfoController(IConfiguration configuration, IWebHostEnvironment host, IRecaptchaService recaptchaService)
        {
            this._configuration = configuration;
            this._emailTemplateUri = configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplate");
            this._host = host;
            this._recaptchaService = recaptchaService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index info view</returns>
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ContactUs view</returns>
        [HttpGet]
        public IActionResult ContactUs()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index info view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsViewModel contactUs)
        {
            ViewBag.PostsFilter = "General";
            
            if (!contactUs.DataTreatment)
            {
                ModelState.AddModelError("DataTreatment", "Debe aceptar las políticas de tratamiento de datos");
            }

            if (!contactUs.TermsAndConditions)
            {
                ModelState.AddModelError("TermsAndConditions", "Debe aceptar los términos y condiciones.");
            }

            var recaptcha = await this._recaptchaService.Validate(this.Request);
            if (!recaptcha.success)
            {
                ModelState.AddModelError(string.Empty, "Hubo un error validando recatpcha. Por favor intente nuevamente.");
            }

            if (!ModelState.IsValid)
            {
                return this.View(contactUs);
            }

            var emailTemplateUrl = $"{this._host.WebRootPath}{this._emailTemplateUri}";
            var emailBody = this.EmailBodyBuilder(contactUs, emailTemplateUrl);
            var emailSender = new EmailSender(this._configuration);
            var emailTo = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToDefault");
            var emailToCc = this._configuration.GetSection("EmailSettings").GetValue<string>("EmailToCcDefault");
            try
            {
                await emailSender.SendEmailAsync(emailTo, "Solicitud de información", emailBody, new List<string>(), new List<string>() { emailToCc } );
            }
            catch (Exception ex)
            {
                ViewBag.PostsFilter = "General";
                ViewBag.Error =  true;
                this.View();
            }

            ViewBag.PostsFilter = "General";
            ViewBag.Error = false;
            return this.View();
        }

        /// <summary>
        /// Headquarterses this instance.
        /// </summary>
        /// <returns>Headquarters view</returns>
        [HttpGet("[controller]/Sedes")]
        public IActionResult Headquarters()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }

        /// <summary>
        /// Hirings this instance.
        /// </summary>
        /// <returns>We are hiring view</returns>
        [HttpGet("[controller]/TrabajeConNosotros")]

        public IActionResult Hiring()
        {
            ViewBag.PostsFilter = "General";
            return this.View();
        }

        /// <summary>
        /// Emails the body builder.
        /// </summary>
        /// <param name="contactUs">The contact us.</param>
        /// <param name="emailTemplateUrl">The email template URL.</param>
        /// <returns>Email body</returns>
        private string EmailBodyBuilder(ContactUsViewModel contactUs, string emailTemplateUrl)
        {
            var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var date = DateTime.Now;
            var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
               .Replace("${logoUrl}", imgUrl)
               .Replace("${firstLabel}", "Un cliente ha realizado una solicitud de contacto")
               .Replace("${ContactType}", contactUs.ContactUsType.ToString())
               .Replace("${WorkDate}", date.ToString("dddd, dd MMMM yyyy"))
               .Replace("${Name}", contactUs.Name)
               .Replace("${Email}", contactUs.EmailAddress)
               .Replace("${Phone}", contactUs.PhoneNumber)
               .Replace("${Message}", contactUs.Message)
               .Replace("${Date}", $"{date.Year}");
            return emailTemplate;
        }
    }
}