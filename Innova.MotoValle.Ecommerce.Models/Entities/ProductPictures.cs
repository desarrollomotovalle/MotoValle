// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductPictures.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//   Product Pictures entity model
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Product Pictures
    /// </summary>
    public partial class ProductPictures
    {
        /// <summary>
        /// Gets or sets the product pictures identifier.
        /// </summary>
        /// <value>
        /// The product pictures identifier.
        /// </value>
        public int ProductPicturesId { get; set; }

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
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the fk products identifier.
        /// </summary>
        /// <value>
        /// The fk products identifier.
        /// </value>
        public int FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the type of the picture.
        /// </summary>
        /// <value>
        /// The type of the picture.
        /// </value>
        public string PictureType { get; set; }

        /// <summary>
        /// Gets or sets the fk products.
        /// </summary>
        /// <value>
        /// The fk products.
        /// </value>
        public virtual Products FkProducts { get; set; }
    }
}
