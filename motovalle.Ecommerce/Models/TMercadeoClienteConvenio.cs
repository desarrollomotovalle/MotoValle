using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class TMercadeoClienteConvenio
    {
        public int TMercadeoConvenioIdConvenio { get; set; }
        public int TMercadeoClientesIdCliente { get; set; }
        public int IdClienteConvenio { get; set; }

        public virtual TMercadeoClientes TMercadeoClientesIdClienteNavigation { get; set; }
        public virtual TMercadeoConvenio TMercadeoConvenioIdConvenioNavigation { get; set; }
    }
}
