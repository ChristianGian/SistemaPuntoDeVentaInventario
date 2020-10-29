﻿using CapaNegocio.Models;
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
        private DateTime hoy = DateTime.Now;

        //Método constructor
        public Registros()
        {
            InitializeComponent();

            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;
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

        private void EsRangoCorrecto()
        {
            if (Convert.ToDateTime(dtpFechaInicio.Text) > Convert.ToDateTime(dtpFechaFin.Text))
            {
                MessageBox.Show("Asegúrese de ingresar un rango de fechas válida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        private void BtnCargarDatos_Click(object sender, RoutedEventArgs e)
        {
            ListarRegProductoseVendidos(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));
            EsRangoCorrecto();
        }
    }
}
