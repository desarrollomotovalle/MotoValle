// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutMercadoPagosViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Mercado Pagos View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos
{
    /// <summary>
    /// Checkout Mercado Pagos View Model
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos.MercadoPagosBaseDataViewModel" />
    public class CheckoutMercadoPagosViewModel : MercadoPagosBaseDataViewModel
    {

        /// <summary>
        /// Gets or sets the customer data.
        /// </summary>
        /// <value>
        /// The customer data.
        /// </value>
        public CheckoutCustomerDataViewModel CustomerData { get; set; }

        /// <summary>
        /// Gets or sets the payment way data.
        /// </summary>
        /// <value>
        /// The payment way data.
        /// </value>
        public CheckoutPaymentWayViewModel PaymentWayData { get; set; }

        /// <summary>
        /// Gets the checkout data.
        /// </summary>
        /// <value>
        /// The checkout data.
        /// </value>
        public CheckoutDataViewModel CheckoutData => new CheckoutDataViewModel(this.CustomerData, this.PaymentWayData);
    }
}
