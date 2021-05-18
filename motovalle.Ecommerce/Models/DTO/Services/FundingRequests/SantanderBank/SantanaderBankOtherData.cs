// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderBankFundingRequest.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Bank Funding Request
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    /// <summary>
    /// Santanader Bank Other Data
    /// </summary>
    public class SantanaderBankOtherData
    {
        /// <summary>
        /// Gets or sets the concesionario radicacion.
        /// </summary>
        /// <value>
        /// The concesionario radicacion.
        /// </value>
        public int ConcesionarioRadicacion { get; set; }

        /// <summary>
        /// Gets or sets the identificacion vendedor.
        /// </summary>
        /// <value>
        /// The identificacion vendedor.
        /// </value>
        public int IdentificacionVendedor { get; set; }

        /// <summary>
        /// Gets or sets the usuario radica.
        /// </summary>
        /// <value>
        /// The usuario radica.
        /// </value>
        public string UsuarioRadica { get; set; }
    }
}