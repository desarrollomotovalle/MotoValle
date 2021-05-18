// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderBankCredentials.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Bank Credentials
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    /// <summary>
    /// Santander Bank Credentials
    /// </summary>
    public class SantanderBankCredentials
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Propiedad para la gestion de la nueva autenticacion del banco santander
        /// </summary>
        public string UserPass { get; set; }
    }
}
