// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewsLetterSubscriptions.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  NewsLetter Subscriptions Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// NewsLetter Subscriptions Model
    /// </summary>
    public partial class NewsLetterSubscriptions
    {
        /// <summary>
        /// Gets or sets the newsletter subscriptions identifier.
        /// </summary>
        /// <value>
        /// The newsletter subscriptions identifier.
        /// </value>
        public int NewsletterSubscriptionsId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string Email { get; set; }
    }
}
