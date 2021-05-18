// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoriesApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Categories Api helper
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
    /// Categories Api helper
    /// </summary>
    internal class CategoriesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public CategoriesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of categories</returns>
        public async Task<List<Categories>> GetAllRecords()
        {
            var categories = new List<Categories>();
            var response = await this._httpClientInstance.GetAsync($"api/Category");
            if (response.IsSuccessStatusCode)
            {
                string categoriesString = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Categories>>(categoriesString);
                return categories;
            }

            return categories;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by Id</returns>
        public async Task<Categories> GetRecord(long id)
        {
            var categories = new Categories();
            var response = await this._httpClientInstance.GetAsync($"api/Category/{id}");
            if (response.IsSuccessStatusCode)
            {
                var categoriesString = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<Categories>(categoriesString);
                return categories;
            }

            return categories;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Category by Id</returns>
        public async Task<Categories> GetForMakeAndModel(long makesId ,long modelsId)
        {
            var categories = new Categories();
            var response = await this._httpClientInstance.GetAsync($"api/Category/Make/{makesId}/Model/{modelsId}");
            if (response.IsSuccessStatusCode)
            {
                var categoriesString = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<Categories>(categoriesString);
                return categories;
            }

            return categories;
        }

        /// <summary>
        /// Gets the products for category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Product for category filtered</returns>
        public async Task<List<ProductAndCategoryBase>> GetProductsForCategory(int categoryId)
        {
            var productsForCategory = new List<ProductAndCategoryBase>();
            var response = await this._httpClientInstance.GetAsync($"api/Category/{categoryId}/products");
            if (response.IsSuccessStatusCode)
            {
                var productsForCategoryString = await response.Content.ReadAsStringAsync();
                productsForCategory = JsonConvert.DeserializeObject<List<ProductAndCategoryBase>>(productsForCategoryString);
                return productsForCategory;
            }

            return productsForCategory;
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>Category created</returns>
        public async Task<Categories> CreateRecord(Categories category)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Category/Create", new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string categoryString = await response.Content.ReadAsStringAsync();
                var categoryCreated = JsonConvert.DeserializeObject<Categories>(categoryString);
                return categoryCreated;
            }

            return category;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Categories updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Category/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Category/Delete/{id}");
        }
    }
}
