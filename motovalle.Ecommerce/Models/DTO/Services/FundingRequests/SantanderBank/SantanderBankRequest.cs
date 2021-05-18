// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderBankRequest.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Bank Request
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    /// <summary>
    /// Santander Bank Request
    /// </summary>
    public class SantanderBankRequest
    {
        /// <summary>
        /// Gets or sets the value vehiculo.
        /// </summary>
        /// <value>
        /// The value vehiculo.
        /// </value>
        public int ValVehiculo { get; set; }

        /// <summary>
        /// Gets or sets the cuota inicial.
        /// </summary>
        /// <value>
        /// The cuota inicial.
        /// </value>
        public int CuotaInicial { get; set; }

        /// <summary>
        /// Gets or sets the plazo.
        /// </summary>
        /// <value>
        /// The plazo.
        /// </value>
        public SantanderBankInstallments Plazo { get; set; }

        /// <summary>
        /// Gets or sets the tasa.
        /// </summary>
        /// <value>
        /// The tasa.
        /// </value>
        public float Tasa { get; set; }

        /// <summary>
        /// Gets or sets the cuota maximum aprob.
        /// </summary>
        /// <value>
        /// The cuota maximum aprob.
        /// </value>
        public int CuotaMaxAprob { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [tiene capacidad].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [tiene capacidad]; otherwise, <c>false</c>.
        /// </value>
        public bool TieneCapacidad { get; set; }

        /// <summary>
        /// Gets or sets the sum semaforo.
        /// </summary>
        /// <value>
        /// The sum semaforo.
        /// </value>
        public int SumSemaforo { get; set; }

        /// <summary>
        /// Gets or sets the cuota oblig ttal.
        /// </summary>
        /// <value>
        /// The cuota oblig ttal.
        /// </value>
        public decimal CuotaObligTtal { get; set; }

        /// <summary>
        /// Gets or sets the ingreso ajustado.
        /// </summary>
        /// <value>
        /// The ingreso ajustado.
        /// </value>
        public int IngresoAjustado { get; set; }

        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        /// <value>
        /// The ratio.
        /// </value>
        public decimal Ratio { get; set; }
    }
}