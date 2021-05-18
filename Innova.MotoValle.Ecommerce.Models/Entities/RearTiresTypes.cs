// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RearTiresTypes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Rear Tires Types
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Rear Tires Types
    /// </summary>
    public partial class RearTiresTypes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RearTiresTypes"/> class.
        /// </summary>
        public RearTiresTypes()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the rear tires identifier.
        /// </summary>
        /// <value>
        /// The rear tires identifier.
        /// </value>
        public int RearTiresTypesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the rear tires.
        /// </summary>
        /// <value>
        /// The name of the rear tires.
        /// </value>
        [Required]
        [Display(Name = "Rear Tires Type")]
        [StringLength(255)]
        public string RearTiresTypeName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
