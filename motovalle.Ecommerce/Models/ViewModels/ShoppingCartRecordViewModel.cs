// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShoppingCartRecordViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Shopping Cart Record View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Shopping Cart Record View Model
    /// </summary>
    public class ShoppingCartRecordViewModel
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        [Required(ErrorMessage = "Debe seleccionar el producto que desea comprar.")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Seleccione un modelo de vehículo")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the inventory item identifier.
        /// </summary>
        /// <value>
        /// The inventory item identifier.
        /// </value>
        [Required(ErrorMessage = "Debe seleccionar el producto que desea comprar.")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Seleccione un artículo")]
        public int InventoryItemId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required(ErrorMessage = "Debe ingresar la cantidad a comprar.")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Cantidad debe ser mayor o igual a 1")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [Required(ErrorMessage = "Debe iniciar sesión para almacenar en el carrito de compras.")]
        public int CustomerId { get; set; }
    }
}
