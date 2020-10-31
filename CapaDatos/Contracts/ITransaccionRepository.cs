using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface ITransaccionRepository : IGenericRepository<Transaccion>
    {
        List<Transaccion> Search(string numTransaccion);
        List<Transaccion> MostrarUltimasTransacciones(string numTransaccion);
        int ActualizarEstadoTransaccion(int idTransaccion);
        int ActualizarCantidadTransaccion(int idTransaccion, string numTransaccion, string idProducto, int cantidad);
        List<Transaccion> ComprobarProductosDuplicados(string numTransaccion, string idProducto);
        decimal ObtenerIgv();
        List<Transaccion> ReadProductosVendidos(DateTime fechaInicio, DateTime fechaFin, string username);
        List<Transaccion> RegistroProductosVendidos(DateTime fechaInicio, DateTime fechaFin);
        List<Transaccion> ReadProductosVendidosAgrupados(DateTime fechaInicio, DateTime fechaFin);
    }
}
