// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDocuments.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product Documents model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Product Documents model
    /// </summary>
    public partial class ProductDocuments
    {
        /// <summary>
        /// Gets or sets the product documents identifier.
        /// </summary>
        /// <value>
        /// The product documents identifier.
        /// </value>
        public int ProductDocumentsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [Display(Name = "File Name")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(45, ErrorMessage = "The field {0} must have {1} characters maximum")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        [Display(Name = "File Path")]
        [StringLength(300, ErrorMessage = "The field {0} must have {1} characters maximum")]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the fk products identifier.
        /// </summary>
        /// <value>
        /// The fk products identifier.
        /// </value>
        [Display(Name = "Product")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int? FkProductsId { get; set; }

        /// <summary>
        /// Gets or sets the fk product documents categories identifier.
        /// </summary>
        /// <value>
        /// The fk product documents categories identifier.
        /// </value>
        [Display(Name = "Document Category")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public int? FkProductDocumentsCategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [Display(Name = "Product")]
        public virtual Products FkProducts { get; set; }

        /// <summary>
        /// Gets or sets the fk product documents categories.
        /// </summary>
        /// <value>
        /// The fk product documents categories.
        /// </value>
        [Display(Name = "Document Category")]
        public virtual ProductDocumentsCategories FkProductDocumentsCategories { get; set; }
    }
}
