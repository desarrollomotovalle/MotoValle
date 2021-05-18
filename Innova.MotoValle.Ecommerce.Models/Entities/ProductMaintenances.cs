// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductMaintenances.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Maintenances Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Product Maintenances Model
    /// </summary>
    public partial class ProductMaintenances
    {
        /// <summary>
        /// Gets or sets the product maintenances identifier.
        /// </summary>
        /// <value>
        /// The product maintenances identifier.
        /// </value>
        [Display(Name = "Id")]
        public int ProductMaintenancesId { get; set; }

        /// <summary>
        /// Gets or sets the fk products identifier.
        /// </summary>
        /// <value>
        /// The fk products identifier.
        /// </value>
        [Display(Name = "Product")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the index of the maintenance.
        /// </summary>
        /// <value>
        /// The index of the maintenance.
        /// </value>
        [Display(Name = "Index")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(1, int.MaxValue)]
        public int MaintenanceIndex { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Display(Name = "Title")]
        [StringLength(255, ErrorMessage = "The field {0} must have {1} characters maximum")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the sales price.
        /// </summary>
        /// <value>
        /// The sales price.
        /// </value>
        [DataType(DataType.Currency)]
        [Display(Name = "Sales Price")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Sales Price must be mayor than {1}")]
        //[RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$", ErrorMessage = "Maximum 16 integer and Two Decimal Points")]
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Description")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProductMaintenances"/> is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enable; otherwise, <c>false</c>.
        /// </value>
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [Display(Name = "Product")]
        public virtual Products FkProducts { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName => this.FkProducts?.ProductName;

        /// <summary>
        /// Gets or sets the name of the make and product.
        /// </summary>
        /// <value>
        /// The name of the make and product.
        /// </value>
        public string MakeAndProductName => $"{this.FkProducts?.FkMakes?.MakeName} - {this.FkProducts?.ProductName}";
    }
}
