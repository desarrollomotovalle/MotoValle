// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColorsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Colors Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Colors Repo facade
    /// </summary>
    public interface IColorsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of customers</returns>
        List<Colors> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Colors by id</returns>
        Colors GetRecord(long id);

        /// <summary>
        /// Getfors the product.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>Colors by Product</returns>
        List<Colors> GetforProduct(long productsId);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Colors created</returns>
        Colors CreateRecord(Colors item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, Colors updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}