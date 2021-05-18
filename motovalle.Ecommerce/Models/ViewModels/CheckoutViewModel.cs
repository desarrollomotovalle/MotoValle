// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckoutViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Checkout View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using global::Ecommerce.Models.Entities;
    using global::Ecommerce.Models.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Checkout View Model
    /// </summary>
    public class CheckoutViewModel
    {
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
        [Required(ErrorMessage = "Correo electrónico es requerido."), EmailAddress, Display(Name = "Correo")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Required(ErrorMessage = "Nombre completo es requerido."), StringLength(100), Display(Name = "Nombre completo")]
        public string FullName => $"{this.FristName} {this.LastName}";

        /// <summary>
        /// Gets or sets the name of the frist.
        /// </summary>
        /// <value>
        /// The name of the frist.
        /// </value>
        [Required(ErrorMessage = "Nombres son requeridos"), Display(Name = "Nombre")]
        public string FristName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "Apellidos son requeridos"), Display(Name = "Apellidos")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the identifier number.
        /// </summary>
        /// <value>
        /// The identifier number.
        /// </value>
        [Required(ErrorMessage = "Número de identificación es requerido."), Display(Name = "Número de identificación")]
        public string IDNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the identifier.
        /// </summary>
        /// <value>
        /// The type of the identifier.
        /// </value>
        [Required(ErrorMessage = "Tipo de indentificación es requerido."), Display(Name = "Tipo de identificación")]
        public string IDType { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Required(ErrorMessage = "Número de teléfono es requerido."), DataType(DataType.PhoneNumber), StringLength(35), Display(Name = "Teléfono/Celular")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the alternate number.
        /// </summary>
        /// <value>
        /// The alternate number.
        /// </value>
        [DataType(DataType.PhoneNumber), StringLength(45), Display(Name = "Número alterno")]
        public string AlternateNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [DataType(DataType.Text), StringLength(45), Display(Name = "Empresa")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Required(ErrorMessage = "País es requerido."), DataType(DataType.Text), StringLength(45), Display(Name = "País")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required(ErrorMessage = "Dirección despacho es requerida."), DataType(DataType.Text), StringLength(70), Display(Name = "Dirección")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Required(ErrorMessage = "Departamento despacho es requerido."), DataType(DataType.Text), StringLength(45), Display(Name = "Departamento")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required(ErrorMessage = "Ciudad despacho es requerida."), DataType(DataType.Text), StringLength(45), Display(Name = "Ciudad")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [DataType(DataType.PostalCode), StringLength(45), Display(Name = "Código postal")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the bill address.
        /// </summary>
        /// <value>
        /// The bill address.
        /// </value>
        [Required(ErrorMessage = "Dirección de facturación es requerida."), StringLength(70), Display(Name = "Dirección de facturación")]
        public string BillAddress { get; set; }

        /// <summary>
        /// Gets or sets the bill city.
        /// </summary>
        /// <value>
        /// The bill city.
        /// </value>
        [Required(ErrorMessage = "Ciudad de facturación es requerida."), StringLength(45), Display(Name = "Ciudad de facturación")]
        public string BillCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the bill.
        /// </summary>
        /// <value>
        /// The state of the bill.
        /// </value>
        [Required(ErrorMessage = "Departamento de facturación es requerido."), StringLength(45), Display(Name = "Departamento de facturación")]
        public string BillState { get; set; }

        /// <summary>
        /// Gets or sets the bill zip code.
        /// </summary>
        /// <value>
        /// The bill zip code.
        /// </value>
        [DataType(DataType.PostalCode), StringLength(45), Display(Name = "Código postal de facturación")]
        public string BillZipCode { get; set; }

        /// <summary>
        /// Gets or sets the iva taxes.
        /// </summary>
        /// <value>
        /// The iva taxes.
        /// </value>
        public List<TaxesTotalViewModel> TotalTaxes { get; set; }

        /// <summary>
        /// Gets or sets the sub total.
        /// </summary>
        /// <value>
        /// The sub total.
        /// </value>
        [Required(ErrorMessage = "Subtotal es requerido."), Range(0, double.PositiveInfinity), Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost.
        /// </summary>
        /// <value>
        /// The shipping cost.
        /// </value>
        [Required(ErrorMessage = "Costos de envío y manipulación es requierido."), Range(0, double.PositiveInfinity), Display(Name = "Costos de envío y manipulación")]
        public decimal ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [Range(0, double.PositiveInfinity)]
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [Required(ErrorMessage = "Total es requerido."), Range(0, double.PositiveInfinity, ErrorMessage = "Total debe ser mayor a 0"), Display(Name = "Total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Gets or sets the payment method selected.
        /// </summary>
        /// <value>
        /// The payment method selected.
        /// </value>
        [Required(ErrorMessage = "Debe seleccionar el método de pago.")]
        public string PaymentMethodSelected { get; set; }

        /// <summary>
        /// Gets or sets the partial amount paid.
        /// </summary>
        /// <value>
        /// The partial amount paid.
        /// </value>
        [Display(Name = "Partial Amount Paid")]
        [DataType(DataType.Currency)]
        [Range(1000000, double.PositiveInfinity, ErrorMessage = "Monto mínimo debe ser $1'000.000. ")]
        public decimal? PartialAmountPaid { get; set; }

        /// <summary>
        /// Gets or sets the g recaptcha response.
        /// </summary>
        /// <value>
        /// The g recaptcha response.
        /// </value>
        public string GRecaptchaResponse { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records.
        /// </summary>
        /// <value>
        /// The shopping cart records.
        /// </value>
        public List<ShoppingCartRecords> ShoppingCartRecords { get; set; }

        /// <summary>
        /// Gets or sets the credit card view model.
        /// </summary>
        /// <value>
        /// The credit card view model.
        /// </value>
        public MercadoPagoCreditCardPaymentViewModel CreditCardViewModel { get; set; }

        /// <summary>
        /// Gets or sets the funding request view model.
        /// </summary>
        /// <value>
        /// The funding request view model.
        /// </value>
        public FundingRequestViewModel FundingRequestViewModel { get; set; }
    }
}
