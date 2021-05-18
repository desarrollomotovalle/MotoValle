// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transmissions.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Transmissions model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Transmissions model
    /// </summary>
    public partial class Transmissions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transmissions"/> class.
        /// </summary>
        public Transmissions()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the transmissions identifier.
        /// </summary>
        /// <value>
        /// The transmissions identifier.
        /// </value>
        public int TransmissionsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the transmission.
        /// </summary>
        /// <value>
        /// The name of the transmission.
        /// </value>
        [Required]
        [Display(Name = "Transmission Name"), StringLength(255)]
        public string TransmissionName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}
