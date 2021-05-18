// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHOrdersTaxesRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// H Orders Taxes Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// H Orders Taxes Repo facade
    /// </summary>
    public interface IHOrdersTaxesRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of H orders taxes</returns>
        List<HOrdersTaxes> GetAllRecords();

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of H orders taxes</returns>
        List<HOrdersTaxes> GetForOrder(long ordersId);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>H orders taxes by id</returns>
        HOrdersTaxes GetRecord(long id);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Body Styles created</returns>
        HOrdersTaxes CreateRecord(HOrdersTaxes item);

        /// <summary>
        /// Creates the records.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        List<HOrdersTaxes> CreateRecords(List<HOrdersTaxes> items);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, HOrdersTaxes updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
