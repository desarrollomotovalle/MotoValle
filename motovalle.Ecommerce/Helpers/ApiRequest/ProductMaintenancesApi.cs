// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductMaintenancesApi.cs" company="Innova Marketing Systems S.A.S">
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
    internal class ProductMaintenancesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMaintenancesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ProductMaintenancesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Product Documents Categories</returns>
        public async Task<List<ProductMaintenances>> GetAllRecords()
        {
            var productDocuments = new List<ProductMaintenances>();
            var response = await this._httpClientInstance.GetAsync("api/ProductMaintenances");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                productDocuments = JsonConvert.DeserializeObject<List<ProductMaintenances>>(productDocumentsString);
                return productDocuments;
            }

            return productDocuments;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <param name="makeName">Name of the make.</param>
        /// <returns>
        /// List of Product Documents Categories
        /// </returns>
        public async Task<List<ProductMaintenances>> GetRecordsByMake(string makeName)
        {
            var productMaintenanaces = new List<ProductMaintenances>();
            var response = await this._httpClientInstance.GetAsync($"api/ProductMaintenances/ByMake/{makeName}");
            if (response.IsSuccessStatusCode)
            {
                var productMaintenanacesString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductMaintenances>>(productMaintenanacesString);
            }

            return productMaintenanaces;
        }

        /// <summary>
        /// Gets the records by product.
        /// </summary>
        /// <param name="makeName">Name of the make.</param>
        /// <returns></returns>
        public async Task<List<ProductMaintenances>> GetRecordsByProduct(int productsId, bool onlyEnables = true)
        {
            var productMaintenanaces = new List<ProductMaintenances>();
            var response = await this._httpClientInstance.GetAsync($"api/ProductMaintenances/ByProduct/{productsId}/All/{onlyEnables}");
            if (response.IsSuccessStatusCode)
            {
                var productMaintenanacesString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductMaintenances>>(productMaintenanacesString);
            }

            return productMaintenanaces;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product Documents Categories pictures by id</returns>
        public async Task<ProductMaintenances> GetRecord(long id)
        {
            var productDocuments = new ProductMaintenances();
            var response = await this._httpClientInstance.GetAsync($"api/ProductMaintenances/{id}");
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                productDocuments = JsonConvert.DeserializeObject<ProductMaintenances>(productDocumentsString);
                return productDocuments;
            }

            return productDocuments;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="productDocuments">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<ProductMaintenances> CreateRecord(ProductMaintenances productDocuments)
        {
            var response = await this._httpClientInstance.PostAsync("api/ProductMaintenances/Create", new StringContent(JsonConvert.SerializeObject(productDocuments), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string productDocumentsString = await response.Content.ReadAsStringAsync();
                var productDocumentsCreated = JsonConvert.DeserializeObject<ProductMaintenances>(productDocumentsString);
                return productDocumentsCreated;
            }

            return productDocuments;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, ProductMaintenances updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/ProductMaintenances/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/ProductMaintenances/Delete/{id}");
        }
    }
}
