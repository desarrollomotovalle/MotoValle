// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsletterSubscriptionsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Newsletter Subscriptions Api helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using global::Ecommerce.Models.Entities;
    using Newtonsoft.Json;

    /// <summary>
    /// Newsletter Subscriptions Api helper
    /// </summary>
    internal class NewsletterSubscriptionsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public NewsletterSubscriptionsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<NewsLetterSubscriptions>> GetAllRecords()
        {
            var newsletterSubscriptions = new List<NewsLetterSubscriptions>();
            var response = await this._httpClientInstance.GetAsync($"api/NewsletterSubscription");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                newsletterSubscriptions = JsonConvert.DeserializeObject<List<NewsLetterSubscriptions>>(newsletterSubscriptionsString);
                return newsletterSubscriptions;
            }

            return newsletterSubscriptions;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<NewsLetterSubscriptions> GetRecord(long id)
        {
            var newsletterSubscription = new NewsLetterSubscriptions();
            var response = await this._httpClientInstance.GetAsync($"api/NewsletterSubscription/{id}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                newsletterSubscription = JsonConvert.DeserializeObject<NewsLetterSubscriptions>(newsletterSubscriptionString);
                return newsletterSubscription;
            }

            return newsletterSubscription;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>NewsLetter Subscriptions pictures by email</returns>
        public async Task<NewsLetterSubscriptions> GetRecord(string email)
        {
            var newsletterSubscription = new NewsLetterSubscriptions();
            var response = await this._httpClientInstance.GetAsync($"api/NewsletterSubscription/ForEmail/{email}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                newsletterSubscription = JsonConvert.DeserializeObject<NewsLetterSubscriptions>(newsletterSubscriptionString);
                return newsletterSubscription;
            }

            return newsletterSubscription;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="newsletterSubscription">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<NewsLetterSubscriptions> CreateRecord(NewsLetterSubscriptions newsletterSubscription)
        {
            var response = await this._httpClientInstance.PostAsync($"api/NewsletterSubscription/Create", new StringContent(JsonConvert.SerializeObject(newsletterSubscription), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<NewsLetterSubscriptions>(newsletterSubscriptionString);
                return newsletterSubscriptionCreated;
            }

            return newsletterSubscription;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, NewsLetterSubscriptions updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/NewsletterSubscription/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/NewsletterSubscription/Delete/{id}");
        }
    }
}
