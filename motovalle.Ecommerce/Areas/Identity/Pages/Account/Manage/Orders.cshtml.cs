// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders Model Razor page
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Models.Entities.Identity;

    /// <summary>
    /// Orders Model Razor page
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class OrdersModel : PageModel
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersModel"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="configuration">The configuration.</param>
        public OrdersModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public IList<Orders> Orders { get; set; }

        /// <summary>
        /// Called when [get asynchronous].
        /// </summary>
        /// <returns>Order Page</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("./Login");
                //return NotFound($"Unable to load user with ID '{this._userManager.GetUserId(User)}'.");
            }

            var ordersApi = new OrdersApi(this._httpClientInstance);
            Orders = await ordersApi.GetRecordsForCustomer(user.CustomersId ?? 0);
            return Page();
        }
    }
}
