// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrdersApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders Api helper
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
    /// Orders Api helper
    /// </summary>
    internal class OrdersApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public OrdersApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>All List of Orders</returns>
        public async Task<List<Orders>> GetAllRecords()
        {
            var orders = new List<Orders>();
            var response = await this._httpClientInstance.GetAsync($"api/Orders");
            if (response.IsSuccessStatusCode)
            {
                var ordersString = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<List<Orders>>(ordersString);
                return orders;
            }

            return orders;
        }

        /// <summary>
        /// Gets the records for customer.
        /// </summary>
        /// <param name="customersId">The customers identifier.</param>
        /// <returns>List of Orders for customer</returns>
        public async Task<List<Orders>> GetRecordsForCustomer(long customersId)
        {
            var ordersForCustomer = new List<Orders>();
            var response = await this._httpClientInstance.GetAsync($"api/Orders/ForCustomer/{customersId}");
            if (response.IsSuccessStatusCode)
            {
                var ordersString = await response.Content.ReadAsStringAsync();
                ordersForCustomer = JsonConvert.DeserializeObject<List<Orders>>(ordersString);
                return ordersForCustomer;
            }

            return ordersForCustomer;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<Orders> GetRecord(long id)
        {
            var orders = new Orders();
            var response = await this._httpClientInstance.GetAsync($"api/Orders/{id}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<Orders>(ordersString);
                return orders;
            }

            return orders;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="orderNumber">The order number.</param>
        /// <returns>
        /// Order for number
        /// </returns>
        public async Task<Orders> GetRecordForOrderNumber(long orderNumber)
        {
            var orders = new Orders();
            var response = await this._httpClientInstance.GetAsync($"api/Orders/ForOrderNumber/{orderNumber}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<Orders>(ordersString);
                return orders;
            }

            return orders;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="idtransaction">The idtransaction.</param>
        /// <returns>
        /// Order for transaction Id
        /// </returns>
        public async Task<Orders> GetRecordForTransaction(long idtransaction)
        {
            var orders = new Orders();
            var response = await this._httpClientInstance.GetAsync($"api/Orders/ForTransaction/{idtransaction}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<Orders>(ordersString);
                return orders;
            }

            return orders;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>
        /// Order with details and product info
        /// </returns>
        public async Task<OrderWithDetailsAndProductInfo> GetOrderWithDetailsWithProductInfo(long ordersId, long customersId)
        {
            var orders = new OrderWithDetailsAndProductInfo();
            var response = await this._httpClientInstance.GetAsync($"api/Orders/GetDetailsWithProductInfo/{ordersId}/Customer/{customersId}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<OrderWithDetailsAndProductInfo>(ordersString);
                return orders;
            }

            return orders;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="newsletterSubscription">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<Orders> CreateRecord(Orders newOrder)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Orders/Create", new StringContent(JsonConvert.SerializeObject(newOrder), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var newOrderCreatedString = await response.Content.ReadAsStringAsync();
                var newOrderCreated = JsonConvert.DeserializeObject<Orders>(newOrderCreatedString);
                return newOrderCreated;
            }

            return newOrder;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Orders updatedItem)
        {
           await this._httpClientInstance.PutAsync($"api/Orders/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Orders/Delete/{id}");
        }
    }
}
