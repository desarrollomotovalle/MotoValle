// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Orders.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Orders model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Orders model
    /// </summary>
    public partial class Orders
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Orders"/> class.
        /// </summary>
        public Orders()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
            this.HOrdersTaxes = new HashSet<HOrdersTaxes>();
        }

        /// <summary>
        /// Gets or sets the orders identifier.
        /// </summary>
        /// <value>
        /// The orders identifier.
        /// </value>
        public int OrdersId { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        [Required]
        [Display(Name = "Order Number")]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the fk customers identifier.
        /// </summary>
        /// <value>
        /// The fk customers identifier.
        /// </value>
        [Required]
        [Display(Name = "FK Customers Id")]
        public int FkCustomersId { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

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
        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the subtotal.
        /// </summary>
        /// <value>
        /// The subtotal.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Subtotal must be mayor or equals than 0")]
        public decimal? Subtotal { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Tax must be mayor or equals than 0")]
        public decimal? Tax { get; set; }

        /// <summary>
        /// Gets or sets the shipping.
        /// </summary>
        /// <value>
        /// The shipping.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Shipping must be mayor or equals than 0")]
        public decimal? Shipping { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Discount must be mayor or equals than 0")]
        public decimal? Discount { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Total must be mayor or equals than 0")]
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
        public string InvoiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the type of the identification.
        /// </summary>
        /// <value>
        /// The type of the identification.
        /// </value>
        [Display(Name = "Identification Type")]
        public string IdentificationType { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        /// <value>
        /// The identification number.
        /// </value>
        [Display(Name = "Identification Number")]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the alternate number.
        /// </summary>
        /// <value>
        /// The alternate number.
        /// </value>
        [Display(Name = "Alternate Number")]
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
        /// Gets or sets the bill to zipcode.
        /// </summary>
        /// <value>
        /// The bill to zipcode.
        /// </value>
        public string BillToZipcode { get; set; }

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
        /// Gets or sets the ship to zipcode.
        /// </summary>
        /// <value>
        /// The ship to zipcode.
        /// </value>
        public string ShipToZipcode { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        [Display(Name = "Transaction Id")]
        [MaxLength(64)]
        [StringLength(64)]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the fk funding requests identifier.
        /// </summary>
        /// <value>
        /// The fk funding requests identifier.
        /// </value>
        [Display(Name = "Funding Request Id")]
        public int? FKFundingRequestsId { get; set; }

        /// <summary>
        /// Gets or sets the partial amount paid.
        /// </summary>
        /// <value>
        /// The partial amount paid.
        /// </value>
        [Display(Name = "Partial Amount Paid")]
        [DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Partial Amount Paid must be mayor or equals than 0")]
        public decimal? PartialAmountPaid { get; set; }

        /// <summary>
        /// Gets or sets the fk funding request identifier.
        /// </summary>
        /// <value>
        /// The fk funding request identifier.
        /// </value>
        [Display(Name = "Funding Request")]
        public virtual FundingRequests FKFundingRequests { get; set; }

        /// <summary>
        /// Gets or sets the fk customers.
        /// </summary>
        /// <value>
        /// The fk customers.
        /// </value>
        [Display(Name = "Customer")]
        public virtual Customers FkCustomers { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the h orders taxes.
        /// </summary>
        /// <value>
        /// The h orders taxes.
        /// </value>
        public virtual ICollection<HOrdersTaxes> HOrdersTaxes { get; set; }
    }
}
