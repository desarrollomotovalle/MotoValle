// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrontTiresTypes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Front Tires Types
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Front Tires Types
    /// </summary>
    public partial class FrontTiresTypes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RearTiresTypes"/> class.
        /// </summary>
        public FrontTiresTypes()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the rear tires identifier.
        /// </summary>
        /// <value>
        /// The rear tires identifier.
        /// </value>
        public int FrontTiresTypesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the rear tires.
        /// </summary>
        /// <value>
        /// The name of the rear tires.
        /// </value>
        [Required]
        [Display(Name = "Front Tires Type")]
        [StringLength(255)]
        public string FrontTiresTypeName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
