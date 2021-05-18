using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using motovalle.Ecommerce.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using motovalle.Ecommerce.Helpers.EmailSender;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderExtended _sender;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, IEmailSenderExtended sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"No se puede cargar al usuario con el correo electrónico: '{email}'.");
            }

            Email = email;
            //// Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = false;
            ////if (DisplayConfirmAccountLink)
            ////{
            ////    var userId = await _userManager.GetUserIdAsync(user);
            ////    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            ////    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            ////    EmailConfirmationUrl = Url.Page(
            ////        "/Account/ConfirmEmail",
            ////        pageHandler: null,
            ////        values: new { area = "Identity", userId = userId, code = code },
            ////        protocol: Request.Scheme);
            ////}

            return Page();
        }
    }
}
