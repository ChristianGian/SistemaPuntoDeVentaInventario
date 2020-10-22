using CapaNegocio.Models;
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

            ListarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));

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

        private void ListarProductosVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                dgdProductosVendidos.ItemsSource = null;
                var lista = transaccion.MostrarProductosVendidos(fechaInicio, fechaFin);
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
            ListarProductosVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));
            EsRangoCorrecto();
        }

        private void BtnHoy_Click(object sender, RoutedEventArgs e)
        {
            ListarProductosVendidos(hoy, hoy);

            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;

        }
    }
}
