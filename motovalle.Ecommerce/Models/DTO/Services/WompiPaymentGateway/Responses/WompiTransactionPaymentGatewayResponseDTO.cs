// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiTransactionPaymentResponse.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Transaction Payment Response
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Wompi Transaction Payment Response
    /// </summary>
    public class WompiTransactionPaymentGatewayResponseDTO
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public WompiTransactionDataResponseDTO Data { get; set; }
    }
}
