// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnlinePaymentsController.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Online Payments Controller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Helpers.EmailSender;
    using motovalle.Ecommerce.Helpers.Services.WompiPaymentGateway;
    using motovalle.Ecommerce.Models;
    using motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events;
    using motovalle.Ecommerce.Models.ViewModels.Wompi;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Online Payments Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("Pagos-en-linea")]
    public class OnlinePaymentsController : Controller
    {
        /// <summary>
        /// The wompi public key
        /// </summary>
        private readonly string _wompiPublicKey;

        /// <summary>
        /// The redirect URL
        /// </summary>
        private readonly string _wompiRedirectURL;

        /// <summary>
        /// The wompi payment gateway service
        /// </summary>
        private readonly IWompiPaymentGatewayService _wompiPaymentGatewayService;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSenderExtended _emailSender;

        /// <summary>
        /// The email cc to
        /// </summary>
        private readonly string _emailCcTo;

        /// <summary>
        /// The email template URL
        /// </summary>
        private readonly string _emailTemplateUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlinePaymentsController" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="wompiPaymentGatewayService">The wompi payment gateway service.</param>
        /// <param name="emailSender">The email sender.</param>
        public OnlinePaymentsController(IConfiguration configuration, IHttpContextAccessor httpContext, IWompiPaymentGatewayService wompiPaymentGatewayService, IEmailSenderExtended emailSender, IWebHostEnvironment host)
        {
            this._wompiPublicKey = configuration.GetSection("WompiSettings").GetValue<string>("PublicKey");
            this._wompiRedirectURL = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}/Pagos-en-linea/wp/resultado";
            this._wompiPaymentGatewayService = wompiPaymentGatewayService;
            this._emailSender = emailSender;
            this._emailCcTo = configuration.GetSection("EmailSettings").GetValue<string>("EmailToCcDefault");
            this._emailTemplateUrl = $"{host.WebRootPath}{configuration.GetSection("EmailSettings").GetValue<string>("EmailTemplateGeneric")}";
        }

        /// <summary>
        /// Wompis this instance.
        /// </summary>
        /// <returns>Wompi View</returns>
        [HttpGet("[action]")]
        public IActionResult Wompi()
        {
            var wompiViewModel = new WompiInitialInfoViewModel(this._wompiPublicKey, this._wompiRedirectURL);
            return PartialView(wompiViewModel);
        }

        /// <summary>
        /// Wompis the confirm information.
        /// </summary>
        /// <param name="wompiInitialInfoViewModel">The wompi initial information view model.</param>
        /// <returns>Wompi Comfirm Info View</returns>
        [HttpPost("wp/confirmar-informacion")]
        public IActionResult WompiConfirmInfo(WompiInitialInfoViewModel wompiInitialInfoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(nameof(Wompi), wompiInitialInfoViewModel);
            }
            
            
            var charactersNumbers = "1234567890";
            var numbers = new char[6];
            var random = new Random();

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = charactersNumbers[random.Next(charactersNumbers.Length)];
            }   
            wompiInitialInfoViewModel.Reference += "-" + new String(numbers);

            return this.View(wompiInitialInfoViewModel);
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="wompiEnviroment">The wompi enviroment.</param>
        /// <returns>
        /// Check View
        /// </returns>
        [AllowAnonymous]
        [HttpGet("wp/resultado")]
        public async Task<IActionResult> WompiResult(string id, WompiEnviroment wompiEnviroment)
        {
            var transactionRequestResult = await this._wompiPaymentGatewayService.GetTransactionInfo(id);
            if (!transactionRequestResult.IsSuccess)
            {
                ViewBag.Error = "true";
            }

            var transactionInfo = transactionRequestResult.Result;
            return this.View(transactionInfo);
        }

        /// <summary>
        /// Wompis the events.
        /// </summary>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <returns>
        /// Event Result
        /// </returns>
        /// <exception cref="Exception">Status no handled
        /// or
        /// Status no handled
        /// or
        /// Event no handled</exception>
        [AllowAnonymous]
        [HttpPost("wp/eventos")]
        public async Task<IActionResult> WompiEvents(WompiEventDTO wompiEvent)
        {
            CommonHelper.SaveWompiEventNotification(Guid.NewGuid().ToString(), wompiEvent, this._wompiPaymentGatewayService.GetHash(wompiEvent));
            if (!this._wompiPaymentGatewayService.IsValidHash(wompiEvent))
            {
                return this.BadRequest("Hash no válido, por favor verique");
            }

            var newStatusInfoRequest = await this._wompiPaymentGatewayService.GetTransactionInfo(wompiEvent.Data.Transaction.Id);
            switch (wompiEvent.Event.ToLower())
            {
                case "transaction.updated":
                    ////Send push notification or mail notification
                    switch (newStatusInfoRequest.Result.Data.Status)
                    {
                        case WompiStatus.PENDING:
                        case WompiStatus.APPROVED:
                        case WompiStatus.DECLINED:
                        case WompiStatus.ERROR:
                        case WompiStatus.VOIDED:
                            ////Send email to customer and Admin
                            try
                            {
                                await Task.WhenAll(
                                        this._emailSender.SendEmailAsync(wompiEvent.Data.Transaction.CustomerEmail, "Información de transacción", this.EmailBodyBuilder(wompiEvent, "customer")),
                                        this._emailSender.SendEmailAsync(this._emailCcTo, "Notificación de pago (Wompi)", this.EmailBodyBuilder(wompiEvent, "admin"))
                                );
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"Error sending e-mails. See => {ex.Message}");
                            }
                            
                            break;
                        default:
                            throw new Exception("Status no handled");
                    }
                    break;
                case "nequi_token.updated":
                    switch (newStatusInfoRequest.Result.Data.Status)
                    {
                        case WompiStatus.APPROVED:
                        case WompiStatus.DECLINED:
                            await Task.WhenAll(
                                this._emailSender.SendEmailAsync(wompiEvent.Data.Transaction.CustomerEmail, "Información de transacción", this.EmailBodyBuilder(wompiEvent, "customer")),
                                this._emailSender.SendEmailAsync(this._emailCcTo, "Notificación de pago (Wompi)", this.EmailBodyBuilder(wompiEvent, "admin"))
                             );
                            break;
                        default:
                            throw new Exception("Status no handled");
                    }
                    break;
                default:
                    throw new Exception("Event no handled");
            }

            return this.Ok();
        }

        /// <summary>
        /// Emails the body builder.
        /// </summary>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <returns>Email content</returns>
        private string EmailBodyBuilder(WompiEventDTO wompiEvent, string to)
        {
            var imgUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/media/brands/motovalle-logo.png";
            var date = DateTime.Now;
            var content = @$"<div>
                                 <b>Fecha de registro:</b> {wompiEvent.SentAtLocal:dddd, dd MMMM yyyy}
                             </div>
                             <div>
                                 <b>Identicador de la transacción:</b> {wompiEvent.Data.Transaction.Id}
                             </div>
                            <div>
                                 <b>Forma de pago:</b> {wompiEvent.Data.Transaction.PaymentMethodType}
                             </div>
                             <div>
                                 <b>Estado:</b> {wompiEvent.Data.Transaction.Status}
                             </div>
                             <div>
                                 <b>Referencia de pago:</b> {wompiEvent.Data.Transaction.Reference}
                             </div>
                             <div>
                                 <b>Tipo de modena:</b> {wompiEvent.Data.Transaction.Currency}
                             </div>
                             <div>
                                 <b>Total:</b> {wompiEvent.Data.Transaction.AmountInPesos:C}
                             </div>";

            if (to.Contains("admin", StringComparison.OrdinalIgnoreCase))
            {
                content += @$"<div>
                                 <b>Correo Cliente:</b> {wompiEvent.Data.Transaction.CustomerEmail}
                             </div>";
            }

            return new StreamReader(this._emailTemplateUrl).ReadToEnd()
               .Replace("${logoUrl}", imgUrl)
               .Replace("${firstLabel}", "Aquí tienes la información de tu transacción")
               .Replace("${Date}", $"{date.Year}")
               .Replace("${content}", $"{content}");
        }
    }
}
