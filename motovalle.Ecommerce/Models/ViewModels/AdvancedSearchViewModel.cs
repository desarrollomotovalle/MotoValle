// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdvancedSearchViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Advanced Search View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Advanced Search View Model
    /// </summary>
    /// <seealso cref="motovalle.Ecommerce.Models.ViewModels.BasicSearchViewModel" />
    public class AdvancedSearchViewModel : BasicSearchViewModel
    {
        /// <summary>
        /// Gets or sets the fuel type identifier.
        /// </summary>
        /// <value>
        /// The fuel type identifier.
        /// </value>
        public int FuelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the nro of seats.
        /// </summary>
        /// <value>
        /// The nro seats.
        /// </value>
        [Required(ErrorMessage = "Indique la cantidad de asientos")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "La cantidad de asientos debe ser mayor a 0")]
        public int NroOfSeats { get; set; }

        /// <summary>
        /// Gets or sets the nro of doors.
        /// </summary>
        /// <value>
        /// The nro of doors.
        /// </value>
        [Required(ErrorMessage = "Indique el número de puertas")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "El número de puertas debe ser mayor a 0")]
        public int NroOfDoors { get; set; }

        /// <summary>
        /// Gets or sets the nro of gears.
        /// </summary>
        /// <value>
        /// The nro of gears.
        /// </value>
        [Required(ErrorMessage = "Indique el número de cambios")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "El número de cambios debe ser mayor a 0")]
        public int NroOfGears { get; set; }

        /// <summary>
        /// Gets or sets the transmission type identifier.
        /// </summary>
        /// <value>
        /// The transmission type identifier.
        /// </value>
        public int TransmissionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the engine type identifier.
        /// </summary>
        /// <value>
        /// The engine type identifier.
        /// </value>
        public int EngineTypeId { get; set; }
    }
}
