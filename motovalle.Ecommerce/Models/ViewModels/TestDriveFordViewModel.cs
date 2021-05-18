// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDriveFord.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Test Drive Ford View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Test Drive Ford View Model
    /// </summary>
    public class TestDriveFordViewModel
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "{0} es requerido.")]
        [StringLength(120)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "{0} es requerido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Celular")]
        [Required(ErrorMessage = "{0} es requerido.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the headquarter.
        /// </summary>
        /// <value>
        /// The headquarter.
        /// </value>
        [Display(Name = "Sede")]
        [Required(ErrorMessage = "Debe seleccionar una {0}")]
        [MinLength(1, ErrorMessage = "Debe seleccionar una {0}")]
        public string Headquarter { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [Display(Name = "Tu próximo Ford")]
        [Required(ErrorMessage = "Debe seleccionar un vehículo")]
        [MinLength(1, ErrorMessage = "Debe seleccionar un vehículo")]
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Display(Name = "Fecha y hora tentativa")]
        [Required(ErrorMessage = "{0} es requerida.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

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
