// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContactViewModel.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Contact View Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Models.ViewModels.Landing
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Contact View Model
    /// </summary>
    public class ContactLavadoGratisViewModel
    {
        [Display(Name ="Nombre(s)")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(3, ErrorMessage = "El campo debe tener al menos 3 caracteres")]
        [MaxLength(30, ErrorMessage = "El campo no debe tener más de 30 caracteres")]
        public string Nombres { get; set; }

        [Display(Name = "Apellido(s)")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(3, ErrorMessage = "El campo debe tener al menos 3 caracteres")]
        [MaxLength(30, ErrorMessage = "El campo no debe tener más de 30 caracteres")]
        public string Apellidos { get; set; }

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Cedula { get; set; }

        [Display(Name = "N° de celular")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(10, ErrorMessage = "El campo debe tener 10 caracteres")]
        [MaxLength(10, ErrorMessage = "El campo debe tener 10 caracteres")]
        public string Celular { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo debe ser válido")]
        public string Correo { get; set; }

        [Display(Name = "Marca del vehículo")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(3, ErrorMessage = "El campo debe tener al menos 3 caracteres")]
        [MaxLength(30, ErrorMessage = "El campo no debe tener más de 30 caracteres")]
        public string MarcaVehiculo { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(4, ErrorMessage = "El campo debe tener al menos 4 caracteres")]
        [MaxLength(30, ErrorMessage = "El campo no debe tener más de 30 caracteres")]
        public string Modelo { get; set; }

        [Display(Name = "Placa")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(6, ErrorMessage = "El campo debe tener 6 caracteres")]
        [MaxLength(30, ErrorMessage = "El campo debe tener 6 caracteres")]
        public string Placa { get; set; }

        [Display(Name = "N° de factura")]
        [Required(ErrorMessage = "Campo requerido")]
        [MinLength(3, ErrorMessage = "El campo debe tener al menos 3 caracteres")]
        [MaxLength(30, ErrorMessage = "El campo no debe tener más de 30 caracteres")]
        public string Factura { get; set; }

        [Display(Name = "Fecha de factura")]
        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es válido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFactura { get; set; }

        [Display(Name = "Ciudad de residencia")]
        [Required(ErrorMessage = "Campo requerido")]        
        public string CiudadResidencia { get; set; }

        [Required]
        //[RegularExpression("True", ErrorMessage = "Debes aceptar las políticas de privacidad")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Debes aceptar las políticas de privacidad")]
        public bool TratamientoDatos { get; set; }
    }
}
