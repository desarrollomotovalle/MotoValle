// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Taxes Api Helper
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
    /// Taxes Api Helper
    /// </summary>
    internal class TaxesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxesApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public TaxesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<Taxes>> GetAllRecords()
        {
            var taxes = new List<Taxes>();
            var response = await this._httpClientInstance.GetAsync($"api/Taxes");
            if (response.IsSuccessStatusCode)
            {
                string taxesString = await response.Content.ReadAsStringAsync();
                taxes = JsonConvert.DeserializeObject<List<Taxes>>(taxesString);
                return taxes;
            }

            return taxes;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<Taxes> GetRecord(long id)
        {
            var taxes = new Taxes();
            var response = await this._httpClientInstance.GetAsync($"api/Taxes/{id}");
            if (response.IsSuccessStatusCode)
            {
                string taxesString = await response.Content.ReadAsStringAsync();
                taxes = JsonConvert.DeserializeObject<Taxes>(taxesString);
                return taxes;
            }

            return taxes;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="taxes">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<Taxes> CreateRecord(Taxes taxes)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Taxes/Create", new StringContent(JsonConvert.SerializeObject(taxes), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string taxesString = await response.Content.ReadAsStringAsync();
                var taxesCreated = JsonConvert.DeserializeObject<Taxes>(taxesString);
                return taxesCreated;
            }

            return taxes;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Taxes updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Taxes/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Taxes/Delete/{id}");
        }
    }
}
