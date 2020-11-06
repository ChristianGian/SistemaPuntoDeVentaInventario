using CapaDatos.Contracts;
using CapaDatos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class DashboardModel
    {
        //Campos
        private IDashboardRepository dashboardRepository;

        //Método construtor
        public DashboardModel()
        {
            dashboardRepository = new DashboardRepository();
        }

        //Métodos
        public decimal ObtenerVentasDiarias()
        {
            return dashboardRepository.ObtenerVentasDiarias();
        }
        public int ObtenerLineaDeProductos()
        {
            return dashboardRepository.ObtenerLineaDeProductos();
        }
        public int ObtenerStockDisponible()
        {
            return dashboardRepository.ObtenerStockDisponible();
        }
        public int ObtenerProductosCriticos()
        {
            return dashboardRepository.ObtenerProductosCriticos();
        }

        public Dictionary<string, decimal> ObtenerVentasPorAnio()
        {
            return dashboardRepository.TotalVentasPorAnio();
        }
    }
}
