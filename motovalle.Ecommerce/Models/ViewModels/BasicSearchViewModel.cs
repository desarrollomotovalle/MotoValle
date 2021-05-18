// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicSearchViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Basic Search View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Basic Search View Model
    /// </summary>
    public class BasicSearchViewModel
    {
        /// <summary>
        /// The price range minimum
        /// </summary>
        private decimal _priceRangeMin;

        /// <summary>
        /// The price range maximum
        /// </summary>
        private decimal _priceRangeMax;

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        [Required(ErrorMessage = "Seleccione el tipo de vehículo")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Seleccione el tipo de vehículo")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the make identifier.
        /// </summary>
        /// <value>
        /// The make identifier.
        /// </value>
        [Required(ErrorMessage = "Seleccione la marca de vehículo")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Seleccione la marca de vehículo")]
        public int MakeId { get; set; }

        /// <summary>
        /// Gets or sets the model identifier.
        /// </summary>
        /// <value>
        /// The model identifier.
        /// </value>
        [Required(ErrorMessage = "Seleccione el modelo de vehículo")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Seleccione el modelo de vehículo")]
        public int ModelId { get; set; }

        /// <summary>
        /// Gets or sets the price range minimum.
        /// </summary>
        /// <value>
        /// The price range minimum.
        /// </value>
        [Required(ErrorMessage = "Seleccione el rango mínimo de precios")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "El rango de precio mínimo debe ser mayor a 0")]
        public decimal PriceRangeMin 
        {
            get => this._priceRangeMin * 1000000;
            set => this._priceRangeMin = value;
        }

        /// <summary>
        /// Gets or sets the price range maximum.
        /// </summary>
        /// <value>
        /// The price range maximum.
        /// </value>
        [Required(ErrorMessage = "Seleccione el rango máximo de precios")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "El rango de precio máximo debe ser mayor a 0")]
        public decimal PriceRangeMax
        {
            get => this._priceRangeMax * 1000000;
            set => this._priceRangeMax = value;
        }

        /// <summary>
        /// Gets or sets the year minimum.
        /// </summary>
        /// <value>
        /// The year minimum.
        /// </value>
        [Required(ErrorMessage = "Debe ingersar el año mínimo de tu vehículo.")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Debe ingersar el año máximo de tu vehículo.")]
        public int YearMin { get; set; }

        /// <summary>
        /// Gets or sets the year maximum.
        /// </summary>
        /// <value>
        /// The year maximum.
        /// </value>
        [Required(ErrorMessage = "Debe ingersar el año máximo de tu vehículo.")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Debe ingersar el año máximo de tu vehículo.")]
        public int YearMax { get; set; }

        /// <summary>
        /// Gets or sets the records per page.
        /// </summary>
        /// <value>
        /// The records per page.
        /// </value>
        public int RecordsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>
        /// The sort by.
        /// </value>
        public int SortBy { get; set; }
    }
}
