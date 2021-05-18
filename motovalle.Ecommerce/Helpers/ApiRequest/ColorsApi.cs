// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Colors Api Helper
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
    /// Colors Api Helper
    /// </summary>
    internal class ColorsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorsApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ColorsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<Colors>> GetAllRecords()
        {
            var colors = new List<Colors>();
            var response = await this._httpClientInstance.GetAsync($"api/Colors");
            if (response.IsSuccessStatusCode)
            {
                string colorsString = await response.Content.ReadAsStringAsync();
                colors = JsonConvert.DeserializeObject<List<Colors>>(colorsString);
                return colors;
            }

            return colors;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<Colors> GetRecord(long id)
        {
            var colors = new Colors();
            var response = await this._httpClientInstance.GetAsync($"api/Colors/{id}");
            if (response.IsSuccessStatusCode)
            {
                var colorsString = await response.Content.ReadAsStringAsync();
                colors = JsonConvert.DeserializeObject<Colors>(colorsString);
                return colors;
            }

            return colors;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="productsId">The products identifier.</param>
        /// <returns>
        /// NewsLetter Subscriptions pictures by id
        /// </returns>
        public async Task<List<Colors>> GetForProduct(long productsId)
        {
            var colors = new List<Colors>();
            var response = await this._httpClientInstance.GetAsync($"api/Colors/GetForProduct/{productsId}");
            if (response.IsSuccessStatusCode)
            {
                var colorsString = await response.Content.ReadAsStringAsync();
                colors = JsonConvert.DeserializeObject<List<Colors>>(colorsString);
                return colors;
            }

            return colors;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="colors">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<Colors> CreateRecord(Colors colors)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Colors/Create", new StringContent(JsonConvert.SerializeObject(colors), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var colorsString = await response.Content.ReadAsStringAsync();
                var colorsCreated = JsonConvert.DeserializeObject<Colors>(colorsString);
                return colorsCreated;
            }

            return colors;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Colors updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Colors/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Colors/Delete/{id}");
        }
    }
}
