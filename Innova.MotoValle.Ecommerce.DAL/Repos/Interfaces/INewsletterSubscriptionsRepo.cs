// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewsletterSubscriptionsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Newsletter Subscriptions Repo facade
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos.Interfaces
{
    using Ecommerce.Models.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Newsletter Subscriptions Repo facade
    /// </summary>
    public interface INewsletterSubscriptionsRepo
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        List<NewsLetterSubscriptions> GetAllRecords();

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        NewsLetterSubscriptions GetRecord(long id);

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>NewsLetter Subscriptions pictures by email</returns>
        NewsLetterSubscriptions GetRecord(string email);

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>NewsLetter Subscriptions created</returns>
        NewsLetterSubscriptions CreateRecord(NewsLetterSubscriptions item);

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        void UpdateRecord(long id, NewsLetterSubscriptions updatedItem);

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteRecord(long id);
    }
}
