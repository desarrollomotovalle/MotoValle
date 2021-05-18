// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGuestUserHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Guest User Helper Facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.Services
{
    /// <summary>
    /// Guest User Helper Facade
    /// </summary>
    public interface IGuestUserHelper
    {
        /// <summary>
        /// Gets the guest identifier.
        /// </summary>
        /// <returns>Guest Id</returns>
        public string GetGuestId();
    }
}