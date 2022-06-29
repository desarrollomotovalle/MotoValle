using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace motovalle.Ecommerce.Models.ViewModels.LandingPetronasQR
{
    public class TMercadeoClientesViewModel
    {
        public int IdCliente { get; set; }

        [MinLength(6, ErrorMessage = "La lomgitud mínima debe ser de 6 caracteres")]
        [MaxLength(10, ErrorMessage = "La lomgitud máxima debe ser de 10 caracteres")]
        [Required(ErrorMessage = "Debe ingresar un número de documento valido")]
        [Display(Name = "Número de documento")]
        public string NroDocumento { get; set; }

        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Solo se permiten letras.")]
        [Required(ErrorMessage = "Debe ingresar un nombre valido")]
        [Display(Name = "Nombre completo.")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo electrónico valido")]
        [EmailAddress(ErrorMessage = "El formato de correo no es valido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe ingresar una marca de vehículo valida")]
        [Display(Name = "Marca del vehículo")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Debe ingresar un modelo de vehículo valido")]
        [Display(Name = "Modelo del vehículo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Debe ingresar una linea de vehículo valida")]
        [Display(Name = "Línea del vehículo")]
        public string Linea { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de contácto valido")]
        [Display(Name = "Número de contacto.")]
        public string NroContacto { get; set; }

        [Required(ErrorMessage = "Debe ingresar una placa de vehículo valida")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Debe ingresar la ciudad de registro")]
        [Display(Name = "Ciudad de registro")]
        public string CiudadRegistro { get; set; }



        // Campos adicionales en el modelo
        public int IdQR { get; set; }

        // campo para la descripcion del convenio
        public string DescripcionConvenio { get; set; }


        // Campo para validar el almacenado en la BD
        public string Control { get; set; }
    }
}
