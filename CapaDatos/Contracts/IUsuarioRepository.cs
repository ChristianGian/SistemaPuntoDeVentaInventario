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
        int Delete(string username);
    }
}
