// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiMerchant.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Merchant
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Merchant
    /// </summary>
    public class WompiMerchantResponseDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the legal.
        /// </summary>
        /// <value>
        /// The name of the legal.
        /// </value>
        [JsonProperty("legal_name")]
        [Display(Name = "Nombre comercio")]
        public string LegalName { get; set; }

        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        /// <value>
        /// The name of the contact.
        /// </value>
        [JsonProperty("contact_name")]
        [Display(Name = "Nombre contacto")]
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [JsonProperty("phone_number")]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the logo URL.
        /// </summary>
        /// <value>
        /// The logo URL.
        /// </value>
        [JsonProperty("logo_url")]
        public object LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the legal identifier.
        /// </summary>
        /// <value>
        /// The type of the legal identifier.
        /// </value>
        [JsonProperty("legal_id_type")]
        [Display(Name = "Tipo de identificación")]
        public string LegalIdType { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty("email")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the legal identifier.
        /// </summary>
        /// <value>
        /// The legal identifier.
        /// </value>
        [JsonProperty("legal_id")]
        [Display(Name = "Id. Comcercio")]
        public string LegalId { get; set; }
    }
}
