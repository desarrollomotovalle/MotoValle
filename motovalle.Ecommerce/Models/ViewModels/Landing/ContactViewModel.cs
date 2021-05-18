// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Contact View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Landing
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Contact View Model
    /// </summary>
    public class ContactViewModel
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Télefono / Celular")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Display(Name = "Comentarios")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [habeas data].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [habeas data]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Acepto pólíticas de privacidad")]
        [Required(ErrorMessage = "Debes aceptar las políticas de privacidad")]
        public bool HabeasData { get; set; }

        /// <summary>
        /// Gets or sets the campaing number.
        /// </summary>
        /// <value>
        /// The campaing number.
        /// </value>
        public int? CampaingNumber { get; set; }
    }
}
