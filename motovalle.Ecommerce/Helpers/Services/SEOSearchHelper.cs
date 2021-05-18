// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SEOSearchHelper.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  SEO Search Helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.Services
{
    using global::Ecommerce.DAL.Repos.Interfaces;
    using global::Ecommerce.Models.Entities;

    /// <summary>
    /// SEO Search Helper
    /// </summary>
    public class SEOSearchHelper
    {
        /// <summary>
        /// The seo repo
        /// </summary>
        private readonly ISearchEngineOptimizationsRepo _seoRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SEOSearchHelper" /> class.
        /// </summary>
        /// <param name="seoRepo">The seo repo.</param>
        public SEOSearchHelper(ISearchEngineOptimizationsRepo seoRepo)
        {
            this._seoRepo = seoRepo;
        }

        /// <summary>
        /// Gets the seo.
        /// </summary>
        /// <param name="pagePath">The page path.</param>
        /// <returns>Search Engine Optimization instance</returns>
        public SearchEngineOptimizations GetSEO(string pagePath)
        {
            if (!string.IsNullOrEmpty(pagePath))
            {
                var blogTemplatePath = "/BLOG/PAGE";
                //var blogpostTemplatePath = "/BLOG/POST/";
                //if (pagePath.ToUpper().Contains(blogpostTemplatePath))
                //{
                //    var indexToSearch = pagePath.IndexOf('/', blogpostTemplatePath.Length);
                //    if (indexToSearch > 0)
                //    {
                //        pagePath = pagePath.Substring(0, indexToSearch);
                //    }
                //}

                if (pagePath.ToUpper().Contains(blogTemplatePath))
                {
                    pagePath = "/BLOG";
                }

                var seoResult = this._seoRepo.GetRecordByPagePath(pagePath.ToUpper());
                if (seoResult == null)
                {
                    return this._seoRepo.GetRecordByPagePath("Default");
                }

                return seoResult;
            }

            return this._seoRepo.GetRecordByPagePath("Default");
        }
    }
}
