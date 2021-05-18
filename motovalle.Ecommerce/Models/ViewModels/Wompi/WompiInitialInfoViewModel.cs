// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WompiInitialInfoViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Wompi Initial Info View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Wompi
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Wompi Initial Info View Model
    /// </summary>
    public class WompiInitialInfoViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WompiInitialInfoViewModel"/> class.
        /// </summary>
        public WompiInitialInfoViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WompiInitialInfoViewModel" /> class.
        /// </summary>
        /// <param name="pulicKey">The pulic key.</param>
        /// <param name="redirectURL">The redirect URL.</param>
        public WompiInitialInfoViewModel(string pulicKey, string redirectURL)
        {
            this.PublicKey = pulicKey;
            this.RedirectURL = redirectURL;
        }

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        public string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the currency.
        /// </summary>
        /// <value>
        /// The type of the currency.
        /// </value>
        [Display(Name = "Moneda")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione una opción válida")]
        [Required(ErrorMessage = "{0} es requerido.")]
        public WompiCurrencyType CurrencyType { get; set; }

        /// <summary>
        /// Gets or sets the amount in pesos.
        /// </summary>
        /// <value>
        /// The amount in pesos.
        /// </value>
        [Display(Name = "Total")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser superior a {1}")]
        [Required(ErrorMessage = "{0} es requerido.")]
        [DataType(DataType.Currency)]
        public int? AmountInPesos { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [Display(Name = "Referencia de pago / Nro. Orden")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} debe ser superior a {1}")]
        [Required(ErrorMessage = "{0} es requerido.")]
        public int? Reference { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>
        /// The redirect URL.
        /// </value>
        [Required(ErrorMessage = "{0} es requerido.")]
        public string RedirectURL { get; set; }

        /// <summary>
        /// Gets the amount in cents.
        /// </summary>
        /// <value>
        /// The amount in cents.
        /// </value>
        [Display(Name = "Total")]
        public string AmountInCents => $"{this.AmountInPesos}00";
    }
}
