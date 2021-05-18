using Ecommerce.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using motovalle.Ecommerce.Helpers;
using motovalle.Ecommerce.Helpers.ApiRequest;
using motovalle.Ecommerce.Helpers.EmailSender;
using motovalle.Ecommerce.Models.Entities.Identity;
using reCAPTCHA.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSenderExtended _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;
        private readonly IRecaptchaService _recaptchaService;
        private readonly HttpClient _httpClientInstance;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSenderExtended emailSender,
            IConfiguration configuration,
            IWebHostEnvironment host,
            IRecaptchaService recaptchaService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._logger = logger;
            this._emailSender = emailSender;
            this._configuration = configuration;
            this._host = host;
            this._recaptchaService = recaptchaService;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Correo electrónico es requerido.")]
            [EmailAddress]
            [Display(Name = "Correo")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Contraseña es requerida.")]
            [StringLength(100, ErrorMessage = "La contraseña debe contener al menos {2} y máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            public string ConfirmPassword { get; set; }

            /// <summary>
            /// Gets or sets the phone number.
            /// </summary>
            /// <value>
            /// The phone number.
            /// </value>
            [Required(ErrorMessage = "Número de teléfono es requerido."), DataType(DataType.PhoneNumber), StringLength(35), Display(Name = "Teléfono/Celular")]
            public string PhoneNumber { get; set; }

            /// <summary>
            /// Gets or sets the first name.
            /// </summary>
            /// <value>
            /// The first name.
            /// </value>
            [Required(ErrorMessage = "Nombres son requeridos."), DataType(DataType.Text), StringLength(70), Display(Name = "Nombres")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the last name.
            /// </summary>
            /// <value>
            /// The last name.
            /// </value>
            [DataType(DataType.Text), StringLength(70), Display(Name = "Apellidos")]
            public string LastName { get; set; }

            /// <summary>
            /// Gets or sets the alternate number.
            /// </summary>
            /// <value>
            /// The alternate number.
            /// </value>
            [DataType(DataType.PhoneNumber), StringLength(45), Display(Name = "Número alterno")]
            public string AlternateNumber { get; set; }

            /// <summary>
            /// Gets or sets the address.
            /// </summary>
            /// <value>
            /// The address.
            /// </value>
            [Required(ErrorMessage = "Dirección es requerida."), DataType(DataType.Text), StringLength(70), Display(Name = "Dirección")]
            public string Address { get; set; }

            /// <summary>
            /// Gets or sets the city.
            /// </summary>
            /// <value>
            /// The city.
            /// </value>
            [Required(ErrorMessage = "Ciudad es requerida."), DataType(DataType.Text), StringLength(45), Display(Name = "Ciudad")]
            public string City { get; set; }

            /// <summary>
            /// Gets or sets the state.
            /// </summary>
            /// <value>
            /// The state.
            /// </value>
            [Required(ErrorMessage = "Departamento es requerido."), DataType(DataType.Text), StringLength(45), Display(Name = "Departamento")]
            public string State { get; set; }

            /// <summary>
            /// Gets or sets the zip code.
            /// </summary>
            /// <value>
            /// The zip code.
            /// </value>
            [DataType(DataType.PostalCode), StringLength(45), Display(Name = "Código postal")]
            public string ZipCode { get; set; }

            /// <summary>
            /// Gets or sets the country.
            /// </summary>
            /// <value>
            /// The country.
            /// </value>
            [Required(ErrorMessage = "País es requerido."), DataType(DataType.Text), StringLength(45), Display(Name = "País")]
            public string Country { get; set; }

            /// <summary>
            /// Gets or sets the name of the company.
            /// </summary>
            /// <value>
            /// The name of the company.
            /// </value>
            [DataType(DataType.Text), StringLength(45), Display(Name = "Empresa"), Required(ErrorMessage = "{0} es requerido")]
            public string CompanyName { get; set; }

            /// <summary>
            /// Gets or sets the bill address.
            /// </summary>
            /// <value>
            /// The bill address.
            /// </value>
            [StringLength(70), Display(Name = "Dirección de facturación")]
            public string BillAddress { get; set; }

            /// <summary>
            /// Gets or sets the bill city.
            /// </summary>
            /// <value>
            /// The bill city.
            /// </value>
            [StringLength(45), Display(Name = "Ciudad de facturación")]
            public string BillCity { get; set; }

            /// <summary>
            /// Gets or sets the state of the bill.
            /// </summary>
            /// <value>
            /// The state of the bill.
            /// </value>
            [StringLength(45), Display(Name = "Departamento de facturación")]
            public string BillState { get; set; }

            /// <summary>
            /// Gets or sets the bill zip code.
            /// </summary>
            /// <value>
            /// The bill zip code.
            /// </value>
            [DataType(DataType.PostalCode), StringLength(45), Display(Name = "Código postal de facturación")]
            public string BillZipCode { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var cookieConsent = HttpContext.Request.Cookies["MotovalleCookieConsent"];
            if (string.IsNullOrEmpty(cookieConsent))
            {
                ModelState.AddModelError(string.Empty, "Debes aceptar el consentimiento de Cookies.");
            }

            var recaptcha = await this._recaptchaService.Validate(this.Request);
            if (!recaptcha.success)
            {
                ModelState.AddModelError(string.Empty, "Hubo un error validando reCATPCHA. Por favor intente nuevamente.");
            }

            if (ModelState.IsValid)
            {
                var fullName = $"{Input.Name} {Input.LastName}";
                var customerApi = new CustomerApi(this._httpClientInstance);
                Customers customerCreated;
                //// Creates a new customer for ecommerce
                try
                {
                    var customer = new Customers()
                    {
                        AlternateNumber = Input.AlternateNumber,
                        EmailAddress = Input.Email,
                        FullName = fullName,
                        PhoneNumber = Input.PhoneNumber,
                        ShipToZipcode = Input.ZipCode,
                        ShipToAddress = Input.Address,
                        ShipToCity = Input.City,
                        ShipToState = Input.State,
                        BillToAddress = Input.BillAddress,
                        BillToCity = Input.BillCity,
                        BillToState = Input.BillState,
                        BillToZipcode = Input.BillZipCode
                    };

                    ////Check previous creation
                    var customerCheck = (await customerApi.GetAllRecords()).FirstOrDefault(x => x.EmailAddress == Input.Email);
                    if (customerCheck == null)
                    {
                        customerCreated = await customerApi.CreateRecord(customer);
                    }
                    else
                    {
                        customer.CustomersId = customerCheck.CustomersId;
                        await customerApi.UpdateRecord(customerCheck.CustomersId, customer);
                        customerCreated = customerCheck;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                    ModelState.AddModelError(string.Empty, "Error creando el nuevo usuario. Intente nuevamente");
                    return Page();
                }

                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    Address = Input.Address,
                    City = Input.City,
                    AlternateNumber = Input.AlternateNumber,
                    CompanyName = Input.CompanyName,
                    Country = Input.Country,
                    Name = Input.Name,
                    LastName = Input.LastName,
                    State = Input.State,
                    ZipCode = Input.ZipCode,
                    FullName = fullName,
                    RegisterDate = DateTime.Now,
                    Status = "ACTIVO",
                    CustomersId = customerCreated.CustomersId
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    ////Set default role
                    var userCreated = await this._userManager.FindByNameAsync(user.UserName);
                    await this._userManager.AddToRoleAsync(userCreated, "CUSTOMER");

                    ////Send email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    try
                    {
                        var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateIdentity")}";
                        var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
                        var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
                            .Replace("${logoUrl}", imgUrl)
                            .Replace("${user}", $"{user.Name}")
                            .Replace("${firstLabel}", "Estás a un solo paso. Por favor confirma tu <b>correo</b> haciendo clic en el link a continuación")
                            .Replace("${callbackUrl}", $"{HtmlEncoder.Default.Encode(callbackUrl)}")
                            .Replace("${buttonCaption}", "Confirmar")
                            .Replace("${Date}", $"{DateTime.Now.Year.ToString()}");

                        await _emailSender.SendEmailAsync(Input.Email, "Confirma tu correo", emailTemplate);

                        if (_userManager.Options.SignIn.RequireConfirmedEmail)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                        }

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                        ModelState.AddModelError(string.Empty, "Error enviando el correo de confirmación.");
                    }
                }

                if (customerCreated.CustomersId > 0)
                {
                    await customerApi.DeleteRecord(customerCreated.CustomersId);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
