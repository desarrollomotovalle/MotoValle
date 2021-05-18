// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsApi.cs" company="Innova Marketing Systems S.A.S">
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
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Body Styles Api Helper
    /// </summary>
    internal class ProductDocumentsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ProductDocumentsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Product Documents Categories</returns>
        public async Task<List<ProductDocuments>> GetAllRecords()
        {
            var productDocuments = new List<ProductDocuments>();
            var response = await this._httpClientInstance.GetAsync($"api/ProductDocuments");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                productDocuments = JsonConvert.DeserializeObject<List<ProductDocuments>>(productDocumentsString);
                return productDocuments;
            }

            return productDocuments;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Product Documents Categories</returns>
        public async Task<List<ProductDocuments>> GetRecordsForProduct(long productsId)
        {
            var productDocuments = new List<ProductDocuments>();
            var response = await this._httpClientInstance.GetAsync($"api/ProductDocuments/ForProduct/{productsId}");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                productDocuments = JsonConvert.DeserializeObject<List<ProductDocuments>>(productDocumentsString);
                return productDocuments;
            }

            return productDocuments;
        }


        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Documents Categories pictures by id</returns>
        public async Task<ProductDocuments> GetRecord(long id)
        {
            var productDocuments = new ProductDocuments();
            var response = await this._httpClientInstance.GetAsync($"api/ProductDocuments/{id}");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                productDocuments = JsonConvert.DeserializeObject<ProductDocuments>(productDocumentsString);
                return productDocuments;
            }

            return productDocuments;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="productDocuments">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<ProductDocuments> CreateRecord(ProductDocuments productDocuments)
        {
            var response = await this._httpClientInstance.PostAsync($"api/ProductDocuments/Create", new StringContent(JsonConvert.SerializeObject(productDocuments), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                var productDocumentsCreated = JsonConvert.DeserializeObject<ProductDocuments>(productDocumentsString);
                return productDocumentsCreated;
            }

            return productDocuments;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, ProductDocuments updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/ProductDocuments/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/ProductDocuments/Delete/{id}");
        }
    }
}
