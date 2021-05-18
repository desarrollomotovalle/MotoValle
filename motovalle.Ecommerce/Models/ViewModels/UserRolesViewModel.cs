// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRolesViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  User Roles View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using motovalle.Ecommerce.Models.Entities.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// User Roles View Model
    /// </summary>
    public class UserRolesViewModel
    {
        /// <summary>
        /// Gets or sets the identifier user.
        /// </summary>
        /// <value>
        /// The identifier user.
        /// </value>
        [Required]
        public long IdUser { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the identifier role.
        /// </summary>
        /// <value>
        /// The identifier role.
        /// </value>
        [Required]
        public long IdRole { get; set; }

        /// <summary>
        /// Gets or sets the identifier role new.
        /// </summary>
        /// <value>
        /// The identifier role new.
        /// </value>
        [Required]
        public long IdRoleNew { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        [NotMapped]
        public List<ApplicationRole> Roles { get; set; }
    }
}
