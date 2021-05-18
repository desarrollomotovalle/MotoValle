// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemsViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Products View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Admin
{
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Products View Model
    /// </summary>
    public class InventoryItemsViewModel : InventoryItems
    {
        /// <summary>
        /// Gets or sets the cover picture file.
        /// </summary>
        /// <value>
        /// The cover picture file.
        /// </value>
        public IFormFile CoverPictureFile { get; set; }

        /// <summary>
        /// Gets or sets the pictures360 file.
        /// </summary>
        /// <value>
        /// The pictures360 file.
        /// </value>
        public IList<IFormFile> Pictures360File { get; set; }

        /// <summary>
        /// Gets or sets the selected taxes.
        /// </summary>
        /// <value>
        /// The selected taxes.
        /// </value>
        [Display(Name = "Choose Taxes")]
        public List<string> SelectedTaxes { get; set; }
    }
}
