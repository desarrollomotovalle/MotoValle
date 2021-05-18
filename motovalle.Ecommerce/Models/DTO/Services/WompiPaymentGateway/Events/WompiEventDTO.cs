// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiEvent.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Event
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Wompi Event
    /// </summary>
    public class WompiEventDTO
    {
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public WompiEventDataDTO Data { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        [JsonProperty("environment")]
        public WompiEnviroment Environment { get; set; }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        [JsonProperty("signature")]
        public WompiEventSignatureDTO Signature { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sent at.
        /// </summary>
        /// <value>
        /// The sent at.
        /// </value>
        [JsonProperty("sent_at")]
        public DateTime SentAt { get; set; }

        /// <summary>
        /// Gets the sent at local.
        /// </summary>
        /// <value>
        /// The sent at local.
        /// </value>
        public DateTime SentAtLocal => this.SentAt.ToLocalTime();
    }
}
