// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsletterSubscriptionsRepo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Newsletter Subscriptions Repo implementations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ecommerce.DAL.Repos
{
    using Ecommerce.DAL.EF;
    using Ecommerce.DAL.Repos.Interfaces;
    using Ecommerce.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Newsletter Subscriptions Repo implementations
    /// </summary>
    /// <seealso cref="Ecommerce.DAL.Repos.Interfaces.INewsletterSubscriptionsRepo" />
    public class NewsletterSubscriptionsRepo : INewsletterSubscriptionsRepo
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ecommerceContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsletterSubscriptionsRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public NewsletterSubscriptionsRepo(ecommerceContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// NewsLetter Subscriptions created
        /// </returns>
        public NewsLetterSubscriptions CreateRecord(NewsLetterSubscriptions item)
        {
            this._db.NewsLetterSubscriptions.Add(item);
            this._db.SaveChanges();
            return item;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteRecord(long id)
        {
            var item = this.GetRecord(id);
            if (item != null)
            {
                this._db.NewsLetterSubscriptions.Remove(item);
                this._db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// List of NewsLetter Subscriptions
        /// </returns>
        public List<NewsLetterSubscriptions> GetAllRecords()
        {
            return this._db.NewsLetterSubscriptions
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// NewsLetter Subscriptions pictures by id
        /// </returns>
        public NewsLetterSubscriptions GetRecord(long id)
        {
            return this._db.NewsLetterSubscriptions
                           .AsNoTracking()
                           .FirstOrDefault(c => c.NewsletterSubscriptionsId == id);
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// NewsLetter Subscriptions pictures by email
        /// </returns>
        public NewsLetterSubscriptions GetRecord(string email)
        {
            return this._db.NewsLetterSubscriptions
                           .AsNoTracking()
                           .FirstOrDefault(c => c.Email.ToLower() == email.ToLower());
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        /// <exception cref="Exception">Record not found!</exception>
        public void UpdateRecord(long id, NewsLetterSubscriptions updatedItem)
        {
            ////Check Products pictures exists
            var item = this.GetRecord(id);
            if (item == null)
            {
                throw new Exception("Record not found!");
            }

            item.Name = updatedItem.Name;
            item.Email = updatedItem.Email;
            this._db.NewsLetterSubscriptions.Update(item);
            this._db.SaveChanges();
        }
    }
}
