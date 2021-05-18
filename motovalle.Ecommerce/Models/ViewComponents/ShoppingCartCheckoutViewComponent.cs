// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartCheckoutViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Checkout View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.Services;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using motovalle.Ecommerce.Resources;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Shopping Cart Checkout View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class ShoppingCartCheckoutViewComponent : ViewComponent
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The guest user helper
        /// </summary>
        private readonly IGuestUserHelper _guestUserHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartCheckoutViewComponent" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="guestUserHelper">The guest user helper.</param>
        public ShoppingCartCheckoutViewComponent(UserManager<ApplicationUser> userManager, IConfiguration configuration, IGuestUserHelper guestUserHelper)
        {
            this._userManager = userManager;
            this._guestUserHelper = guestUserHelper;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <returns>Shopping cart view component</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var shoppingCartItemsToCheckout = await this.GetShoppingCartItemsToCheckoutAsync();
            return this.View(shoppingCartItemsToCheckout);
        }

        /// <summary>
        /// Gets the shopping cart items to checkout asynchronous.
        /// </summary>
        /// <returns>List of shopping cart to checkout</returns>
        private async Task<List<ShoppingCartCheckoutViewModel>> GetShoppingCartItemsToCheckoutAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                    var user = await this._userManager.FindByNameAsync(User.Identity.Name);
                    var customerId = user.CustomersId ?? 0;
                    return await shoppingCartApi.GetShoppingCartCheckout(user.CustomersId ?? 0);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = StaticResources.NotAvailable;
                    return new List<ShoppingCartCheckoutViewModel>();
                }
            }
            else
            {
                try
                {
                    var shoppingCartGuestApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                    var guestID = this._guestUserHelper.GetGuestId();
                    return await shoppingCartGuestApi.GetShoppingCartCheckout(guestID);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = StaticResources.NotAvailable;
                    return new List<ShoppingCartCheckoutViewModel>();
                }
            }
        }
    }
}