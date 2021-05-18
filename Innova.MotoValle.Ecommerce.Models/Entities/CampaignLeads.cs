// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CampaignLeads.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Campaign Leads
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Campaign Leads
    /// </summary>
    public partial class CampaignLeads
    {
        /// <summary>
        /// Gets or sets the campaign leads identifier.
        /// </summary>
        /// <value>
        /// The campaign leads identifier.
        /// </value>
        [Display(Name = "Id")]
        public int CampaignLeadsId { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

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
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(45)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Phone]
        [StringLength(45)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        [StringLength(800)]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the campaign.
        /// </summary>
        /// <value>
        /// The campaign.
        /// </value>
        [Required]
        public int Campaign { get; set; }

        /// <summary>
        /// Gets or sets the information from.
        /// </summary>
        /// <value>
        /// The information from.
        /// </value>
        [Required]
        [StringLength(200)]
        [Display(Name = "Info Obteined From")]
        public string InfoFrom { get; set; }

        /// <summary>
        /// Gets or sets the send to.
        /// </summary>
        /// <value>
        /// The send to.
        /// </value>
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email Sent To")]
        public string EmailSentTo { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Required]
        public int Status { get; set; }
    }
}
