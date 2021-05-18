// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterestedCustomersApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Interested Customers Api Helper
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
    /// Interested Customers Api Helper
    /// </summary>
    internal class InterestedCustomersApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterestedCustomersApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public InterestedCustomersApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Interested Customers</returns>
        public async Task<List<InterestedCustomers>> GetAllRecords()
        {
            var interestedCustomers = new List<InterestedCustomers>();
            var response = await this._httpClientInstance.GetAsync($"api/InterestedCustomers");
            if (response.IsSuccessStatusCode)
            {
                var interestedCustomersString = await response.Content.ReadAsStringAsync();
                interestedCustomers = JsonConvert.DeserializeObject<List<InterestedCustomers>>(interestedCustomersString);
                return interestedCustomers;
            }

            return interestedCustomers;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Interested Customers pictures by id</returns>
        public async Task<InterestedCustomers> GetRecord(long id)
        {
            var interestedCustomers = new InterestedCustomers();
            var response = await this._httpClientInstance.GetAsync($"api/InterestedCustomers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var interestedCustomersString = await response.Content.ReadAsStringAsync();
                interestedCustomers = JsonConvert.DeserializeObject<InterestedCustomers>(interestedCustomersString);
                return interestedCustomers;
            }

            return interestedCustomers;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="interestedCustomers">The item.</param>
        /// <returns>Interested Customers created</returns>
        public async Task<InterestedCustomers> CreateRecord(InterestedCustomers interestedCustomers)
        {
            var response = await this._httpClientInstance.PostAsync($"api/InterestedCustomers/Create", new StringContent(JsonConvert.SerializeObject(interestedCustomers), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var interestedCustomersString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<InterestedCustomers>(interestedCustomersString);
                return newsletterSubscriptionCreated;
            }

            return interestedCustomers;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, InterestedCustomers updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/InterestedCustomers/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/InterestedCustomers/Delete/{id}");
        }
    }
}
