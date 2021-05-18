// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsCategoriesApi.cs" company="Innova Marketing Systems S.A.S">
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
    internal class ProductDocumentsCategoriesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsCategoriesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ProductDocumentsCategoriesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Product Documents Categories</returns>
        public async Task<List<ProductDocumentsCategories>> GetAllRecords()
        {
            var productDocumentsCategories = new List<ProductDocumentsCategories>();
            var response = await this._httpClientInstance.GetAsync($"api/ProductDocumentsCategories");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsCategoriesString = await response.Content.ReadAsStringAsync();
                productDocumentsCategories = JsonConvert.DeserializeObject<List<ProductDocumentsCategories>>(productDocumentsCategoriesString);
                return productDocumentsCategories;
            }

            return productDocumentsCategories;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Documents Categories pictures by id</returns>
        public async Task<ProductDocumentsCategories> GetRecord(long id)
        {
            var productDocumentsCategories = new ProductDocumentsCategories();
            var response = await this._httpClientInstance.GetAsync($"api/ProductDocumentsCategories/{id}");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsCategoriesString = await response.Content.ReadAsStringAsync();
                productDocumentsCategories = JsonConvert.DeserializeObject<ProductDocumentsCategories>(productDocumentsCategoriesString);
                return productDocumentsCategories;
            }

            return productDocumentsCategories;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="productDocumentsCategories">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<ProductDocumentsCategories> CreateRecord(ProductDocumentsCategories productDocumentsCategories)
        {
            var response = await this._httpClientInstance.PostAsync($"api/ProductDocumentsCategories/Create", new StringContent(JsonConvert.SerializeObject(productDocumentsCategories), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsCategoriesString = await response.Content.ReadAsStringAsync();
                var productDocumentsCategoriesCreated = JsonConvert.DeserializeObject<ProductDocumentsCategories>(productDocumentsCategoriesString);
                return productDocumentsCategoriesCreated;
            }

            return productDocumentsCategories;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, ProductDocumentsCategories updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/ProductDocumentsCategories/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/ProductDocumentsCategories/Delete/{id}");
        }
    }
}
