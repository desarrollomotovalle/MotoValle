using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using motovalle.Ecommerce.Helpers.EmailSender;
using motovalle.Ecommerce.Models.Entities.Identity;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderExtended _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSenderExtended emailSender, IConfiguration configuration, IWebHostEnvironment host)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            this._configuration = configuration;
            this._host = host;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Correo es requerido")]
            [EmailAddress]
            [Display(Name = "Correo electrónico")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code, userId = user.Id },
                    protocol: Request.Scheme);

                try
                {
                    var emailTemplateUrl = $"{this._host.WebRootPath}{this._configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateIdentity")}";
                    var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
                    var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
                        .Replace("${logoUrl}", imgUrl)
                        .Replace("${user}", $"{user.Name}")
                        .Replace("${firstLabel}", "Estás a un solo paso de reestablecer tu contaseña. Por favor confirma tu <b>correo</b> haciendo clic en el link a continuación:")
                        .Replace("${callbackUrl}", $"{HtmlEncoder.Default.Encode(callbackUrl)}")
                        .Replace("${buttonCaption}", "Confirmar")
                        .Replace("${Date}", $"{DateTime.Now.Year.ToString()}");

                    await _emailSender.SendEmailAsync(Input.Email, "Reestablecer contraseña", emailTemplate);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error enviando el correo de confirmación.");
                }

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
