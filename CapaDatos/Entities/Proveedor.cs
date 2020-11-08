using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Direccion { get; set; }
        public string PersonaDeContacto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
    }
}
