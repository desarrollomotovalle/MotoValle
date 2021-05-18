// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiEventsTransaction.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Events Transaction
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Events Transaction
    /// </summary>
    public class WompiEventTransactionDTO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        [Display(Name = "Transacción")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the amount in cents.
        /// </summary>
        /// <value>
        /// The amount in cents.
        /// </value>
        [JsonProperty("amount_in_cents")]
        [Display(Name = "Total en centavos")]
        public int AmountInCents { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [JsonProperty("reference")]
        [Display(Name = "Referencia")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the customer email.
        /// </summary>
        /// <value>
        /// The customer email.
        /// </value>
        [JsonProperty("customer_email")]
        [Display(Name = "Correo")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [JsonProperty("currency")]
        [Display(Name = "Moneda")]
        public WompiCurrencyType Currency { get; set; }

        /// <summary>
        /// Gets or sets the type of the payment method.
        /// </summary>
        /// <value>
        /// The type of the payment method.
        /// </value>
        [JsonProperty("payment_method_type")]
        [Display(Name = "Método de pago")]
        public WompiPaymentMethodType PaymentMethodType { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        [Display(Name = "Estado")]
        public WompiStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        /// <value>
        /// The shipping address.
        /// </value>
        [JsonProperty("shipping_address")]
        [Display(Name = "Dirección")]
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment link identifier.
        /// </summary>
        /// <value>
        /// The payment link identifier.
        /// </value>
        [JsonProperty("payment_link_id")]
        public string PaymentLinkId { get; set; }

        /// <summary>
        /// Gets or sets the payment source identifier.
        /// </summary>
        /// <value>
        /// The payment source identifier.
        /// </value>
        [JsonProperty("payment_source_id")]
        public string PaymentSourceId { get; set; }

        /// <summary>
        /// Gets the amount in pesos.
        /// </summary>
        /// <value>
        /// The amount in pesos.
        /// </value>
        [DataType(DataType.Currency)]
        [Display(Name = "Total en pesos")]
        public int AmountInPesos => int.Parse(this.AmountInCents.ToString()[0..^2]);
    }
}
