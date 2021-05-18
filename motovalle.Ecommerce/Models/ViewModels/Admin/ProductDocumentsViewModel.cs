// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Documents View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Admin
{
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Product Documents View Model
    /// </summary>
    public class ProductDocumentsViewModel : ProductDocuments
    {
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile File { get; set; }
    }
}
