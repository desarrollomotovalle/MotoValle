// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChamberDimensions.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Chamber Dimensions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Chamber Dimensions
    /// </summary>
    public partial class ChamberDimensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChamberDimensions"/> class.
        /// </summary>
        public ChamberDimensions()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the hitch system identifier.
        /// </summary>
        /// <value>
        /// The hitch system identifier.
        /// </value>
        public int ChamberDimensionsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hitch pin.
        /// </summary>
        /// <value>
        /// The name of the hitch pin.
        /// </value>
        [Required]
        [Display(Name = "Chamber Dimension Name")]
        [StringLength(255)]
        public string ChamberDimensionName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
