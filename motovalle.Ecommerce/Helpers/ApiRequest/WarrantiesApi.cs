// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WarrantiesApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Warranties Api Api Helper
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
    /// Warranties Api Helper
    /// </summary>
    internal class WarrantiesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarrantiesApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public WarrantiesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<Warranties>> GetAllRecords()
        {
            var warranties = new List<Warranties>();
            var response = await this._httpClientInstance.GetAsync($"api/Warranties");
            if (response.IsSuccessStatusCode)
            {
                var warrantiesString = await response.Content.ReadAsStringAsync();
                warranties = JsonConvert.DeserializeObject<List<Warranties>>(warrantiesString);
                return warranties;
            }

            return warranties;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<Warranties> GetRecord(long id)
        {
            var warranties = new Warranties();
            var response = await this._httpClientInstance.GetAsync($"api/Warranties/{id}");
            if (response.IsSuccessStatusCode)
            {
                string warrantiesString = await response.Content.ReadAsStringAsync();
                warranties = JsonConvert.DeserializeObject<Warranties>(warrantiesString);
                return warranties;
            }

            return warranties;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="warranties">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<Warranties> CreateRecord(Warranties warranties)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Warranties/Create", new StringContent(JsonConvert.SerializeObject(warranties), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string warrantiesString = await response.Content.ReadAsStringAsync();
                var warrantiesCreated = JsonConvert.DeserializeObject<Warranties>(warrantiesString);
                return warrantiesCreated;
            }

            return warranties;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Warranties updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Warranties/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Warranties/Delete/{id}");
        }
    }
}
