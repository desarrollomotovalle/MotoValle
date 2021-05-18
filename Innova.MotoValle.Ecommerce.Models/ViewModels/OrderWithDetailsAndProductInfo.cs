// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderWithDetailsAndProductInfo.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Order With Details And Product Info view model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Ecommerce.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Order With Details And Product Info view model
    /// </summary>
    public class OrderWithDetailsAndProductInfo
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the type of the identification.
        /// </summary>
        /// <value>
        /// The type of the identification.
        /// </value>
        public string IdentificationType { get; set; }

        /// <summary>
        /// Gets or sets the identifiation number.
        /// </summary>
        /// <value>
        /// The identifiation number.
        /// </value>
        public string IdentifiationNumber { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        /// <value>
        /// The payment method.
        /// </value>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the sub total.
        /// </summary>
        /// <value>
        /// The sub total.
        /// </value>
        [DataType(DataType.Currency)]
        public decimal? SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        [DataType(DataType.Currency)]
        public decimal? Tax { get; set; }

        /// <summary>
        /// Gets or sets the shipping.
        /// </summary>
        /// <value>
        /// The shipping.
        /// </value>
        [DataType(DataType.Currency)]
        public decimal? Shipping { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [DataType(DataType.Currency)]
        public decimal? Discount { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [DataType(DataType.Currency), Display(Name = "Total")]
        public decimal? Total { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the invoice URL.
        /// </summary>
        /// <value>
        /// The invoice URL.
        /// </value>
        public string InvoiceURL { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the alternate number.
        /// </summary>
        /// <value>
        /// The alternate number.
        /// </value>
        public string AlternateNumber { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the bill to address.
        /// </summary>
        /// <value>
        /// The bill to address.
        /// </value>
        public string BillToAddress { get; set; }

        /// <summary>
        /// Gets or sets the bill to city.
        /// </summary>
        /// <value>
        /// The bill to city.
        /// </value>
        public string BillToCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the bill to.
        /// </summary>
        /// <value>
        /// The state of the bill to.
        /// </value>
        public string BillToState { get; set; }

        /// <summary>
        /// Gets or sets the bill to zip code.
        /// </summary>
        /// <value>
        /// The bill to zip code.
        /// </value>
        public string BillToZipCode { get; set; }

        /// <summary>
        /// Gets or sets the ship to address.
        /// </summary>
        /// <value>
        /// The ship to address.
        /// </value>
        public string ShipToAddress { get; set; }

        /// <summary>
        /// Gets or sets the ship to city.
        /// </summary>
        /// <value>
        /// The ship to city.
        /// </value>
        public string ShipToCity { get; set; }

        /// <summary>
        /// Gets or sets the state of the ship to.
        /// </summary>
        /// <value>
        /// The state of the ship to.
        /// </value>
        public string ShipToState { get; set; }
        /// <summary>
        /// Gets or sets the ship to zip code.
        /// </summary>
        /// <value>
        /// The ship to zip code.
        /// </value>
        public string ShipToZipCode { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the fk funding request identifier.
        /// </summary>
        /// <value>
        /// The fk funding request identifier.
        /// </value>
        public int FKFundingRequestId { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        [DataType(DataType.Date)]
        [Display(Name = "Date Ordered")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the partial amount paid.
        /// </summary>
        /// <value>
        /// The partial amount paid.
        /// </value>
        [Display(Name = "Partial Amount Paid")]
        [DataType(DataType.Currency)]
        public decimal? PartialAmountPaid { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        public List<OrderDetailWithProductInfo> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the h orders taxes.
        /// </summary>
        /// <value>
        /// The h orders taxes.
        /// </value>
        public List<HOrdersTaxes> HOrdersTaxes { get; set; }
    }
}
