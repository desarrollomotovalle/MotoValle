// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using MercadoPago.Common;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.ApiRequest.Services;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Helpers.MercadoPagoPayment.Facade;
    using motovalle.Ecommerce.Helpers.Services;
    using motovalle.Ecommerce.Helpers.Services.WompiPaymentGateway;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using motovalle.Ecommerce.Models.ViewModels;
    using motovalle.Ecommerce.Models.ViewModels.Checkout;
    using motovalle.Ecommerce.Models.ViewModels.Checkout.FundingRequest;
    using motovalle.Ecommerce.Models.ViewModels.Checkout.GatewayPayment.MercadoPagos;
    using Newtonsoft.Json;
    using reCAPTCHA.AspNetCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Shopping Cart Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ShoppingCartController : Controller
    {
        #region Ctor
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The mercado pago public key
        /// </summary>
        private readonly string _mercadoPagoPublicKey;

        /// <summary>
        /// The mercado pago access token
        /// </summary>
        private readonly string _mercadoPagoAccessToken;

        /// <summary>
        /// The mercado pago client identifier
        /// </summary>
        private readonly string _mercadoPagoClientId;

        /// <summary>
        /// The mercado pagoclient secret
        /// </summary>
        private readonly string _mercadoPagoClientSecret;

        /// <summary>
        /// The shipping cost
        /// </summary>
        private readonly decimal _shippingCost;

        /// <summary>
        /// The funding request template
        /// </summary>
        private readonly string _fundingRequestTemplate;

        /// <summary>
        /// The funding request email to
        /// </summary>
        private readonly string _fundingRequestEmailTo;

        /// <summary>
        /// The funding request email to cc
        /// </summary>
        private readonly string _fundingRequestEmailToCc;

        /// <summary>
        /// The funding request email to CC2
        /// </summary>
        private readonly string _fundingRequestEmailToCc2;

        /// <summary>
        /// The wompi public key
        /// </summary>
        private readonly string _wompiPublicKey;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IWebHostEnvironment _host;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSenderExtended _emailSender;

        /// <summary>
        /// The services helper
        /// </summary>
        private readonly IServicesHelper _servicesHelper;

        /// <summary>
        /// The recaptcha service
        /// </summary>
        private readonly IRecaptchaService _recaptchaService;

        /// <summary>
        /// The mercado pago helper
        /// </summary>
        private readonly IMercadoPagoHelper _mercadoPagoHelper;

        /// <summary>
        /// The guest user helper
        /// </summary>
        private readonly IGuestUserHelper _guestUserHelper;

        /// <summary>
        /// The wompi payment gateway service
        /// </summary>
        private readonly IWompiPaymentGatewayService _wompiPaymentGatewayService;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="host">The host.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="servicesHelper">The services helper.</param>
        /// <param name="recaptchaService">The recaptcha service.</param>
        /// <param name="mercadoPagoHelper">The mercado pago helper.</param>
        /// <param name="guestUserHelper">The guest user helper.</param>
        /// <param name="wompiPaymentGatewayService">The wompi payment gateway service.</param>
        public ShoppingCartController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IWebHostEnvironment host, IEmailSenderExtended emailSender, IServicesHelper servicesHelper, IRecaptchaService recaptchaService, IMercadoPagoHelper mercadoPagoHelper, IGuestUserHelper guestUserHelper, IWompiPaymentGatewayService wompiPaymentGatewayService)
        {
            this._host = host;
            this._userManager = userManager;
            this._emailSender = emailSender;
            this._servicesHelper = servicesHelper;
            this._recaptchaService = recaptchaService;
            this._mercadoPagoHelper = mercadoPagoHelper;
            this._guestUserHelper = guestUserHelper;
            this._wompiPaymentGatewayService = wompiPaymentGatewayService;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._mercadoPagoPublicKey = configuration.GetSection("MercadoPagoSettings").GetValue<string>("PublicKey");
            this._mercadoPagoAccessToken = configuration.GetSection("MercadoPagoSettings").GetValue<string>("AccessToken");
            this._mercadoPagoClientId = configuration.GetSection("MercadoPagoSettings").GetValue<string>("ClientID");
            this._mercadoPagoClientSecret = configuration.GetSection("MercadoPagoSettings").GetValue<string>("ClientSecret");
            this._shippingCost = configuration.GetSection("CostSettings").GetValue<decimal>("Shipping");
            this._fundingRequestTemplate = configuration.GetSection("WestBankFundingRequest").GetValue<string>("ManualInvoiceTemplate");
            this._fundingRequestEmailTo = configuration.GetSection("WestBankFundingRequest").GetValue<string>("ManualInvoiceEmailTo");
            this._fundingRequestEmailToCc = configuration.GetSection("WestBankFundingRequest").GetValue<string>("ManualInvoiceEmailToCc");
            this._fundingRequestEmailToCc2 = configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            this._wompiPublicKey = configuration.GetSection("WompiSettings").GetValue<string>("PublicKey");
            this._configuration = configuration;
        }
        #endregion

        #region Shopping Cart BL
        /// <summary>
        /// Pays this instance.
        /// </summary>
        /// <returns>Index View</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Customers the data.
        /// </summary>
        /// <returns>
        /// Customer Data view
        /// </returns>
        [Route("[controller]/Cliente")]
        [HttpGet]
        public async Task<IActionResult> CustomerData()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await this._userManager.GetUserAsync(User);
                var customer = await this.GetCustomer(user.CustomersId);
                var checkoutCustomerPersonalData = new CheckoutCustomerDataViewModel()
                {
                    Email = customer.EmailAddress,
                    FristName = user.Name,
                    LastName = user.LastName,
                    AddressesData = new CheckoutCustomerAddressesDataViewModel() { Country = user.Country }
                };

                return this.View(checkoutCustomerPersonalData);
            }

            return this.View();
        }

        /// <summary>
        /// Payments the ways.
        /// </summary>
        /// <param name="checkoutCustomerDataViewModel">The checkout customer personal data view model.</param>
        /// <returns>
        /// Payment Ways View
        /// </returns>
        [Route("[controller]/Forma-de-pago")]
        [HttpGet]
        public IActionResult PaymentWays(CheckoutCustomerDataViewModel checkoutCustomerDataViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(CustomerData), checkoutCustomerDataViewModel);
            }

            var checkoutDataViewModel = new CheckoutDataViewModel(checkoutCustomerDataViewModel);
            return this.View(checkoutDataViewModel);
        }

        /// <summary>
        /// Payments the ways.
        /// </summary>
        /// <param name="checkoutDataViewModel">The checkout data view model.</param>
        /// <returns>Last View</returns>
        [Route("[controller]/Forma-de-pago")]
        [HttpPost]
        public IActionResult PaymentWays(CheckoutDataViewModel checkoutDataViewModel)
        {
            //var ase = ValidateStock();

            if (!ModelState.IsValid)
            {
                return this.View(checkoutDataViewModel);
            }

            switch (checkoutDataViewModel.PaymentWayData.PaymentWayOption)
            {
                case PaymentWaysOptions.FullPay:
                case PaymentWaysOptions.PartialPay:
                    switch (checkoutDataViewModel.PaymentWayData.GatewayPayment)
                    {
                        case GatewayPayments.MP:
                            ViewBag.MercadoPagoPublicKey = this._mercadoPagoPublicKey;
                            return this.View("MPGatewayPayment", checkoutDataViewModel);
                        case GatewayPayments.Wompi:
                            ViewBag.WompiPublicKey = this._wompiPublicKey;
                            return this.View("WompiGatewayPayment", checkoutDataViewModel);
                        default:
                            ModelState.AddModelError(string.Empty, "Pasarela de pago no soportada");
                            return this.View(checkoutDataViewModel);
                    }
                case PaymentWaysOptions.FundingRequest:
                    switch (checkoutDataViewModel.PaymentWayData.FundingRequestInstitution)
                    {
                        case FundingRequestInstitutions.SantanderBank:
                            TempData["CheckoutDataViewModel"] = JsonConvert.SerializeObject(checkoutDataViewModel);
                            return this.RedirectToAction(nameof(FundingRequestSantaderBank));
                        default:
                            ModelState.AddModelError(string.Empty, "Banco no soportado");
                            return this.View(checkoutDataViewModel);
                    }
                default:
                    ModelState.AddModelError(string.Empty, "Forma de pago no soportada");
                    return this.View(checkoutDataViewModel);
            }
        }

        /// <summary>
        /// Wompis the gateway payment result.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="env">The env.</param>
        /// <returns>
        /// Wompi Result
        /// </returns>
        [HttpGet("[controller]/wp/resultado")]
        public async Task<IActionResult> WompiGatewayPaymentResult(string id, WompiEnviroment env)
        {
            var transactionRequestResult = await this._wompiPaymentGatewayService.GetTransactionInfo(id);
            if (!transactionRequestResult.IsSuccess)
            {
                ViewBag.Error = "true";
            }

            ViewBag.FromShoppingCart = "true";
            var transactionInfo = transactionRequestResult.Result;
            var ordersApi = new OrdersApi(this._httpClientInstance);
            var order = await ordersApi.GetRecordForOrderNumber(long.Parse(transactionInfo.Data.Reference));
            order.Status = transactionInfo.Data.Status.ToString();
            order.TransactionId = transactionInfo.Data.Id;
            await ordersApi.UpdateRecord(order.OrdersId, order);
            ViewBag.OrdersId = order.OrdersId;

            return this.View("~/Views/OnlinePayments/WompiResult.cshtml", transactionInfo);
        }

        /// <summary>
        /// Mps the gateway payment.
        /// </summary>
        /// <param name="mercadoPagosViewModel">The mercado pagos view model.</param>
        /// <returns>Mercado Pagos Payment</returns>
        [HttpPost]
        public async Task<IActionResult> MPGatewayPayment(CheckoutMercadoPagosViewModel mercadoPagosViewModel)
        {
            if (!ModelState.IsValid || !string.IsNullOrEmpty(await this.VerifyStock()))
            {
                ViewBag.MercadoPagoPublicKey = this._configuration.GetSection("MercadoPagoSettings").GetValue<string>("PublicKey");
                ViewBag.Error = "true";
                return this.View(mercadoPagosViewModel);
            }

            ////Create the order, order details and taxes
            var order = await this.CreateOrder(mercadoPagosViewModel.CheckoutData);
            var baseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = this._mercadoPagoHelper.ToMercadoPagoPayment(mercadoPagosViewModel, order.OrderNumber, $"{baseUri}/Webhooks");
            var paymentResult = await this._mercadoPagoHelper.SavePayment(payment);
            
            if (!paymentResult.IsSuccess)
            {
                ViewBag.ErrorMessage = paymentResult.Message;
                return this.View();
            }

            var ordersApi = new OrdersApi(this._httpClientInstance);
            order.Status = paymentResult.Result.Status.ToString().ToUpper();
            order.TransactionId = paymentResult.Result.Id.ToString();
            await ordersApi.UpdateRecord(order.OrdersId, order);
            

            await this.CleanShoppingCart();
            return this.RedirectToAction(nameof(MPGatewayPaymentResult), paymentResult.Result);
        }

        /// <summary>
        /// Mps the gateway payment result.
        /// </summary>
        /// <param name="mercadoPagoPaymentResponse">The mercado pago payment response.</param>
        /// <returns>MP Result</returns>
        [HttpGet("[controller]/mp/resultado")]
        public async Task<IActionResult> MPGatewayPaymentResult(MercadoPagoPaymentResponse mercadoPagoPaymentResponse)
        {
            var ordersApi = new OrdersApi(this._httpClientInstance);
            var order = await ordersApi.GetRecordForTransaction(mercadoPagoPaymentResponse.Id ?? 0);
            ViewBag.OrdersId = order.OrdersId;
            return this.View(mercadoPagoPaymentResponse);
        }

        /// <summary>
        /// Fundings the request santader bank.
        /// </summary>
        /// <returns>
        /// Funding Request Santader Bank
        /// </returns>
        [Route("[controller]/Solicitud-de-credito/Banco-Santander")]
        [HttpGet]
        public IActionResult FundingRequestSantaderBank()
        {
            var checkoutDataViewModel = JsonConvert.DeserializeObject<CheckoutDataViewModel>((string)TempData["CheckoutDataViewModel"]);
            var checkoutFundingRequestSantaderBankViewModel = new CheckoutFundingRequestSantaderBankViewModel() { CheckoutData = checkoutDataViewModel };
            return this.View(checkoutFundingRequestSantaderBankViewModel);
        }

        /// <summary>
        /// Fundings the request santader bank.
        /// </summary>
        /// <param name="checkoutFundingRequestSantaderBankViewModel">The checkout funding request santader bank view model.</param>
        /// <returns>
        /// Funding Request Santader Bank
        /// </returns>
        [Route("[controller]/Solicitud-de-credito/Banco-Santander")]
        [HttpPost]
        public async Task<IActionResult> FundingRequestSantaderBank(CheckoutFundingRequestSantaderBankViewModel checkoutFundingRequestSantaderBankViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
                {
                    ControlStatus = "ModelError",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage)
                });
            }

            ////Get Token Auth
            var authResponse = await this._servicesHelper.SantanderBankAuth();
            if (!authResponse.IsSuccess)
            {
                return this.Ok(new FundingRequestGeneralResponse<SantanderBankFundingResponse>()
                {
                    ControlStatus = "Error",
                    Message = $"Error al Autenticarse con el Banco Santander => {authResponse.Message}"
                });
            }

            var token = authResponse.Result.Token;
            var requestBody = CommonHelper.ToSantanderBankFundingRequest(checkoutFundingRequestSantaderBankViewModel, this._configuration);
            var fundingResponse = await this._servicesHelper.SantanderFundingRequest(requestBody, token);
            var reportId = Guid.NewGuid().ToString();
            var fundingRequesResponseGeneral = this._servicesHelper.ToFundingRequestGeneralResponse(fundingResponse, reportId);
            var requestBodySerialized = JsonConvert.SerializeObject(requestBody);
            var responseSerialized = JsonConvert.SerializeObject(fundingRequesResponseGeneral);
            CommonHelper.SaveRequestReportOnTempPath(reportId, requestBodySerialized, responseSerialized, "Santander Bank");

            return this.Ok(fundingRequesResponseGeneral);
        }

        /// <summary>
        /// Fundings the request santader bank create order.
        /// </summary>
        /// <param name="santanderBankRequestData">The santander bank request data.</param>
        /// <param name="santanderResponse">The santander response.</param>
        /// <returns>Order when payment way is Funding request to Santander Bank</returns>
        [HttpPost]
        public async Task<IActionResult> FundingRequestSantaderBankCreateOrder(CheckoutFundingRequestSantaderBankViewModel santanderBankRequestData, [FromQuery] string santanderResponse)
        {
            if (string.IsNullOrEmpty(santanderResponse) || !string.IsNullOrEmpty(await this.VerifyStock()))
            {
                return this.BadRequest("Por favor verifique la inforamción envíada o valide que haya disponibilidad de stock");
            }

            var fundingRequestGeneralResponse = JsonConvert.DeserializeObject<FundingRequestGeneralResponse<SantanderBankFundingResponse>>(Encoding.UTF8.GetString(Convert.FromBase64String(santanderResponse)));
            try
            {
                ////Create the order, order details, taxes and Funding Tracking & update stock
                var order = await this.CreateOrder(santanderBankRequestData, fundingRequestGeneralResponse);
                await this.SendEmailForFundingRequest(santanderBankRequestData, fundingRequestGeneralResponse, order);
                await this.CleanShoppingCart();
                return this.Ok(new { ControlStatus = "FundingRequestOk", OrderId = order.OrdersId });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates the temporary order.
        /// </summary>
        /// <param name="checkoutDataViewModel">The checkout data view model.</param>
        /// <returns>Order Temp</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTempOrder(CheckoutDataViewModel checkoutDataViewModel)
        {
            

            if (!ModelState.IsValid || !string.IsNullOrEmpty(await this.VerifyStock()))
            {
                return this.BadRequest("Por favor verifique la inforamción envíada o valide que haya disponibilidad de stock");
            }

            try
            {
                ////Create the order, order details and taxes
                var order = await this.CreateOrder(checkoutDataViewModel);
                await this.CleanShoppingCart();
                return this.Ok(new { ControlStatus = "Ok", OrdersId = order.OrdersId, OrderNumber = order.OrderNumber });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        #region Mercado Pagos Web Tokenize
        /// <summary>
        /// MP the external web tokenize.
        /// </summary>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <returns>MP External Web Tokenize</returns>
        public async Task<IActionResult> MPExternalWebTokenize(MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Verifique los datos ingresados";
                return this.View(mPExternalWebTokenizeViewModel);
            }

            var totalAmount = await this.GetTotalAmount(mPExternalWebTokenizeViewModel.InventoryItemId);
            ViewBag.MercadoPagoPublicKey = this._mercadoPagoPublicKey;
            ViewBag.TotalAmount = totalAmount;
            if (totalAmount <= 0)
            {
                ViewBag.Message = "Verifique los datos ingresados";
                ModelState.AddModelError("InventoryItemId", "Ítem a pagar no está habilitado.");
                return this.View(mPExternalWebTokenizeViewModel);
            }

            return this.View(mPExternalWebTokenizeViewModel);
        }

        /// <summary>
        /// Mps the external web tokenize.
        /// </summary>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <param name="mPCheckoutDataDTO">The m p checkout data dto.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MPExternalWebTokenize(MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel, MPCheckoutDataDTO mPCheckoutDataDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MercadoPagoPublicKey = this._mercadoPagoPublicKey;
                ViewBag.Message = "Verifique los datos ingresados";
                return this.View(mPExternalWebTokenizeViewModel);
            }

            var customerId = await this.CheckCustomer(mPExternalWebTokenizeViewModel);
            var order = await this.CreateOrder(mPExternalWebTokenizeViewModel, customerId);
            var mpPayment = this._mercadoPagoHelper.ToMercadoPagoPayment(order.OrderNumber, mPExternalWebTokenizeViewModel, mPCheckoutDataDTO, await this.GetTotalAmount(mPExternalWebTokenizeViewModel.InventoryItemId));
            var paymentResult = await this._mercadoPagoHelper.SavePayment(mpPayment);
            if (!paymentResult.IsSuccess)
            {
                ViewBag.Message = paymentResult.Message;
                ViewBag.MercadoPagoPublicKey = this._mercadoPagoPublicKey;
                await this.DeleteOrder(order);
                return this.View(mPExternalWebTokenizeViewModel);
            }

            var detailsResult = await this.CreateOrderDetails(mPExternalWebTokenizeViewModel, order.OrdersId);
            var hOrderTaxes = await this.CreateOrderTaxesHistory(order.OrdersId, mPExternalWebTokenizeViewModel);
            await this.UpdateOrderMercadoPagoPayment(order, paymentResult.Result.Status, paymentResult.Result.Id);
            TempData["FromAppPayment"] = "true";
            TempData["OrdersId"] = order.OrdersId;
            return this.RedirectToAction("PaymentDetails", "Orders", new { ordersId = order.OrdersId });
        } 
        #endregion
        #endregion

        #region Add/Remove Item To Shopping Cart
        /// <summary>
        /// Adds to cart.
        /// </summary>
        /// <param name="shoppingCartRecordViewModel">The shopping cart record.</param>
        /// <returns>Add to cart object on Json format</returns>

        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] ShoppingCartRecordViewModel shoppingCartRecordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(new { State = "Invalid" });
            }

            if (User.Identity.IsAuthenticated)
            {
                var user = await this._userManager.GetUserAsync(User);
                var shoppingCartRecord = new ShoppingCartRecords()
                {
                    DateCreated = DateTime.Now,
                    FkCustomersId = user.CustomersId ?? 0,
                    FkInventoryItemsId = shoppingCartRecordViewModel.InventoryItemId,
                    FkProductsId = shoppingCartRecordViewModel.ProductId,
                    Quantity = Math.Abs(shoppingCartRecordViewModel.Quantity)
                };

                try
                {
                    this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                    var shoppingCartRecordsApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                    var shoppingCartRecords = await shoppingCartRecordsApi.CreateRecord(shoppingCartRecord);
                    return this.Json(new { State = "Ok", ShoppingCartRecords = shoppingCartRecords });
                }
                catch (Exception)
                {
                    return this.Json(new { State = "Invalid" });
                }
            }
            else
            {
                var guestID = this.GetGuestId();
                var shoppingCartRecord = new ShoppingCartRecordsGuest()
                {
                    DateCreated = DateTime.Now,
                    GuestId = guestID,
                    FkInventoryItemsId = shoppingCartRecordViewModel.InventoryItemId,
                    FkProductsId = shoppingCartRecordViewModel.ProductId,
                    Quantity = Math.Abs(shoppingCartRecordViewModel.Quantity)
                };

                try
                {
                    var shoppingCartRecordsApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                    var shoppingCartRecords = await shoppingCartRecordsApi.CreateRecord(shoppingCartRecord);
                    return this.Json(new { State = "Ok", ShoppingCartRecords = shoppingCartRecords });
                }
                catch (Exception)
                {
                    return this.Json(new { State = "Invalid" });
                }
            }
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="shoppingCartRecordsId">The shopping cart records identifier.</param>
        /// <returns></returns>

        [HttpDelete]
        [Route("[controller]/[action]/{shoppingCartRecordsId:int}")]
        public async Task<IActionResult> RemoveItem(int shoppingCartRecordsId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                    var shoppingCartRecordsApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                    await shoppingCartRecordsApi.DeleteRecord(shoppingCartRecordsId);
                    return this.Content("Ok");
                }
                catch (Exception)
                {
                    return this.Content("Error");
                }
            }
            else
            {
                try
                {
                    var shoppingCartRecordsApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                    await shoppingCartRecordsApi.DeleteRecord(shoppingCartRecordsId);
                    return this.Content("Ok");
                }
                catch (Exception)
                {
                    return this.Content("Error");
                }
            }
        }
        #endregion

        #region View Components
        /// <summary>
        /// Gets the shopping cart summary view component.
        /// </summary>
        /// <returns>Shopping cart view component</returns>
        [HttpGet]

        public async Task<IActionResult> GetShoppingCartSummaryViewComponent()
        {
            await Task.Delay(500);
            return ViewComponent("ShoppingCartSummary");
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <returns>Shopping cart checkout view component</returns>
        [HttpGet]

        public async Task<IActionResult> GetShoppingCartCheckout()
        {
            await Task.Delay(500);
            return ViewComponent("ShoppingCartCheckout");
        }

        /// <summary>
        /// Gets the shopping cart summary checkout.
        /// </summary>
        /// <returns>Shopping cart checkout view component</returns>
        [HttpGet]

        public async Task<IActionResult> GetShoppingCartSummaryCheckout()
        {
            await Task.Delay(500);
            return ViewComponent("ShoppingCartSummaryCheckout");
        }
        #endregion

        #region Common
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Customer task</returns>
        private Task<Customers> GetCustomer(int? customersId)
        {
            var customerApi = new CustomerApi(this._httpClientInstance);
            return customerApi.GetRecord(customersId ?? 0);
        }

        /// <summary>
        /// Updates the order mercado pago payment.
        /// </summary>
        /// <param name="orderCreated">The order created.</param>
        /// <param name="status">The status.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        private async Task UpdateOrderMercadoPagoPayment(Orders orderCreated, PaymentStatus? status, long? transactionId)
        {
            var ordersApi = new OrdersApi(this._httpClientInstance);
            orderCreated.TransactionId = transactionId.ToString();
            orderCreated.Status = status.ToString().ToUpper();
            await ordersApi.UpdateRecord(orderCreated.OrdersId, orderCreated);
        }

        /// <summary>
        /// Deletes the order.
        /// </summary>
        /// <param name="orderCreated">The order created.</param>
        private async Task DeleteOrder(Orders orderCreated)
        {
            var ordersApi = new OrdersApi(this._httpClientInstance);
            await ordersApi.DeleteRecord(orderCreated.OrdersId);
        }

        /// <summary>
        /// Updates the product stock.
        /// </summary>
        /// <param name="shoppingCartRecords">The shopping cart records.</param>
        private async Task UpdateStock(List<ShoppingCartRecords> shoppingCartRecords)
        {
            ////Update Inventory Stock
            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            foreach (var item in shoppingCartRecords)
            {
                var inventoryItem = await inventoryItemsApi.GetRecord(item.FkInventoryItemsId);
                inventoryItem.QuantityInStock -= item.Quantity;
                await inventoryItemsApi.UpdateRecord(item.FkInventoryItemsId, inventoryItem);
            }

            ////Update product Stock
            var productsApi = new ProductsApi(this._httpClientInstance);
            foreach (var item in shoppingCartRecords)
            {
                var product = await productsApi.GetRecord(item.FkProductsId);
                product.QuantityInStock -= item.Quantity;
                await productsApi.UpdateRecord(item.FkProductsId, product);
            }
        }

        /// <summary>
        /// Verifies the stock.
        /// </summary>
        /// <returns>String control</returns>
        private async Task<string> VerifyStock()
        {
            var shoppingCartRecords = new List<ShoppingCartRecords>();
            if (User.Identity.IsAuthenticated)
            {
                var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                var user = await this._userManager.GetUserAsync(User);
                shoppingCartRecords = await shoppingCartApi.GetRecordsForCustomer((long)user.CustomersId);
            }
            else
            {
                var guestId = this.GetGuestId();
                var shoppingCartApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                var shoppingCartRecordsGuest = await shoppingCartApi.GetRecordsForCustomer(guestId);
                shoppingCartRecords = shoppingCartRecordsGuest.ConvertAll(x => new ShoppingCartRecords()
                {
                    FkCustomersId = 0,
                    FkInventoryItemsId = x.FkInventoryItemsId ?? 0,
                    FkProductsId = x.FkProductsId,
                    Quantity = x.Quantity
                });
            }

            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            var message = string.Empty;
            foreach (var item in shoppingCartRecords)
            {
                var inventoryItem = await inventoryItemsApi.GetRecord(item.FkInventoryItemsId);

                if (inventoryItem.QuantityInStock - item.Quantity < 0)
                {
                    message += $"No hay existencias del producto: {inventoryItem.FkProducts.ProductName}" + Environment.NewLine;
                }
            }

            return message;
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        /// <param name="userClaim">The user claim.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// Token user
        /// </returns>
        private async Task<string> GetUserToken(ClaimsPrincipal userClaim, IConfiguration configuration)
        {
            var user = await this._userManager.GetUserAsync(userClaim);
            var token = await this._userManager.GetAuthenticationTokenAsync(user, configuration.GetSection("JwtSettings").GetValue<string>("Issuer"), configuration.GetSection("JwtSettings").GetValue<string>("Subject"));
            return token;
        }

        /// <summary>
        /// Gets the total amount.
        /// </summary>
        /// <param name="inventoryItemId">The inventory item ids.</param>
        /// <returns>Total Amount</returns>
        private async Task<decimal> GetTotalAmount(int inventoryItemId)
        {
            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            decimal taxesPrice = 0, totalAmount = 0;
            var inventoryItem = await inventoryItemsApi.GetRecord(inventoryItemId);
            if ((inventoryItem?.AllowShow ?? 0) <= 0)
            {
                return 0;
            }

            if (inventoryItem.TaxesForInventory.Any())
            {
                taxesPrice = inventoryItem.TaxesForInventory.Aggregate(taxesPrice, (sum, tax) => sum + (inventoryItem.SalesPrice * tax.FkTaxes.TaxPercent / 100) ?? 0);
            }

            totalAmount += Math.Floor(inventoryItem.SalesPrice ?? 0) + Math.Ceiling(taxesPrice);
            return totalAmount;
        }

        /// <summary>
        /// Gets the total taxes.
        /// </summary>
        /// <param name="inventoryItemId">The inventory item identifier.</param>
        /// <returns>Total Taxes</returns>
        private async Task<decimal> GetTotalTaxes(int inventoryItemId)
        {
            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            decimal taxesPrice = 0;
            var inventoryItem = await inventoryItemsApi.GetRecord(inventoryItemId);
            if (inventoryItem.TaxesForInventory.Any())
            {
                taxesPrice = inventoryItem.TaxesForInventory.Aggregate(taxesPrice, (sum, tax) => sum + (inventoryItem.SalesPrice * tax.FkTaxes.TaxPercent / 100) ?? 0);
            }

            return Math.Ceiling(taxesPrice);
        }

        /// <summary>
        /// Creates the order taxes history.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <returns>Taxes for order history</returns>
        private async Task<List<HOrdersTaxes>> CreateOrderTaxesHistory(int ordersId, MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel)
        {
            var hOrdersTaxes = new List<HOrdersTaxes>();
            var hOrdersTaxesApi = new HOrdersTaxesApi(this._httpClientInstance);
            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            var inventoryItem = await inventoryItemsApi.GetRecord(mPExternalWebTokenizeViewModel.InventoryItemId);
            foreach (var item in inventoryItem.TaxesForInventory)
            {
                var taxTotal = (inventoryItem.SalesPrice * item.FkTaxes.TaxPercent / 100) ?? 0;
                hOrdersTaxes.Add(
                    new HOrdersTaxes()
                    {
                        FkOrdersId = ordersId,
                        TaxName = item.FkTaxes.TaxName,
                        TaxPercent = item.FkTaxes.TaxPercent,
                        TaxTotal = Math.Ceiling(taxTotal)
                    });
            }

            return await hOrdersTaxesApi.CreateRecords(hOrdersTaxes);
        }

        /// <summary>
        /// Creates the order details.
        /// </summary>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>Order details</returns>
        private async Task<List<OrderDetails>> CreateOrderDetails(MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel, int orderId)
        {
            var orderDetailsList = new List<OrderDetails>();
            var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            var inventoryItem = await inventoryItemsApi.GetRecord(mPExternalWebTokenizeViewModel.InventoryItemId);
            orderDetailsList.Add(new OrderDetails()
            {
                FKInventoryItemsId = mPExternalWebTokenizeViewModel.InventoryItemId,
                FkOrdersId = orderId,
                FkProductsId = inventoryItem.FkProductsId,
                ItemPrice = inventoryItem.SalesPrice ?? 0,
                Notes = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss} - Pago por APP.",
                Quantity = 1,
                Tax = await this.GetTotalTaxes(mPExternalWebTokenizeViewModel.InventoryItemId)
            });

            var orderDetailsCreated = await orderDetailsApi.CreateRecords(orderDetailsList);
            return orderDetailsCreated;
        }

        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Order created Id</returns>
        private async Task<Orders> CreateOrder(MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel, int customersId)
        {
            var ordersApi = new OrdersApi(this._httpClientInstance);
            var total = await this.GetTotalAmount(mPExternalWebTokenizeViewModel.InventoryItemId);
            var taxes = await this.GetTotalTaxes(mPExternalWebTokenizeViewModel.InventoryItemId);
            var subTotal = (total - taxes) + this._shippingCost;
            var newOrder = new Orders()
            {
                BillToAddress = mPExternalWebTokenizeViewModel.ShipToAddress,
                BillToCity = mPExternalWebTokenizeViewModel.ShipToCity,
                BillToState = mPExternalWebTokenizeViewModel.ShipToState,
                BillToZipcode = mPExternalWebTokenizeViewModel.ShipToZipcode,
                IdentificationNumber = mPExternalWebTokenizeViewModel.IdentificationNumber,
                IdentificationType = mPExternalWebTokenizeViewModel.IdentificationType,
                FullName = mPExternalWebTokenizeViewModel.FullName,
                PhoneNumber = mPExternalWebTokenizeViewModel.PhoneNumber,
                ShipToAddress = mPExternalWebTokenizeViewModel.ShipToAddress,
                ShipToCity = mPExternalWebTokenizeViewModel.ShipToCity,
                ShipToState = mPExternalWebTokenizeViewModel.ShipToState,
                ShipToZipcode = mPExternalWebTokenizeViewModel.ShipToZipcode,
                Country = "Colombia",
                Discount = 0,
                FkCustomersId = customersId,
                Notes = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente - Pago total.",
                OrderDate = DateTime.Now,
                PaymentMethod = "Credit Card",
                Shipping = this._shippingCost,
                Status = "Pendiente de pago",
                Subtotal = subTotal,
                Tax = taxes,
                Total = total
            };

            newOrder = await ordersApi.CreateRecord(newOrder);
            return newOrder;
        }

        /// <summary>
        /// Checks the customer.
        /// </summary>
        /// <param name="mPExternalWebTokenizeViewModel">The m p external web tokenize view model.</param>
        /// <returns>Customer Id</returns>
        private async Task<int> CheckCustomer(MPExternalWebTokenizeViewModel mPExternalWebTokenizeViewModel)
        {
            var customersApi = new CustomerApi(this._httpClientInstance);
            var customer = (await customersApi.GetAllRecords()).FirstOrDefault(x => x.EmailAddress == mPExternalWebTokenizeViewModel.Email);
            if (customer != null)
            {
                return customer.CustomersId;
            }

            var newCustomer = new Customers()
            {
                BillToAddress = mPExternalWebTokenizeViewModel.ShipToAddress,
                BillToCity = mPExternalWebTokenizeViewModel.ShipToCity,
                BillToState = mPExternalWebTokenizeViewModel.ShipToState,
                BillToZipcode = mPExternalWebTokenizeViewModel.ShipToZipcode,
                EmailAddress = mPExternalWebTokenizeViewModel.Email,
                FullName = mPExternalWebTokenizeViewModel.FullName,
                PhoneNumber = mPExternalWebTokenizeViewModel.PhoneNumber,
                ShipToAddress = mPExternalWebTokenizeViewModel.ShipToAddress,
                ShipToCity = mPExternalWebTokenizeViewModel.ShipToCity,
                ShipToState = mPExternalWebTokenizeViewModel.ShipToState,
                ShipToZipcode = mPExternalWebTokenizeViewModel.ShipToZipcode
            };

            newCustomer = await customersApi.CreateRecord(newCustomer);
            return newCustomer.CustomersId;
        }

        /// <summary>
        /// Gets the guest identifier.
        /// </summary>
        /// <returns>Guest Identifier</returns>
        private string GetGuestId()
        {
            return this._guestUserHelper.GetGuestId();
        }

        /// <summary>
        /// Cleans the shopping cart.
        /// </summary>
        /// <returns>Clean Shopping Cart</returns>
        private async Task CleanShoppingCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await this._userManager.GetUserAsync(User);
                this._httpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, await this.GetUserToken(User, this._configuration));
                var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                await shoppingCartApi.DeleteRecordsForCustomer((long)user.CustomersId);
            }
            else
            {
                var guestID = this.GetGuestId();
                var shoppingCartGuestApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                await shoppingCartGuestApi.DeleteRecordsForCustomer(guestID);
            }
        }

        /// <summary>
        /// Checks the customer.
        /// </summary>
        /// <param name="checkoutDataViewModel">The checkout data view model.</param>
        /// <returns>Customer</returns>
        private async Task<Customers> CheckCustomer(CheckoutDataViewModel checkoutDataViewModel)
        {
            var customersApi = new CustomerApi(this._httpClientInstance);
            var customer = (await customersApi.GetAllRecords()).FirstOrDefault(x => x.EmailAddress == checkoutDataViewModel.CustomerData.Email);
            if (customer != null)
            {
                return customer;
            }

            var newCustomer = new Customers()
            {
                BillToAddress = checkoutDataViewModel.CustomerData.AddressesData.BillAddress,
                BillToCity = checkoutDataViewModel.CustomerData.AddressesData.BillCity,
                BillToState = checkoutDataViewModel.CustomerData.AddressesData.BillState,
                BillToZipcode = checkoutDataViewModel.CustomerData.AddressesData.BillZipCode,
                EmailAddress = checkoutDataViewModel.CustomerData.Email,
                FullName = checkoutDataViewModel.CustomerData.FullName,
                PhoneNumber = checkoutDataViewModel.CustomerData.PhoneNumber,
                ShipToAddress = checkoutDataViewModel.CustomerData.AddressesData.Address,
                ShipToCity = checkoutDataViewModel.CustomerData.AddressesData.City,
                ShipToState = checkoutDataViewModel.CustomerData.AddressesData.State,
                ShipToZipcode = checkoutDataViewModel.CustomerData.AddressesData.ZipCode
            };

            return await customersApi.CreateRecord(newCustomer);
        }

        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <param name="checkoutDataViewModel">The checkout data view model.</param>
        /// <returns>Orders</returns>
        /// <exception cref="Exception">
        /// No hay registros para procesar
        /// or
        /// Error al crear Orden. Por favor contáctanos.
        /// </exception>
        private async Task<Orders> CreateOrder(CheckoutDataViewModel checkoutDataViewModel)
        {
            var shoppingCartRecords = await this.GetShoppingCartRecords(checkoutDataViewModel);
            if (!shoppingCartRecords.Any())
            {
                throw new Exception("No hay registros para procesar");
            }

            var costsDataTotal = await this.GetCostsDataForShoppingCart(shoppingCartRecords.First().FkCustomersId);
            var paymentMehod = checkoutDataViewModel.PaymentWayData.GatewayPayment switch
            {
                GatewayPayments.MP => "Mercado Pagos",
                GatewayPayments.Wompi => "Wompi",
                _ => throw new Exception("Payment method invalid.")
            };

            var notes = checkoutDataViewModel.PaymentWayData.PaymentWayOption switch
            {
                PaymentWaysOptions.FullPay => $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente - Pago total.",
                PaymentWaysOptions.FundingRequest => $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente.",
                PaymentWaysOptions.PartialPay => $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente - Pago parcial de: {checkoutDataViewModel.PaymentWayData.PartialAmount ?? 0:C}",
                _ => throw new Exception("Payment method invalid."),
            };

            var order = new Orders()
            {
                AlternateNumber = checkoutDataViewModel.CustomerData.AlternateNumber,
                BillToAddress = checkoutDataViewModel.CustomerData.AddressesData.BillAddress,
                BillToCity = checkoutDataViewModel.CustomerData.AddressesData.BillCity,
                BillToState = checkoutDataViewModel.CustomerData.AddressesData.BillState,
                BillToZipcode = checkoutDataViewModel.CustomerData.AddressesData.BillZipCode,
                Country = checkoutDataViewModel.CustomerData.AddressesData.Country,
                Discount = 0,
                FkCustomersId = shoppingCartRecords.First().FkCustomersId,
                Notes = notes,
                OrderDate = DateTime.UtcNow,
                PaymentMethod = paymentMehod,
                PhoneNumber = checkoutDataViewModel.CustomerData.PhoneNumber,
                Shipping = this._shippingCost,
                ShipToAddress = checkoutDataViewModel.CustomerData.AddressesData.Address,
                ShipToCity = checkoutDataViewModel.CustomerData.AddressesData.City,
                ShipToState = checkoutDataViewModel.CustomerData.AddressesData.State,
                ShipToZipcode = checkoutDataViewModel.CustomerData.AddressesData.ZipCode,
                Status = "VOID",
                Subtotal = costsDataTotal.SubTotal <= 0 ? checkoutDataViewModel.PaymentWayData.TotalAmount : costsDataTotal.SubTotal,
                Tax = costsDataTotal.TotalTaxes,
                Total = costsDataTotal.Total <= 0 ? checkoutDataViewModel.PaymentWayData.TotalAmount : costsDataTotal.Total,
                FullName = checkoutDataViewModel.CustomerData.FullName,
                IdentificationNumber = checkoutDataViewModel.CustomerData.IDNumber,
                IdentificationType = checkoutDataViewModel.CustomerData.IDType,
                PartialAmountPaid = checkoutDataViewModel.PaymentWayData.PaymentWayOption == PaymentWaysOptions.PartialPay ? checkoutDataViewModel.PaymentWayData.PartialAmount : 0,
                HOrdersTaxes = this.GetOrderTaxesHistory(costsDataTotal.TotalTaxesDetailed),
                OrderDetails = this.GetOrderDetails(shoppingCartRecords, costsDataTotal)
            };

            var ordersApi = new OrdersApi(this._httpClientInstance);
            var newOrder = await ordersApi.CreateRecord(order);
            if (newOrder.OrdersId <= 0)
            {
                throw new Exception("Error al crear Orden. Por favor contáctanos.");
            }

            await this.UpdateStock(shoppingCartRecords);
            return newOrder;
        }

        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <param name="santanderBankRequestData">The santander bank request data.</param>
        /// <param name="fundingRequestGeneralResponse">The funding request general response.</param>
        /// <exception cref="Exception">
        /// No hay registros para procesar
        /// or
        /// Error al crear Orden. Por favor contáctanos.
        /// </exception>
        private async Task<Orders> CreateOrder(CheckoutFundingRequestSantaderBankViewModel santanderBankRequestData, FundingRequestGeneralResponse<SantanderBankFundingResponse> fundingRequestGeneralResponse)
        {
            var shoppingCartRecords = await this.GetShoppingCartRecords(santanderBankRequestData.CheckoutData);
            if (!shoppingCartRecords.Any())
            {
                throw new Exception("No hay registros para procesar");
            }

            var costsDataTotal = await this.GetCostsDataForShoppingCart(shoppingCartRecords.First().FkCustomersId);
            var paymentMehod = santanderBankRequestData.CheckoutData.PaymentWayData.FundingRequestInstitution switch
            {
                FundingRequestInstitutions.SantanderBank => "Funding Request",
                _ => throw new Exception("Payment method invalid.")
            };

            var notes = santanderBankRequestData.CheckoutData.PaymentWayData.PaymentWayOption switch
            {
                PaymentWaysOptions.FullPay => $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente - Pago total.",
                PaymentWaysOptions.FundingRequest => $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente - {fundingRequestGeneralResponse.Message}.",
                PaymentWaysOptions.PartialPay => $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Orden creada exitosamente - Pago parcial de: {santanderBankRequestData.CheckoutData.PaymentWayData.PartialAmount ?? 0:C}",
                _ => throw new Exception("Payment method invalid."),
            };

            var order = new Orders()
            {
                AlternateNumber = santanderBankRequestData.CheckoutData.CustomerData.AlternateNumber,
                BillToAddress = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.BillAddress,
                BillToCity = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.BillCity,
                BillToState = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.BillState,
                BillToZipcode = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.BillZipCode,
                Country = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.Country,
                Discount = 0,
                FkCustomersId = shoppingCartRecords.First().FkCustomersId,
                Notes = notes,
                OrderDate = DateTime.UtcNow,
                PaymentMethod = paymentMehod,
                PhoneNumber = santanderBankRequestData.CheckoutData.CustomerData.PhoneNumber,
                Shipping = this._shippingCost,
                ShipToAddress = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.Address,
                ShipToCity = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.City,
                ShipToState = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.State,
                ShipToZipcode = santanderBankRequestData.CheckoutData.CustomerData.AddressesData.ZipCode,
                Status = "In Process",
                Subtotal = costsDataTotal.SubTotal <= 0 ? santanderBankRequestData.CheckoutData.PaymentWayData.TotalAmount : costsDataTotal.SubTotal,
                Tax = costsDataTotal.TotalTaxes,
                Total = costsDataTotal.Total <= 0 ? santanderBankRequestData.CheckoutData.PaymentWayData.TotalAmount : costsDataTotal.Total,
                FullName = santanderBankRequestData.CheckoutData.CustomerData.FullName,
                IdentificationNumber = santanderBankRequestData.CheckoutData.CustomerData.IDNumber,
                IdentificationType = santanderBankRequestData.CheckoutData.CustomerData.IDType,
                PartialAmountPaid = 0,
                FKFundingRequests = new FundingRequests()
                {
                    BankName = CommonHelper.GetBankName(Banks.SantanderBank),
                    FKCustomersId = shoppingCartRecords.First().FkCustomersId,
                    IdentificationNumber = santanderBankRequestData.CheckoutData.CustomerData.IDNumber,
                    IdentificationType = santanderBankRequestData.CheckoutData.CustomerData.IDType,
                    InitialFee = santanderBankRequestData.SantanderBankCreditOnline.InitialFee,
                    Installments = int.Parse(CommonHelper.GetSantanderBankInstallmentsMessage(santanderBankRequestData.SantanderBankCreditOnline.Installments)),
                    Names = santanderBankRequestData.CheckoutData.CustomerData.FullName,
                    Notes = $"Solicitud de crédito almacenada. - {fundingRequestGeneralResponse.Message}",
                    LastName = santanderBankRequestData.CheckoutData.CustomerData.LastName,
                    Profession = string.Empty,
                    RequestDate = DateTime.UtcNow,
                    DateOfBirth = DateTime.MinValue,
                    Status = CommonHelper.GetSantanderBankSemaforoMessage(fundingRequestGeneralResponse.Response.Semaforo),
                    TotalAmountRequest = costsDataTotal.Total,
                    CityOfResidence = string.Empty,
                    EconomicActivity = CommonHelper.GetSantanderBankEconomicActivityMessage(santanderBankRequestData.SantanderBankCreditOnline.ActividadEconomica),
                    IndependentActivity = CommonHelper.GetSantanderBankIndependentActivityMessage(santanderBankRequestData.SantanderBankCreditOnline.ActividadIndependiente),
                    MonthlyIncome = santanderBankRequestData.SantanderBankCreditOnline.MonthlyIncome
                },
                HOrdersTaxes = this.GetOrderTaxesHistory(costsDataTotal.TotalTaxesDetailed),
                OrderDetails = this.GetOrderDetails(shoppingCartRecords, costsDataTotal)
            };

            var ordersApi = new OrdersApi(this._httpClientInstance);
            var newOrder = await ordersApi.CreateRecord(order);
            if (newOrder.OrdersId <= 0)
            {
                throw new Exception("Error al crear Orden. Por favor contáctanos.");
            }

            await this.UpdateStock(shoppingCartRecords);
            return newOrder;
        }

        /// <summary>
        /// Sends the email for funding request.
        /// </summary>
        /// <param name="santanderBankRequestData">The santander bank request data.</param>
        /// <param name="fundingRequestGeneralResponse">The funding request general response.</param>
        /// <param name="order">The order.</param>
        /// <exception cref="Exception">Error sending emails => {ex.Message}</exception>
        private async Task SendEmailForFundingRequest(CheckoutFundingRequestSantaderBankViewModel santanderBankRequestData, FundingRequestGeneralResponse<SantanderBankFundingResponse> fundingRequestGeneralResponse, Orders order)
        {
            var emailfullPath = $"{this._host.WebRootPath}{this._fundingRequestTemplate}";
            var logoUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var contentTemplate = new StreamReader(emailfullPath)
                .ReadToEnd()
                .Replace("${logoUrl}", logoUrl)
                .Replace("${firstLabel}", @"Se ha realizado una solicitud de financiación para una Orden creada por medio de la tienda Motovalle - Ecommerce. En pocos minutos llegará el soporte de la orden. <br/><br/>
                                            A continuación encontrará toda la información de dicha solicitud de financiación.")
                .Replace("${bankName}", CommonHelper.GetBankName(Banks.SantanderBank))
                .Replace("${bankResponse}", fundingRequestGeneralResponse.Message)
                .Replace("${name}", santanderBankRequestData.CheckoutData.CustomerData.FullName)
                .Replace("${idType}", santanderBankRequestData.SantanderBankCreditOnline.DocType.ToString())
                .Replace("${idNumber}", santanderBankRequestData.SantanderBankCreditOnline.DocNumber)
                .Replace("${profession}", string.Empty)
                .Replace("${email}", santanderBankRequestData.CheckoutData.CustomerData.Email)
                .Replace("${phoneNumber}", $"{santanderBankRequestData.CheckoutData.CustomerData.PhoneNumber} - {santanderBankRequestData.CheckoutData.CustomerData.AlternateNumber}")
                .Replace("${dateOfBirth}", string.Empty)
                .Replace("${cityOfResidence}", string.Empty)
                .Replace("${issueDate}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                .Replace("${requestId}", order.FKFundingRequestsId.ToString())
                .Replace("${orderNumber}", order.OrderNumber.ToString())
                .Replace("${totalAmount}", santanderBankRequestData.SantanderBankCreditOnline.TotalAmount.ToString("C"))
                .Replace("${initialFee}", santanderBankRequestData.SantanderBankCreditOnline.InitialFee.ToString("C"))
                .Replace("${installments}", CommonHelper.GetSantanderBankInstallmentsMessage(santanderBankRequestData.SantanderBankCreditOnline.Installments))
                .Replace("${economicActivity}", CommonHelper.GetSantanderBankEconomicActivityMessage(santanderBankRequestData.SantanderBankCreditOnline.ActividadEconomica))
                .Replace("${independentActivity}", CommonHelper.GetSantanderBankIndependentActivityMessage(santanderBankRequestData.SantanderBankCreditOnline.ActividadIndependiente))
                .Replace("${monthlyIncome}", santanderBankRequestData.SantanderBankCreditOnline.MonthlyIncome.ToString("C"))
                .Replace("${Date}", DateTime.Now.Year.ToString());

            try
            {
                await this._emailSender.SendEmailAsync(this._fundingRequestEmailTo, "Nueva solicitud de crédito para una orden", contentTemplate, null, new List<string>() { this._fundingRequestEmailToCc, this._fundingRequestEmailToCc2 });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending emails => {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the order details.
        /// </summary>
        /// <param name="shoppingCartRecords">The shopping cart records.</param>
        /// <param name="costsDataTotal">The costs data total.</param>
        /// <returns>Order Detailes</returns>
        private ICollection<OrderDetails> GetOrderDetails(List<ShoppingCartRecords> shoppingCartRecords, CheckoutCostsDataViewModel costsDataTotal)
        {
            return shoppingCartRecords.ConvertAll(x => new OrderDetails()
            {
                FKInventoryItemsId = x.FkInventoryItemsId,
                FkProductsId = x.FkProductsId,
                ItemPrice = costsDataTotal.ShoppingCartCheckoutViewModel.FirstOrDefault(y => y.InventoryItemsId == x.FkInventoryItemsId)?.SalesPriceInventory ?? this.GetInventoryPrice(x.FkInventoryItemsId).Result,
                Notes = $"{DateTime.UtcNow:MM/dd/yyyy HH:mm:ss} - Pendiente de envío.",
                Quantity = x.Quantity,
                Tax = costsDataTotal.ShoppingCartCheckoutViewModel.FirstOrDefault(y => y.InventoryItemsId == x.FkInventoryItemsId)?.Taxes?.Sum(x => x.TaxValue) ?? 0
            });
        }

        /// <summary>
        /// Gets the inventory price.
        /// </summary>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <returns>Sales Price</returns>
        private async Task<decimal> GetInventoryPrice(int inventoryItemsId)
        {
            var inventoryItemsApi = new InventoryItemsApi(this._httpClientInstance);
            var inventoryItem = await inventoryItemsApi.GetRecord(inventoryItemsId);
            if (inventoryItem.SalesPrice == null)
            {
                var productsApi = new ProductsApi(this._httpClientInstance);
                var product = await productsApi.GetRecord(inventoryItem.FkProductsId);
                return product.SalesPrice ?? 0;
            }

            return inventoryItem.SalesPrice ?? 0;
        }

        /// <summary>
        /// Gets the order taxes history.
        /// </summary>
        /// <param name="taxesTotalViewModel">The taxes total view model.</param>
        /// <returns>Taxes History</returns>
        private List<HOrdersTaxes> GetOrderTaxesHistory(List<TaxesTotalViewModel> taxesTotalViewModel)
        {
            var hOrdersTaxesApi = new HOrdersTaxesApi(this._httpClientInstance);
            return taxesTotalViewModel.ConvertAll(x => new HOrdersTaxes()
            {
                TaxName = x.TaxName,
                TaxPercent = x.TaxPercent,
                TaxTotal = x.TaxTotal
            });
        }

        /// <summary>
        /// Gets the shopping cart records.
        /// </summary>
        /// <param name="checkoutData">The checkout data.</param>
        /// <returns>
        /// Shopping Cart Records
        /// </returns>
        private async Task<List<ShoppingCartRecords>> GetShoppingCartRecords(CheckoutDataViewModel checkoutData)
        {
            var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
            if (User.Identity.IsAuthenticated)
            {
                var user = await this._userManager.GetUserAsync(User);
                return await shoppingCartApi.GetRecordsForCustomer((long)user.CustomersId);
            }
            else
            {
                var shoppingCartGuestApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                var guestId = this.GetGuestId();
                var shoppingCartCheckoutViewModel = await shoppingCartGuestApi.GetShoppingCartCheckout(guestId);
                var shoppingCartRecordsGuest = await shoppingCartGuestApi.GetRecordsForCustomer(guestId);
                var customer = await this.CheckCustomer(checkoutData);
                return shoppingCartRecordsGuest.ConvertAll(x => new ShoppingCartRecords()
                {
                    FkInventoryItemsId = x.FkInventoryItemsId ?? 0,
                    FkProductsId = x.FkProductsId,
                    Quantity = x.Quantity,
                    FkCustomersId = customer.CustomersId
                });
            }
        }

        /// <summary>
        /// Gets the taxes for shopping cart records.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of Taxes Total</returns>
        private async Task<CheckoutCostsDataViewModel> GetCostsDataForShoppingCart(long customersId)
        {
            var shoppingCartItemsToCheckout = new List<ShoppingCartCheckoutViewModel>();
            var totalTaxesGeneral = new List<TaxesTotalViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                var shoppingCartApi = new ShoppingCartRecordsApi(this._httpClientInstance);
                shoppingCartItemsToCheckout = await shoppingCartApi.GetShoppingCartCheckout(customersId);
                totalTaxesGeneral = await shoppingCartApi.GetTaxesInfoForShoppingCart(customersId);
            }
            else
            {
                var guestId = this.GetGuestId();
                var shoppingCartGuestApi = new ShoppingCartRecordsGuestApi(this._httpClientInstance);
                shoppingCartItemsToCheckout = await shoppingCartGuestApi.GetShoppingCartCheckout(guestId);
                totalTaxesGeneral = await shoppingCartGuestApi.GetTaxesInfoForShoppingCart(guestId);
            }

            foreach (var item in shoppingCartItemsToCheckout)
            {
                foreach (var tax in item.Taxes)
                {
                    totalTaxesGeneral.Find(x => x.TaxName == tax.TaxName).TaxTotal += tax.TaxValue;
                }
            }

            return new CheckoutCostsDataViewModel()
            {
                ShippingCost = this._shippingCost,
                SubTotal = shoppingCartItemsToCheckout.Sum(x => x.SubTotal),
                Total = shoppingCartItemsToCheckout.Sum(x => x.Total),
                TotalTaxes = totalTaxesGeneral.Sum(x => x.TaxTotal),
                TotalTaxesDetailed = totalTaxesGeneral,
                ShoppingCartCheckoutViewModel = shoppingCartItemsToCheckout
            };
        }

        /// <summary>
        /// Validates the stock.
        /// </summary>
        /// <returns>Validate Stock</returns>
        public async Task<IActionResult> ValidateStock()
        {
            var stockControl = await this.VerifyStock();            
            return this.Content(stockControl);
        }
        #endregion
    }
}