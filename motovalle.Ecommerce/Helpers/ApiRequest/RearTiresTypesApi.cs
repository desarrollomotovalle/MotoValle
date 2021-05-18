// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RearTiresTypesApi.cs" company="Innova Marketing Systems S.A.S">
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
    internal class RearTiresTypesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="RearTiresTypesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public RearTiresTypesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<RearTiresTypes>> GetAllRecords()
        {
            var bodyStyles = new List<RearTiresTypes>();
            var response = await this._httpClientInstance.GetAsync($"api/RearTiresTypes");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<List<RearTiresTypes>>(newsletterSubscriptionsString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<RearTiresTypes> GetRecord(long id)
        {
            var bodyStyles = new RearTiresTypes();
            var response = await this._httpClientInstance.GetAsync($"api/RearTiresTypes/{id}");
            if (response.IsSuccessStatusCode)
            {
                string bodyStylesString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<RearTiresTypes>(bodyStylesString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="bodyStyles">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<RearTiresTypes> CreateRecord(RearTiresTypes bodyStyles)
        {
            var response = await this._httpClientInstance.PostAsync($"api/RearTiresTypes/Create", new StringContent(JsonConvert.SerializeObject(bodyStyles), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string bodyStylesString = await response.Content.ReadAsStringAsync();
                var bodyStylesCreated = JsonConvert.DeserializeObject<RearTiresTypes>(bodyStylesString);
                return bodyStylesCreated;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, RearTiresTypes updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/RearTiresTypes/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/RearTiresTypes/Delete/{id}");
        }
    }
}
