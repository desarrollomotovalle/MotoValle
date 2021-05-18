// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignLeadsApi.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Campaign Leads Api Helper
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
    /// Campaign Leads Api Helper
    /// </summary>
    internal class CampaignLeadsApi
    {
        /// <summary>
        /// The HTTP client instance
        /// </summary>
        private readonly HttpClient _httpClientInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignLeadsApi" /> class.
        /// </summary>
        /// <param name="httpClientInstance">The HTTP client instance.</param>
        public CampaignLeadsApi(HttpClient httpClientInstance)
        {
            this._httpClientInstance = httpClientInstance;
        }

        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>List of NewsLetter Subscriptions</returns>
        public async Task<List<CampaignLeads>> GetAllRecords()
        {
            var campaignLeads = new List<CampaignLeads>();
            var response = await this._httpClientInstance.GetAsync($"api/CampaignLeads");
            if (response.IsSuccessStatusCode)
            {
                string newsletterSubscriptionsString = await response.Content.ReadAsStringAsync();
                campaignLeads = JsonConvert.DeserializeObject<List<CampaignLeads>>(newsletterSubscriptionsString);
                return campaignLeads;
            }

            return campaignLeads;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>NewsLetter Subscriptions pictures by id</returns>
        public async Task<CampaignLeads> GetRecord(long id)
        {
            var campaignLeads = new CampaignLeads();
            var response = await this._httpClientInstance.GetAsync($"api/CampaignLeads/{id}");
            if (response.IsSuccessStatusCode)
            {
                string campaignLeadsString = await response.Content.ReadAsStringAsync();
                campaignLeads = JsonConvert.DeserializeObject<CampaignLeads>(campaignLeadsString);
                return campaignLeads;
            }

            return campaignLeads;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        /// <param name="campaignLeads">The item.</param>
        /// <returns>Newsletter Subscriptions created</returns>
        public async Task<CampaignLeads> CreateRecord(CampaignLeads campaignLeads)
        {
            var response = await this._httpClientInstance.PostAsync($"api/CampaignLeads/Create", new StringContent(JsonConvert.SerializeObject(campaignLeads), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string campaignLeadsString = await response.Content.ReadAsStringAsync();
                var campaignLeadsCreated = JsonConvert.DeserializeObject<CampaignLeads>(campaignLeadsString);
                return campaignLeadsCreated;
            }

            return campaignLeads;
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedItem">The updated item.</param>
        public async Task UpdateRecord(long id, CampaignLeads updatedItem)
        {
            await this._httpClientInstance.PutAsync($"api/CampaignLeads/Update/{id}", new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task DeleteRecord(long id)
        {
            await this._httpClientInstance.DeleteAsync($"api/CampaignLeads/Delete/{id}");
        }
    }
}
