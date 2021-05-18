// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DriveTrains.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Drive Trains model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Drive Trains model
    /// </summary>
    public partial class DriveTrains
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DriveTrains"/> class.
        /// </summary>
        public DriveTrains()
        {
            this.Products = new HashSet<Products>();
        }

        /// <summary>
        /// Gets or sets the drive trains identifier.
        /// </summary>
        /// <value>
        /// The drive trains identifier.
        /// </value>
        [Required]
        public int DriveTrainsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the drive train.
        /// </summary>
        /// <value>
        /// The name of the drive train.
        /// </value>
        [Required]
        public string DriveTrainName { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Products> Products { get; set; }
    }
}