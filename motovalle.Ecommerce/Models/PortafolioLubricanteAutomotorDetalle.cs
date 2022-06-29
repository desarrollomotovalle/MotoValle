using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class PortafolioLubricanteAutomotorDetalle
    {
        public PortafolioLubricanteAutomotorDetalle()
        {
            PortafolioLubricanteAutomotor = new HashSet<PortafolioLubricanteAutomotor>();
        }

        public int IdDetallePortafolioAutomotor { get; set; }
        public string TituloDetalle { get; set; }
        public string DescripcionTituloDetalle { get; set; }
        public string TituloDescripcionAplicaciones { get; set; }
        public string DescripcionAplicaciones { get; set; }
        public string TituloCaracteristicas { get; set; }
        public string Caracteristica1 { get; set; }
        public string Caracteristica2 { get; set; }
        public string Caracteristica3 { get; set; }
        public string Caracteristica4 { get; set; }
        public string Caracteristica5 { get; set; }
        public string Caracteristica6 { get; set; }
        public string Caracteristica7 { get; set; }
        public string TituloEspecificaciones { get; set; }
        public string Especificacion1 { get; set; }
        public string Especificacion2 { get; set; }
        public string Especificacion3 { get; set; }
        public string Especificacion4 { get; set; }
        public string Especificacion5 { get; set; }
        public string Especificacion6 { get; set; }
        public string Especificacion7 { get; set; }
        public string TituloSeguridad { get; set; }
        public string DescripcionSalud { get; set; }

        public virtual ICollection<PortafolioLubricanteAutomotor> PortafolioLubricanteAutomotor { get; set; }
    }
}
