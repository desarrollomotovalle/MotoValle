// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MercadoPagoCreditCardPaymentViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Mercado Pago Credit card Payment View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Mercado Pago Credit card Payment View Model
    /// </summary>
    public class MercadoPagoCreditCardPaymentViewModel
    {
        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        /// <value>
        /// The transaction amount.
        /// </value>
        [Required(ErrorMessage = "Monto Total es requerido")]
        [Range(0, double.PositiveInfinity)]
        [Display(Name = "Monto total")]
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets the card token.
        /// </summary>
        /// <value>
        /// The card token.
        /// </value>
        [Required(ErrorMessage = "Token de la tarjeta es requerido")]
        public string CardToken { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required(ErrorMessage = "Descripción de la compra es requerida.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        [Required(ErrorMessage = "Método de págo es requerido.")]
        [Display(Name = "Método de pago")]
        public string PaymentMethodId { get; set; }

        /// <summary>
        /// Gets or sets the installments.
        /// </summary>
        /// <value>
        /// The installments.
        /// </value>
        [Required(ErrorMessage = "Coutas es requerido.")]
        [Range(1, double.PositiveInfinity)]
        [Display(Name = "Cuotas")]
        public int Installments { get; set; }

        /// <summary>
        /// Gets or sets the identifier number.
        /// </summary>
        /// <value>
        /// The identifier number.
        /// </value>
        [Required(ErrorMessage = "Número de identificación es requerido.")]
        [Display(Name = "Número de identificación")]
        public string IDNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage = "Correo electrónico es requerido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>
        /// The issuer identifier.
        /// </value>
        public string IssuerId { get; set; }

        /// <summary>
        /// Gets or sets the type of the identifier number.
        /// </summary>
        /// <value>
        /// The type of the identifier number.
        /// </value>
        [Required(ErrorMessage = "Tipo de indentificación es requerido.")]
        public string IDNumberType { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessage = "Nombres son requeridos.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessage = "Apellidos son requeridos.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required(ErrorMessage = "Dirección es requerida.")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Required(ErrorMessage = "Número de teléfono es requerido.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the ship address.
        /// </summary>
        /// <value>
        /// The ship address.
        /// </value>
        [Required(ErrorMessage = "Dirección de envío es requerida.")]
        public string ShipAddress { get; set; }

        /// <summary>
        /// Gets or sets the ship zip code.
        /// </summary>
        /// <value>
        /// The ship zip code.
        /// </value>
        public string ShipZipCode { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the mp customer identifier.
        /// </summary>
        /// <value>
        /// The mp customer identifier.
        /// </value>
        public string MPCustomerId { get; set; }
    }
}
