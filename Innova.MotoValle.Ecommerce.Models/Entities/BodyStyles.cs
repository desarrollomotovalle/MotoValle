// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BodyStyles.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Body Styles model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Body Styles model
    /// </summary>
    public partial class BodyStyles
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyStyles"/> class.
        /// </summary>
        public BodyStyles()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the body styles identifier.
        /// </summary>
        /// <value>
        /// The body styles identifier.
        /// </value>
        public int BodyStylesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the body style.
        /// </summary>
        /// <value>
        /// The name of the body style.
        /// </value>
        [Required]
        [Display(Name = "Body Style Name")]
        public string BodyStyleName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}