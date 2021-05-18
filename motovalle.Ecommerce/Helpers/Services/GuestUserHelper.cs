// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuestUserHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Guest User Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.Services
{
    using Microsoft.AspNetCore.Http;
    using System;

    /// <summary>
    /// Guest User Helper
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Helpers.Services.IGuestUserHelper" />
    public class GuestUserHelper : IGuestUserHelper
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The cookie name
        /// </summary>
        public const string _cookieName = "ShoppingCartMotovalleEcommerceGuestID";

        /// <summary>
        /// Initializes a new instance of the <see cref="GuestUserHelper"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public GuestUserHelper(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the guest identifier.
        /// </summary>
        /// <returns>Guest ID</returns>
        public string GetGuestId()
        {
            string guestID;
            if (string.IsNullOrEmpty(this._httpContextAccessor.HttpContext.Request.Cookies[_cookieName]))
            {
                guestID = Guid.NewGuid().ToString();
                var option = new CookieOptions() { Expires = DateTime.Now.AddDays(30) };
                this._httpContextAccessor.HttpContext.Response.Cookies.Append(_cookieName, guestID, option);
            }
            else
            {
                guestID = this._httpContextAccessor.HttpContext.Request.Cookies[_cookieName];
            }

            return guestID;
        }
    }
}
