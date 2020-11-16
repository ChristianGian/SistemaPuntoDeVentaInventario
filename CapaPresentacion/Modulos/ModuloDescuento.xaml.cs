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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapaPresentacion.Modulos
{
    /// <summary>
    /// Lógica de interacción para ModuloDescuento.xaml
    /// </summary>
    public partial class ModuloDescuento : Window
    {
        //Campos
        private TransaccionModel transaccion = new TransaccionModel();

        //Método costructor
        public ModuloDescuento()
        {
            InitializeComponent();

            DataContext = transaccion;

            txtDescuento.Focus();
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirmar descuento", "Agregar descuento", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    transaccion = DataContext as TransaccionModel;
                    transaccion.Estado = CapaNegocio.ValueObjects.EntityState.Actualizado;
                    transaccion.PorcentajeDesc = Convert.ToDecimal(txtDescuento.Text) / 100;
                    transaccion.Descuento = Convert.ToDecimal(txtTotalDescuento.Text);

                    bool validar = new Helps.DataValidation(transaccion).Validar();

                    if (validar)
                    {
                        transaccion.GuardarCambios();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregar descuento", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtDescuento_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtDescuento.Text != "")
                {
                    double precio = Convert.ToDouble(txtPrecio.Text);
                    double porcentajeDesc = Convert.ToDouble(txtDescuento.Text) / 100;

                    double totalDescuento = precio * porcentajeDesc;

                    txtTotalDescuento.Text = totalDescuento.ToString();
                }
                else
                {
                    txtTotalDescuento.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Calcular descuento", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Eventos
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
        }
        #endregion
    }
}
