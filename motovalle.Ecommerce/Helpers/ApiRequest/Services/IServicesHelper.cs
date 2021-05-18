// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServicesHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Services Helper Facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest.Services
{
    using motovalle.Ecommerce.Models.DTO;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank;
    using System.Threading.Tasks;

    /// <summary>
    /// Services Helper Facade
    /// </summary>
    public interface IServicesHelper
    {
        /// <summary>
        /// Santanders the bank authentication.
        /// </summary>
        /// <returns>
        /// Santander Bank Auth Response
        /// </returns>
        public Task<GenericResponse<SantanderBankAuthResponse>> SantanderBankAuth();

        /// <summary>
        /// Santanders the funding request.
        /// </summary>
        /// <param name="santanderBankFundingRequest">The santander bank funding request.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Santander Bank Funding Request Response
        /// </returns>
        public Task<GenericResponse<SantanderBankFundingResponse>> SantanderFundingRequest(SantanderBankFundingRequest santanderBankFundingRequest, string token);

        /// <summary>
        /// Converts to fundingrequestgeneralresponse.
        /// </summary>
        /// <param name="santanderBankFundingResponse">The santander bank funding response.</param>
        /// <param name="reportId">The report identifier.</param>
        /// <returns>
        /// Funding Request General Response
        /// </returns>
        public FundingRequestGeneralResponse<SantanderBankFundingResponse> ToFundingRequestGeneralResponse(GenericResponse<SantanderBankFundingResponse> santanderBankFundingResponse, string reportId);
    }
}
