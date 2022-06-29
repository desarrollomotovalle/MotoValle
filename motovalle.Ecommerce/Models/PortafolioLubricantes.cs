using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class PortafolioLubricantes
    {
        public int IdPortafolioAutomotriz { get; set; }
        public int IdDetallePortafolioAutomotriz { get; set; }
        public string Categoria { get; set; }
        public string ReferenciaProducto { get; set; }
        public string GamaProducto { get; set; }
        public string TipoAceite { get; set; }
        public string ViscosidadAceite { get; set; }
        public string UrlImagen { get; set; }
        public string DescripcionProducto1 { get; set; }
        public string DescripcionProducto2 { get; set; }
        public string DescripcionProducto3 { get; set; }
        public string DescripcionProducto4 { get; set; }
        public string DescripcionProducto5 { get; set; }
        public string DescripcionProducto6 { get; set; }
        public string DescripcionProducto7 { get; set; }

        public virtual PortafolioLubricantesDetalle IdDetallePortafolioAutomotrizNavigation { get; set; }
    }
}
