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
    /// Santander Bank Funding Request
    /// </summary>
    public class SantanderBankFundingRequest
    {
        /// <summary>
        /// Gets or sets the datos basicos.
        /// </summary>
        /// <value>
        /// The datos basicos.
        /// </value>
        public SantanderDatosBasicos DatosBasicos { get; set; }

        /// <summary>
        /// Gets or sets the datos financieros.
        /// </summary>
        /// <value>
        /// The datos financieros.
        /// </value>
        public SantanderDatosFinancieros DatosFinancieros { get; set; }

        /// <summary>
        /// Gets or sets the otros datos.
        /// </summary>
        /// <value>
        /// The otros datos.
        /// </value>
        public SantanaderBankOtherData OtrosDatos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is juriscoop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is juriscoop; otherwise, <c>false</c>.
        /// </value>
        public bool IsJuriscoop { get; set; }
    }
}
