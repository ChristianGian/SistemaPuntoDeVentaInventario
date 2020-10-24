using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaComun.Cache
{
    public static class UserCache
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Rol { get; set; }
        public static string Nombres { get; set; }
        public static string Apellidos { get; set; }
        public static string EstadoUsuario { get; set; }
    }
}
