// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomerApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Customer Api
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using global::Ecommerce.Models.Entities;
    using Newtonsoft.Json;

    /// <summary>
    /// Customer Api
    /// </summary>
    internal class CustomerApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public CustomerApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of All Customers</returns>
        public async Task<List<Customers>> GetAllRecords()
        {
            var shoppingCartRecords = new List<Customers>();
            var response = await this._httpClientInstance.GetAsync($"api/Customer");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordsString = await response.Content.ReadAsStringAsync();
                shoppingCartRecords = JsonConvert.DeserializeObject<List<Customers>>(shoppingCartRecordsString);
                return shoppingCartRecords;
            }

            return shoppingCartRecords;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get Customer for id</returns>
        public async Task<Customers> GetRecord(long id)
        {
            var customer = new Customers();
            var response = await this._httpClientInstance.GetAsync($"api/Customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                var customerString = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<Customers>(customerString);
                return customer;
            }

            return customer;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="customer">The make.</param>
        /// <returns>Customers created</returns>
        public async Task<Customers> CreateRecord(Customers customer)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Customer/Create", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var customersString = await response.Content.ReadAsStringAsync();
                var customersCreated = JsonConvert.DeserializeObject<Customers>(customersString);
                return customersCreated;
            }

            return customer;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Customers updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Customer/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Customer/Delete/{id}");
        }
    }
}
