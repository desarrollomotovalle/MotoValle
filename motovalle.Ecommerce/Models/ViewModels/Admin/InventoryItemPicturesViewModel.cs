// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemPicturesViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Item Pictures View Model
// </summary> 
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Admin
{
    using global::Ecommerce.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Inventory Item Pictures View Model
    /// </summary>
    public class InventoryItemPicturesViewModel : InventoryItemPictures
    {
        /// <summary>
        /// Gets or sets the picture file.
        /// </summary>
        /// <value>
        /// The picture file.
        /// </value>
        public IFormFile PictureFile { get; set; }
    }
}
