// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Models.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Models model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Models model
    /// </summary>
    public partial class Models
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Models" /> class.
        /// </summary>
        public Models()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the models identifier.
        /// </summary>
        /// <value>
        /// The models identifier.
        /// </value>
        public int ModelsId { get; set; }

        /// <summary>
        /// Gets or sets the fk makes identifier.
        /// </summary>
        /// <value>
        /// The fk makes identifier.
        /// </value>
        [Required]
        public int FkMakesId { get; set; }

        /// <summary>
        /// Gets or sets the fk categories identifier.
        /// </summary>
        /// <value>
        /// The fk categories identifier.
        /// </value>
        [Required]
        public int FkCategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        /// <value>
        /// The name of the model.
        /// </value>
        [Required]
        [Display(Name = "Model Name")]
        [StringLength(45)]
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the fk makes.
        /// </summary>
        /// <value>
        /// The fk makes.
        /// </value>
        [Display(Name = "Make")]
        public virtual Makes FkMakes { get; set; }

        /// <summary>
        /// Gets or sets the fk category.
        /// </summary>
        /// <value>
        /// The fk categories.
        /// </value>
        [Display(Name = "Category")]
        public virtual Categories FkCategories { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [Display(Name = "Products")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
