// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiPaymentGatewayService.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Wompi Payment Gateway Service
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.Services.WompiPaymentGateway
{
    using Microsoft.Extensions.Configuration;
    using motovalle.Ecommerce.Helpers;
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events;
    using motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Wompi Payment Gateway Service
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Helpers.WompiPaymentGateway.IWompiPaymentGatewayService" />
    public class WompiPaymentGatewayService : IWompiPaymentGatewayService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The public key
        /// </summary>
        private readonly string _publicKey;

        /// <summary>
        /// The secret key
        /// </summary>
        private readonly string _secretKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="WompiHelper" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="configuration">The configuration.</param>
        public WompiPaymentGatewayService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            this._httpClient = httpClient.CreateClient("Wompi");
            this._publicKey = configuration.GetSection("WompiSettings").GetValue<string>("PublicKey");
            this._secretKey = configuration.GetSection("WompiSettings").GetValue<string>("TechnicalKey");
        }

        /// <summary>
        /// Gets the presigned acceptance.
        /// </summary>
        /// <returns>
        /// Wompi Merchant Response
        /// </returns>
        public async Task<GenericResponse<WompiMerchantResponseDTO>> GetPresignedAcceptance()
        {
            var uriEndpoint = $"merchants/{this._publicKey}";
            try
            {
                var responseString = await this._httpClient.GetStringAsync(uriEndpoint);
                var response = JsonConvert.DeserializeObject<WompiMerchantResponseDTO>(responseString);
                return new GenericResponse<WompiMerchantResponseDTO>()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<WompiMerchantResponseDTO>()
                {
                    IsSuccess = true,
                    Message = ex.Message,
                    Result = null
                };
            }

        }

        /// <summary>
        /// Gets the transaction information.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// Wompi Transaction Payment Gateway Response
        /// </returns>
        public async Task<GenericResponse<WompiTransactionPaymentGatewayResponseDTO>> GetTransactionInfo(string transactionId)
        {
            var uriEndpoint = $"transactions/{transactionId}";
            try
            {
                var responseString = await this._httpClient.GetStringAsync(uriEndpoint);
                var response = JsonConvert.DeserializeObject<WompiTransactionPaymentGatewayResponseDTO>(responseString);
                return new GenericResponse<WompiTransactionPaymentGatewayResponseDTO>()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Result = response
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<WompiTransactionPaymentGatewayResponseDTO>()
                {
                    IsSuccess = true,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <returns>Hash</returns>
        public string GetHash(WompiEventDTO wompiEvent)
        {
            var localHash = new StringBuilder();
            foreach (var propName in wompiEvent.Signature.Properties)
            {
                localHash.Append(wompiEvent.Data.GetType().GetMember(propName).GetValue(0));
            }

            localHash.Append(wompiEvent.Timestamp);
            localHash.Append(this._secretKey);
            return CommonHelper.ComputeSha256Hash(localHash.ToString());
        }

        /// <summary>
        /// Determines whether [is valid hash] [the specified wompi event].
        /// </summary>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <returns>
        ///   <c>true</c> if [is valid hash] [the specified wompi event]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidHash(WompiEventDTO wompiEvent)
        {
            var hash = this.GetHash(wompiEvent);
            return hash == wompiEvent.Signature.Checksum;
        }
    }
}
