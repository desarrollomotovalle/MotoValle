// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItemPictures.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Inventory Item Pictures model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Inventory Item Pictures model
    /// </summary>
    public partial class InventoryItemPictures
    {
        /// <summary>
        /// Gets or sets the inventory item pictures identifier.
        /// </summary>
        /// <value>
        /// The inventory item pictures identifier.
        /// </value>
        public int InventoryItemPicturesId { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items identifier.
        /// </summary>
        /// <value>
        /// The fk inventory items identifier.
        /// </value>
        [Required]
        [Display(Name = "Inventory Item")]
        public int FkInventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the picture.
        /// </summary>
        /// <value>
        /// The name of the picture.
        /// </value>
        [Display(Name = "Picture Name")]
        public string PictureName { get; set; }

        /// <summary>
        /// Gets or sets the picture URL.
        /// </summary>
        /// <value>
        /// The picture URL.
        /// </value>
        [Required]
        [Display(Name = "Picture Path")]
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the width of the picture.
        /// </summary>
        /// <value>
        /// The width of the picture.
        /// </value>
        [Display(Name = "Picture Width")]
        public int? PictureWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the picture.
        /// </summary>
        /// <value>
        /// The height of the picture.
        /// </value>
        [Display(Name = "Picture Height")]
        public int? PictureHeight { get; set; }

        /// <summary>
        /// Gets or sets the alternate text.
        /// </summary>
        /// <value>
        /// The alternate text.
        /// </value>
        [Required]
        [Display(Name = "Alternate Text")]
        public string AlternateText { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items.
        /// </summary>
        /// <value>
        /// The fk inventory items.
        /// </value>
        [Display(Name = "Inventory Item")]
        public virtual InventoryItems FkInventoryItems { get; set; }
    }
}
