// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutCustomerAddressesDataViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout Customer Addresses Data View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Checkout
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Checkout Customer Addresses Data View Model
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Models.ViewModels.Checkout.CheckoutCustomerDataViewModel" />
    public class CheckoutCustomerAddressesDataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutCustomerAddressesDataViewModel"/> class.
        /// </summary>
        public CheckoutCustomerAddressesDataViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutCustomerAddressesDataViewModel"/> class.
        /// </summary>
        /// <param name="checkoutCustomerAddressesDataViewModel">The checkout customer addresses data view model.</param>
        public CheckoutCustomerAddressesDataViewModel(CheckoutCustomerAddressesDataViewModel checkoutCustomerAddressesDataViewModel)
        {
            this.Country = checkoutCustomerAddressesDataViewModel.Country;
            this.Address = checkoutCustomerAddressesDataViewModel.Address;
            this.State = checkoutCustomerAddressesDataViewModel.State;
            this.City = checkoutCustomerAddressesDataViewModel.City;
            this.ZipCode = checkoutCustomerAddressesDataViewModel.ZipCode;
            this.BillAddress = checkoutCustomerAddressesDataViewModel.BillAddress;
            this.BillCity = checkoutCustomerAddressesDataViewModel.BillCity;
            this.BillState = checkoutCustomerAddressesDataViewModel.BillState;
            this.BillZipCode = checkoutCustomerAddressesDataViewModel.BillZipCode;
        }

        #region Base Address
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Display(Name = "País"), Required(ErrorMessage = "{0} es requerido."), DataType(DataType.Text), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Display(Name = "Dirección"), Required(ErrorMessage = "{0} es requerida."), DataType(DataType.Text), StringLength(70, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Display(Name = "Departamento"), Required(ErrorMessage = "{0} despacho es requerido."), DataType(DataType.Text), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Display(Name = "Ciudad"), Required(ErrorMessage = "{0} es requerida."), DataType(DataType.Text), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [Display(Name = "Código postal"), DataType(DataType.PostalCode), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string ZipCode { get; set; }
        #endregion

        #region Bill Address
        /// <summary>
        /// Gets or sets the bill address.
        /// </summary>
        /// <value>
        /// The bill address.
        /// </value>
        [Display(Name = "Dirección de facturación"), Required(ErrorMessage = "Dirección de facturación es requerida."), StringLength(70, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string BillAddress { get; set; }

        /// <summary>
        /// Gets or sets the bill city.
        /// </summary>
        /// <value>
        /// The bill city.
        /// </value>
        [Display(Name = "Ciudad de facturación"), Required(ErrorMessage = "{0} es requerida."), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string BillCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the bill.
        /// </summary>
        /// <value>
        /// The state of the bill.
        /// </value>
        [Display(Name = "Departamento"), Required(ErrorMessage = "{0} es requerido."), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string BillState { get; set; }

        /// <summary>
        /// Gets or sets the bill zip code.
        /// </summary>
        /// <value>
        /// The bill zip code.
        /// </value>
        [Display(Name = "Código postal"), DataType(DataType.PostalCode), StringLength(45, ErrorMessage = "{0} debe contener máximo {1} carácteres")]
        public string BillZipCode { get; set; } 
        #endregion
    }
}
