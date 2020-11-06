using CapaDatos.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositories
{
    public class DashboardRepository : MasterRepository, IDashboardRepository
    {
        //Tarjetas
        public int ObtenerLineaDeProductos()
        {
            var tabla = ExecuteReader("CantidadDeLineaDeProductos");
            int n = 0;

            foreach (DataRow item in tabla.Rows)
            {
                n = Convert.ToInt32(item[0]);
            }
            return n;
        }

        public int ObtenerProductosCriticos()
        {
            var tabla = ExecuteReader("TotalProductosCriticos");
            int n = 0;

            foreach (DataRow item in tabla.Rows)
            {
                n = Convert.ToInt32(item[0]);
            }
            return n;
        }

        public int ObtenerStockDisponible()
        {
            var tabla = ExecuteReader("TotalStockDisponible");
            int n = 0;

            foreach (DataRow item in tabla.Rows)
            {
                n = Convert.ToInt32(item[0]);
            }
            return n;
        }

        public decimal ObtenerVentasDiarias()
        {
            var tabla = ExecuteReader("TotalVentasDiarias");
            decimal n = 0;

            foreach (DataRow item in tabla.Rows)
            {
                n = Convert.ToDecimal(item[0]);
            }
            return n;
        }

        //Gráficos
        public Dictionary<string, decimal> TotalVentasPorAnio()
        {
            var tabla = ExecuteReader("TotalVentasPorAnio");
            var ventas = new Dictionary<string, decimal>();

            foreach (DataRow item in tabla.Rows)
            {
                ventas.Add(item[0].ToString(), Convert.ToDecimal(item[1]));
            }

            return ventas;
        }
    }
}
