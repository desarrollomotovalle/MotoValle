// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Models Api Helper
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
    /// Epa Classes Api Helper
    /// </summary>
    internal class ModelsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ModelsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Models</returns>
        public async Task<List<Models>> GetAllRecords()
        {
            var bodyStyles = new List<Models>();
            var response = await this._httpClientInstance.GetAsync($"api/Models");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<List<Models>>(newsletterSubscriptionsString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Models pictures by id</returns>
        public async Task<Models> GetRecord(long id)
        {
            var bodyStyles = new Models();
            var response = await this._httpClientInstance.GetAsync($"api/Models/{id}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<Models>(newsletterSubscriptionString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="bodyStyles">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<Models> CreateRecord(Models bodyStyles)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Models/Create", new StringContent(JsonConvert.SerializeObject(bodyStyles), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<Models>(newsletterSubscriptionString);
                return newsletterSubscriptionCreated;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Models updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Models/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Models/Delete/{id}");
        }
    }
}
