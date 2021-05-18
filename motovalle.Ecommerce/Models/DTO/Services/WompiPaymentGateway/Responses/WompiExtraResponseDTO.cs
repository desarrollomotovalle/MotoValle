// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiExtra.cs" company="Inventek Software">
//   © All rights reserved
// </copyright>
// <summary>
// Wompi Extra
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Extra
    /// </summary>
    public class WompiExtraResponseDTO
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
        /// Gets or sets the brand.
        /// </summary>
        /// <value>
        /// The brand.
        /// </value>
        [JsonProperty("brand")]
        [Display(Name = "Franquicia")]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the last four.
        /// </summary>
        /// <value>
        /// The last four.
        /// </value>
        [JsonProperty("last_four")]
        [Display(Name = "Últ. 4 dígitos")]
        public string LastFour { get; set; }
    }
}
