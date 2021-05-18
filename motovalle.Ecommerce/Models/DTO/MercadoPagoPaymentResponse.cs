// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MercadoPagoResponse.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pago Response Object
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MercadoPago.Common;
    using MercadoPago.DataStructures.Payment;

    /// <summary>
    /// Mercado Pago Response Object
    /// </summary>
    public class MercadoPagoPaymentResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Display(Name = "Id de Transacción")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [Display(Name = "Orden #")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Display(Name = "Estado Transacción")]
        public PaymentStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the status details.
        /// </summary>
        /// <value>
        /// The status details.
        /// </value>
        [Display(Name = "Detalle transacción")]
        public string StatusDetail { get; set; }

        /// <summary>
        /// Gets or sets the date approved.
        /// </summary>
        /// <value>
        /// The date approved.
        /// </value>
        [Display(Name = "Fecha de transacción")]
        public DateTime? DateApproved { get; set; }

        /// <summary>
        /// Gets or sets the payer.
        /// </summary>
        /// <value>
        /// The payer.
        /// </value>
        public Payer Payer { get; set; }

        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        [Display(Name = "Foma de pago")]
        public string PaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the payment type identifier.
        /// </summary>
        /// <value>
        /// The payment type identifier.
        /// </value>
        public PaymentTypeId? PaymentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the statement descriptor.
        /// </summary>
        /// <value>
        /// The statement descriptor.
        /// </value>
        public string StatementDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the mercado pago customer response.
        /// </summary>
        /// <value>
        /// The mercado pago customer response.
        /// </value>
        public MercadoPagoCustomerResponse MercadoPagoCustomerResponse { get; set; }

        /// <summary>
        /// Gets the date approved local.
        /// </summary>
        /// <value>
        /// The date approved local.
        /// </value>
        public DateTime? DateApprovedLocal => this.DateApproved?.ToLocalTime();
    }
}
