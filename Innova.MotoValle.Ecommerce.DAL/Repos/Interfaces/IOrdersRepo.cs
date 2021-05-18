// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrdersRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Orders Repo facade
    /// </summary>
    public interface IOrdersRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of orders</returns>
        List<Orders> GetAllRecords();

        /// <summary>
        /// Gets the records for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of orders</returns>
        List<Orders> GetRecordsForCustomer(long customersId);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order by Id</returns>
        Orders GetRecord(long id);

        /// <summary>
        /// Gets the record for order number.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>Order for number</returns>
        public Orders GetRecordForOrderNumber(long orderNumber);

        /// <summary>
        /// Gets the record for transaction identifier.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// Order for Transaction id
        /// </returns>
        public Orders GetRecordForTransactionId(string transactionId);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Order created</returns>
        Orders CreateRecord(Orders item);

        /// <summary>
        /// Gets the record for identifier and customer.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Order for Id and customer</returns>
        Orders GetRecordForIdAndCustomer(long ordersId, long customersId);

        /// <summary>
        /// Gets the order with details and product information.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>
        /// Order details with product info
        /// </returns>
        OrderWithDetailsAndProductInfo GetOrderWithDetailsAndProductInfo(long ordersId, long customersId);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, Orders updatedItem);
        
        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
