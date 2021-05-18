// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseFormViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Base Form View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base Form View Model
    /// </summary>
    public class BaseFormViewModel
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
