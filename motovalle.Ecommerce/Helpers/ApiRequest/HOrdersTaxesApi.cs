// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HOrdersTaxesApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Body Styles Api Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest
{
    using global::Ecommerce.Models.Entities;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Body Styles Api Helper
    /// </summary>
    internal class HOrdersTaxesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="HOrdersTaxesApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public HOrdersTaxesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<HOrdersTaxes>> GetAllRecords()
        {
            var bodyStyles = new List<HOrdersTaxes>();
            var response = await this._httpClientInstance.GetAsync($"api/HOrdersTaxes");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<List<HOrdersTaxes>>(newsletterSubscriptionsString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>
        /// List of NewsLetter Subscriptions
        /// </returns>
        public async Task<List<HOrdersTaxes>> GetForOrder(long ordersId)
        {
            var bodyStyles = new List<HOrdersTaxes>();
            var response = await this._httpClientInstance.GetAsync($"api/HOrdersTaxes/ForOder/{ordersId}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<List<HOrdersTaxes>>(newsletterSubscriptionsString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<HOrdersTaxes> GetRecord(long id)
        {
            var bodyStyles = new HOrdersTaxes();
            var response = await this._httpClientInstance.GetAsync($"api/HOrdersTaxes/{id}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<HOrdersTaxes>(newsletterSubscriptionString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="bodyStyles">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<HOrdersTaxes> CreateRecord(HOrdersTaxes bodyStyles)
        {
            var response = await this._httpClientInstance.PostAsync($"api/HOrdersTaxes/Create", new StringContent(JsonConvert.SerializeObject(bodyStyles), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<HOrdersTaxes>(newsletterSubscriptionString);
                return newsletterSubscriptionCreated;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="newsletterSubscription">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<List<HOrdersTaxes>> CreateRecords(List<HOrdersTaxes> newOrdersDetails)
        {
            var response = await this._httpClientInstance.PostAsync($"api/HOrdersTaxes/Create/Massive", new StringContent(JsonConvert.SerializeObject(newOrdersDetails), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var newOrdersDetailsCreatedString = await response.Content.ReadAsStringAsync();
                var newOrderDetailsCreated = JsonConvert.DeserializeObject<List<HOrdersTaxes>>(newOrdersDetailsCreatedString);
                return newOrderDetailsCreated;
            }

            return newOrdersDetails;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, HOrdersTaxes updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/HOrdersTaxes/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/HOrdersTaxes/Delete/{id}");
        }
    }
}
