// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderBankAuthResponse.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Bank Auth Response
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    using System;

    /// <summary>
    /// Santander Bank Auth Response
    /// </summary>
    public class SantanderBankAuthResponse
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the expire on.
        /// </summary>
        /// <value>
        /// The expire on.
        /// </value>
        public DateTime Caducidad { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }
    }
}
