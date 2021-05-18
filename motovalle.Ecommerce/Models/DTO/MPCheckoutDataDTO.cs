// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MPCheckoutDataDTO.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pagos Checkout Data DTO
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO
{
    /// <summary>
    /// Mercado Pagos Checkout Data DTO
    /// </summary>
    public class MPCheckoutDataDTO
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        public string Payment_method_id { get; set; }

        /// <summary>
        /// Gets or sets the installments.
        /// </summary>
        /// <value>
        /// The installments.
        /// </value>
        public int Installments { get; set; }

        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>
        /// The issuer identifier.
        /// </value>
        public string Issuer_id { get; set; }
    }
}
