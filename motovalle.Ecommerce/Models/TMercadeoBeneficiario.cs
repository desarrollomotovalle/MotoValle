using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class TMercadeoBeneficiario
    {
        public int IdBeneficiario { get; set; }
        public string NroDocumento { get; set; }
        public string Nombre { get; set; }
        public int TMercadeoConvenioIdConvenio { get; set; }

        public virtual TMercadeoConvenio TMercadeoConvenioIdConvenioNavigation { get; set; }
    }
}
