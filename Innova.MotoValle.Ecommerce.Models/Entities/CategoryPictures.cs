// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPictures.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Category Pictures model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Category Pictures model
    /// </summary>
    public partial class CategoryPictures
    {
        /// <summary>
        /// Gets or sets the category pictures identifier.
        /// </summary>
        /// <value>
        /// The category pictures identifier.
        /// </value>
        public int CategoryPicturesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the picture.
        /// </summary>
        /// <value>
        /// The name of the picture.
        /// </value>
        public string PictureName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the picture URL.
        /// </summary>
        /// <value>
        /// The picture URL.
        /// </value>
        [Required]
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the fk categories identifier.
        /// </summary>
        /// <value>
        /// The fk categories identifier.
        /// </value>
        public int FkCategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the type of the picture.
        /// </summary>
        /// <value>
        /// The type of the picture.
        /// </value>
        public string PictureType { get; set; }

        /// <summary>
        /// Gets or sets the fk categories.
        /// </summary>
        /// <value>
        /// The fk categories.
        /// </value>
        public virtual Categories FkCategories { get; set; }
    }
}