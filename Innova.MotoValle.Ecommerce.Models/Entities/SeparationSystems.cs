// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeparationSystems.cs" company="Innova Marketing Systems S.A.S">
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
    public partial class SeparationSystems
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeparationSystems"/> class.
        /// </summary>
        public SeparationSystems()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the separation systems identifier.
        /// </summary>
        /// <value>
        /// The separation systems identifier.
        /// </value>
        public int SeparationSystemsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the rear tires.
        /// </summary>
        /// <value>
        /// The name of the rear tires.
        /// </value>
        [Required]
        [Display(Name = "Separation System Name")]
        [StringLength(255)]
        public string SeparationSystemName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
