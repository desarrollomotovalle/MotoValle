using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class TMercadeoClientes
    {
        public TMercadeoClientes()
        {
            TMercadeoClienteConvenio = new HashSet<TMercadeoClienteConvenio>();
        }

        public int IdCliente { get; set; }
        public string NroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Linea { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NroContacto { get; set; }
        public string Placa { get; set; }
        public string CiudadRegistro { get; set; }

        public virtual ICollection<TMercadeoClienteConvenio> TMercadeoClienteConvenio { get; set; }
    }
}
