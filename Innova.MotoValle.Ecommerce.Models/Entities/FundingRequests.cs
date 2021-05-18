// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FundingRequest.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Funding Request Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Funding Request Model
    /// </summary>
    public partial class FundingRequests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundingRequests"/> class.
        /// </summary>
        public FundingRequests()
        {
            this.Orders = new HashSet<Orders>();
        }

        /// <summary>
        /// Gets or sets the funding request identifier.
        /// </summary>
        /// <value>
        /// The funding request identifier.
        /// </value>
        [Key]
        [Display(Name = "Funding Request Id")]
        public int FundingRequestId { get; set; }

        /// <summary>
        /// Gets or sets the fk customers identifier.
        /// </summary>
        /// <value>
        /// The fk customers identifier.
        /// </value>
        [Required]
        [Display(Name = "Customer")]
        public int FKCustomersId { get; set; }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>
        /// The request date.
        /// </value>
        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        [Display(Name = "Last Update Date")]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        public string Names { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the type of the identification.
        /// </summary>
        /// <value>
        /// The type of the identification.
        /// </value>
        [Display(Name = "ID Type")]
        public string IdentificationType { get; set; }

        /// <summary>
        /// Gets or sets the identification number.
        /// </summary>
        /// <value>
        /// The identification number.
        /// </value>
        [Display(Name = "ID Number")]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the city of residence.
        /// </summary>
        /// <value>
        /// The city of residence.
        /// </value>
        [Display(Name = "City Of Residence")]
        public string CityOfResidence { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        /// <value>
        /// The name of the bank.
        /// </value>
        [Required, MaxLength(100)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the total amount request.
        /// </summary>
        /// <value>
        /// The total amount request.
        /// </value>
        [Required, DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Total Amount Request must be mayor than 0")]
        [Display(Name = "Total Amount Request")]
        public decimal TotalAmountRequest { get; set; }

        /// <summary>
        /// Gets or sets the initial fee.
        /// </summary>
        /// <value>
        /// The initial fee.
        /// </value>
        [Display(Name = "Initial Fee")]
        [Required, DataType(DataType.Currency)]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Initial Fee must be mayor than 0")]
        public decimal InitialFee { get; set; }

        /// <summary>
        /// Gets or sets the installemnts.
        /// </summary>
        /// <value>
        /// The installemnts.
        /// </value>
        public int Installments { get; set; }

        /// <summary>
        /// Gets or sets the profession.
        /// </summary>
        /// <value>
        /// The profession.
        /// </value>
        public string Profession { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [Display(Name = "Request Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the updated for.
        /// </summary>
        /// <value>
        /// The updated for.
        /// </value>
        [Display(Name = "Updated For")]
        [StringLength(105)]
        public string UpdatedFor { get; set; }

        /// <summary>
        /// Gets or sets the economic activity.
        /// </summary>
        /// <value>
        /// The economic activity.
        /// </value>
        [Display(Name = "Ecomonic Activity")]
        [StringLength(105)]
        public string EconomicActivity { get; set; }

        /// <summary>
        /// Gets or sets the independent activity.
        /// </summary>
        /// <value>
        /// The independent activity.
        /// </value>
        [Display(Name = "Independent Activity")]
        [StringLength(105)]
        public string IndependentActivity { get; set; }

        /// <summary>
        /// Gets or sets the monthly income.
        /// </summary>
        /// <value>
        /// The monthly income.
        /// </value>
        [Display(Name = "Monthly Income")]
        [DataType(DataType.Currency)]
        public decimal MonthlyIncome { get; set; }

        /// <summary>
        /// Gets or sets the fk customers.
        /// </summary>
        /// <value>
        /// The fk customers.
        /// </value>
        [Display(Name = "Customer")]
        public virtual Customers FKCustomers { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
