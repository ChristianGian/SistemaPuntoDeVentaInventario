using CapaNegocio.Models;
using Microsoft.Reporting.WinForms;
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
    /// Lógica de interacción para Registros.xaml
    /// </summary>
    public partial class Registros : UserControl
    {
        //Campos
        private TransaccionModel transaccion = new TransaccionModel();
        private ProductoModel producto = new ProductoModel();
        private OrdenCanceladaModel ordenCancelada = new OrdenCanceladaModel();
        private StockModel stock = new StockModel(); 
        private DateTime hoy = DateTime.Now;

        //Método constructor
        public Registros()
        {
            InitializeComponent();

            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;

            dtpFechaInicioTab2.SelectedDate = hoy;
            dtpFechaFinTab2.SelectedDate = hoy;

            dtpFechaInicioTab5.SelectedDate = hoy;
            dtpFechaFinTab5.SelectedDate = hoy;

            dtpFechaInicioTab6.SelectedDate = hoy;
            dtpFechaFinTab6.SelectedDate = hoy;

            ListarProductosCriticos();
            ListaDeInventario();
        }

        #region Métodosde ayuda
        private void ListarRegProductoseVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var lista = transaccion.RegistroProductosVendidos(fechaInicio, fechaFin);
                dgdRegProductosVendidos.ItemsSource = null;

                if (cmbOrdenarPor.Text == "Ordenar por cantidad")
                {
                    dgdRegProductosVendidos.ItemsSource = from p in lista
                                                          orderby p.Cantidad descending
                                                          select p;
                }
                else if (cmbOrdenarPor.Text == "Ordenar por monto total")
                {
                    dgdRegProductosVendidos.ItemsSource = from p in lista
                                                          orderby p.Total descending
                                                          select p;
                }
                else
                {
                    MessageBox.Show("Por favor seleccione una opción para ordenar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar registros", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarGraficoTopVentas()
        {
            var lista = transaccion.RegistroProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));
            Chart chart = this.FindName("MiWinformChart") as Chart;

            if (cmbOrdenarPor.Text == "Ordenar por cantidad")
            {
                chart.DataSource = from p in lista
                                   orderby p.Cantidad descending
                                   select p;

                chart.Series["series"].XValueMember = "IdProducto";
                chart.Series["series"].YValueMembers = "Cantidad";
            }
            else if (cmbOrdenarPor.Text == "Ordenar por monto total")
            {
                chart.DataSource = from p in lista
                                   orderby p.Total descending
                                   select p;

                chart.Series["series"].XValueMember = "IdProducto";
                chart.Series["series"].YValueMembers = "Total";
            }
            else
            {
                return;
            }

            //Detalles
            chart.Series[0].IsValueShownAsLabel = true;
            chart.Palette = ChartColorPalette.Pastel;
            //chart.Titles.Add($"Ventas por meses ({DateTime.Now.Year})");
            //txbTituloChart.Text = $"Ventas por Meses de {DateTime.Now.Year} en Soles";
            chart.Legends.Add("");
        }

        private void ListarProductosVendidosAgrupados(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                dgdProductosVendidosAgrupados.ItemsSource = null;
                var lista = transaccion.MostrarProductosVendidosAgrupados(fechaInicio, fechaFin);
                dgdProductosVendidosAgrupados.ItemsSource = lista;

                lblTotal.Content = $"{lista.Sum(l => l.Total):C}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar productos vendidos agrupados", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListarOrdenesCanceladas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                dgdProductosCriticos.ItemsSource = null;
                dgdOrdenesCanceladas.ItemsSource = ordenCancelada.ObtenerTodo(fechaInicio, fechaFin);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar órdenes canceladas", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListarStock(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                dgdStockEnHistoria.ItemsSource = null;
                dgdStockEnHistoria.ItemsSource = stock.BuscarStockPorFecha(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar stock en historia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EsRangoCorrecto()
        {
            if (Convert.ToDateTime(dtpFechaInicio.Text) > Convert.ToDateTime(dtpFechaFin.Text))
            {
                MessageBox.Show("Asegúrese de ingresar un rango de fechas válida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        //TAB 1: Top 10 más vendidos
        private void BtnCargarDatos_Click(object sender, RoutedEventArgs e)
        {
            ListarRegProductoseVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));
            CargarGraficoTopVentas();
            EsRangoCorrecto();
        }

        private void BtnImprimirTab1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ReportDataSource> datos = new List<ReportDataSource>();
                ReportDataSource topVendidos = new ReportDataSource();

                topVendidos.Name = "DS_TopVendidos";

                var lista = transaccion.RegistroProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));

                if (cmbOrdenarPor.Text == "Ordenar por cantidad")
                {
                    topVendidos.Value = from p in lista
                                        orderby p.Cantidad descending
                                        select p;
                }
                else if (cmbOrdenarPor.Text == "Ordenar por monto total")
                {
                    topVendidos.Value = from p in lista
                                        orderby p.Total descending
                                        select p;
                }
                else
                {
                    MessageBox.Show("Por favor seleccione una opción para ordenar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                datos.Add(topVendidos);

                Reportes.ReporteRegistrosTop10 listaTopVendidos = new Reportes.ReporteRegistrosTop10("CapaPresentacion.Reportes.ReporteRegistrosTop10.rdlc", datos);
                listaTopVendidos.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Imprimir", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //TAB 2: Prodcutos vendidos (Agrupados)
        private void BtnCargarDatosTab2_Click(object sender, RoutedEventArgs e)
        {
            ListarProductosVendidosAgrupados(Convert.ToDateTime(dtpFechaInicioTab2.Text), Convert.ToDateTime(dtpFechaFinTab2.Text));
            EsRangoCorrecto();
        }

        //TAB 3: Prodcutos críticos
        private void ListarProductosCriticos()
        {
            try
            {
                dgdProductosCriticos.ItemsSource = null;
                dgdProductosCriticos.ItemsSource = producto.MostrarProductosCriticos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar productos críticos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //TAB 4: Lista de inventario
        private void ListaDeInventario()
        {
            try
            {
                ProductoModel producto = new ProductoModel();

                dgdListaDeInventarios.ItemsSource = null;
                dgdListaDeInventarios.ItemsSource = producto.MostrarListaDeInvetario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lista de inventario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ReportDataSource> datos = new List<ReportDataSource>();
                ReportDataSource inventario = new ReportDataSource();

                inventario.Name = "DS_ListaDeInventario";
                inventario.Value = producto.MostrarListaDeInvetario();
                datos.Add(inventario);

                Reportes.ReporteListaDeInventario listaDeInventario = new Reportes.ReporteListaDeInventario("CapaPresentacion.Reportes.ReporteListaDeInventario.rdlc", datos);
                listaDeInventario.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Imprimir", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //TAB 5: Productos cancelados
        private void BtnCargarDatosTab5_Click(object sender, RoutedEventArgs e)
        {
            ListarOrdenesCanceladas(Convert.ToDateTime(dtpFechaInicioTab5.Text), Convert.ToDateTime(dtpFechaFinTab5.Text));
            EsRangoCorrecto();
        }

        private void BtnCargarDatosTab6_Click(object sender, RoutedEventArgs e)
        {
            ListarStock(Convert.ToDateTime(dtpFechaInicioTab6.Text), Convert.ToDateTime(dtpFechaFinTab6.Text));
            EsRangoCorrecto();
        }

        private void btnImprimirTab6_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
