// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TurningRadiusTypes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Conditioning Systems
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Conditioning Systems
    /// </summary>
    public partial class TurningRadiusTypes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurningRadiusTypes"/> class.
        /// </summary>
        public TurningRadiusTypes()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the hitch system identifier.
        /// </summary>
        /// <value>
        /// The hitch system identifier.
        /// </value>
        public int TurningRadiusTypesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hitch pin.
        /// </summary>
        /// <value>
        /// The name of the hitch pin.
        /// </value>
        [Required]
        [Display(Name = "Turning Radius Name")]
        [StringLength(255)]
        public string TurningRadiusTypeName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
