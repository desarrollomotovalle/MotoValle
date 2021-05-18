// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MercadoPagoPayment.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pago Payment Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.MercadoPagoPayment
{
    using MercadoPago;
    using MercadoPago.Common;
    using MercadoPago.DataStructures.Payment;
    using MercadoPago.Resources;
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Card = MercadoPago.Resources.Card;

    /// <summary>
    /// Mercado Pago Payment Helper
    /// </summary>
    internal class MercadoPagoPayment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MercadoPagoPayment" /> class.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="accessToken">The access token.</param>
        public MercadoPagoPayment(string clientId, string clientSecret, string accessToken)
        {
            SDK.ClientId ??= clientId;
            SDK.ClientSecret ??= clientSecret;
            SDK.AccessToken ??= accessToken;
        }

        /// <summary>
        /// Gets the available payment methods.
        /// </summary>
        /// <returns>List of PaymentMethods</returns>
        internal List<PaymentMethod> GetAvailablePaymentMethods()
        {
            return PaymentMethod.All();
        }

        /// <summary>
        /// Payments the builder.
        /// </summary>
        /// <param name="mercadoPagoPaymentViewModel">The mercado pago payment view model.</param>
        /// <returns></returns>
        internal Payment PaymentBuilder(MercadoPagoCreditCardPaymentViewModel mercadoPagoPaymentViewModel)
        {
            return new Payment()
            {
                Description = mercadoPagoPaymentViewModel.Description,
                ExternalReference = mercadoPagoPaymentViewModel.OrderNumber.ToString(),
                Installments = mercadoPagoPaymentViewModel.Installments,
                IssuerId = string.IsNullOrEmpty(mercadoPagoPaymentViewModel.IssuerId) ? null : mercadoPagoPaymentViewModel.IssuerId,
                //NotificationUrl = "MySite/webhooks",
                PaymentMethodId = mercadoPagoPaymentViewModel.PaymentMethodId,
                Payer = new Payer()
                {
                    Id = mercadoPagoPaymentViewModel.MPCustomerId,
                    Address = new Address()
                    {
                        StreetName = mercadoPagoPaymentViewModel.Address,
                        ZipCode = mercadoPagoPaymentViewModel.ZipCode
                    },
                    Email = mercadoPagoPaymentViewModel.Email,
                    Identification = new Identification()
                    {
                        Number = mercadoPagoPaymentViewModel.IDNumber,
                        Type = mercadoPagoPaymentViewModel.IDNumberType
                    },
                    FirstName = mercadoPagoPaymentViewModel.FirstName,
                    LastName = mercadoPagoPaymentViewModel.LastName,
                    Type = PayerType.customer
                },
                StatementDescriptor = "Motovalle Ecommerce",
                TransactionAmount = mercadoPagoPaymentViewModel.TransactionAmount,
                Token = mercadoPagoPaymentViewModel.CardToken
            };
        }

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="payment">The payment.</param>
        /// <returns>Json status of pay</returns>
        internal MercadoPagoPaymentResponse SavePayment(Payment payment)
        {
            payment.Save();
            return new MercadoPagoPaymentResponse()
            {
                Id = payment.Id,
                Status = payment.Status,
                StatusDetail = payment.StatusDetail,
                DateApproved = payment.DateApproved,
                Payer = payment.Payer,
                PaymentMethodId = payment.PaymentMethodId,
                PaymentTypeId = payment.PaymentTypeId,
                StatementDescriptor = payment.StatementDescriptor
            };
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="mercadoPagoPaymentViewModel">The mercado pago payment view model.</param>
        internal async Task<MercadoPagoCustomerResponse> CreateCustomer(MercadoPagoCreditCardPaymentViewModel mercadoPagoPaymentViewModel)
        {
            try
            {
                var filters = new Dictionary<string, string> { { "email", mercadoPagoPaymentViewModel.Email } };
                var findCustomer = Customer.Search(filters);
                if (findCustomer.Any())
                {
                    return new MercadoPagoCustomerResponse()
                    {
                        Code = "200",
                        CustomerId = findCustomer.First().Id,
                        Message = "Ok"
                    };
                }

                var customer = new Customer()
                {
                    Email = mercadoPagoPaymentViewModel.Email,
                };

                customer.Save();
                await Task.Delay(500);

                var card = new Card()
                {
                    CustomerId = customer.Id,
                    Token = mercadoPagoPaymentViewModel.CardToken
                };

                card.Save();
                await Task.Delay(500);
                return new MercadoPagoCustomerResponse()
                {
                    Code = "200",
                    CustomerId = customer.Id,
                    Message = "Ok"
                };
            }
            catch (MPException e) when (e.Error != null)
            {
                var message = e.Error.Cause[0].Code switch
                {
                    "106" => "No puedes realizar pagos a usuarios de otros países.",
                    "109" => $"{mercadoPagoPaymentViewModel.PaymentMethodId} no puede prcesar pagos en {mercadoPagoPaymentViewModel.Installments.ToString()} cuotas.",
                    "126" => "No pudimos procesar tu pago.",
                    "129" => $"{mercadoPagoPaymentViewModel.PaymentMethodId} no procesa pagos del monto seleccionado. Elige otra tarjeta u otro medio de pago.",
                    "145" => "No pudimos procesar tu pago. Usuarios involucrados inválidos.",
                    "150" => "No puedes realizar pagos. Cliente no puede hacer pagos actualmente.",
                    "151" => "No puedes realizar pagos. El cliente es el mismo propietario.",
                    "160" => "No pudimos procesar tu pago. Recaudador no autorizado para operar.",
                    "204" => $"{mercadoPagoPaymentViewModel.PaymentMethodId} no está disponible en el momento. Intente nuevamente en unos minutos.",
                    "801" => "Realizaste un pago similar hace instantes. Intenta nuevamente en unos minutos.",
                    _ => "No pudimos procesar tu pago",
                };

                return new MercadoPagoCustomerResponse()
                {
                    Code = e.Error.Cause[0].Code,
                    CustomerId = string.Empty,
                    Message = message
                };
            }
        }
    }
}