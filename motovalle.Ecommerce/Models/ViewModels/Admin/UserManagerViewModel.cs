// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManagerViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
// User Manager View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Admin
{
    using motovalle.Ecommerce.Models.Entities.Identity;
    using System.Collections.Generic;

    /// <summary>
    /// User Manager View Model
    /// </summary>
    public class UserManagerViewModel
    {
        /// <summary>
        /// Gets or sets the application user.
        /// </summary>
        /// <value>
        /// The application user.
        /// </value>
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IList<string> Roles { get; set; }
    }
}