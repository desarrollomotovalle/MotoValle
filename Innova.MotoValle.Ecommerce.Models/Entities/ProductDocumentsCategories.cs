// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocumentsCategories.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Documents Categories Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Product Documents Categories Model
    /// </summary>
    public partial class ProductDocumentsCategories
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDocumentsCategories" /> class.
        /// </summary>
        public ProductDocumentsCategories()
        {
            this.ProductDocuments = new HashSet<ProductDocuments>();
        }

        /// <summary>
        /// Gets or sets the product documents categories identifier.
        /// </summary>
        /// <value>
        /// The product documents categories identifier.
        /// </value>
        public int ProductDocumentsCategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(45, ErrorMessage = "The field {0} must have {1} characters maximum")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the product documents.
        /// </summary>
        /// <value>
        /// The product documents.
        /// </value>
        public virtual ICollection<ProductDocuments> ProductDocuments { get; set; }
    }
}
