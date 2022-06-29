using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class TMercadeoConvenio
    {
        public TMercadeoConvenio()
        {
            TMercadeoClienteConvenio = new HashSet<TMercadeoClienteConvenio>();
        }

        public int IdConvenio { get; set; }
        public string CodConvenio { get; set; }
        public string DesConvenio { get; set; }
        public string Beneficio { get; set; }
        public string NomPropietario { get; set; }

        public virtual TMercadeoBeneficiario TMercadeoBeneficiario { get; set; }
        public virtual ICollection<TMercadeoClienteConvenio> TMercadeoClienteConvenio { get; set; }
    }
}
