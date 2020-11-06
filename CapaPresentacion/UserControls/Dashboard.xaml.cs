using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion.UserControls
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        //Campos
        private DashboardModel dashboard = new DashboardModel();

        //Método constructor
        public Dashboard()
        {
            InitializeComponent();

            CargarDatos();
        }

        //Métodos
        private void CargarDatos()
        {
            txbVentasDiarias.Text = $"{dashboard.ObtenerVentasDiarias():C}";
            txbLineaDeProducto.Text = $"{dashboard.ObtenerLineaDeProductos():N0}";
            txbStockDisponible.Text = $"{dashboard.ObtenerStockDisponible():N0}";
            txbProductosCriticos.Text = $"{dashboard.ObtenerProductosCriticos():N0}";
        }
    }
}
