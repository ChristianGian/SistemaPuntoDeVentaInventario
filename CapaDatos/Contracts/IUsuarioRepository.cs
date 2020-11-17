using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        //Métodos propios
        List<Usuario> ReadCajero(string username);
        List<Usuario> ReadAdmin(string username);
        int Delete(string username);
        bool Login(string username, string password);
        List<Usuario> LoginPermisos(string username, string password);
        int CambiarPassword(string username, string password);
        int ActualizarEstadoUsuario(string username, string estadoUsuario);
    }
}
