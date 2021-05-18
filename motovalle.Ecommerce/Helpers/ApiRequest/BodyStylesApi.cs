// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BodyStylesApi.cs" company="Innova Marketing Systems S.A.S">
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
    internal class BodyStylesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyStylesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public BodyStylesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<BodyStyles>> GetAllRecords()
        {
            var bodyStyles = new List<BodyStyles>();
            var response = await this._httpClientInstance.GetAsync($"api/BodyStyles");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<List<BodyStyles>>(newsletterSubscriptionsString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<BodyStyles> GetRecord(long id)
        {
            var bodyStyles = new BodyStyles();
            var response = await this._httpClientInstance.GetAsync($"api/BodyStyles/{id}");
            if (response.IsSuccessStatusCode)
            {
                string bodyStylesString = await response.Content.ReadAsStringAsync();
                bodyStyles = JsonConvert.DeserializeObject<BodyStyles>(bodyStylesString);
                return bodyStyles;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="bodyStyles">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<BodyStyles> CreateRecord(BodyStyles bodyStyles)
        {
            var response = await this._httpClientInstance.PostAsync($"api/BodyStyles/Create", new StringContent(JsonConvert.SerializeObject(bodyStyles), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string bodyStylesString = await response.Content.ReadAsStringAsync();
                var bodyStylesCreated = JsonConvert.DeserializeObject<BodyStyles>(bodyStylesString);
                return bodyStylesCreated;
            }

            return bodyStyles;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, BodyStyles updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/BodyStyles/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/BodyStyles/Delete/{id}");
        }
    }
}
