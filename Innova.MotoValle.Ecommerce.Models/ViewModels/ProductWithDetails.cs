// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductWithDetails.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Product With Details View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models.ViewModels
{
    /// <summary>
    /// Product With Details View Model
    /// </summary>
    /// <seealso cref="Ecommerce.Models.ViewModels.ProductAndMakeBase" />
    public class ProductWithDetails : ProductAndMakeBase
    {
        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>
        /// The upc.
        /// </value>
        public string Upc { get; set; }

        /// <summary>
        /// Gets or sets the product long description.
        /// </summary>
        /// <value>
        /// The product long description.
        /// </value>
        [Display(Name = "Product Long Description")]
        public string ProductLongDescription { get; set; }

        /// <summary>
        /// Gets or sets the trim.
        /// </summary>
        /// <value>
        /// The trim.
        /// </value>
        [StringLength(255)]
        public string Trim { get; set; }

        /// <summary>
        /// Gets or sets the gas mileage.
        /// </summary>
        /// <value>
        /// The gas mileage.
        /// </value>
        [StringLength(255)]
        public string GasMileage { get; set; }

        /// <summary>
        /// Gets or sets the name of the engine type.
        /// </summary>
        /// <value>
        /// The name of the engine type.
        /// </value>
        [Display(Name = "Engine Type Name"), StringLength(255)]
        public string EngineTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the epa class.
        /// </summary>
        /// <value>
        /// The name of the epa class.
        /// </value>
        [Display(Name = "Epa Class Name"), StringLength(45)]
        public string EpaClassName { get; set; }

        /// <summary>
        /// Gets or sets the name of the drive train.
        /// </summary>
        /// <value>
        /// The name of the drive train.
        /// </value>
        [Display(Name = "Drive Train Name"), StringLength(255)]
        public string DriveTrainName { get; set; }

        /// <summary>
        /// Gets or sets the base weight.
        /// </summary>
        /// <value>
        /// The base weight.
        /// </value>
        [Display(Name = "Base Weight")]
        public int BaseWeight { get; set; }

        /// <summary>
        /// Gets or sets the picture360 URL.
        /// </summary>
        /// <value>
        /// The picture360 URL.
        /// </value>
        [Display(Name = "Picture 360 Url"), StringLength(255)]
        public string Picture360Url { get; set; }

        /// <summary>
        /// Gets or sets the picture360 quantity.
        /// </summary>
        /// <value>
        /// The picture360 quantity.
        /// </value>
        public int Picture360Quantity { get; set; }

        /////Massey Fieds
        /// <summary>
        /// Gets or sets the cylinders.
        /// </summary>
        /// <value>
        /// The cylinders.
        /// </value>
        [Display(Name = "Cilindros")]
        public int? Cylinders { get; set; }

        /// <summary>
        /// Gets or sets the load capacity.
        /// </summary>
        /// <value>
        /// The load capacity.
        /// </value>
        [Display(Name = "Capacidad de alce")]
        public int? LoadCapacity { get; set; }

        /// <summary>
        /// Gets or sets the weight without platform.
        /// </summary>
        /// <value>
        /// The weight without platform.
        /// </value>
        [Display(Name = "Peso sin plataforma")]
        public int? SingleWeight { get; set; }

        /// <summary>
        /// Gets or sets the productivity.
        /// </summary>
        /// <value>
        /// The productivity.
        /// </value>
        [Display(Name = "Productividad")]
        [StringLength(45)]
        public string Productivity { get; set; }

        /// <summary>
        /// Gets or sets the length of the bale.
        /// </summary>
        /// <value>
        /// The length of the bale.
        /// </value>
        [Display(Name = "Longitud de la paca")]
        [StringLength(45)]
        public string BaleLength { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        [Display(Name = "Velocidad")]
        [StringLength(45)]
        public string Speed { get; set; }

        /// <summary>
        /// Gets or sets the recommendedpower.
        /// </summary>
        /// <value>
        /// The recommendedpower.
        /// </value>
        [Display(Name = "Potencia recomendada")]
        [StringLength(45)]
        public string RecommendedPower { get; set; }

        /// <summary>
        /// Gets or sets the width of the working.
        /// </summary>
        /// <value>
        /// The width of the working.
        /// </value>
        [Display(Name = "Ancho de trabajo")]
        public decimal? WorkingWidth { get; set; }

        /// <summary>
        /// Gets or sets the number of lines.
        /// </summary>
        /// <value>
        /// The number of lines.
        /// </value>
        [Display(Name = "Número de líneas")]
        public int? NumberOfLines { get; set; }

        /// <summary>
        /// Gets or sets the number of discs.
        /// </summary>
        /// <value>
        /// The number of discs.
        /// </value>
        [Display(Name = "Número de discos")]
        public int? NumberOfDiscs { get; set; }

        /// <summary>
        /// Gets or sets the input rotation.
        /// </summary>
        /// <value>
        /// The input rotation.
        /// </value>
        [Display(Name = "Rotación de entrada (TDF)")]
        [StringLength(45)]
        public string InputRotation { get; set; }

        /// <summary>
        /// Gets or sets the standard cover.
        /// </summary>
        /// <value>
        /// The standard cover.
        /// </value>
        [Display(Name = "Cubierta estándar")]
        [StringLength(45)]
        public string StandardCover { get; set; }

        /// <summary>
        /// Gets or sets the width of the cutting.
        /// </summary>
        /// <value>
        /// The width of the cutting.
        /// </value>
        [Display(Name = "Ancho de corte")]
        [StringLength(45)]
        public string CuttingWidth { get; set; }

        /// <summary>
        /// Gets or sets the aspiration types identifier.
        /// </summary>
        /// <value>
        /// The aspiration types identifier.
        /// </value>
        [Display(Name = "Aspiración")]
        public string AspirationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the fk rear tires types identifier.
        /// </summary>
        /// <value>
        /// The fk rear tires types identifier.
        /// </value>
        [Display(Name = "Neumáticos traseros")]
        public string RearTiresTypeName { get; set; }

        /// <summary>
        /// Gets or sets the fk front tires types identifier.
        /// </summary>
        /// <value>
        /// The fk front tires types identifier.
        /// </value>
        [Display(Name = "Neumáticos delanteros")]
        public string FrontTiresTypeName { get; set; }

        /// <summary>
        /// Gets or sets the fk separation systems identifier.
        /// </summary>
        /// <value>
        /// The fk separation systems identifier.
        /// </value>
        [Display(Name = "Sistema de separación")]
        public string SeparationSystemName { get; set; }

        /// <summary>
        /// Gets or sets the fk hitch systems identifier.
        /// </summary>
        /// <value>
        /// The fk hitch systems identifier.
        /// </value>
        [Display(Name = "Sistema de enganche")]
        public string HitchSystemName { get; set; }

        /// <summary>
        /// Gets or sets the fk hitch pin identifier.
        /// </summary>
        /// <value>
        /// The fk hitch pin identifier.
        /// </value>
        [Display(Name = "Perno de enganche")]
        public string HitchPinName { get; set; }

        /// <summary>
        /// Gets or sets the fk chamber dimensions identifier.
        /// </summary>
        /// <value>
        /// The fk chamber dimensions identifier.
        /// </value>
        [Display(Name = "Chamber Dimensions")]
        public string ChamberDimensionName { get; set; }

        /// <summary>
        /// Gets or sets the fk configuration types identifier.
        /// </summary>
        /// <value>
        /// The fk configuration types identifier.
        /// </value>
        [Display(Name = "Configuración")]
        public string ConfigurationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the fk conditioning systems identifier.
        /// </summary>
        /// <value>
        /// The fk conditioning systems identifier.
        /// </value>
        [Display(Name = "Sistema de acondicionamiento")]
        public string ConditioningSystemName { get; set; }

        /// <summary>
        /// Gets or sets the fk blades types identifier.
        /// </summary>
        /// <value>
        /// The fk blades types identifier.
        /// </value>
        [Display(Name = "Cuchillas")]
        public string BladeTypeName { get; set; }

        /// <summary>
        /// Gets or sets the fk turning radius types identifier.
        /// </summary>
        /// <value>
        /// The fk turning radius types identifier.
        /// </value>
        [Display(Name = "Radio de giro")]
        public string TurningRadiusTypeName { get; set; }
    }
}
