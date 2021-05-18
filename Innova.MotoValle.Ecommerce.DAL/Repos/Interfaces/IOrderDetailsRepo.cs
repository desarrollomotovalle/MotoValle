// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderDetailsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order Details Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Order Details Repo facade
    /// </summary>
    public interface IOrderDetailsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of order details</returns>
        List<OrderDetails> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order details by Id</returns>
        OrderDetails GetRecord(long id);

        /// <summary>
        /// Gets the record for identifier and customer.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of Order details for id and customer</returns>
        List<OrderDetails> GetRecordForIdAndCustomer(long ordersId, long customersId);

        /// <summary>
        /// Gets the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>List of order details for order</returns>
        List<OrderDetails> GetRecordsForOrder(long ordersId);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Order details created</returns>
        OrderDetails CreateRecord(OrderDetails item);

        /// <summary>
        /// Creates the records.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>List of records created</returns>
        List<OrderDetails> CreateRecords(List<OrderDetails> items);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, OrderDetails updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);

        /// <summary>
        /// Deletes the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        void DeleteRecordsForOrder(long ordersId);
    }
}
