// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiEventsSignature.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Events Signature
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Wompi Events Signature
    /// </summary>
    public class WompiEventSignatureDTO
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        [JsonProperty("properties")]
        public List<string> Properties { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>
        /// The checksum.
        /// </value>
        [JsonProperty("checksum")]
        public string Checksum { get; set; }
    }
}
