// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiEventsData.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Events Data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events
{
    using Newtonsoft.Json;

    /// <summary>
    /// Wompi Events Data
    /// </summary>
    public class WompiEventDataDTO
    {
        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        [JsonProperty("transaction")]
        public WompiEventTransactionDTO Transaction { get; set; }
    }
}
