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
    public class ContactLavadoGratisViewModel
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
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Display(Name = "Apellido(s)")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        public string IdentificationCard { get; set; }

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
        [Display(Name = "Correo eletrónico")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Display(Name = "Marca del vehículo")]
        public string VehicleBrand { get; set; }

        /// <summary>
        /// Gets model Of vehicle
        /// </summary>
        /// 
        [Display(Name = "Modelo del vehículo")]
        public string Model { get; set; }


        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Año")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Placa")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Número de factura")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        public string Bill { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Fecha de factura")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        public string BillDate { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        //[Display(Name = "Ciudad")]
        //[Required(ErrorMessage = "{0} es obligatorio.")]
        //public string City { get; set; }

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


        /// Propedades adicionales
        /// 
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "{0} es obligatorio.")]
        public string Apellidos { get; set; }


    }
}
