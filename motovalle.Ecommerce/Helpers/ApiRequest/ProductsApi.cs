// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Products Api helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.ApiRequest
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Products Api helper
    /// </summary>
    internal class ProductsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MakesApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public ProductsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<List<Products>> GetAllRecords()
        {
            var products = new List<Products>();
            var response = await this._httpClientInstance.GetAsync("api/Products");
            if (response.IsSuccessStatusCode)
            {
                string productsString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(productsString);
                return products;
            }

            return products;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get product for id</returns>
        public async Task<Products> GetRecord(long id)
        {
            var products = new Products();
            var response = await this._httpClientInstance.GetAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                string productsString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<Products>(productsString);
                return products;
            }

            return products;
        }

        /// <summary>
        /// Gets the product with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>List product details</returns>
        public async Task<List<ProductWithDetails>> GetProductWithDetails(int productId)
        {
            var productsWithDetails = new List<ProductWithDetails>();
            var response = await this._httpClientInstance.GetAsync($"api/Products/{productId}/Details");
            if (response.IsSuccessStatusCode)
            {
                var productsWithDetailsString = await response.Content.ReadAsStringAsync();
                productsWithDetails = JsonConvert.DeserializeObject<List<ProductWithDetails>>(productsWithDetailsString);
                return productsWithDetails;
            }

            return productsWithDetails;
        }

        /// <summary>
        /// Gets the inventory for product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory For Product
        /// </returns>
        public async Task<List<InventoryForProduct>> GetInventoryForProduct(int productId, int makesId)
        {
            var inventoryItems = new List<InventoryForProduct>();
            var response = await this._httpClientInstance.GetAsync($"api/Products/{productId}/Make/{makesId}/Inventory");
            if (response.IsSuccessStatusCode)
            {
                var inventoryItemsString = await response.Content.ReadAsStringAsync();
                inventoryItems = JsonConvert.DeserializeObject<List<InventoryForProduct>>(inventoryItemsString);
            }

            return inventoryItems;
        }

        /// <summary>
        /// Gets the inventory and product base with details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="inventoryItemsId">The inventory items identifier.</param>
        /// <param name="makesId">The makes identifier.</param>
        /// <returns>
        /// List of Inventory and product base with details
        /// </returns>
        public async Task<List<InventoryAndProductBaseWithDetails>> GetInventoryAndProductBaseWithDetails(int productId, int inventoryItemsId, int makesId)
        {
            var inventoryAndProductBaseWithDetails = new List<InventoryAndProductBaseWithDetails>();
            var response = await this._httpClientInstance.GetAsync($"api/Products/{productId}/Make/{makesId}/Inventory/{inventoryItemsId}/Details");
            if (response.IsSuccessStatusCode)
            {
                var inventoryAndProductBaseWithDetailsString = await response.Content.ReadAsStringAsync();
                inventoryAndProductBaseWithDetails = JsonConvert.DeserializeObject<List<InventoryAndProductBaseWithDetails>>(inventoryAndProductBaseWithDetailsString);
                return inventoryAndProductBaseWithDetails;
            }

            return inventoryAndProductBaseWithDetails;
        }

        /// <summary>
        /// Productses the with enable maintenance by make.
        /// </summary>
        /// <param name="makesName">Name of the makes.</param>
        /// <returns>Products with maintenance</returns>
        public async Task<List<Products>> ProductsWithEnableMaintenanceByMake(string makesName)
        {
            var products = new List<Products>();
            var response = await this._httpClientInstance.GetAsync($"api/Products/GetProductsWithEnableMaintenanceByMake/{makesName}");
            if (response.IsSuccessStatusCode)
            {
                var productsString = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Products>>(productsString);
                return products;
            }

            return products;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="product">The make.</param>
        /// <returns>Makes created</returns>
        public async Task<Products> CreateRecord(Products product)
        {
            var response = await this._httpClientInstance.PostAsync($"api/Products/Create", new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string productString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Products>(productString);
            }

            return product;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, Products updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/Products/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/Products/Delete/{id}");
        }
    }
}
