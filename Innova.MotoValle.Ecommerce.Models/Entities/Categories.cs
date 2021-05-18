// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Categories.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Categories model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Categories model
    /// </summary>
    public partial class Categories
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Categories" /> class.
        /// </summary>
        public Categories()
        {
            this.CategoryPictures = new HashSet<CategoryPictures>();
            this.Products = new HashSet<Products>();
            this.Models = new HashSet<Models>();
        }

        /// <summary>
        /// Gets or sets the categories identifier.
        /// </summary>
        /// <value>
        /// The categories identifier.
        /// </value>
        public int CategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category pictures.
        /// </summary>
        /// <value>
        /// The category pictures.
        /// </value>
        public virtual ICollection<CategoryPictures> CategoryPictures { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual ICollection<Models> Models { get; set; }
    }
}