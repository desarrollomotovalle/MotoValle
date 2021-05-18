// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebhooksController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Webhooks mercado pago notification Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using MercadoPago;
    using MercadoPago.Resources;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Webhooks mercado pago notification Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class WebhooksController : Controller
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        private readonly string _accessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhooksController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public WebhooksController(IConfiguration configuration)
        {
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._accessToken = configuration.GetSection("MercadoPagoSettings").GetValue<string>("AccessToken");
        }

        /// <summary>
        /// Indexes the specified topic.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>IPN notification object</returns>
        [HttpPost]
        public async Task<IActionResult> Index(string topic, string id)
        {
            try
            {
                switch (topic)
                {
                    case "Payment":
                        var ordersApi = new OrdersApi(this._httpClientInstance);
                        var order = await ordersApi.GetRecordForTransaction(long.Parse(id));
                        var payment = Payment.FindById(long.Parse(id), accessToken: this._accessToken);
                        order.Status = payment.Status.ToString();
                        await ordersApi.UpdateRecord(order.OrdersId, order);
                        return this.Json(new { ControlMessage = "Payment Received", Payment = payment });
                    case "MerchantOrder":
                        var merchantOrder = MerchantOrder.FindById(id);
                        return this.Json(new { ControlMessage = "Payment Received", MerchantOrder = merchantOrder });
                    default:
                        return this.Json(new { ControlMessage = $"IPN Notification with topic '{topic}' cannot be handled." });
                }
            }
            catch (MPException ex) when (ex.Error != null)
            {
                if (ex.Error.Cause[0].Code == "2000")
                {
                    return this.NotFound(ex.Error.Cause[0].Description);
                }

                return this.BadRequest();
            }
        }

        /// <summary>
        /// Finds the payment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("[controller]/[action]/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> FindPayment(long id)
        {
            try
            {
                var ordersApi = new OrdersApi(this._httpClientInstance);
                var order = await ordersApi.GetRecordForTransaction(id);
                var payment = Payment.FindById(id, accessToken: this._accessToken);
                return this.Ok(new { PaymentEstatus = payment.Status, PaymentEstatusDetail = payment.StatusDetail, OrderStatus = order.Status, OrderStatusDetail = order.Notes });
            }
            catch (MPException ex) when (ex.Error != null)
            {
                if (ex.Error.Cause[0].Code == "2000")
                {
                    return this.NotFound(ex.Error.Cause[0].Description);
                }

                return this.BadRequest();
            }
        }
    }
}