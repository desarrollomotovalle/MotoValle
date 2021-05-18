// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DriveTrainsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Drive Trains Api Helper
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
    /// Drive Trains Api Helper
    /// </summary>
    internal class DriveTrainsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriveTrainsApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public DriveTrainsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<DriveTrains>> GetAllRecords()
        {
            var driveTrains = new List<DriveTrains>();
            var response = await this._httpClientInstance.GetAsync($"api/DriveTrains");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                driveTrains = JsonConvert.DeserializeObject<List<DriveTrains>>(newsletterSubscriptionsString);
                return driveTrains;
            }

            return driveTrains;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<DriveTrains> GetRecord(long id)
        {
            var driveTrains = new DriveTrains();
            var response = await this._httpClientInstance.GetAsync($"api/DriveTrains/{id}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                driveTrains = JsonConvert.DeserializeObject<DriveTrains>(newsletterSubscriptionString);
                return driveTrains;
            }

            return driveTrains;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="driveTrains">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<DriveTrains> CreateRecord(DriveTrains driveTrains)
        {
            var response = await this._httpClientInstance.PostAsync($"api/DriveTrains/Create", new StringContent(JsonConvert.SerializeObject(driveTrains), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<DriveTrains>(newsletterSubscriptionString);
                return newsletterSubscriptionCreated;
            }

            return driveTrains;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, DriveTrains updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/DriveTrains/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/DriveTrains/Delete/{id}");
        }
    }
}
