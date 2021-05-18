// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutPaymentWayViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Payment Way View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Checkout Payment Way View Model
    /// </summary
    public class CheckoutPaymentWayViewModel
    {
        /// <summary>
        /// Gets or sets the payment ways option.
        /// </summary>
        /// <value>
        /// The payment way option.
        /// </value>
        [Display(Name = "Forma de pago")]
        [Required(ErrorMessage = "{0} no es válido")]
        public PaymentWaysOptions PaymentWayOption { get; set; }

        /// <summary>
        /// Gets or sets the gateway payments.
        /// </summary>
        /// <value>
        /// The gateway payments.
        /// </value>
        [Display(Name = "Pasarela de pago")]
        public GatewayPayments GatewayPayment { get; set; }

        /// <summary>
        /// Gets or sets the funding request option.
        /// </summary>
        /// <value>
        /// The funding request option.
        /// </value>
        [Display(Name = "Entidad Financiera")]
        public FundingRequestInstitutions FundingRequestInstitution { get; set; }

        /// <summary>
        /// Gets or sets the partial amount.
        /// </summary>
        /// <value>
        /// The partial amount.
        /// </value>
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Parcial")]
        public int? PartialAmount { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        [DataType(DataType.Currency)]
        [Display(Name = "Monto Total")]
        public int TotalAmount { get; set; }

        /// <summary>
        /// Gets the partial amount in cent.
        /// </summary>
        /// <value>
        /// The partial amount in cent.
        /// </value>
        public string PartialAmountInCents => $"{this.PartialAmount.ToString() ?? string.Empty}00";

        /// <summary>
        /// Gets the total amount in cents.
        /// </summary>
        /// <value>
        /// The total amount in cents.
        /// </value>
        public string TotalAmountInCents => $"{this.TotalAmount}00";

        /// <summary>
        /// Gets or sets the external reference.
        /// </summary>
        /// <value>
        /// The external reference.
        /// </value>
        public string ExternalReference => Guid.NewGuid().ToString();
    }
}
