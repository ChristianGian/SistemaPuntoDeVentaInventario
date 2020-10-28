using CapaComun.Cache;
using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Lógica de interacción para ModuloCancelarOrdenDetalle.xaml
    /// </summary>
    public partial class ModuloCancelarOrdenDetalle : Window
    {
        public ModuloCancelarOrdenDetalle()
        {
            InitializeComponent();

            txtCanceladoPor.Text = UserCache.Username;
        }

        #region Métodos de ayuda

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtCantidadCancelada_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);
        #endregion

        private void BtnCancelarOrden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbAgregarAInventario.Text != "" && txtCantidadCancelada.Text != "" && txtRazon.Text != "")
                {
                    if (Convert.ToInt32(txtCantidad.Text) >= (Convert.ToInt32(txtCantidadCancelada.Text)))
                    {
                        ModuloCancelarOrden cancelarOrden = new ModuloCancelarOrden();
                        cancelarOrden.ShowDialog();

                        string username = cancelarOrden.txtUsername.Text;

                        //Guardamos la orden cancelada en tblCancel
                        GuardarOrdenCancelada(username);

                        if (cancelarOrden.Valido)
                        {
                            //Restauramos la cantidad de produtos en la tblProduct
                            if (cmbAgregarAInventario.SelectedIndex == 0)
                            {
                                RestaurarCantidadDeProductos();
                            }
                            //Disminuimos la cantidad de productos comprados en la tblCart
                            TransaccionModel transaccion = new TransaccionModel();
                            int respuesta = transaccion.ActualizarCantidadTransaccion(Convert.ToInt32(txtIdTransacion.Text), txtNumTransaccion.Text, txtIdProducto.Text, (-1 * Convert.ToInt32(txtCantidadCancelada.Text)));

                            if (respuesta > 0)
                            {
                                MessageBox.Show("Transacción de pedido cancelada con éxito", "Cancelar orden", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();

                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //
        private void GuardarOrdenCancelada(string username)
        {
            OrdenCanceladaModel ordenCancelada = new OrdenCanceladaModel();

            ordenCancelada.NumTransaccion = txtNumTransaccion.Text;
            ordenCancelada.IdProducto = txtIdProducto.Text;
            ordenCancelada.Precio = Convert.ToDecimal(txtPrecio.Text);
            ordenCancelada.Cantidad = Convert.ToInt32(txtCantidad.Text);
            ordenCancelada.Fecha = DateTime.Now;
            ordenCancelada.AnuladoPor = username;
            ordenCancelada.CanceladoPor = txtCanceladoPor.Text;
            ordenCancelada.Razon = txtRazon.Text;
            ordenCancelada.Accion = cmbAgregarAInventario.Text;

            bool validar = new Helps.DataValidation(ordenCancelada).Validar();

            if (validar)
            {
                string resultado = ordenCancelada.GuardarCambios();
                MessageBox.Show(resultado, "Cancelar Orden", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RestaurarCantidadDeProductos()
        {
            ProductoModel producto = new ProductoModel();
            //producto.ActualizarProductoCantidad(ordenCancelada.IdProducto, (-1 * cantidadCancelada));
            producto.ActualizarProductoCantidad(txtIdProducto.Text, (-1 * Convert.ToInt32(txtCantidadCancelada.Text)));
        }
    }
}
