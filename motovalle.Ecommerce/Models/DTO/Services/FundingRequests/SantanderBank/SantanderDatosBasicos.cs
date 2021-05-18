// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderDatosBasicos.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Datos Básicos
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank
{
    /// <summary>
    /// Santander Datos Básicos
    /// </summary>
    public class SantanderDatosBasicos
    {
        /// <summary>
        /// Gets or sets the tipo documento.
        /// </summary>
        /// <value>
        /// The tipo documento.
        /// </value>
        public SantanderBankDocTypes TipoDocumento { get; set; }

        /// <summary>
        /// Gets or sets the numero documento.
        /// </summary>
        /// <value>
        /// The numero documento.
        /// </value>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Gets or sets the nombre1.
        /// </summary>
        /// <value>
        /// The nombre1.
        /// </value>
        public string Nombre1 { get; set; }

        /// <summary>
        /// Gets or sets the valor vehiculo.
        /// </summary>
        /// <value>
        /// The valor vehiculo.
        /// </value>
        public int ValorVehiculo { get; set; }

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
        /// Gets or sets the celular.
        /// </summary>
        /// <value>
        /// The celular.
        /// </value>
        public string Celular { get; set; }

        /// <summary>
        /// Gets or sets the correo personal.
        /// </summary>
        /// <value>
        /// The correo personal.
        /// </value>
        public string CorreoPersonal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [asociado juriscoop].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [asociado juriscoop]; otherwise, <c>false</c>.
        /// </value>
        public bool AsociadoJuriscoop { get; set; }
    }
}