// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiShippingAddress.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Shipping Address
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Shipping Address
    /// </summary>
    public class WompiShippingAddressResponseDTO
    {
        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The address line1.
        /// </value>
        [JsonProperty("address_line_1")]
        [Display(Name = "Dirección")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [JsonProperty("country")]
        [Display(Name = "País")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        [JsonProperty("region")]
        [Display(Name = "Región")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty("city")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [JsonProperty("phone_number")]
        [Display(Name = "Teléfono")]
        public long PhoneNumber { get; set; }
    }
}
