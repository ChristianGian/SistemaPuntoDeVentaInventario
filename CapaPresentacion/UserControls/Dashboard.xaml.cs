using CapaNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
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
            VentasPorAnio();

            //ventas = new Dictionary<int, double>();
            //for (int i = 0; i < 10; i++)
            //    ventas.Add(i, 10 * i);

            //Chart chart = this.FindName("MyWinformChart") as Chart;
            //chart.DataSource = ventas;
            //chart.Series["series"].XValueMember = "Key";
            //chart.Series["series"].YValueMembers = "Value";
        }

        //Métodos
        private void CargarDatos()
        {
            txbVentasDiarias.Text = $"{dashboard.ObtenerVentasDiarias():C}";
            txbLineaDeProducto.Text = $"{dashboard.ObtenerLineaDeProductos():N0}";
            txbStockDisponible.Text = $"{dashboard.ObtenerStockDisponible():N0}";
            txbProductosCriticos.Text = $"{dashboard.ObtenerProductosCriticos():N0}";
        }

        private void VentasPorAnio()
        {
            var ventas = dashboard.ObtenerVentasPorAnio();

            Chart chart = this.FindName("MiWinformChart") as Chart;
            chart.DataSource = ventas;
            chart.Series["series"].XValueMember = "Key";
            chart.Series["series"].YValueMembers = "Value";

            //Detalles
            chart.Series[0].IsValueShownAsLabel = true;
            chart.Palette = ChartColorPalette.Pastel;
            //chart.Titles.Add($"Ventas por meses ({DateTime.Now.Year})");
            txbTituloChart.Text = $"Ventas por Meses de {DateTime.Now.Year} en Soles";
            chart.Legends.Add("");
        }
    }
}
