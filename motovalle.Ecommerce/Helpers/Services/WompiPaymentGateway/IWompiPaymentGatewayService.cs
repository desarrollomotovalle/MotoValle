// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWompiPaymentGatewayService.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Wompi Payment Gateway Service Facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.Services.WompiPaymentGateway
{
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Events;
    using motovalle.Ecommerce.Models.DTO.Services.WompiPaymentGateway.Responses;
    using System.Threading.Tasks;

    /// <summary>
    /// Wompi Payment Gateway Service Facade
    /// </summary>
    public interface IWompiPaymentGatewayService
    {
        /// <summary>
        /// Gets the presigned acceptance.
        /// </summary>
        /// <returns>Wompi Merchant Response</returns>
        Task<GenericResponse<WompiMerchantResponseDTO>> GetPresignedAcceptance();

        /// <summary>
        /// Gets the transaction information.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Wompi Transaction Payment Gateway Response</returns>
        Task<GenericResponse<WompiTransactionPaymentGatewayResponseDTO>> GetTransactionInfo(string transactionId);

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <returns>Hash</returns>
        string GetHash(WompiEventDTO wompiEvent);

        /// <summary>
        /// Determines whether [is valid hash] [the specified wompi event].
        /// </summary>
        /// <param name="wompiEvent">The wompi event.</param>
        /// <returns>
        ///   <c>true</c> if [is valid hash] [the specified wompi event]; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidHash(WompiEventDTO wompiEvent);
    }
}