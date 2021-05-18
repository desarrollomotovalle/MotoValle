using Ecommerce.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using motovalle.Ecommerce.Helpers;
using motovalle.Ecommerce.Helpers.ApiRequest;
using motovalle.Ecommerce.Helpers.EmailSender;
using motovalle.Ecommerce.Helpers.Services;
using motovalle.Ecommerce.Models.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSenderExtended _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;
        private readonly IGuestUserHelper _guestUserHelper;
        private readonly HttpClient _httpClientInstance;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSenderExtended emailSender,
            IConfiguration configuration,
            IWebHostEnvironment host,
            IGuestUserHelper guestUserHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            this._configuration = configuration;
            this._host = host;
            this._guestUserHelper = guestUserHelper;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Correo electrónico es requerido")]
            [EmailAddress]
            [Display(Name = "Correo")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Contraseña es requerida")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [Display(Name = "¿Recuérdame?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var cookieConsent = HttpContext.Request.Cookies["MotovalleCookieConsent"];
            if (string.IsNullOrEmpty(cookieConsent))
            {
                ModelState.AddModelError(string.Empty, "Debes aceptar el consentimiento de Cookies.");
            }

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    //Create claims details based on the user information
                    var user = await this._userManager.FindByEmailAsync(Input.Email);
                    var roles = await this._userManager.GetRolesAsync(user);
                    var claims = new List<Claim>() {
                        new Claim(JwtRegisteredClaimNames.Sub, this._configuration.GetSection("JwtSettings").GetValue<string>("Subject")),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("FirstName", user.Name),
                        new Claim("LastName", user.LastName),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email)
                    };

                    if (roles.Count > 0)
                    {
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim("role", item));
                        }
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration.GetSection("JwtSettings").GetValue<string>("Key")));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(this._configuration.GetSection("JwtSettings").GetValue<string>("Issuer"), this._configuration.GetSection("JwtSettings").GetValue<string>("Audience"), claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    var wroteToken = new JwtSecurityTokenHandler().WriteToken(token);
                    await this._userManager.SetAuthenticationTokenAsync(user, this._configuration.GetSection("JwtSettings").GetValue<string>("Issuer"), this._configuration.GetSection("JwtSettings").GetValue<string>("Subject"), wroteToken);
                    claims.Clear();

                    //Transform Guest Cart in Real Cart
                    var guestID = this._guestUserHelper.GetGuestId();
                    var shoppingCartGuestApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                    var shoppingCartGuest = await shoppingCartGuestApi.GetRecordsForCustomer(guestID);
                    if (shoppingCartGuest != null && shoppingCartGuest.Count > 0)
                    {
                        this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, wroteToken);
                        var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                        var oldShoppingCart = await shoppingCartApi.GetRecordsForCustomer((long)user.CustomersId);
                        if (oldShoppingCart != null && oldShoppingCart.Count() > 0)
                        {
                            await shoppingCartApi.DeleteRecordsForCustomer((long)user.CustomersId);
                        }

                        foreach (var item in shoppingCartGuest)
                        {
                            var record = new ShoppingCartRecords()
                            {
                                FkInventoryItemsId = item.FkInventoryItemsId ?? 0,
                                FkProductsId = item.FkProductsId,
                                Quantity = item.Quantity,
                                FkCustomersId = user.CustomersId ?? 0
                            };

                            await shoppingCartApi.CreateRecord(record);
                        }

                        await shoppingCartGuestApi.DeleteRecordsForCustomer(guestID);
                    }

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout", new { LockoutEnd = user.LockoutEnd });
                }
                if (result.IsNotAllowed)
                {
                    _logger.LogWarning("User email is not confirmed.");
                    ModelState.AddModelError(string.Empty, "Correo no ha sido confirmado.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "El correo de verificación ha sido enviado. Por favor revisa tu buzón de correo electrónico.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
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
                    .Replace("${Date}", $"{DateTime.Now.Year}");

                await _emailSender.SendEmailAsync(Input.Email, "Confirma tu correo", emailTemplate);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error enviando el correo de confirmación.");
            }


            ModelState.AddModelError(string.Empty, "El correo de verificación ha sido enviado. Por favor revisa tu buzón de correo electrónico.");
            return Page();
        }
    }
}
