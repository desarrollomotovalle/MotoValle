// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using global::Ecommerce.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.ApiRequest;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Models.Entities.Identity;
    using motovalle.Ecommerce.Resources;

    /// <summary>
    /// Orders Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class OrdersController : Controller
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The invoice manual template URI
        /// </summary>
        private readonly string _invoiceManualTemplateUri;

        /// <summary>
        /// The email to
        /// </summary>
        private readonly string _notifationCustomerTemplate;

        /// <summary>
        /// The email to
        /// </summary>
        private readonly string _emailTo;

        /// <summary>
        /// The email to cc
        /// </summary>
        private readonly string _emailToCc;

        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// The host
        /// </summary>
        private readonly IWebHostEnvironment _host;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSenderExtended _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="host">The host.</param>
        public OrdersController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IWebHostEnvironment host, IEmailSenderExtended emailSender)
        {
            this._host = host;
            this._userManager = userManager;
            this._httpClientInstance = new HttpClientHelper(configuration).GetHttpClient();
            this._invoiceManualTemplateUri = configuration.GetSection("ManualInvoiceSettings").GetValue<string>("ManualInvoiceTemplate");
            this._emailTo = configuration.GetSection("ManualInvoiceSettings").GetValue<string>("ManualInvoiceEmailTo");
            this._emailToCc = configuration.GetSection("ManualInvoiceSettings").GetValue<string>("ManualInvoiceEmailToCc");
            this._notifationCustomerTemplate = configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateIdentity");
            this._emailSender = emailSender;
        }

        /// <summary>
        /// Orders the details.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>
        /// Order details view
        /// </returns>
        [HttpGet("[controller]/[action]/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(long orderId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["FromAppPayment"] = "true";
                TempData["OrdersId"] = orderId;
                return this.RedirectToAction(nameof(PaymentDetails), new { ordersId = orderId });
            }

            var user = await this._userManager.GetUserAsync(User);
            var orderDetails = new OrderWithDetailsAndProductInfo();
            ViewBag.Email = user.Email;
            try
            {
                var orderApi = new OrdersApi(this._httpClientInstance);
                var order = await orderApi.GetRecord(orderId);
                if (order.FkCustomersId != (user.CustomersId ?? 0))
                {
                    return this.View("NotFound");
                }

                var orderDetailsApi = new OrdersApi(this._httpClientInstance);
                orderDetails = await orderDetailsApi.GetOrderWithDetailsWithProductInfo(orderId, user.CustomersId ?? 0);
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(orderDetails);
        }

        /// <summary>
        /// Payments the details.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>Order details view</returns>
        [AllowAnonymous]
        public async Task<IActionResult> PaymentDetails(int ordersId)
        {
            var customerApi = new CustomerApi(this._httpClientInstance);
            var orderApi = new OrdersApi(this._httpClientInstance);
            var orderDetails = new OrderWithDetailsAndProductInfo();
            try
            {
                var order = await orderApi.GetRecord(ordersId);
                var customer = await customerApi.GetRecord(order.FkCustomersId);
                orderDetails = await orderApi.GetOrderWithDetailsWithProductInfo(ordersId, customer.CustomersId);
                ViewBag.Email = customer.EmailAddress;
            }
            catch (Exception)
            {
                ViewBag.Message = StaticResources.NotAvailable;
            }

            return this.View(nameof(Details), orderDetails);
        }

        /// <summary>
        /// Gets the manual invoice PDF.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="sendEmails">if set to <c>true</c> [send emails].</param>
        /// <param name="isPaymentDetails">The is payment details.</param>
        /// <returns></returns>
        [HttpGet("[controller]/[action]/{orderId}/Emails/{sendEmails}/{isPaymentDetails?}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetManualInvoicePDF(long orderId, bool sendEmails, bool? isPaymentDetails = false)
        {
            try
            {
                var orderApi = new OrdersApi(this._httpClientInstance);
                var order = await orderApi.GetRecord(orderId);
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                var customersApi = new CustomerApi(this._httpClientInstance);
                var orderDetails = await orderDetailsApi.GetRecordsForOrderAndCustomer(orderId, order.FkCustomersId);
                if (orderDetails.Count <= 0)
                {
                    return this.View("NotFound");
                }

                var customer = await customersApi.GetRecord(order.FkCustomersId);
                var orderDetailsWithProductInfo = await orderApi.GetOrderWithDetailsWithProductInfo(orderId, customer.CustomersId);
                var date = DateTime.Now;
                var pdfBuilder = new PdfContentBuilder();
                var fileName = $"MotovalleEcommerceOrder{order.OrderNumber}_{order.IdentificationNumber}_{date:ddMMyyyyHHmmss}.pdf";
                var logoUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
                var manualInvoicePath = $"{this._host.WebRootPath}{this._invoiceManualTemplateUri}";
                var contentTemplate = new PdfContentBuilder().GetManualInvoiceContent(manualInvoicePath, customer.EmailAddress, customer.PhoneNumber, customer.FullName, logoUrl, orderDetailsWithProductInfo, date);
                var savedPath = pdfBuilder.CreatePDF(this._host.WebRootPath, fileName, contentTemplate);

                if (sendEmails)
                {
                    var callbackUrl = string.Empty;
                    if (isPaymentDetails ?? false)
                    {
                        callbackUrl = HtmlEncoder.Default.Encode(Url.Action($"PaymentDetails?ordersId={order.OrdersId}", "Orders", values: null, protocol: Request.Scheme)).Replace("%2F", "/").Replace("%3F", "?").Replace("%3D", "=");
                    }
                    else
                    {
                        callbackUrl = HtmlEncoder.Default.Encode(Url.Action($"Details/{order.OrdersId}", "Orders", values: null, protocol: Request.Scheme)).Replace("%2F", "/");
                    }
                    
                    var attachements = new List<string>
                    {
                        savedPath
                    };

                    await this.Sendemails(customer.FullName, customer.EmailAddress, contentTemplate, attachements, callbackUrl);
                }

                return PhysicalFile(savedPath, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return this.Ok(new { Status = "Exception", Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the manual invoice PDF.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns></returns>
        [HttpGet("[controller]/[action]/{orderId}/Customer/{customersId}")]
        public async Task<IActionResult> NotificationForUpdateOrderStatus(long orderId, long customersId)
        {
            try
            {
                var customersApi = new CustomerApi(this._httpClientInstance);
                var orderDetailsApi = new OrderDetailsApi(this._httpClientInstance);
                var customer = await customersApi.GetRecord(customersId);
                var orderDetails = await orderDetailsApi.GetRecordsForOrderAndCustomer(orderId, customersId);
                if (orderDetails.Count <= 0)
                {
                    return this.View("NotFound");
                }

                var orderApi = new OrdersApi(this._httpClientInstance);
                var orderDetailsWithProductInfo = await orderApi.GetOrderWithDetailsWithProductInfo(orderId, customersId);
                var order = await orderApi.GetRecord(orderId);
                var date = DateTime.Now;
                var pdfBuilder = new PdfContentBuilder();
                var fileName = $"OrderDetailMotovalleEcommerce_{date:ddMMyyyyHHmmss}.pdf";
                var logoUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
                var manualInvoicePath = $"{this._host.WebRootPath}{this._invoiceManualTemplateUri}";
                var contentTemplate = new PdfContentBuilder().GetManualInvoiceContent(manualInvoicePath, customer.EmailAddress, customer.PhoneNumber, customer.FullName, logoUrl, orderDetailsWithProductInfo, date);
                var savedPath = pdfBuilder.CreatePDF(this._host.WebRootPath, fileName, contentTemplate);
                var callbackUrl = HtmlEncoder.Default.Encode(Url.Action($"Details/{order.OrdersId}", "Orders", values: null, protocol: Request.Scheme)).Replace("%2F", "/");
                var attachements = new List<string>
                {
                    savedPath
                };

                await this.Sendemails(customer.FullName, customer.EmailAddress, contentTemplate, attachements, callbackUrl, $"Actualización de estado de la Orden #{order.OrderNumber}", $"Se ha actualizado el estado de tu orden (Order #{order.OrderNumber})");
                System.IO.File.Delete(savedPath);
                return this.Ok();
            }
            catch (Exception)
            {
                return this.Ok(new { Status = "Exception" });
            }
        }

        /// <summary>
        /// Sendemailses the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="contentTemplate">The content template.</param>
        /// <param name="attachements">The attachements.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        private async Task Sendemails(string userName, string userEmail, string contentTemplate, List<string> attachements, string callbackUrl, string internalSubject = null, string userSubject = null)
        {
            var emailTemplateUrl = $"{this._host.WebRootPath}{this._notifationCustomerTemplate}";
            var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var emailTemplate = new StreamReader(emailTemplateUrl).ReadToEnd()
                .Replace("${logoUrl}", imgUrl)
                .Replace("${user}", $"{userName}")
                .Replace("${callbackUrl}", $"{callbackUrl}")
                .Replace("${buttonCaption}", "Ver detalle")
                .Replace("${firstLabel}", @"Acabas de realizar una compra por medio de nuestro <b>Ecommerce</b>. Adjunto está tu orden.<br/><br/> 
                        Te informaremos cuando suceda alguna actualización.<br/><br/>
                        <div style=""text-align:center""><b>¡Muchas gracias por confiar en nosotros!.<b><div/>")
                .Replace("${Date}", $"{DateTime.Now.Year}");

            await Task.WhenAll(
                    this._emailSender.SendEmailAsync(this._emailTo, internalSubject ?? "Nueva orden registrada", contentTemplate, attachements, new List<string>() { this._emailToCc }),
                    this._emailSender.SendEmailAsync(userEmail, userSubject ?? "Gracias por confiar en nosotros", emailTemplate, attachements));
        }
    }
}