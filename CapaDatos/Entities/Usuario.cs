using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string EstadoUsuario { get; set; }
    }
}
