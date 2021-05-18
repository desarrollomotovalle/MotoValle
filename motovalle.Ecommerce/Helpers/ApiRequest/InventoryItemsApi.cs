// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Items Api Helper
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
    ///  Inventory Items Api Helper
    /// </summary>
    internal class InventoryItemsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemsApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public InventoryItemsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Inventory Items</returns>
        public async Task<List<InventoryItems>> GetAllRecords()
        {
            var inventoryItems = new List<InventoryItems>();
            var response = await this._httpClientInstance.GetAsync($"api/InventoryItems");
            if (response.IsSuccessStatusCode)
            {
                string inventoryItemsString = await response.Content.ReadAsStringAsync();
                inventoryItems = JsonConvert.DeserializeObject<List<InventoryItems>>(inventoryItemsString);
                return inventoryItems;
            }

            return inventoryItems;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Inventory Items pictures by id</returns>
        public async Task<InventoryItems> GetRecord(long id)
        {
            var inventoryItems = new InventoryItems();
            var response = await this._httpClientInstance.GetAsync($"api/InventoryItems/{id}");
            if (response.IsSuccessStatusCode)
            {
                string inventoryItemsString = await response.Content.ReadAsStringAsync();
                inventoryItems = JsonConvert.DeserializeObject<InventoryItems>(inventoryItemsString);
                return inventoryItems;
            }

            return inventoryItems;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="inventoryItems">The item.</param>
        /// <returns>Inventory Items created</returns>
        public async Task<InventoryItems> CreateRecord(InventoryItems inventoryItems)
        {
            var response = await this._httpClientInstance.PostAsync($"api/InventoryItems/Create", new StringContent(JsonConvert.SerializeObject(inventoryItems), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string inventoryItemsString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<InventoryItems>(inventoryItemsString);
                return newsletterSubscriptionCreated;
            }

            return inventoryItems;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, InventoryItems updatedItem)
        {
            var response = await this._httpClientInstance.PutAsync($"api/InventoryItems/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                throw new System.Exception(await response.Content.ReadAsStringAsync()); 
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/InventoryItems/Delete/{id}");
        }
    }
}
