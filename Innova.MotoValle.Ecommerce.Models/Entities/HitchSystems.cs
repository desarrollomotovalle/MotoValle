// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HitchSystems.cs" company="Innova Marketing Systems S.A.S">
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
    public partial class HitchSystems
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HitchSystems"/> class.
        /// </summary>
        public HitchSystems()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the hitch system identifier.
        /// </summary>
        /// <value>
        /// The hitch system identifier.
        /// </value>
        public int HitchSystemsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hitch system.
        /// </summary>
        /// <value>
        /// The name of the hitch system.
        /// </value>
        [Required]
        [Display(Name = "Hitch System Name")]
        [StringLength(255)]
        public string HitchSystemName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
