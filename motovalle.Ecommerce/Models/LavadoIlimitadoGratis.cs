using System;
using System.Collections.Generic;

namespace motovalle.Ecommerce.Models
{
    public partial class LavadoIlimitadoGratis
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string MarcaVehiculo { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Factura { get; set; }
        public DateTime FechaFactura { get; set; }
        public string CiudadResidencia { get; set; }
        public bool TratamientoDatos { get; set; }
    }
}
