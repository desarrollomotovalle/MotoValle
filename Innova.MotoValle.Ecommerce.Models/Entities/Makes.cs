// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Makes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Makes model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Makes model
    /// </summary>
    public partial class Makes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Makes"/> class.
        /// </summary>
        public Makes()
        {
            this.Colors = new HashSet<Colors>();
            this.Models = new HashSet<Models>();
            this.Products = new HashSet<Products>();
            this.HeroImages = new HashSet<HeroImages>();
        }

        /// <summary>
        /// Gets or sets the makes identifier.
        /// </summary>
        /// <value>
        /// The makes identifier.
        /// </value>
        public int MakesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the make.
        /// </summary>
        /// <value>
        /// The name of the make.
        /// </value>
        [Required]
        public string MakeName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>
        /// The colors.
        /// </value>
        public virtual ICollection<Colors> Colors { get; set; }

        /// <summary>
        /// Gets or sets the models.
        /// </summary>
        /// <value>
        /// The models.
        /// </value>
        public virtual ICollection<Models> Models { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }

        /// <summary>
        /// Gets or sets the hero images.
        /// </summary>
        /// <value>
        /// The hero images.
        /// </value>
        public virtual ICollection<HeroImages> HeroImages { get; set; }
    }
}
