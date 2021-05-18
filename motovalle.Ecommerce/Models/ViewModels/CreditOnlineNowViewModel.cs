// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreditOnlineNowViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Credit Online Now View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Credit Online Now View Model
    /// </summary>
    public class CreditOnlineNowViewModel : SantanderBankCreditOnlineViewModel
    {
        #region General Information
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no es una dirección válida.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0} no es una dirección válida.")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Número de contacto")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(15, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the alternate phone number.
        /// </summary>
        /// <value>
        /// The alternate phone number.
        /// </value>
        [Display(Name = "Número de contacto alterno")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(15, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        [DataType(DataType.PhoneNumber)]
        public string AlternatePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [Display(Name = "Empresa donde labora")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string CompanyName { get; set; }
        #endregion

        #region Addresses
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [Display(Name = "Código postal")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Display(Name = "País")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string Country { get; set; }
        #endregion

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
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName => $"{this.Name} {this.LastName}";
    }
}
