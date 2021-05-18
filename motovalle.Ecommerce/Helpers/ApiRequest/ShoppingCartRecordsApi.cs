// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordsApi.cs" company="Innova Marketing Systems S.A.S">
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
    internal class ShoppingCartRecordsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartRecordsApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ShoppingCartRecordsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of All ShoppingCartRecords</returns>
        public async Task<List<ShoppingCartRecords>> GetAllRecords()
        {
            var shoppingCartRecords = new List<ShoppingCartRecords>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecords");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordsString = await response.Content.ReadAsStringAsync();
                shoppingCartRecords = JsonConvert.DeserializeObject<List<ShoppingCartRecords>>(shoppingCartRecordsString);
                return shoppingCartRecords;
            }

            return shoppingCartRecords;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get shopping cart for id</returns>
        public async Task<ShoppingCartRecords> GetRecord(long id)
        {
            var shoppingCartRecord = new ShoppingCartRecords();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecords/{id}");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordString = await response.Content.ReadAsStringAsync();
                shoppingCartRecord = JsonConvert.DeserializeObject<ShoppingCartRecords>(shoppingCartRecordString);
                return shoppingCartRecord;
            }

            return shoppingCartRecord;
        }

        /// <summary>
        /// Gets the records for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of All ShoppingCartRecords for customer</returns>
        public async Task<List<ShoppingCartRecords>> GetRecordsForCustomer(long customersId)
        {
            var shoppingCartRecords = new List<ShoppingCartRecords>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecords/ForCustomer/{customersId}");
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordsString = await response.Content.ReadAsStringAsync();
                shoppingCartRecords = JsonConvert.DeserializeObject<List<ShoppingCartRecords>>(shoppingCartRecordsString);
                return shoppingCartRecords;
            }

            return shoppingCartRecords;
        }

        /// <summary>
        /// Gets the shopping cart checkout.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of Shopping cart checkout for customer</returns>
        public async Task<List<ShoppingCartCheckoutViewModel>> GetShoppingCartCheckout(long customersId)
        {
            var shoppingCartCheckout = new List<ShoppingCartCheckoutViewModel>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecords/Checkout/{customersId}");
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
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>
        /// List of Shopping cart checkout for customer
        /// </returns>
        public async Task<List<TaxesTotalViewModel>> GetTaxesInfoForShoppingCart(long customersId)
        {
            var taxesInfo = new List<TaxesTotalViewModel>();
            var response = await this._httpClientInstance.GetAsync($"api/ShoppingCartRecords/TaxesInfo/{customersId}");
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
        public async Task<ShoppingCartRecords> CreateRecord(ShoppingCartRecords shoppingCartRecord)
        {
            var response = await this._httpClientInstance.PostAsync($"api/ShoppingCartRecords/Create", new StringContent(JsonConvert.SerializeObject(shoppingCartRecord), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var shoppingCartRecordString = await response.Content.ReadAsStringAsync();
                var shoppingCartRecordCreated = JsonConvert.DeserializeObject<ShoppingCartRecords>(shoppingCartRecordString);
                return shoppingCartRecordCreated;
            }

            return shoppingCartRecord;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, ShoppingCartRecords updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/ShoppingCartRecords/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            try
            {
                var result = await this._httpClientInstance.DeleteAsync($"api/ShoppingCartRecords/Delete/{id}");
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes the records for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        public async Task DeleteRecordsForCustomer(long customersId)
        {
           await this._httpClientInstance.DeleteAsync($"api/ShoppingCartRecords/Delete/ForCustomer/{customersId}");
        }
    }
}
