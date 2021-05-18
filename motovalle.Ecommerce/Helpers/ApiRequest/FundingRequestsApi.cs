// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FundingRequestApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Funding Requests Api
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
    /// Funding Requests Api
    /// </summary>
    internal class FundingRequestsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public FundingRequestsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Funding requests</returns>
        public async Task<List<FundingRequests>> GetAllRecords()
        {
            var fundingRequests = new List<FundingRequests>();
            var response = await this._httpClientInstance.GetAsync($"api/FundingRequests");
            if (response.IsSuccessStatusCode)
            {
                var fundingRequestsString = await response.Content.ReadAsStringAsync();
                fundingRequests = JsonConvert.DeserializeObject<List<FundingRequests>>(fundingRequestsString);
                return fundingRequests;
            }

            return fundingRequests;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Funding requests by Id</returns>
        public async Task<FundingRequests> GetRecord(long id)
        {
            var fundingRequest = new FundingRequests();
            var response = await this._httpClientInstance.GetAsync($"api/FundingRequests/{id}");
            if (response.IsSuccessStatusCode)
            {
                var fundingRequestsString = await response.Content.ReadAsStringAsync();
                fundingRequest = JsonConvert.DeserializeObject<FundingRequests>(fundingRequestsString);
                return fundingRequest;
            }

            return fundingRequest;
        }

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Product for category filtered</returns>
        public async Task<FundingRequests> GetRecordForOrder(int ordersId)
        {
            var fundingRequests = new FundingRequests();
            var response = await this._httpClientInstance.GetAsync($"api/FundingRequests/ForOrder/{ordersId}");
            if (response.IsSuccessStatusCode)
            {
                var fundingRequestsString = await response.Content.ReadAsStringAsync();
                fundingRequests = JsonConvert.DeserializeObject<FundingRequests>(fundingRequestsString);
                return fundingRequests;
            }

            return fundingRequests;
        }

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Product for category filtered</returns>
        public async Task<FundingRequests> GetRecordForOrderNumber(int orderNumber)
        {
            var fundingRequests = new FundingRequests();
            var response = await this._httpClientInstance.GetAsync($"api/FundingRequests/ForOrderNumber/{orderNumber}");
            if (response.IsSuccessStatusCode)
            {
                var fundingRequestsString = await response.Content.ReadAsStringAsync();
                fundingRequests = JsonConvert.DeserializeObject<FundingRequests>(fundingRequestsString);
                return fundingRequests;
            }

            return fundingRequests;
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="fundingRequests">The category.</param>
        /// <returns>Category created</returns>
        public async Task<FundingRequests> CreateRecord(FundingRequests fundingRequests)
        {
            var response = await this._httpClientInstance.PostAsync($"api/FundingRequests/Create", new StringContent(JsonConvert.SerializeObject(fundingRequests), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string fundingRequestsString = await response.Content.ReadAsStringAsync();
                var fundingRequestsCreated = JsonConvert.DeserializeObject<FundingRequests>(fundingRequestsString);
                return fundingRequestsCreated;
            }

            return fundingRequests;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, FundingRequests updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/FundingRequests/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/FundingRequests/Delete/{id}");
        }
    }
}
