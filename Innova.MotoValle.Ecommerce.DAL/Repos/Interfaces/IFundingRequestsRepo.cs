// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFundingRequestsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Funding Requests Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Funding Requests Repo facade
    /// </summary>
    public interface IFundingRequestsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Funding requests</returns>
        List<FundingRequests> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Funding request for id</returns>
        FundingRequests GetRecord(long id);

        /// <summary>
        /// Gets the rercord for order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>funding request for order</returns>
        FundingRequests GetRercordForOrder(long orderId);

        /// <summary>
        /// Gets the rercord for order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>
        /// Fundinf request for order number
        /// </returns>
        FundingRequests GetRercordForOrderNumber(long orderNumber);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Funding Request created</returns>
        FundingRequests CreateRecord(FundingRequests item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, FundingRequests updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
