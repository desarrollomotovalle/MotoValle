// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HitchPines.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Hitch Systems
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Hitch Systems
    /// </summary>
    public partial class HitchPines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HitchPines"/> class.
        /// </summary>
        public HitchPines()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the hitch system identifier.   
        /// </summary>
        /// <value>
        /// The hitch system identifier.
        /// </value>
        public int HitchPinesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hitch pin.
        /// </summary>
        /// <value>
        /// The name of the hitch pin.
        /// </value>
        [Required]
        [Display(Name = "Hitch Pin Name")]
        [StringLength(255)]
        public string HitchPinName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
