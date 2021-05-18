// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInterestedCustomersRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Interested Customers Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Interested Customers Repo facade
    /// </summary>
    public interface IInterestedCustomersRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Interested customers</returns>
        List<InterestedCustomers> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Interested customers by id</returns>
        InterestedCustomers GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Interested customer created</returns>
        InterestedCustomers CreateRecord(InterestedCustomers item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, InterestedCustomers updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}