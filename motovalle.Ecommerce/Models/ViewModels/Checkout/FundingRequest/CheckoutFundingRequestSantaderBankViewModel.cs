// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutFundingRequestSantaderBankViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Funding Request Santader Bank View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout.FundingRequest
{
    /// <summary>
    /// Checkout Funding Request Santader Bank View Model
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Models.ViewModels.BaseFormViewModel" />
    public class CheckoutFundingRequestSantaderBankViewModel : BaseFormViewModel
    {
        /// <summary>
        /// Gets or sets the checkout data.
        /// </summary>
        /// <value>
        /// The checkout data.
        /// </value>
        public CheckoutDataViewModel CheckoutData { get; set; }

        /// <summary>
        /// Gets or sets the santander bank credit online.
        /// </summary>
        /// <value>
        /// The santander bank credit online.
        /// </value>
        public SantanderBankCreditOnlineViewModel SantanderBankCreditOnline { get; set; }
    }
}
