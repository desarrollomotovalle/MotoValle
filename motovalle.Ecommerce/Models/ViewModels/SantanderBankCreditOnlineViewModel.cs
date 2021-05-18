// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SantanderBankCreditOnlineViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Santander Bank Credit Online View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Santander Bank Credit Online View Model
    /// </summary>
    public class SantanderBankCreditOnlineViewModel
    {
        #region Datos Basicos
        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        /// <value>
        /// The type of the document.
        /// </value>
        [Display(Name = "Tipo de identifiación")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public SantanderBankDocTypes DocType { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>
        /// The document number.
        /// </value>
        [Display(Name = "Número de identifiación")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string DocNumber { get; set; }

        /// <summary>
        /// Gets or sets the total amunt.
        /// </summary>
        /// <value>
        /// The total amunt.
        /// </value>
        [Display(Name = "Monto a solicitar")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser mayor a {1} peso")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the initial fee.
        /// </summary>
        /// <value>
        /// The initial fee.
        /// </value>
        [Display(Name = "Cuota Inicial")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser mayor a {1} peso")]
        public int InitialFee { get; set; }

        /// <summary>
        /// Gets or sets the plazo.
        /// </summary>
        /// <value>
        /// The plazo.
        /// </value>
        [Display(Name = "Plazo")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public SantanderBankInstallments Installments { get; set; }
        #endregion

        #region Datos Financieros
        /// <summary>
        /// Gets or sets the actividad economica.
        /// </summary>
        /// <value>
        /// The actividad economica.
        /// </value>
        [Display(Name = "Actividad Economica")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public SantanderBankEconomicActivity ActividadEconomica { get; set; }

        /// <summary>
        /// Gets or sets the actividad independiente.
        /// </summary>
        /// <value>
        /// The actividad independiente.
        /// </value>
        [Display(Name = "Actividad Independiente")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public SantanderBankIndependentActivity ActividadIndependiente { get; set; }

        /// <summary>
        /// Gets or sets the monthly income.
        /// </summary>
        /// <value>
        /// The monthly income.
        /// </value>
        [Display(Name = "Ingreso Mensual")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser mayor a {1} peso")]
        public int MonthlyIncome { get; set; } 
        #endregion
    }
}