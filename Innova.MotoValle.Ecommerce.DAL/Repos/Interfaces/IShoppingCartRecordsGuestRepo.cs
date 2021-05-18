// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IShoppingCartRecordsGuestRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using Ecommerce.Models.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Shopping Cart Records Repo facade
    /// </summary>
    public interface IShoppingCartRecordsGuestRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of shopping cart records</returns>
        List<ShoppingCartRecordsGuest> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Shopping cart record by id</returns>
        ShoppingCartRecordsGuest GetRecord(long id);

        /// <summary>
        /// Gets the records for custumer.
        /// </summary>
        /// <param name="guestId">The guest identifier.</param>
        /// <returns>
        /// List of shopping cart records for customer id
        /// </returns>
        List<ShoppingCartRecordsGuest> GetRecordsForCustumer(string guestId);

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="guestId">The guest identifier.</param>
        /// <returns>
        /// List of shopping cart checkout
        /// </returns>
        List<ShoppingCartCheckoutViewModel> GetShoppingCartCheckout(string guestId);

        /// <summary>
        /// Gets the taxes information for shopping cart.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>Names of Taxes</returns>
        List<TaxesTotalViewModel> GetTaxesInfoForShoppingCart(string guestId);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Shopping Cart Records created</returns>
        ShoppingCartRecordsGuest CreateRecord(ShoppingCartRecordsGuest item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, ShoppingCartRecordsGuest updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);

        /// <summary>
        /// Deletes the records for customer.
        /// </summary>
        /// <param name="guestId">The guest identifier.</param>
        void DeleteRecordsForCustomer(string guestId);
    }
}
