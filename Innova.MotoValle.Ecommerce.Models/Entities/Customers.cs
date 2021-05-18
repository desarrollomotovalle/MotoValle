// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Customers.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Customers model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    ///  Customers model
    /// </summary>
    public partial class Customers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Customers"/> class.
        /// </summary>
        public Customers()
        {
            this.Orders = new HashSet<Orders>();
            this.ShoppingCartRecords = new HashSet<ShoppingCartRecords>();
            this.FundingRequests = new HashSet<FundingRequests>();
        }

        /// <summary>
        /// Gets or sets the customers identifier.
        /// </summary>
        /// <value>
        /// The customers identifier.
        /// </value>
        public int CustomersId { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }

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
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public virtual ICollection<Orders> Orders { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart records.
        /// </summary>
        /// <value>
        /// The shopping cart records.
        /// </value>
        public virtual ICollection<ShoppingCartRecords> ShoppingCartRecords { get; set; }

        /// <summary>
        /// Gets or sets the funding request.
        /// </summary>
        /// <value>
        /// The funding request.
        /// </value>
        public virtual ICollection<FundingRequests> FundingRequests { get; set; }
    }
}