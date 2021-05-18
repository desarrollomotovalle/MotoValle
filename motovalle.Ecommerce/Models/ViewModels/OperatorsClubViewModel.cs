// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorsClubViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Operators Club View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Operators Club View Model
    /// </summary>
    public class OperatorsClubViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Nombre es requerido")]
        [Display(Name = "Nombres"), StringLength(105)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "Apellidos son requeridos")]
        [Display(Name = "Apellidos"), StringLength(105)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Required(ErrorMessage = "Celular es requerido")]
        [Display(Name = "Celular"), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Correo es requerido")]
        [Display(Name = "Correo"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [Required(ErrorMessage = "Fecha de nacimiento es requerida")]
        [Display(Name = "Fecha de nacimiento"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the serial.
        /// </summary>
        /// <value>
        /// The serial.
        /// </value>
        [Required(ErrorMessage = "Serie del Tractor - Modelo es Requerido")]
        [Display(Name = "Serie del Tractor - Modelo"), StringLength(200)]
        public string Serial { get; set; }

        /// <summary>
        /// Gets or sets the chasis number.
        /// </summary>
        /// <value>
        /// The chasis number.
        /// </value>
        [Required(ErrorMessage = "Número de Chasis es Requerido")]
        [Display(Name = "Número de chasis"), StringLength(200)]
        public string ChasisNumber { get; set; }

        /// <summary>
        /// Gets or sets the ubication.
        /// </summary>
        /// <value>
        /// The ubication.
        /// </value>
        [Required(ErrorMessage = "Ubicación, ciudad es Requerido")]
        [Display(Name = "Ubicación, ciudad"), StringLength(200)]
        public string Ubication { get; set; }

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
