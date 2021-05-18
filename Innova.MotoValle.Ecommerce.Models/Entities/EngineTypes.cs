// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineTypes.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Engine Types model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    ///  Engine Types model
    /// </summary>
    public partial class EngineTypes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineTypes"/> class.
        /// </summary>
        public EngineTypes()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the engine types identifier.
        /// </summary>
        /// <value>
        /// The engine types identifier.
        /// </value>
        [Required]
        public int EngineTypesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the engine type.
        /// </summary>
        /// <value>
        /// The name of the engine type.
        /// </value>
        [Required]
        [Display(Name = "Engine Type Name")]
        public string EngineTypeName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
