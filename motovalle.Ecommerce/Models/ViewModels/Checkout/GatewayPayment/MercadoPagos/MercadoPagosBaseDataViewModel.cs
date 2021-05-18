// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MercadoPagosBaseDataViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pagos Base Data View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos
{
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Mercado Pagos Base Data View Model
    /// </summary>
    public class MercadoPagosBaseDataViewModel
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        [Required]
        public string Payment_method_id { get; set; }

        /// <summary>
        /// Gets or sets the installments.
        /// </summary>
        /// <value>
        /// The installments.
        /// </value>
        [Required]
        public int Installments { get; set; }

        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>
        /// The issuer identifier.
        /// </value>
        [Required]
        public string Issuer_id { get; set; }
    }
}
