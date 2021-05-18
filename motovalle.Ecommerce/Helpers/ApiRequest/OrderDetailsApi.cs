// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderDetailsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order Details Api Helper
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
    /// Order Details Api Helper
    /// </summary>
    public class OrderDetailsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailsApi"/> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public OrderDetailsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>All List of Orders</returns>
        public async Task<List<OrderDetails>> GetAllRecords()
        {
            var ordersDetails = new List<OrderDetails>();
            var response = await this._httpClientInstance.GetAsync($"api/OrderDetails");
            if (response.IsSuccessStatusCode)
            {
                var ordersString = await response.Content.ReadAsStringAsync();
                ordersDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(ordersString);
                return ordersDetails;
            }

            return ordersDetails;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order details for id</returns>
        public async Task<OrderDetails> GetRecord(long id)
        {
            var ordersDetails = new OrderDetails();
            var response = await this._httpClientInstance.GetAsync($"api/OrderDetails/{id}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                ordersDetails = JsonConvert.DeserializeObject<OrderDetails>(ordersString);
                return ordersDetails;
            }

            return ordersDetails;
        }

        /// <summary>
        /// Gets the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>List of order details for order</returns>
        public async Task<List<OrderDetails>> GetRecordsForOrder(long ordersId)
        {
            var ordersDetails = new List<OrderDetails>();
            var response = await this._httpClientInstance.GetAsync($"api/OrderDetails/GetForOrder/{ordersId}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                ordersDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(ordersString);
                return ordersDetails;
            }

            return ordersDetails;
        }

        /// <summary>
        /// Gets the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        /// <returns>List of order details for order</returns>
        public async Task<List<OrderDetails>> GetRecordsForOrderAndCustomer(long ordersId, long customersId)
        {
            var ordersDetails = new List<OrderDetails>();
            var response = await this._httpClientInstance.GetAsync($"api/OrderDetails/{ordersId}/Customer/{customersId}");
            if (response.IsSuccessStatusCode)
            {
                string ordersString = await response.Content.ReadAsStringAsync();
                ordersDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(ordersString);
                return ordersDetails;
            }

            return ordersDetails;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="newsletterSubscription">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<OrderDetails> CreateRecord(OrderDetails newOrderDetails)
        {
            var response = await this._httpClientInstance.PostAsync($"api/OrderDetails/Create", new StringContent(JsonConvert.SerializeObject(newOrderDetails), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var newOrderDetailsCreatedString = await response.Content.ReadAsStringAsync();
                var newOrderDetailsCreated = JsonConvert.DeserializeObject<OrderDetails>(newOrderDetailsCreatedString);
                return newOrderDetailsCreated;
            }

            return newOrderDetails;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="newsletterSubscription">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<List<OrderDetails>> CreateRecords(List<OrderDetails> newOrdersDetails)
        {
            var response = await this._httpClientInstance.PostAsync($"api/OrderDetails/Create/Massive", new StringContent(JsonConvert.SerializeObject(newOrdersDetails), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var newOrdersDetailsCreatedString = await response.Content.ReadAsStringAsync();
                var newOrderDetailsCreated = JsonConvert.DeserializeObject<List<OrderDetails>>(newOrdersDetailsCreatedString);
                return newOrderDetailsCreated;
            }

            return newOrdersDetails;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, OrderDetails updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/OrderDetails/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/OrderDetails/Delete/{id}");
        }

        /// <summary>
        /// Deletes the records for order.
        /// </summary>
        /// <param name="ordersId">The orders identifier.</param>
        public async Task DeleteRecordsForOrder(long ordersId)
        {
            await this._httpClientInstance.DeleteAsync($"api/OrderDetails/Delete/ForOrder/{ordersId}");
        }
    }
}
