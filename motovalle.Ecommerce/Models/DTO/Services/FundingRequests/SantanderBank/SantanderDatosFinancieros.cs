// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderDatosFinancieros.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Datos Financieros
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    /// <summary>
    /// Santander Datos Financieros
    /// </summary>
    public class SantanderDatosFinancieros
    {
        /// <summary>
        /// Gets or sets the actividad economica.
        /// </summary>
        /// <value>
        /// The actividad economica.
        /// </value>
        public SantanderBankEconomicActivity ActividadEconomica { get; set; }

        /// <summary>
        /// Gets or sets the actividad independiente.
        /// </summary>
        /// <value>
        /// The actividad independiente.
        /// </value>
        public SantanderBankIndependentActivity ActividadIndependiente { get; set; }

        /// <summary>
        /// Gets or sets the ingreso mensual.
        /// </summary>
        /// <value>
        /// The ingreso mensual.
        /// </value>
        public int IngresoMensual { get; set; }
    }
}