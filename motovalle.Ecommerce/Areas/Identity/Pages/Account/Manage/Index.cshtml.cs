using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using motovalle.Ecommerce.Helpers;
using motovalle.Ecommerce.Helpers.ApiRequest;
using motovalle.Ecommerce.Models.Entities.Identity;

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HttpClient _httpClientInstance;
        private readonly IConfiguration _configuration;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
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
            [DataType(DataType.Text), StringLength(45), Display(Name = "Empresa")]
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

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var customerApi = new CustomerApi(this._httpClientInstance);

            try
            {
                var customer = await customerApi.GetRecord(user.CustomersId ?? 0);
                Username = userName;

                Input = new InputModel
                {
                    PhoneNumber = phoneNumber,
                    Name = user.Name,
                    LastName = user.LastName,
                    AlternateNumber = user.AlternateNumber,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    Country = user.Country,
                    CompanyName = user.CompanyName,
                    BillAddress = customer.BillToAddress,
                    BillCity = customer.BillToCity,
                    BillState = customer.BillToState,
                    BillZipCode = customer.BillToZipcode
                };
            }
            catch (Exception)
            {
                StatusMessage = "Error cargando información del usuario. Por favor intente nuevamnete.";
                Input = new InputModel
                {
                    PhoneNumber = phoneNumber,
                    Name = user.Name,
                    LastName = user.LastName,
                    AlternateNumber = user.AlternateNumber,
                    Address = user.Address,
                    City = user.City,
                    State = user.State,
                    ZipCode = user.ZipCode,
                    Country = user.Country,
                    CompanyName = user.CompanyName
                };
            }

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

        public async Task<IActionResult> OnPostAsync()
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

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Error inesperado al intentar configurar el número de teléfono.";
                    return RedirectToPage();
                }
            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
                var setAddressResult = await _userManager.UpdateAsync(user);
                if (!setAddressResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting address for user with ID '{userId}'.");
                }
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting city for user with ID '{userId}'.");
                }
            }

            if (Input.AlternateNumber != user.AlternateNumber)
            {
                user.AlternateNumber = Input.AlternateNumber;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting alternate number for user with ID '{userId}'.");
                }
            }

            if (Input.CompanyName != user.CompanyName)
            {
                user.CompanyName = Input.CompanyName;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting company name for user with ID '{userId}'.");
                }
            }

            if (Input.Country != user.Country)
            {
                user.Country = Input.Country;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting country for user with ID '{userId}'.");
                }
            }

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting first name for user with ID '{userId}'.");
                }
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting last name for user with ID '{userId}'.");
                }
            }

            if (Input.State != user.State)
            {
                user.State = Input.State;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting state for user with ID '{userId}'.");
                }
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting state for user with ID '{userId}'.");
                }
            }

            if (Input.ZipCode != user.ZipCode)
            {
                user.ZipCode = Input.ZipCode;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting zip code for user with ID '{userId}'.");
                }
            }

            var fullName = $"{Input.Name} {Input.LastName}";
            if (fullName != user.FullName)
            {
                user.FullName = fullName;
                var setResult = await _userManager.UpdateAsync(user);
                if (!setResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting full name for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            ////Updates customer entity
            try
            {
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var customerApi = new CustomerApi(this._httpClientInstance);
                var customer = await customerApi.GetRecord(user.CustomersId ?? 0);
                customer.AlternateNumber = Input.AlternateNumber;
                customer.FullName = fullName;
                customer.PhoneNumber = Input.PhoneNumber;
                customer.ShipToZipcode = Input.ZipCode;
                customer.ShipToAddress = Input.Address;
                customer.ShipToCity = Input.City;
                customer.ShipToState = Input.State;
                customer.BillToAddress = Input.BillAddress;
                customer.BillToCity = Input.BillCity;
                customer.BillToState = Input.BillState;
                customer.BillToZipcode = Input.BillZipCode;
                await customerApi.UpdateRecord(customer.CustomersId, customer);
            }
            catch (Exception)
            {
                StatusMessage = "Error inesperado al intentar actualizar el perfil del usuario.";
                return Page();
            }

            StatusMessage = "Tu perfil ha sido actualizado";
            return RedirectToPage();
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
    }
}
