// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MakesApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Makes Api helper
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
    /// Makes Api helper
    /// </summary>
    internal class MakesApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public MakesApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of Makes</returns>
        public async Task<List<Makes>> GetAllRecords()
        {
            var makes = new List<Makes>();
            var response = await this._httpClientInstance.GetAsync($"api/Makes");
            if (response.IsSuccessStatusCode)
            {
                var makesString = await response.Content.ReadAsStringAsync();
                makes = JsonConvert.DeserializeObject<List<Makes>>(makesString);
                return makes;
            }

            return makes;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get make for id</returns>
        public async Task<Makes> GetRecord(long id)
        {
            var make = new Makes();
            var response = await this._httpClientInstance.GetAsync($"api/Makes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var makeString = await response.Content.ReadAsStringAsync();
                make = JsonConvert.DeserializeObject<Makes>(makeString);
                return make;
            }

            return make;
        }

        /// <summary>
        /// Gets the name of the make for.
        /// </summary>
        /// <param name="makeName">Name of the make.</param>
        /// <returns>Get make for name</returns>
        public async Task<Makes> GetRecord(string makeName)
        {
            var makes = new Makes();
            var response = await this._httpClientInstance.GetAsync($"api/Makes/ByName/{makeName}");
            if (response.IsSuccessStatusCode)
            {
                string makesString = await response.Content.ReadAsStringAsync();
                makes = JsonConvert.DeserializeObject<Makes>(makesString);
                return makes;
            }

            return makes;
        }

        /// <summary>
        /// Gets the models for make.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="modelsId">The models identifier.</param>
        /// <returns>Model for make and model Id</returns>
        public async Task<List<ModelAndCategoryBase>> GetModelsForMake(int makesId, int modelsId)
        {
            var modelAndCategoryBase = new List<ModelAndCategoryBase>();
            var response = await this._httpClientInstance.GetAsync($"api/Makes/{makesId}/models/{modelsId}");
            if (response.IsSuccessStatusCode)
            {
                var modelAndCategoryBaseString = await response.Content.ReadAsStringAsync();
                modelAndCategoryBase = JsonConvert.DeserializeObject<List<ModelAndCategoryBase>>(modelAndCategoryBaseString);
                return modelAndCategoryBase;
            }

            return modelAndCategoryBase;
        }

        /// <summary>
        /// Gets the models for make and category.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoriesId">The categories identifier.</param>
        /// <returns>Models And Category Base for make</returns>
        public async Task<List<ModelAndCategoryBase>> GetModelsForMakeAndCategory(int makesId, int categoriesId)
        {
            var modelsForMakeAndCateogry = new List<ModelAndCategoryBase>();
            var response = await this._httpClientInstance.GetAsync($"api/Makes/{makesId}/category/{categoriesId}");
            if (response.IsSuccessStatusCode)
            {
                var modelsForMakeAndCategoryString = await response.Content.ReadAsStringAsync();
                modelsForMakeAndCateogry = JsonConvert.DeserializeObject<List<ModelAndCategoryBase>>(modelsForMakeAndCategoryString);
                return modelsForMakeAndCateogry;
            }

            return modelsForMakeAndCateogry;
        }

        /// <summary>
        /// Gets the product and make base.
        /// </summary>
        /// <param name="makesId">The makes identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="modelId">The model identifier.</param>
        /// <param name="minYear">The minimum year.</param>
        /// <param name="maxYear">The maximum year.</param>
        /// <param name="minPrice">The minimum price.</param>
        /// <param name="maxPrice">The maximum price.</param>
        /// <returns>
        /// List of products for make
        /// </returns>
        public async Task<List<ProductAndMakeBase>> GetProductAndMakeBase(int makesId, int categoryId = 0, int modelId = 0, int minYear = 0, int maxYear = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            var productsForMake = new List<ProductAndMakeBase>();
            var response = await this._httpClientInstance.GetAsync($"api/Makes/{makesId}/category/{categoryId}/model/{modelId}/products?minYear={minYear}&maxYear={maxYear}&minPrice={minPrice}&maxPrice={maxPrice}");
            if (response.IsSuccessStatusCode)
            {
                var productsForMakeString = await response.Content.ReadAsStringAsync();
                productsForMake = JsonConvert.DeserializeObject<List<ProductAndMakeBase>>(productsForMakeString);
                return productsForMake;
            }

            return productsForMake;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="make">The make.</param>
        /// <returns>Makes created</returns>
        public async Task<Makes> CreateRecord(Makes make)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Makes/Create", new StringContent(JsonConvert.SerializeObject(make), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var makeString = await response.Content.ReadAsStringAsync();
                var makeCreated = JsonConvert.DeserializeObject<Makes>(makeString);
                return makeCreated;
            }

            return make;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Makes updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Makes/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Makes/Delete/{id}");
        }
    }
}