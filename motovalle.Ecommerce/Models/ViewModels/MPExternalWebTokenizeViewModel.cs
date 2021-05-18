// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MPExternalWebTokenizeViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pago External Web Tokenize View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Mercado Pago External Web Tokenize View Model
    /// </summary>
    public class MPExternalWebTokenizeViewModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [EmailAddress(ErrorMessage = "{0} no es una dirección válida")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the type of the identification.
        /// </summary>
        /// <value>
        /// The type of the identification.
        /// </value>
        [Display(Name = "Tipo de Docuemnto")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string IdentificationType { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        /// <value>
        /// The identification number.
        /// </value>
        [Display(Name = "Número de documento")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the ship to zipcode.
        /// </summary>
        /// <value>
        /// The ship to zipcode.
        /// </value>
        [Display(Name = "Código postal de envío")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string ShipToZipcode { get; set; }

        /// <summary>
        /// Gets or sets the state of the ship to.
        /// </summary>
        /// <value>
        /// The state of the ship to.
        /// </value>
        [Display(Name = "Departamento de envío")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string ShipToState { get; set; }

        /// <summary>
        /// Gets or sets the ship to city.
        /// </summary>
        /// <value>
        /// The ship to city.
        /// </value>
        [Display(Name = "Ciudad de envío")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(45, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string ShipToCity { get; set; }

        /// <summary>
        /// Gets or sets the ship to address.
        /// </summary>
        /// <value>
        /// The ship to address.
        /// </value>
        [Display(Name = "Dirección de envío")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string ShipToAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Número de contacto")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(70, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        [Phone(ErrorMessage = "{0} no contiene un formato válido")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Descripción Compra")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [StringLength(100, ErrorMessage = "{0} debe contener máximo {1} caracteres")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the inventory item identifier.
        /// </summary>
        /// <value>
        /// The inventory item identifier.
        /// </value>
        [Display(Name = "Ítem a pagar")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} no es válido")]
        public int InventoryItemId { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Display(Name = "Nombre Completo")]
        public string FullName => $"{this.FirstName} - {this.LastName}";
    }
}
