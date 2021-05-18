// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutCustomerPErsonalDataViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Customer Personal Data View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Checkout Customer Personal Data View Model
    /// </summary>
    public class CheckoutCustomerDataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutCustomerDataViewModel"/> class.
        /// </summary>
        public CheckoutCustomerDataViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutCustomerDataViewModel"/> class.
        /// </summary>
        /// <param name="checkoutCustomerPersonalDataViewModel">The checkout customer personal data view model.</param>
        public CheckoutCustomerDataViewModel(CheckoutCustomerDataViewModel checkoutCustomerPersonalDataViewModel)
        {
            this.CustomersId = checkoutCustomerPersonalDataViewModel.CustomersId;
            this.Email = checkoutCustomerPersonalDataViewModel.Email;
            this.FristName = checkoutCustomerPersonalDataViewModel.FristName;
            this.LastName = checkoutCustomerPersonalDataViewModel.LastName;
            this.IDType = checkoutCustomerPersonalDataViewModel.IDType;
            this.IDNumber = checkoutCustomerPersonalDataViewModel.IDNumber;
            this.PhoneNumber = checkoutCustomerPersonalDataViewModel.PhoneNumber;
            this.AlternateNumber = checkoutCustomerPersonalDataViewModel.AlternateNumber;
            this.CompanyName = checkoutCustomerPersonalDataViewModel.CompanyName;
            this.AddressesData = checkoutCustomerPersonalDataViewModel.AddressesData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutCustomerDataViewModel"/> class.
        /// </summary>
        /// <param name="checkoutCustomerPersonalDataViewModel">The checkout customer personal data view model.</param>
        /// <param name="checkoutCustomerAddressesDataViewModel">The checkout customer addresses data view model.</param>
        public CheckoutCustomerDataViewModel(CheckoutCustomerDataViewModel checkoutCustomerPersonalDataViewModel, CheckoutCustomerAddressesDataViewModel checkoutCustomerAddressesDataViewModel)
        {
            this.CustomersId = checkoutCustomerPersonalDataViewModel.CustomersId;
            this.Email = checkoutCustomerPersonalDataViewModel.Email;
            this.FristName = checkoutCustomerPersonalDataViewModel.FristName;
            this.LastName = checkoutCustomerPersonalDataViewModel.LastName;
            this.IDType = checkoutCustomerPersonalDataViewModel.IDType;
            this.IDNumber = checkoutCustomerPersonalDataViewModel.IDNumber;
            this.PhoneNumber = checkoutCustomerPersonalDataViewModel.PhoneNumber;
            this.AlternateNumber = checkoutCustomerPersonalDataViewModel.AlternateNumber;
            this.CompanyName = checkoutCustomerPersonalDataViewModel.CompanyName;
            this.AddressesData = new CheckoutCustomerAddressesDataViewModel(checkoutCustomerAddressesDataViewModel);
        }

        #region Customer Base Data
        /// <summary>
        /// Gets or sets the customers identifier.
        /// </summary>
        /// <value>
        /// The customers identifier.
        /// </value>
        [Required]
        public int CustomersId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Display(Name = "Correo"), Required(ErrorMessage = "{0} es requerido."), DataType(DataType.EmailAddress, ErrorMessage = "{0} no es válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the frist.
        /// </summary>
        /// <value>
        /// The name of the frist.
        /// </value>
        [Display(Name = "Nombre"), Required(ErrorMessage = "{0} es requerido."), StringLength(50, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string FristName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Display(Name = "Apellidos"), Required(ErrorMessage = "{0} son requeridos"), StringLength(50, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the type of the identifier.
        /// </summary>
        /// <value>
        /// The type of the identifier.
        /// </value>
        [Display(Name = "Tipo de identificación"), Required(ErrorMessage = "{0} es requerido.")]
        public string IDType { get; set; }

        /// <summary>
        /// Gets or sets the identifier number.
        /// </summary>
        /// <value>
        /// The identifier number.
        /// </value>
        [Display(Name = "Número de identificación"), Required(ErrorMessage = "{0} es requerido."), StringLength(20, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string IDNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Teléfono/Celular"), Required(ErrorMessage = "{0} es requerido."), DataType(DataType.PhoneNumber), StringLength(35, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the alternate number.
        /// </summary>
        /// <value>
        /// The alternate number.
        /// </value>
        [Display(Name = "Número alterno"), DataType(DataType.PhoneNumber), StringLength(35, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string AlternateNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [Display(Name = "Empresa"), DataType(DataType.Text), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName => $"{this.FristName} {this.LastName}";
        #endregion

        /// <summary>
        /// Gets or sets the addresses data.
        /// </summary>
        /// <value>
        /// The addresses data.
        /// </value>
        public CheckoutCustomerAddressesDataViewModel AddressesData { get; set; }
    }
}
