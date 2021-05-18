// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FundingRequestGeneralResponse.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Funding Request General Response
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.DTO.Services.FundingRequests
{
    using System.Collections.Generic;

    /// <summary>
    /// Funding Request General Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FundingRequestGeneralResponse<T>
    {
        /// <summary>
        /// Gets or sets the control status.
        /// </summary>
        /// <value>
        /// The control status.
        /// </value>
        public string ControlStatus { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public T Response { get; set; }
    }
}
