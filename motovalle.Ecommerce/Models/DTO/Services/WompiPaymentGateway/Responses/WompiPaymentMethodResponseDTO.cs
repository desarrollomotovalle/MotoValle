// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiPaymentMethod.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Payment Method
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Payment Method
    /// </summary>
    public class WompiPaymentMethodResponseDTO
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        [Display(Name = "Método de pago")]
        public WompiPaymentMethodType Type { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [JsonProperty("phone_number")]
        [Display(Name = "Número de contacto")]
        public long PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the extra.
        /// </summary>
        /// <value>
        /// The extra.
        /// </value>
        [JsonProperty("extra")]
        public WompiExtraResponseDTO Extra { get; set; }

        /// <summary>
        /// Gets or sets the installments.
        /// </summary>
        /// <value>
        /// The installments.
        /// </value>
        [JsonProperty("installments")]
        [Display(Name = "Cuotas")]
        public int Installments { get; set; }
    }
}
