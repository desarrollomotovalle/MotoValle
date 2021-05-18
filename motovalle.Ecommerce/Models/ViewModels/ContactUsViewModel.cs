// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactUsViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Contact Us View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using motovalle.Ecommerce.Models;

    /// <summary>
    /// Contact Us View Model
    /// </summary>
    public class ContactUsViewModel
    {
        /// <summary>
        /// Gets or sets the type of the contact us.
        /// </summary>
        /// <value>
        /// The type of the contact us.
        /// </value>
        [Required(ErrorMessage = "Debe seleccionar un tema", AllowEmptyStrings = false)]
        public ContactUsType ContactUsType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Debe ingresar un Nombre")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar una dirección de correo electrónico")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Required(ErrorMessage = "Debe ingresar el mensaje")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [data treatment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [data treatment]; otherwise, <c>false</c>.
        /// </value>
        [Required(ErrorMessage = "Acepte el consentimiento de tratamiento de datos")]
        public bool DataTreatment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [terms and conditions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [terms and conditions]; otherwise, <c>false</c>.
        /// </value>
        [Required(ErrorMessage = "Acepte los terminos y condiciones")]
        public bool TermsAndConditions { get; set; }
    }
}