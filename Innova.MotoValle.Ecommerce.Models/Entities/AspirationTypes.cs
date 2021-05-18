// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AspirationTypes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Aspiration Types
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Aspiration Types
    /// </summary>
    public partial class AspirationTypes
    {
        public AspirationTypes()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the aspiration types identifier.
        /// </summary>
        /// <value>
        /// The aspiration types identifier.
        /// </value>
        public int AspirationTypesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the aspiration type.
        /// </summary>
        /// <value>
        /// The name of the aspiration type.
        /// </value>
        [Required]
        [Display(Name = "Aspiration Type Name")]
        [StringLength(255)]
        public string AspirationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
