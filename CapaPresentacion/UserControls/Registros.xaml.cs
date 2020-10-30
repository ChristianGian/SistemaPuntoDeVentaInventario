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
        private DateTime hoy = DateTime.Now;

        //Método constructor
        public Registros()
        {
            InitializeComponent();

            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;

            dtpFechaInicioTab2.SelectedDate = hoy;
            dtpFechaFinTab2.SelectedDate = hoy;

            ListarProductosCriticos();
            ListaDeInventario();
        }

        #region Métodosde ayuda
        private void ListarRegProductoseVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                dgdRegProductosVendidos.ItemsSource = null;
                dgdRegProductosVendidos.ItemsSource = transaccion.RegistroProductosVendidos(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar registros", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            EsRangoCorrecto();
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

        //TAB 3: Prodcutos críticos
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
    }
}
