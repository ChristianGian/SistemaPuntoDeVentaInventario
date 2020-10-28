using CapaComun.Cache;
using CapaNegocio.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace CapaPresentacion.Modulos
{
    /// <summary>
    /// Lógica de interacción para ModuloProductosVendidos.xaml
    /// </summary>
    public partial class ModuloProductosVendidos : Window
    {
        //Campos
        private TransaccionModel transaccion = new TransaccionModel();
        private DateTime hoy = DateTime.Now;

        //Método constructor
        public ModuloProductosVendidos()
        {
            InitializeComponent();

            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;

            LlenarCmbCajero();
            cmbCajero.SelectedIndex = 0;
        }

        #region Métodos de ayuda
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ListarProductosVendidos(DateTime fechaInicio, DateTime fechaFin, string username)
        {
            try
            {
                dgdProductosVendidos.ItemsSource = null;
                var lista = transaccion.MostrarProductosVendidos(fechaInicio, fechaFin, username);
                dgdProductosVendidos.ItemsSource = lista;

                decimal total = 0;

                for (int i = 0; i < lista.Count; i++)
                {
                    total += lista[i].Total;
                }

                lblTotal.Content = $"{total:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar productos vendidos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LlenarCmbCajero()
        {
            UsuarioModel usuario = new UsuarioModel();

            var lista = usuario.ObtenerCajeros();
            lista.Insert(0, new UsuarioModel
            {
                Username = "Todos"
            });

            cmbCajero.ItemsSource = lista;
            
            cmbCajero.DisplayMemberPath = "Username";
            cmbCajero.SelectedValuePath = "Username";
        }

        private void EsRangoCorrecto()
        {
            if (Convert.ToDateTime(dtpFechaInicio.Text) > Convert.ToDateTime(dtpFechaFin.Text))
            {
                MessageBox.Show("Asegúrese de ingresar un rango de fechas válida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCajero.Text == "Todos")
            {
                ListarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text), "");

            }
            else
            {
                ListarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text), cmbCajero.Text);
            }
            EsRangoCorrecto();
        }

        private void BtnHoy_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCajero.Text == "Todos")
            {
                ListarProductosVendidos(hoy, hoy, "");

            }
            else
            {
                ListarProductosVendidos(hoy, hoy, cmbCajero.Text);
            }

            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;

        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ReportDataSource> datos = new List<ReportDataSource>();
                ReportDataSource prodVendido = new ReportDataSource();

                prodVendido.Name = "DS_ProductosVendidos";
                if (cmbCajero.Text == "Todos")
                    prodVendido.Value = transaccion.MostrarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text), "");
                else
                    prodVendido.Value = transaccion.MostrarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text), cmbCajero.Text);
                datos.Add(prodVendido);

                Reportes.ReporteProductosVendidos productosVendidos = new Reportes.ReporteProductosVendidos("CapaPresentacion.Reportes.ReporteProductosVendidos.rdlc", datos);
                productosVendidos.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Imprimir", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BtnCancelarOrden_Click(object sender, RoutedEventArgs e)
        {
            ModuloCancelarOrdenDetalle cancelarOrdenDetalle = new ModuloCancelarOrdenDetalle();
            cancelarOrdenDetalle.DataContext = dgdProductosVendidos.SelectedItem as TransaccionModel;
            cancelarOrdenDetalle.ShowDialog();

            //Actualizamos la lista
            if (cmbCajero.Text == "Todos")

                ListarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text), "");
            else
                ListarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text), cmbCajero.Text);
        }
    }
}
