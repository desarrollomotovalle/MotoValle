// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InterestedCustomerViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Interested Customer View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using global::Ecommerce.Models.Entities;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Interested Customer View Model
    /// </summary>
    /// <seealso cref="Ecommerce.Models.Entities.InterestedCustomers" />
    public class InterestedCustomerViewModel : InterestedCustomers
    {
        /// <summary>
        /// Gets or sets a value indicating whether [habeas data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [habeas data]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Acepto pólíticas de privacidad")]
        [Required(ErrorMessage = "Debes aceptar las políticas de privacidad")]
        public bool HabeasData { get; set; }
    }
}