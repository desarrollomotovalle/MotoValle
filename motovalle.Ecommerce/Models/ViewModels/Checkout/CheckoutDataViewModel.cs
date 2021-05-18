// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutDataViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Data View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout
{
    /// <summary>
    /// Checkout Data View Model
    /// </summary>
    public class CheckoutDataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutDataViewModel"/> class.
        /// </summary>
        public CheckoutDataViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutDataViewModel"/> class.
        /// </summary>
        /// <param name="customerData">The customer data.</param>
        public CheckoutDataViewModel(CheckoutCustomerDataViewModel customerData)
        {
            this.CustomerData = customerData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutDataViewModel" /> class.
        /// </summary>
        /// <param name="customerData">The customer data.</param>
        /// <param name="paymentWayData">The payment way data.</param>
        public CheckoutDataViewModel(CheckoutCustomerDataViewModel customerData, CheckoutPaymentWayViewModel paymentWayData)
        {
            this.CustomerData = customerData;
            this.PaymentWayData = paymentWayData;
        }

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
    }
}
