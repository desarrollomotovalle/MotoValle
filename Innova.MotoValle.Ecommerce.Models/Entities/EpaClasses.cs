// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EpaClasses.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Epa Classes model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Epa Classes model
    /// </summary>
    public partial class EpaClasses
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpaClasses"/> class.
        /// </summary>
        public EpaClasses()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the epa classes identifier.
        /// </summary>
        /// <value>
        /// The epa classes identifier.
        /// </value>
        [Required]
        public int EpaClassesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the epa class.
        /// </summary>
        /// <value>
        /// The name of the epa class.
        /// </value>
        [Required]
        public string EpaClassName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
