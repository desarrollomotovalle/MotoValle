// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Colors.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Colors model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Colors model
    /// </summary>
    public partial class Colors
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Colors"/> class.
        /// </summary>
        public Colors()
        {
            this.InventoryItems = new HashSet<InventoryItems>();
        }

        /// <summary>
        /// Gets or sets the colors identifier.
        /// </summary>
        /// <value>
        /// The colors identifier.
        /// </value>
        [Required]
        public int ColorsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        /// <value>
        /// The name of the color.
        /// </value>
        [Required]
        [Display(Name = "Color Name")]
        public string ColorName { get; set; }

        /// <summary>
        /// Gets or sets the fk makes identifier.
        /// </summary>
        /// <value>
        /// The fk makes identifier.
        /// </value>
        [Display(Name = "Make")]
        public int? FkMakesId { get; set; }

        /// <summary>
        /// Gets or sets the fk makes.
        /// </summary>
        /// <value>
        /// The fk makes.
        /// </value>
        [Display(Name = "Make")]
        public virtual Makes FkMakes { get; set; }

        /// <summary>
        /// Gets or sets the inventory items.
        /// </summary>
        /// <value>
        /// The inventory items.
        /// </value>
        public virtual ICollection<InventoryItems> InventoryItems { get; set; }
    }
}