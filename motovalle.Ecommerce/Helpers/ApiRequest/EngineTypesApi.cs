// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineTypesApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Engine Types Api Helper
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
    /// Engine Types Api Helpe
    /// </summary>
    internal class EngineTypesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineTypesApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public EngineTypesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of EngineTypes</returns>
        public async Task<List<EngineTypes>> GetAllRecords()
        {
            var engineTypes = new List<EngineTypes>();
            var response = await this._httpClientInstance.GetAsync($"api/EngineTypes");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                engineTypes = JsonConvert.DeserializeObject<List<EngineTypes>>(newsletterSubscriptionsString);
                return engineTypes;
            }

            return engineTypes;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>EngineTypes by id</returns>
        public async Task<EngineTypes> GetRecord(long id)
        {
            var engineTypes = new EngineTypes();
            var response = await this._httpClientInstance.GetAsync($"api/EngineTypes/{id}");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                engineTypes = JsonConvert.DeserializeObject<EngineTypes>(newsletterSubscriptionString);
                return engineTypes;
            }

            return engineTypes;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="engineTypes">The item.</param>
        /// <returns>EngineTypes created</returns>
        public async Task<EngineTypes> CreateRecord(EngineTypes engineTypes)
        {
            var response = await this._httpClientInstance.PostAsync($"api/EngineTypes/Create", new StringContent(JsonConvert.SerializeObject(engineTypes), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<EngineTypes>(newsletterSubscriptionString);
                return newsletterSubscriptionCreated;
            }

            return engineTypes;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, EngineTypes updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/EngineTypes/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/EngineTypes/Delete/{id}");
        }
    }
}
