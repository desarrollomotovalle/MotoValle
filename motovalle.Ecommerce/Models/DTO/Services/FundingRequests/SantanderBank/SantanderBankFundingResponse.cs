// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderBankFundingResponse.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Bank Funding Response
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    /// <summary>
    /// Santander Bank Funding Response
    /// </summary>
    public class SantanderBankFundingResponse
    {
        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public SantanderBankRequest Request { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [inhabilitar value veh].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [inhabilitar value veh]; otherwise, <c>false</c>.
        /// </value>
        public bool InhabilitarValVeh { get; set; }

        /// <summary>
        /// Gets or sets the monto sol.
        /// </summary>
        /// <value>
        /// The monto sol.
        /// </value>
        public int MontoSol { get; set; }

        /// <summary>
        /// Gets or sets the monto sol maximum.
        /// </summary>
        /// <value>
        /// The monto sol maximum.
        /// </value>
        public int MontoSolMax { get; set; }

        /// <summary>
        /// Gets or sets the cuota proyectada.
        /// </summary>
        /// <value>
        /// The cuota proyectada.
        /// </value>
        public int CuotaProyectada { get; set; }

        /// <summary>
        /// Gets or sets the valor seguro mes.
        /// </summary>
        /// <value>
        /// The valor seguro mes.
        /// </value>
        public int ValorSeguroMes { get; set; }

        /// <summary>
        /// Gets or sets the valor seguro mes maximum.
        /// </summary>
        /// <value>
        /// The valor seguro mes maximum.
        /// </value>
        public int ValorSeguroMesMax { get; set; }

        /// <summary>
        /// Gets or sets the cuota seguro.
        /// </summary>
        /// <value>
        /// The cuota seguro.
        /// </value>
        public int CuotaSeguro { get; set; }

        /// <summary>
        /// Gets or sets the cuota seguro maximum.
        /// </summary>
        /// <value>
        /// The cuota seguro maximum.
        /// </value>
        public int CuotaSeguroMax { get; set; }

        /// <summary>
        /// Gets or sets the valor seguro.
        /// </summary>
        /// <value>
        /// The valor seguro.
        /// </value>
        public int ValorSeguro { get; set; }

        /// <summary>
        /// Gets or sets the valor seguro maximum.
        /// </summary>
        /// <value>
        /// The valor seguro maximum.
        /// </value>
        public int ValorSeguroMax { get; set; }

        /// <summary>
        /// Gets or sets the semaforo.
        /// </summary>
        /// <value>
        /// The semaforo.
        /// </value>
        public SantanderBankSemaforo Semaforo { get; set; }

        /// <summary>
        /// Gets or sets the titulo.
        /// </summary>
        /// <value>
        /// The titulo.
        /// </value>
        public string Titulo { get; set; }
    }
}
