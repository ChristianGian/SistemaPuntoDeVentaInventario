using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        //Métodos propios
        int Delete(string idPK);
        int ActualizarProductoCantidad(string idProducto, int cantidadComprada);
        List<Producto> ReadProductosCriticos();
    }
}
