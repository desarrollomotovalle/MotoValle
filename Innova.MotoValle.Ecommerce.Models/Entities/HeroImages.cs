// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeroImages.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Hero Images Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.Entities
{
    /// <summary>
    /// Hero Images Model
    /// </summary>
    public class HeroImages
    {
        /// <summary>
        /// Gets or sets the hero image identifier.
        /// </summary>
        /// <value>
        /// The hero image identifier.
        /// </value>
        public int HeroImagesId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HeroImages"/> is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enable; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets the fk makes identifier.
        /// </summary>
        /// <value>
        /// The fk makes identifier.
        /// </value>
        [Display(Name = "Make")]
        public int? FkMakesId { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        [Required]
        [Range(1, int.MaxValue)]
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        [Display(Name = "Image URL")]
        [StringLength(255)]
        public string ImageURL { get; set; }

        /// <summary>
        /// Gets or sets the has button.
        /// </summary>
        /// <value>
        /// The has button.
        /// </value>
        [Display(Name = "Image Has Button?")]
        [Required]
        public bool HasButton { get; set; }

        /// <summary>
        /// Gets or sets the button URL.
        /// </summary>
        /// <value>
        /// The button URL.
        /// </value>
        [Display(Name = "Button URL")]
        [StringLength(255)]
        public string ButtonURL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show span].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show span]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(Name = "Show Span")]
        public bool ShowSpan { get; set; }

        /// <summary>
        /// Gets or sets the span text.
        /// </summary>
        /// <value>
        /// The span text.
        /// </value>
        [Display(Name = "Span Text")]
        [StringLength(70)]
        public string SpanText { get; set; }

        /// <summary>
        /// Gets or sets the span complement text.
        /// </summary>
        /// <value>
        /// The span complement text.
        /// </value>
        [Display(Name = "Span Complement Text")]
        [StringLength(70)]
        public string SpanComplementText { get; set; }

        /// <summary>
        /// Gets or sets the fk makes.
        /// </summary>
        /// <value>
        /// The fk makes.
        /// </value>
        public virtual Makes FkMakes { get; set; }
    }
}
