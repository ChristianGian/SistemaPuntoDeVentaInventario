using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        //Métodos propios
        List<Stock> BuscarStockPorFecha(DateTime fechaInicio, DateTime fechaFin);
        List<Stock> BuscarStockPorFechaDetalle(DateTime fechaInicio, DateTime fechaFin);
        List<Stock> ReadStockActual(string numReferencia);
    }
}
