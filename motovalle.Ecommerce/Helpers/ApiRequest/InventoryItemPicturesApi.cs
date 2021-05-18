// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemPicturesApi.cs" company="Innova Marketing Systems S.A.S">
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
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Body Styles Api Helper
    /// </summary>
    internal class InventoryItemPicturesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemPicturesApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public InventoryItemPicturesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<InventoryItemPictures>> GetAllRecords()
        {
            var inventoryItemPictures = new List<InventoryItemPictures>();
            var response = await this._httpClientInstance.GetAsync($"api/InventoryItemPictures");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                inventoryItemPictures = JsonConvert.DeserializeObject<List<InventoryItemPictures>>(responseString);
                return inventoryItemPictures;
            }

            return inventoryItemPictures;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<InventoryItemPictures>> GetForInventory(long inventoryItemsId)
        {
            var inventoryItemPictures = new List<InventoryItemPictures>();
            var response = await this._httpClientInstance.GetAsync($"api/InventoryItemPictures/GetForInventory/{inventoryItemsId}");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                inventoryItemPictures = JsonConvert.DeserializeObject<List<InventoryItemPictures>>(responseString);
                return inventoryItemPictures;
            }

            return inventoryItemPictures;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<InventoryItemPictures> GetRecord(long id)
        {
            var inventoryItemPictures = new InventoryItemPictures();
            var response = await this._httpClientInstance.GetAsync($"api/InventoryItemPictures/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                inventoryItemPictures = JsonConvert.DeserializeObject<InventoryItemPictures>(responseString);
                return inventoryItemPictures;
            }

            return inventoryItemPictures;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="inventoryItemPictures">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<InventoryItemPictures> CreateRecord(InventoryItemPictures inventoryItemPictures)
        {
            var response = await this._httpClientInstance.PostAsync($"api/InventoryItemPictures/Create", new StringContent(JsonConvert.SerializeObject(inventoryItemPictures), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                var newsletterSubscriptionCreated = JsonConvert.DeserializeObject<InventoryItemPictures>(responseString);
                return newsletterSubscriptionCreated;
            }

            return inventoryItemPictures;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, InventoryItemPictures updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/InventoryItemPictures/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/InventoryItemPictures/Delete/{id}");
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="inventoryItemsId">The identifier.</param>
        public async Task DeleteForInventory(long inventoryItemsId)
        {
            await this._httpClientInstance.DeleteAsync($"api/InventoryItemPictures/Delete/ForInventory/{inventoryItemsId}");
        }
    }
}
