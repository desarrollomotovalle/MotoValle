// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Warranties.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Warranties model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Warranties model
    /// </summary>
    public partial class Warranties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Warranties"/> class.
        /// </summary>
        public Warranties()
        {
            this.InventoryItems = new HashSet<InventoryItems>();
        }

        /// <summary>
        /// Gets or sets the warranties identifier.
        /// </summary>
        /// <value>
        /// The warranties identifier.
        /// </value>
        public int WarrantiesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the warranty.
        /// </summary>
        /// <value>
        /// The name of the warranty.
        /// </value>
        [Required]
        [Display(Name = "Warranty Name"), StringLength(45)]
        public string WarrantyName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the inventory items.
        /// </summary>
        /// <value>
        /// The inventory items.
        /// </value>
        public virtual ICollection<InventoryItems> InventoryItems { get; set; }
    }
}
