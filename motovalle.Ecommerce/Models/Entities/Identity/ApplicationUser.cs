// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="Innova Marketing Systems S.A.S">
//    © All rights reserved
// </copyright>
// <summary>
//   Identity user extended model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.Entities.Identity
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Identity user extended model
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser{System.Int32}" />
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class ApplicationUser : IdentityUser<int>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [PersonalData, Required, StringLength(70)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [PersonalData, Required, StringLength(70)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [PersonalData, Required, StringLength(140)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the alternate number.
        /// </summary>
        /// <value>
        /// The alternate number.
        /// </value>
        [PersonalData, StringLength(45)]
        public string AlternateNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [PersonalData, Required, StringLength(60)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [PersonalData, Required, StringLength(45)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [PersonalData, Required, StringLength(45)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [PersonalData, StringLength(45)]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [PersonalData, Required, StringLength(45)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [PersonalData, Required, StringLength(45)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the register date.
        /// </summary>
        /// <value>
        /// The register date.
        /// </value>
        [PersonalData]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [PersonalData, Required, StringLength(45)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the customers identifier.
        /// </summary>
        /// <value>
        /// The customers identifier.
        /// </value>
        public int? CustomersId { get; set; }
    }
}
