// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutFundingRequestWithSantaderBankResponseViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Funding Request With Santader Bank Response View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout.FundingRequest
{
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests;
    using motovalle.Ecommerce.Models.DTO.Services.FundingRequests.SantanderBank;

    /// <summary>
    /// Checkout Funding Request With Santader Bank Response View Model
    /// </summary>
    public class CheckoutFundingRequestWithSantaderBankResponseViewModel
    {
        /// <summary>
        /// Gets or sets the checkout funding request santader bank.
        /// </summary>
        /// <value>
        /// The checkout funding request santader bank.
        /// </value>
        public CheckoutFundingRequestSantaderBankViewModel CheckoutFundingRequestSantaderBank { get; set; }

        /// <summary>
        /// Gets or sets the funding request general response santader bank.
        /// </summary>
        /// <value>
        /// The funding request general response santader bank.
        /// </value>
        public FundingRequestGeneralResponse<SantanderBankFundingResponse> FundingRequestGeneralResponseSantaderBank { get; set; }
    }
}
