// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMercadoPagoHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pago Helper Interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.MercadoPagoPayment.Facade
{
    using MercadoPago.Resources;
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Mercado Pago Helper Interface
    /// </summary>
    public interface IMercadoPagoHelper
    {
        /// <summary>
        /// Availables the payment methods.
        /// </summary>
        /// <returns>
        /// List of avaiable methods
        /// </returns>
        GenericResponse<List<PaymentMethod>> AvailablePaymentMethods();

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="mpPayment">The mp payment.</param>
        /// <returns>
        /// Generic Response with MP payment Response
        /// </returns>
        Task<GenericResponse<MercadoPagoPaymentResponse>> SavePayment(Payment mpPayment);

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="mercadoPagoPaymentViewModel">The mercado pago payment view model.</param>
        /// <returns>Create Customer Response</returns>
        Task<MercadoPagoCustomerResponse> CreateCustomer(MercadoPagoCreditCardPaymentViewModel mercadoPagoPaymentViewModel);

        /// <summary>
        /// Finds the payment by transaction identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Payment
        /// </returns>
        GenericResponse<Payment> FindPaymentByTransactionId(long id);

        /// <summary>
        /// Finds the merchant order by transaction identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Merchant Order
        /// </returns>
        GenericResponse<MerchantOrder> FindMerchantOrderByTransactionId(string id);

        /// <summary>
        /// Converts to mercadopagopayment.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <param name="mPCheckoutDataDTO">The m p checkout data dto.</param>
        /// <param name="totalAmount">The total amount.</param>
        /// <returns>
        /// Mercado pago Payment
        /// </returns>
        Payment ToMercadoPagoPayment(int orderNumber, MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel, MPCheckoutDataDTO mPCheckoutDataDTO, decimal totalAmount);

        /// <summary>
        /// Converts to mercadopagopayment.
        /// </summary>
        /// <param name="mercadoPagosViewModel">The mercado pagos view model.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="notificationUrl">The notification URL.</param>
        /// <returns>Payment</returns>
        Payment ToMercadoPagoPayment(CheckoutMercadoPagosViewModel mercadoPagosViewModel, int orderNumber, string notificationUrl);
    }
}
