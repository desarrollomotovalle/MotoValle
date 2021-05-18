// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WestBankFundingRequestViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  West Bank Funding Request View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// West Bank Funding Request View Model
    /// </summary>
    public class FundingRequestViewModel
    {
        /// <summary>
        /// Gets or sets the customers identifier.
        /// </summary>
        /// <value>
        /// The customers identifier.
        /// </value>
        [Required(ErrorMessage = "Cliente es requerido.")]
        public int CustomersId { get; set; }

        /// <summary>
        /// Gets or sets the total amount request.
        /// </summary>
        /// <value>
        /// The total amount request.
        /// </value>
        [Display(Name = "Total a solicitar"), Required(ErrorMessage = "Monto total es requerido."), DataType(DataType.Currency)]
        public decimal TotalAmountRequest { get; set; }

        /// <summary>
        /// Gets or sets the initial fee.
        /// </summary>
        /// <value>
        /// The initial fee.
        /// </value>
        [Display(Name = "Cuota inicial"), DataType(DataType.Currency)]
        public decimal InitialFee { get; set; }

        /// <summary>
        /// Gets or sets the funding installments.
        /// </summary>
        /// <value>
        /// The funding installments.
        /// </value>
        [Display(Name = "¿A cuántas cuotas quieres pagar?"), Required(ErrorMessage = "Coutas a pagar es requerido.")]
        public SantanderBankInstallments FundingInstallments { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the profession.
        /// </summary>
        /// <value>
        /// The profession.
        /// </value>
        [Display(Name = "Profesión"), Required(ErrorMessage = "Profesión es requerida")]
        public string Profession { get; set; }

        /// <summary>
        /// Gets or sets the identifier number.
        /// </summary>
        /// <value>
        /// The identifier number.
        /// </value>
        [Display(Name = "Número de identificación")]
        public string IDNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the identifier.
        /// </summary>
        /// <value>
        /// The type of the identifier.
        /// </value>
        [Required(ErrorMessage = "Tipo de identificación es requerido.")]
        [Display(Name = "Tipo de identificación")]
        public SantanderBankDocTypes IDType { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [Display(Name = "Fecha de nacimiento"), Required(ErrorMessage = "Fecha de nacimiento es requerida."), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the city of residence.
        /// </summary>
        /// <value>
        /// The city of residence.
        /// </value>
        [Display(Name = "Ciudad de residencia"), Required(ErrorMessage = "Ciudad de residencia es requerida.")]
        public string CityOfResidence { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [electronic signature agreement].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [electronic signature agreement]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Acuerdo de firma electrónica"), Required(ErrorMessage = "Acuerdo de firma electrónoca es requerido.")]
        public bool ElectronicSignatureAgreement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [risk center agreement].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [risk center agreement]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Consulta en centrales de riesgo, operadores de seguridad social y tratamiento de datos personales"), Required(ErrorMessage = "Consulta en centrales de riesgo... Es requerido.")]
        public bool RiskCenterAgreement { get; set; }

        /// <summary>
        /// Gets or sets the santander bank economic activity.
        /// </summary>
        /// <value>
        /// The santander bank economic activity.
        /// </value>
        [Display(Name = "Actividad Económica")]
        public SantanderBankEconomicActivity SantanderBankEconomicActivity { get; set; }

        /// <summary>
        /// Gets or sets the santander bank independent activity.
        /// </summary>
        /// <value>
        /// The santander bank independent activity.
        /// </value>
        [Display(Name = "Actividad Independiente")]
        public SantanderBankIndependentActivity SantanderBankIndependentActivity { get; set; }

        /// <summary>
        /// Gets or sets the monthly earn.
        /// </summary>
        /// <value>
        /// The monthly earn.
        /// </value>
        [Display(Name = "Ingreso Mensual")]
        [DataType(DataType.Currency)]
        public int MonthlyIncome { get; set; }

        /// <summary>
        /// Gets or sets the semaforo.
        /// </summary>
        /// <value>
        /// The semaforo.
        /// </value>
        public SantanderBankSemaforo Semaforo { get; set; }

        /// <summary>
        /// Gets or sets the request message.
        /// </summary>
        /// <value>
        /// The request message.
        /// </value>
        public string RequestResultMessage { get; set; }

        /// <summary>
        /// Gets or sets the bank.
        /// </summary>
        /// <value>
        /// The bank.
        /// </value>
        public Banks Bank { get; set; } = Banks.WestBank;
    }
}
