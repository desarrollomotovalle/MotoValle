// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelAndCategoryBase.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Model And Category Base view model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Model And Category Base view model
    /// </summary>
    public class ModelAndCategoryBase
    {
        /// <summary>
        /// Gets or sets the models identifier.
        /// </summary>
        /// <value>
        /// The models identifier.
        /// </value>
        public int ModelsId { get; set; }

        /// <summary>
        /// Gets or sets the name of the model.
        /// </summary>
        /// <value>
        /// The name of the model.
        /// </value>
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets the name of the make.
        /// </summary>
        /// <value>
        /// The name of the make.
        /// </value>
        [Display(Name = "Make Name")]
        public string MakeName { get; set; }

        /// <summary>
        /// Gets or sets the categories identifier.
        /// </summary>
        /// <value>
        /// The categories identifier.
        /// </value>
        public int CategoriesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
