// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MercadoPagoHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pago Helper Implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.MercadoPagoPayment
{
    using MercadoPago;
    using MercadoPago.Common;
    using MercadoPago.DataStructures.Payment;
    using MercadoPago.Resources;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers.MercadoPagoPayment.Facade;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Card = MercadoPago.Resources.Card;

    /// <summary>
    /// Mercado Pago Helper Implementations
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Helpers.MercadoPagoPayment.Facade.IMercadoPagoHelper" />
    public class MercadoPagoHelper : IMercadoPagoHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MercadoPagoHelper"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MercadoPagoHelper(IConfiguration configuration)
        {
            SDK.ClientId ??= configuration.GetSection("MercadoPagoSettings").GetValue<string>("ClientID");
            SDK.ClientSecret ??= configuration.GetSection("MercadoPagoSettings").GetValue<string>("ClientSecret");
            SDK.AccessToken ??= configuration.GetSection("MercadoPagoSettings").GetValue<string>("AccessToken");
        }

        /// <summary>
        /// Availables the payment methods.
        /// </summary>
        /// <returns>
        /// List of avaiable methods
        /// </returns>
        public GenericResponse<List<PaymentMethod>> AvailablePaymentMethods()
        {
            try
            {
                var paymentMethods = PaymentMethod.All();
                return new GenericResponse<List<PaymentMethod>>()
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = paymentMethods
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<List<PaymentMethod>>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = new List<PaymentMethod>()
                };
            }
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="mercadoPagoPaymentViewModel">The mercado pago payment view model.</param>
        /// <returns>
        /// Create Customer Response
        /// </returns>
        public async Task<MercadoPagoCustomerResponse> CreateCustomer(MercadoPagoCreditCardPaymentViewModel mercadoPagoPaymentViewModel)
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

                await customer.SaveAsync();
                var card = new Card()
                {
                    CustomerId = customer.Id,
                    Token = mercadoPagoPaymentViewModel.CardToken
                };

                await card.SaveAsync();
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
                    "109" => $"{mercadoPagoPaymentViewModel.PaymentMethodId} no puede prcesar pagos en {mercadoPagoPaymentViewModel.Installments} cuotas.",
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

        /// <summary>
        /// Finds the merchant order by transaction identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Merchant Order
        /// </returns>
        public GenericResponse<MerchantOrder> FindMerchantOrderByTransactionId(string id)
        {
            try
            {
                var merchantOrder = MerchantOrder.FindById(id);
                return new GenericResponse<MerchantOrder>()
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = merchantOrder
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<MerchantOrder>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = new MerchantOrder()
                };
            }
        }

        /// <summary>
        /// Finds the payment by transaction identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Payment
        /// </returns>
        public GenericResponse<Payment> FindPaymentByTransactionId(long id)
        {
            try
            {
                var payment = Payment.FindById(id);
                return new GenericResponse<Payment>()
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = payment
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<Payment>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = new Payment()
                };
            }
        }

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="mpPayment">The mp payment.</param>
        /// <returns>
        /// Generic Response with MP payment Response
        /// </returns>
        public async Task<GenericResponse<MercadoPagoPaymentResponse>> SavePayment(Payment mpPayment)
        {
            try
            {
                await mpPayment.SaveAsync();
                var response = new GenericResponse<MercadoPagoPaymentResponse>()
                {
                    Result = new MercadoPagoPaymentResponse()
                    {
                        Id = mpPayment.Id,
                        Status = mpPayment.Status,
                        StatusDetail = mpPayment.StatusDetail,
                        DateApproved = mpPayment.DateApproved,
                        Payer = mpPayment.Payer,
                        PaymentMethodId = mpPayment.PaymentMethodId,
                        PaymentTypeId = mpPayment.PaymentTypeId,
                        StatementDescriptor = mpPayment.StatementDescriptor,
                        Reference = mpPayment.ExternalReference
                    }
                };

                switch (mpPayment.Status)
                {
                    case PaymentStatus.pending:
                        response.IsSuccess = true;
                        response.Message = "Pago Pendiente. En unos minutos te enviaremos la notifiación del estado de pago.";
                        return response;
                    case PaymentStatus.approved:
                        response.IsSuccess = true;
                        response.Message = "Compra efectuada exitosamente";
                        return response;
                    case PaymentStatus.authorized:
                        response.IsSuccess = true;
                        response.Message = "Pago Autorizado";
                        return response;
                    case PaymentStatus.in_process:
                        response.IsSuccess = true;
                        response.Message = "Transacción en proceso. En menos de 2 días hábiles te enviaremos por e-mail el resultado.";
                        return response;
                    case PaymentStatus.in_mediation:
                        response.IsSuccess = true;
                        response.Message = "Pago en mediación con el Banco";
                        return response;
                    case PaymentStatus.rejected:
                        var message = mpPayment.StatusDetail switch
                        {
                            "cc_rejected_bad_filled_card_number" => "Revisa el número de tu tarjeta.",
                            "cc_rejected_bad_filled_date" => "Revisa la fecha de vencimiento.",
                            "cc_rejected_bad_filled_other" => "Revisa los datos ingresados.",
                            "cc_rejected_bad_filled_security_code" => "Revisa el código de seguridad.",
                            "cc_rejected_blacklist" => "Tarjeta en lista negra. No pudimos procesar tu pago.",
                            "cc_rejected_call_for_authorize" => $"Debes autorizar ante {mpPayment.PaymentMethodId} el pago de la transacción a Mercado Pago",
                            "cc_rejected_card_disabled" => $"Llama a {mpPayment.PaymentMethodId} para que active tu tarjeta.El teléfono está al dorso de tu tarjeta.",
                            "cc_rejected_card_error" => "No pudimos procesar tu pago. Intenta nuevamente.",
                            "cc_rejected_duplicated_payment" => "Ya hiciste un pago por ese valor. Si necesitas volver a pagar usa otra tarjeta u otro medio de pago.",
                            "cc_rejected_high_risk" => "Tu pago fue rechazado por alto riesgo en la transacción. Elige otro medio de pago, te recomendamos con medios en efectivo.",
                            "cc_rejected_insufficient_amount" => $"Tu {mpPayment.PaymentMethodId} no tiene fondos suficientes.",
                            "cc_rejected_invalid_installments" => $"{mpPayment.PaymentMethodId} no procesa pagos en installments cuotas.",
                            "cc_rejected_max_attempts" => "Llegaste al límite de intentos permitidos. Elige otra tarjeta u otro medio de pago.",
                            "cc_rejected_other_reason" => $"{mpPayment.PaymentMethodId} no procesó el pago",
                            _ => "No pudimos procesar tu pago. Intenta nuevamente."
                        };

                        response.IsSuccess = false;
                        response.Message = $"Transacción rechazada. {message}";
                        return response;
                    case PaymentStatus.cancelled:
                        response.IsSuccess = false;
                        response.Message = "Transacción Cancelada por el banco.";
                        break;
                    case PaymentStatus.refunded:
                        response.IsSuccess = true;
                        response.Message = "Pago Reintegrado.";
                        break;
                    case PaymentStatus.charged_back:
                        response.IsSuccess = true;
                        response.Message = "Pago Cargado nuevamente.";
                        break;
                    default:
                        response.IsSuccess = true;
                        response.Message = "No pudimos procesar tu pago";
                        break;
                }

                return response;
            }
            catch (MPException ex) when (ex.ErrorMessage != null)
            {
                var message = ex.Error.Cause[0].Code switch
                {
                    "1" => "Error en parámetros.",
                    "3" => "El token debe ser para test",
                    "5" => "Debes dar clic en verificar tarjeta.",
                    "106" => "No puedes realizar pagos a usuarios de otros países.",
                    "109" => $"{mpPayment.PaymentMethodId} no puede prcesar pagos en {mpPayment.Installments} cuotas.",
                    "126" => "No pudimos procesar tu pago.",
                    "129" => $"{mpPayment.PaymentMethodId} no procesa pagos del monto seleccionado. Elige otra tarjeta u otro medio de pago.",
                    "145" => "No pudimos procesar tu pago.",
                    "150" => "No puedes realizar pagos.",
                    "151" => "No puedes realizar pagos.",
                    "160" => "No pudimos procesar tu pago.",
                    "204" => $"{mpPayment.PaymentMethodId} no está disponible en el momento.",
                    "801" => "Realizaste un pago similar hace instantes. Intenta nuevamente en unos minutos.",
                    "1000" => "Número de filas excede los límites",
                    "1001" => "El formato de la fecha debe ser yyyy-MM-dd'T'HH:mm:ss.SSSZ",
                    "2001" => "Ya se posteó el ismo request en el último minuto.",
                    "2002" => "Cliente no encontrado.",
                    "2004" => "Falló el POST a Gateway Transactions API.",
                    "2006" => "Card token no encontrado.",
                    "2007" => "Falló conexión a Card Token API",
                    "2009" => "Card token issuer no puede ser nulo",
                    "2060" => "El cliente no puede ser igual al vendedor",
                    "3000" => "Debes ingresar el campo 'Titular de la tarjeta'.",
                    "3001" => "Debes ingresar el campo 'Titular de la tarjeta'.",
                    "3003" => "Favor recargue la página y genere la verificación de su tarjeta nuevamente.",
                    "3004" => "Favor recargue la página y genere la verificación de su tarjeta nuevamente.",
                    "3005" => "Acción inválida, el recurso esta en un estado que no permite esta operación. Para más información consulta el estado del recurso.",
                    "3006" => "Favor recargue la página y genere la verificación de su tarjeta nuevamente.", //"cardtoken_id inválido."
                    "3007" => "El parámetro client_id no puede ser nulo ni vacío.",
                    "3008" => "Cardtoken no encontrado.",
                    "3009" => "Cliente no autorizado.",
                    "3010" => "La tarjeta no se encuentra en la lista blanca.",
                    "3011" => "Métodp de pago no encontrado.",
                    "3012" => "Longitud del código de seguridad erróneo.",
                    "3013" => "Favor ingrese el código de seguridad,",
                    "3014" => "Forma de pago inválida.",
                    "3015" => "Verifique el número de la tarjeta.",
                    "3016" => "Verifique el número de la tarjeta.",//"card_number inválido."
                    "3017" => "Verifique el número de la tarjeta.", //"El parámetro card_number_id no puede ser nulo ni vacío.",
                    "3018" => "Verifique la fecha de vencimiento.",//"El parámetro expiration_month no puede ser nulo ni vacío."
                    "3019" => "Verifique la fecha de vencimiento.", //"El parámetro expiration_year no puede ser nulo ni vacío."
                    "3020" => "Debes ingresar el campo 'Titular de la tarjeta'.",//"El parámetro cardholder.name no puede ser nulo ni vacío."
                    "3021" => "Debes ingresar los campos 'Titular de la tarjeta' y 'Número de identificación'.",
                    "3022" => "Debes ingresar los campos 'Titular de la tarjeta' y 'Número de identificación'.",
                    "3023" => "Favor seleccione el tipo de documento.",//"El parámetro cardholder.document.subtype no puede ser nulo ni vacío."
                    "3024" => "Acción inválida, reembolsos parciales no soportados para esta transacción.",
                    "3025" => "Código de autorización inválido.",
                    "3026" => $"Tarjeta inválida para {mpPayment.PaymentMethodId}.",
                    "3027" => "Forma de pago inválida.",
                    "3028" => $"Método de pago {mpPayment.PaymentMethodId} inválido.",
                    "3029" => "Mes de expiración de tarjeta inválido.",
                    "3030" => "Año de expiración de tarjeta inválido.",
                    "4000" => "Debes ingresar el número de la tarjeta.",
                    "4001" => "Forma de pago no puede ser nulo.",
                    "4002" => "Debes ingresar el monto de la transacción.",
                    "4003" => "El monto de la transacción debe ser numérico",
                    "4004" => "Debes de seleccionar la cantidad de cuotas",
                    "4005" => "Cantidad de cuotas debe ser numérico.",
                    "4006" => "Pagador está mal formado.",
                    "4007" => "site_id no puede ser nulo.",
                    "4012" => "payer.id no puede ser nulo.",
                    "4013" => "payer.type no puede ser nulo.",
                    "4015" => "payment_method_reference_id no puede ser nulo.",
                    "4016" => "payment_method_reference_id debe ser numérico.",
                    "4017" => "status no puede ser nulo.",
                    "4018" => "payment_id no puede ser nulo.",
                    "4019" => "payment_id debe ser numérico.",
                    "4020" => "notificaction_url debe ser una url válida.",
                    "4021" => "notificaction_url debe tener una longitud menor a 500 caracteres.",
                    "4022" => "metadata debe ser un JSON válido.",
                    "4023" => "Total de la transacción no puede ser nulo.",
                    "4024" => "Total de la transacción debe ser numérico.",
                    "4025" => "refund_id no puede ser nulo.",
                    "4026" => "coupon_amount inválido.",
                    "4027" => "campaign_id debe ser numérico.",
                    "4028" => "coupon_amount atributte debe ser numérico.",
                    "4029" => "Tipo de payer inválido.",
                    "4037" => "Monto de la transacción inválido.",
                    "4038" => "application_fee no puede ser mayor que transaction_amount.",
                    "4039" => "application_fee no puede ser un valor negativo.",
                    "4049" => "Valor de la transacción debe ser mayor a cero",
                    "4050" => "Email debe ser un email válido.",
                    "4051" => "La longitud de Email debe ser menor que 254 caracteres.",
                    _ => "No pudimos procesar tu pago",
                };

                return new GenericResponse<MercadoPagoPaymentResponse>()
                {
                    IsSuccess = false,
                    Message = message
                };
            }
            catch (MPException ex) when (ex.ErrorMessage == null)
            {
                return new GenericResponse<MercadoPagoPaymentResponse>()
                {
                    IsSuccess = false,
                    Message = "No pudimos procesar tu pago"
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<MercadoPagoPaymentResponse>()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

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
        public Payment ToMercadoPagoPayment(int orderNumber, MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel, MPCheckoutDataDTO mPCheckoutDataDTO, decimal totalAmount)
        {
            return new Payment()
            {
                Description = mPExternalWebTokenizeViewModel.Description,
                ExternalReference = orderNumber.ToString(),
                Installments = mPCheckoutDataDTO.Installments,
                IssuerId = string.IsNullOrEmpty(mPCheckoutDataDTO.Issuer_id) ? null : mPCheckoutDataDTO.Issuer_id,
                //NotificationUrl = "MySite/webhooks",
                PaymentMethodId = mPCheckoutDataDTO.Payment_method_id,
                Payer = new Payer()
                {
                    Address = new Address()
                    {
                        StreetName = mPExternalWebTokenizeViewModel.ShipToAddress,
                        ZipCode = mPExternalWebTokenizeViewModel.ShipToZipcode, 
                        City = mPExternalWebTokenizeViewModel.ShipToCity
                    },
                    Email = mPExternalWebTokenizeViewModel.Email,
                    FirstName = mPExternalWebTokenizeViewModel.FirstName,
                    LastName = mPExternalWebTokenizeViewModel.LastName,
                    Type = PayerType.customer
                },
                StatementDescriptor = "MOTORES DEL VALLE SAS",
                TransactionAmount = totalAmount,
                Token = mPCheckoutDataDTO.Token
            };
        }

        /// <summary>
        /// Converts to mercadopagopayment.
        /// </summary>
        /// <param name="mercadoPagosViewModel">The mercado pagos view model.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="notificationUrl">The notification URL.</param>
        /// <returns>Payment</returns>
        public Payment ToMercadoPagoPayment(CheckoutMercadoPagosViewModel mercadoPagosViewModel, int orderNumber, string notificationUrl)
        {
            return new Payment()
            {
                Description = "COMPRA TIENDA MOTOVALLE",
                ExternalReference = orderNumber.ToString(),
                Installments = mercadoPagosViewModel.Installments,
                IssuerId = mercadoPagosViewModel.Issuer_id,
                ////NotificationUrl = notificationUrl, //notificationUrl,
                PaymentMethodId = mercadoPagosViewModel.Payment_method_id,
                StatementDescriptor = "MOTORES DEL VALLE SAS",
                TransactionAmount = mercadoPagosViewModel.CheckoutData.PaymentWayData.PaymentWayOption == PaymentWaysOptions.PartialPay ? mercadoPagosViewModel.CheckoutData.PaymentWayData.PartialAmount : mercadoPagosViewModel.CheckoutData.PaymentWayData.TotalAmount,
                Token = mercadoPagosViewModel.Token,
                Payer = new Payer()
                {
                    Email = mercadoPagosViewModel.CheckoutData.CustomerData.Email,
                    FirstName = mercadoPagosViewModel.CheckoutData.CustomerData.FristName,
                    LastName = mercadoPagosViewModel.CheckoutData.CustomerData.LastName
                }
            };
        }
    }
}