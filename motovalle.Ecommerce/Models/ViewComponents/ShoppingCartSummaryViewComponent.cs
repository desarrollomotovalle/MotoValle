// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartSummaryViewComponent.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Summary View Component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewComponents
{
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.Services;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Shopping Cart summary View Component
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class ShoppingCartSummaryViewComponent : ViewComponent
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
        /// Initializes a new instance of the <see cref="ShoppingCartSummaryViewComponent" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="guestUserHelper">The guest user helper.</param>
        public ShoppingCartSummaryViewComponent(UserManager<ApplicationUser> userManager, IConfiguration configuration, IGuestUserHelper guestUserHelper)
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
            var shoppingCartItems = await this.GetShoppingCartItemsAsync();
            return this.View(shoppingCartItems);
        }

        /// <summary>
        /// Gets the shopping cart items asynchronous.
        /// </summary>
        /// <returns>List of shopping carts records for user</returns>
        private async Task<List<ShoppingCartRecords>> GetShoppingCartItemsAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                    var user = await this._userManager.FindByNameAsync(User.Identity.Name);
                    var customerId = user.CustomersId ?? 0;
                    return await shoppingCartApi.GetRecordsForCustomer(customerId);
                }
                catch (Exception)
                {
                    return new List<ShoppingCartRecords>();
                }
            }
            else
            {
                try
                {
                    var guestID = this._guestUserHelper.GetGuestId();
                    var shoppingCartGuestApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                    var shoppingCartGuest = await shoppingCartGuestApi.GetRecordsForCustomer(guestID);
                    var shoppingCart = new List<ShoppingCartRecords>();
                    Parallel.ForEach(shoppingCartGuest, (item) =>
                    {
                        shoppingCart.Add(new ShoppingCartRecords()
                        {
                            FkProductsId = item.FkProductsId,
                            Quantity = item.Quantity,
                            FkInventoryItemsId = item.FkInventoryItemsId ?? 0
                        });
                    });

                    return shoppingCart;
                }
                catch (Exception)
                {
                    return new List<ShoppingCartRecords>();
                }
            }
        }
    }
}