// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterestedCustomers.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// Interested Customers Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Interested Customers Model
    /// </summary>
    public partial class InterestedCustomers
    {
        /// <summary>
        /// Gets or sets the interested customers identifier.
        /// </summary>
        /// <value>
        /// The interested customers identifier.
        /// </value>
        public int InterestedCustomersId { get; set; }

        /// <summary>
        /// Gets or sets the create on.
        /// </summary>
        /// <value>
        /// The create on.
        /// </value>
        [DataType(DataType.DateTime)]
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(105)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [StringLength(105)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the fk inventory items identifier.
        /// </summary>
        /// <value>
        /// The fk inventory items identifier.
        /// </value>
        [Display(Name = "Inventory Item")]
        public int FkInventoryItemsId { get; set; }

        /// <summary>
        /// Gets or sets the managed.
        /// </summary>
        /// <value>
        /// The managed.
        /// </value>
        public sbyte? Managed { get; set; }

        /// <summary>
        /// Gets or sets the managed on.
        /// </summary>
        /// <value>
        /// The managed on.
        /// </value>
        public DateTime? ManagedOn { get; set; }

        /// <summary>
        /// Gets or sets the managed for.
        /// </summary>
        /// <value>
        /// The managed for.
        /// </value>
        public string ManagedFor { get; set; }

        /// <summary>
        /// Gets or sets the retake.
        /// </summary>
        /// <value>
        /// The retake.
        /// </value>
        public string Retake { get; set; }

        /// <summary>
        /// Gets or sets the headquarter.
        /// </summary>
        /// <value>
        /// The headquarter.
        /// </value>
        public string Headquarter { get; set; }

        /// <summary>
        /// Gets or sets the inventory items.
        /// </summary>
        /// <value>
        /// The inventory items.
        /// </value>
        [Display(Name = "Inventory Item")]
        public virtual InventoryItems FkInventoryItems { get; set; }
    }
}
