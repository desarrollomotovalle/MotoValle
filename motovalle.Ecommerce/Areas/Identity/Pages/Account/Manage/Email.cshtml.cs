using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using motovalle.Ecommerce.Helpers.EmailSender;
using motovalle.Ecommerce.Models.Entities.Identity;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSenderExtended _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;

        public EmailModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSenderExtended emailSender,
            IConfiguration configuration,
            IWebHostEnvironment host)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this._configuration = configuration;
            this._host = host;
        }

        public string Username { get; set; }

        public string Correo { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Nuevo correo es requerido.")]
            [EmailAddress]
            [Display(Name = "Nuevo crreo electrónico")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Correo = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("./Login");
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("./Login");
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);


                try
                {
                    var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateIdentity")}";
                    var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
                    var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
                        .Replace("${logoUrl}", imgUrl)
                        .Replace("${user}", $"{user.Name}")
                        .Replace("${firstLabel}", "Estás a un solo paso. Por favor confirma tu <b>correo</b> haciendo clic en el link a continuación")
                        .Replace("${buttonCaption}", "Confirmar")
                        .Replace("${callbackUrl}", $"{HtmlEncoder.Default.Encode(callbackUrl)}")
                        .Replace("${Date}", $"{DateTime.Now.Year.ToString()}");

                    await _emailSender.SendEmailAsync(email, "Confirma tu correo", emailTemplate);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error enviando el correo de confirmación.");
                }

                StatusMessage = "Enlace de confirmación para cambiar el correo electrónico ha sido enviado. Por favor revise su buzón de correo electrónico.";
                return RedirectToPage();
            }

            StatusMessage = "Su correo electrónico no ha cambiado.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("./Login");
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
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
                    .Replace("${buttonCaption}", "Confirmar")
                    .Replace("${callbackUrl}", $"{HtmlEncoder.Default.Encode(callbackUrl)}")
                    .Replace("${Date}", $"{DateTime.Now.Year.ToString()}");

                await _emailSender.SendEmailAsync(email, "Confirma tu correo", emailTemplate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error enviando el correo de confirmación.");
            }


            StatusMessage = "El mensaje de verificación ha sido enviado. Por favor revise el buzon de su correo electrónico.";
            return RedirectToPage();
        }
    }
}
