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
using System.Windows.Shapes;

namespace CapaPresentacion.Modulos
{
    /// <summary>
    /// Lógica de interacción para GraficoRegProductosVendidos.xaml
    /// </summary>
    public partial class GraficoRegProductosVendidos : Window
    {
        //Campos
        private TransaccionModel transaccion = new TransaccionModel();

        //Método constructor
        public GraficoRegProductosVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            InitializeComponent();

            CargarGrafico(fechaInicio, fechaFin);
        }

        #region Eventos
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void CargarGrafico(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = transaccion.MostrarProductosVendidosAgrupados(fechaInicio, fechaFin);

            Chart chart = this.FindName("MiWinformChart") as Chart;
            chart.DataSource = from p in lista
                               orderby p.Total descending
                               select p;
            chart.Series["series"].XValueMember = "NombreProducto";
            chart.Series["series"].YValueMembers = "Total";
            chart.Series["series"].LabelFormat = "{C}";

            //Detalles
            chart.Series[0].IsValueShownAsLabel = true;
            chart.Palette = ChartColorPalette.Pastel;
            //chart.Titles.Add($"Ventas por meses ({DateTime.Now.Year})");
            //txbTituloChart.Text = $"Ventas por Meses de {DateTime.Now.Year} en Soles";
            chart.Legends.Add("");
        }
    }
}
