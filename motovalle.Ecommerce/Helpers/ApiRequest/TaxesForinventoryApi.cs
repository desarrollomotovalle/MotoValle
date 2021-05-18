// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxesForInventoryApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Taxes For Inventory Api Helper
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
    /// Taxes For Inventory Api Helper
    /// </summary>
    internal class TaxesForInventoryApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxesForInventoryApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public TaxesForInventoryApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<TaxesForInventory>> GetAllRecords()
        {
            var taxesForInventory = new List<TaxesForInventory>();
            var response = await this._httpClientInstance.GetAsync($"api/TaxesForInventory");
            if (response.IsSuccessStatusCode)
            {
                string taxesForInventoryString = await response.Content.ReadAsStringAsync();
                taxesForInventory = JsonConvert.DeserializeObject<List<TaxesForInventory>>(taxesForInventoryString);
                return taxesForInventory;
            }

            return taxesForInventory;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<TaxesForInventory> GetRecord(long id)
        {
            var taxesForInventory = new TaxesForInventory();
            var response = await this._httpClientInstance.GetAsync($"api/TaxesForInventory/{id}");
            if (response.IsSuccessStatusCode)
            {
                string taxesForInventoryString = await response.Content.ReadAsStringAsync();
                taxesForInventory = JsonConvert.DeserializeObject<TaxesForInventory>(taxesForInventoryString);
                return taxesForInventory;
            }

            return taxesForInventory;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<List<TaxesForInventory>> GetForInventory(long inventoryItemsId)
        {
            var taxesForInventory = new List<TaxesForInventory>();
            var response = await this._httpClientInstance.GetAsync($"api/TaxesForInventory/ForInventory/{inventoryItemsId}");
            if (response.IsSuccessStatusCode)
            {
                string taxesForInventoryString = await response.Content.ReadAsStringAsync();
                taxesForInventory = JsonConvert.DeserializeObject<List<TaxesForInventory>>(taxesForInventoryString);
                return taxesForInventory;
            }

            return taxesForInventory;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="taxesForInventory">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<TaxesForInventory> CreateRecord(TaxesForInventory taxesForInventory)
        {
            var response = await this._httpClientInstance.PostAsync($"api/TaxesForInventory/Create", new StringContent(JsonConvert.SerializeObject(taxesForInventory), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string taxesForInventoryString = await response.Content.ReadAsStringAsync();
                var taxesForInventoryCreated = JsonConvert.DeserializeObject<TaxesForInventory>(taxesForInventoryString);
                return taxesForInventoryCreated;
            }

            return taxesForInventory;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="taxesForInventory">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<List<TaxesForInventory>> CreateRecords(List<TaxesForInventory> taxesForInventory)
        {
            var response = await this._httpClientInstance.PostAsync($"api/TaxesForInventory/Create/Massive", new StringContent(JsonConvert.SerializeObject(taxesForInventory), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string taxesForInventoryString = await response.Content.ReadAsStringAsync();
                var taxesForInventoryCreated = JsonConvert.DeserializeObject<List<TaxesForInventory>>(taxesForInventoryString);
                return taxesForInventoryCreated;
            }

            return taxesForInventory;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, TaxesForInventory updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/TaxesForInventory/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/TaxesForInventory/Delete/{id}");
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteForInventory(long inventoryItemsId)
        {
            await this._httpClientInstance.DeleteAsync($"api/TaxesForInventory/Delete/ForInventory/{inventoryItemsId}");
        }
    }
}
