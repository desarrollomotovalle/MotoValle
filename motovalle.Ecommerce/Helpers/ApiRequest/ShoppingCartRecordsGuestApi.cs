// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordsGuestApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Records Api
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Newtonsoft.Json;

    /// <summary>
    /// Shopping Cart Records Api
    /// </summary>
    internal class ShoppingCartRecordsGuestApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartRecordsGuestApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ShoppingCartRecordsGuestApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of All ShoppingCartRecordsGuest</returns>
        public async Task<List<ShoppingCartRecordsGuest>> GetAllRecords()
        {
            var shoppingCartRecords = new List<ShoppingCartRecordsGuest>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecordsGuest");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordsString = await response.Content.ReadAsStringAsync();
                shoppingCartRecords = JsonConvert.DeserializeObject<List<ShoppingCartRecordsGuest>>(shoppingCartRecordsString);
                return shoppingCartRecords;
            }

            return shoppingCartRecords;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get shopping cart for id</returns>
        public async Task<ShoppingCartRecordsGuest> GetRecord(long id)
        {
            var shoppingCartRecord = new ShoppingCartRecordsGuest();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecordsGuest/{id}");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordString = await response.Content.ReadAsStringAsync();
                shoppingCartRecord = JsonConvert.DeserializeObject<ShoppingCartRecordsGuest>(shoppingCartRecordString);
                return shoppingCartRecord;
            }

            return shoppingCartRecord;
        }

        /// <summary>
        /// Gets the records for customer.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        /// <returns>List of All ShoppingCartRecordsGuest for customer</returns>
        public async Task<List<ShoppingCartRecordsGuest>> GetRecordsForCustomer(string guestId)
        {
            var shoppingCartRecords = new List<ShoppingCartRecordsGuest>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecordsGuest/ForCustomer/{guestId}");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordsString = await response.Content.ReadAsStringAsync();
                shoppingCartRecords = JsonConvert.DeserializeObject<List<ShoppingCartRecordsGuest>>(shoppingCartRecordsString);
                return shoppingCartRecords;
            }

            return shoppingCartRecords;
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        /// <returns>List of Shopping cart checkout for customer</returns>
        public async Task<List<ShoppingCartCheckoutViewModel>> GetShoppingCartCheckout(string guestId)
        {
            var shoppingCartCheckout = new List<ShoppingCartCheckoutViewModel>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecordsGuest/Checkout/{guestId}");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartCheckoutString = await response.Content.ReadAsStringAsync();
                shoppingCartCheckout = JsonConvert.DeserializeObject<List<ShoppingCartCheckoutViewModel>>(shoppingCartCheckoutString);
                return shoppingCartCheckout;
            }

            return shoppingCartCheckout;
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        /// <returns>
        /// List of Shopping cart checkout for customer
        /// </returns>
        public async Task<List<TaxesTotalViewModel>> GetTaxesInfoForShoppingCart(string guestId)
        {
            var taxesInfo = new List<TaxesTotalViewModel>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecordsGuest/TaxesInfo/{guestId}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                taxesInfo = JsonConvert.DeserializeObject<List<TaxesTotalViewModel>>(responseString);
                return taxesInfo;
            }

            return taxesInfo;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="shoppingCartRecord">The make.</param>
        /// <returns>Makes created</returns>
        public async Task<ShoppingCartRecordsGuest> CreateRecord(ShoppingCartRecordsGuest shoppingCartRecord)
        {
            var response = await this._httpClientInstance.PostAsync($"api/ShoppingCartRecordsGuest/Create", new StringContent(JsonConvert.SerializeObject(shoppingCartRecord), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordString = await response.Content.ReadAsStringAsync();
                var shoppingCartRecordCreated = JsonConvert.DeserializeObject<ShoppingCartRecordsGuest>(shoppingCartRecordString);
                return shoppingCartRecordCreated;
            }

            return shoppingCartRecord;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, ShoppingCartRecordsGuest updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/ShoppingCartRecordsGuest/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/ShoppingCartRecordsGuest/Delete/{id}");
        }

        /// <summary>
        /// Deletes the records for customer.
        /// </summary>
        /// <param name="guestId">The customers identifier.</param>
        public async Task DeleteRecordsForCustomer(string guestId)
        {
            await this._httpClientInstance.DeleteAsync($"api/ShoppingCartRecordsGuest/Delete/ForCustomer/{guestId}");
        }
    }
}