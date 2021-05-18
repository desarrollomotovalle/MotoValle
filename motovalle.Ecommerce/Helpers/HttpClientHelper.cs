// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Api helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.Extensions.Configuration;
    
    /// <summary>
    /// Api helper
    /// </summary>
    internal class HttpClientHelper
    {
        /// <summary>
        /// The timeout
        /// </summary>
        private readonly double _timeout;

        /// <summary>
        /// The API base URL
        /// </summary>
        private readonly string _apiBaseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelper" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public HttpClientHelper(IConfiguration configuration)
        {
            this._timeout = configuration.GetSection("ServicesSettings").GetValue<double>("Timeout");
            this._apiBaseUrl = configuration.GetSection("ServicesSettings").GetValue<string>("APIBaseURL");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>Instance of new HttpClient</returns>
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(this._apiBaseUrl),
                Timeout = TimeSpan.FromMilliseconds(this._timeout)
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}