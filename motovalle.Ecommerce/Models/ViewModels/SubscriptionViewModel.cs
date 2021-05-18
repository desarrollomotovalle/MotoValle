// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Subscription View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Subscription View Model
    /// </summary>
    public class SubscriptionViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Debe ingresar el nombre")]
        [Display(Name = "Nombre Completo")]
        [MinLength(5, ErrorMessage = "Nombre debe contener mínimo {0} caracteres"), MaxLength(105, ErrorMessage = "Nombre debe contener máximo {0} caracteres")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Debe ingresar el correo electrónico")]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de correo incorrecto")]
        [EmailAddress(ErrorMessage = "Formato de correo incorrecto")]
        public string Email { get; set; }

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
