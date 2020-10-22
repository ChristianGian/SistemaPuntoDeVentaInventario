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
        decimal ObtenerIgv();
        List<Transaccion> ReadProductosVendidos(DateTime fechaInicio, DateTime fechaFin);
    }
}
