// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiTransactionData.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Transaction Data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Transaction Data
    /// </summary>
    public class WompiTransactionDataResponseDTO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        [Display(Name = "Id. Transacción")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [JsonProperty("created_at")]
        [Display(Name = "Fecha de registro")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the amount in cents.
        /// </summary>
        /// <value>
        /// The amount in cents.
        /// </value>
        [JsonProperty("amount_in_cents")]
        [Display(Name = "Total en centavos")]
        [DataType(DataType.Currency)]
        public long AmountInCents { get; set; }

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
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [JsonProperty("reference")]
        [Display(Name = "Referencia de pago")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the customer email.
        /// </summary>
        /// <value>
        /// The customer email.
        /// </value>
        [JsonProperty("customer_email")]
        [Display(Name = "Correo del cliente")]
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
        /// Gets or sets the payment method.
        /// </summary>
        /// <value>
        /// The payment method.
        /// </value>
        [JsonProperty("payment_method")]
        public WompiPaymentMethodResponseDTO PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        /// <value>
        /// The shipping address.
        /// </value>
        [JsonProperty("shipping_address")]
        public WompiShippingAddressResponseDTO ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the payment link identifier.
        /// </summary>
        /// <value>
        /// The payment link identifier.
        /// </value>
        [JsonProperty("payment_link_id")]
        public string PaymentLinkId { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the merchant.
        /// </summary>
        /// <value>
        /// The merchant.
        /// </value>
        [JsonProperty("merchant")]
        public WompiMerchantResponseDTO Merchant { get; set; }

        /// <summary>
        /// Gets the amount in pesos.
        /// </summary>
        /// <value>
        /// The amount in pesos.
        /// </value>
        [DataType(DataType.Currency)]
        [Display(Name = "Total en pesos")]
        public int AmountInPesos => int.Parse(this.AmountInCents.ToString()[0..^2]);

        /// <summary>
        /// Gets the created at local.
        /// </summary>
        /// <value>
        /// The created at local.
        /// </value>
        [Display(Name = "Fecha de registro")]
        public DateTime CreatedAtLocal => this.CreatedAt.ToLocalTime();
    }
}