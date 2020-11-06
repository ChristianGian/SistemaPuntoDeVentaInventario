using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contracts
{
    public interface IDashboardRepository
    {
        decimal ObtenerVentasDiarias();
        int ObtenerLineaDeProductos();
        int ObtenerStockDisponible();
        int ObtenerProductosCriticos();
    }
}
