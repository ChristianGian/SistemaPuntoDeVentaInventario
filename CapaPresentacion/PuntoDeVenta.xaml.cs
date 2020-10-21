using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using CapaPresentacion.Modulos;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para PuntoDeVenta.xaml
    /// </summary>
    public partial class PuntoDeVenta : Window
    {
        //Campos
        private TransaccionModel transaccion = new TransaccionModel();
        private ProductoModel producto = new ProductoModel();

        //Método constructor
        public PuntoDeVenta()
        {
            InitializeComponent();

            producto.ObtenerTodo();
            lblFecha.Content = DateTime.Now.ToLongDateString();
            lblFechaDetalle.Content = DateTime.Now.ToLongDateString();

            //Hora y fecha - DispatcherTimer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }


        #region Métodos de ayuda
        private void ObtenerNrTransaccion()
        {
            try
            {
                string fechaActual = DateTime.Now.ToString("yyyyMMdd");
                string numTransaccion;
                int contar;

                var listaTransaccion = transaccion.Buscar(fechaActual);

                if (listaTransaccion.Count > 0)
                {
                    numTransaccion = listaTransaccion[0].NumTransaccion;
                    contar = Convert.ToInt32(numTransaccion.Substring(8, 4));
                    lblNumTransaccion.Content = fechaActual + (contar + 1);
                }
                else
                {
                    numTransaccion = fechaActual + "1001";
                    lblNumTransaccion.Content = numTransaccion;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "N° de transacción", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MostrarUltimasTransacciones(string numTransaccion)
        {
            try
            {
                var lista = transaccion.UltimasTransacciones(numTransaccion);

                dgdProductos.ItemsSource = null;
                dgdProductos.ItemsSource = lista;

                decimal total = 0;
                decimal descuento = 0;

                for (int i = 0; i < lista.Count; i++)
                {
                    total += lista[i].Total;
                    descuento += lista[i].Descuento;
                }

                lblTotal.Content = $"{total:C}";
                lblMuestraTotal.Content = $"{total:C}";
                lblDescuento.Content = $"{descuento:C}";
                ObtenerDetalleTotal(total, descuento);

                //Habilitar boton de Descuento y Liquidar pago
                if (lista.Count > 0)
                {
                    btnAgregarDescuento.IsEnabled = true;
                    btnLiquidarPago.IsEnabled = true;
                }
                else
                {
                    btnLiquidarPago.IsEnabled = false;
                    btnAgregarDescuento.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar productos de transacción", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ObtenerDetalleTotal(decimal t, decimal d)
        {
            decimal total = t;

            decimal descuento = d;
            decimal igv = total * transaccion.ObtenerPorcentajeIgv();
            decimal subtotal = total - igv;

            lblSubTotal.Content = $"{subtotal:C}";
            lblIgv.Content = $"{igv:C}";

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblHoraDetalle.Content = DateTime.Now.ToLongTimeString();
        }

        private void Limpiar()
        {
            btnAgregarDescuento.IsEnabled = false;
            btnLiquidarPago.IsEnabled = false;

            dgdProductos.ItemsSource = null;

            lblSubTotal.Content = "0";
            lblDescuento.Content = "0";
            lblIgv.Content = "0";
            lblTotal.Content = "0";
            lblMuestraTotal.Content = "0";

            ObtenerNrTransaccion();
        }
        #endregion

        #region Handler de Botones del Menú

        private void BtnNuevaTransaccion_Click(object sender, RoutedEventArgs e)
        {
            ObtenerNrTransaccion();

            txtBuscarCodigoBarra.IsEnabled = true;
            btnBuscarProducto.IsEnabled = true;
            txtBuscarCodigoBarra.Focus();
        }

        private void BtnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (lblNumTransaccion.Content.ToString() != "00000000000000000")
            {
                ModuloVistaProductosPOS vistaProductosPOS = new ModuloVistaProductosPOS();

                vistaProductosPOS.NumTransaccion = lblNumTransaccion.Content.ToString();
                vistaProductosPOS.ShowDialog();

                MostrarUltimasTransacciones(lblNumTransaccion.Content.ToString());

                txtBuscarCodigoBarra.Clear();
                txtBuscarCodigoBarra.Focus();
            }
        }

        private void BtnAgregarDescuento_Click(object sender, RoutedEventArgs e)
        {
            ModuloDescuento moduloDescuento = new ModuloDescuento();

            transaccion = dgdProductos.SelectedItem as TransaccionModel;

            if (transaccion != null)
            {
                moduloDescuento.DataContext = transaccion;

                moduloDescuento.ShowDialog();

                MostrarUltimasTransacciones(lblNumTransaccion.Content.ToString());
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Agregar descuento", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }

        private void BtnLiquidarPago_Click(object sender, RoutedEventArgs e)
        {
            ModuloLiquidarPago liquidarPago = new ModuloLiquidarPago();

            string t = lblMuestraTotal.Content.ToString().Replace("S", "").Replace("/", "").Replace(",", "").Replace(" ", "");

            liquidarPago.txtTotalAPagar.Text = t;

            var listaIdProductoAPagar = new List<TransaccionModel>();

            foreach (TransaccionModel item in dgdProductos.Items)
            {
                listaIdProductoAPagar.Add(new TransaccionModel
                {
                    IdTransaccion = item.IdTransaccion,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    NumTransaccion = lblNumTransaccion.Content.ToString()
                });
            }

            liquidarPago.ListaIdProductoAPagar = listaIdProductoAPagar;
            liquidarPago.NumTransaccion = lblNumTransaccion.Content.ToString();
            liquidarPago.ShowDialog();

            if (liquidarPago.enter) Limpiar();
        }

        private void btnCancelarVentas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVentasDiarias_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCambiarPassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void TxtBuscarCodigoBarra_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtBuscarCodigoBarra.Text == "")
                {
                    return ;
                }
                else
                {
                    dgdProductos.ItemsSource = null;
                    var lista = producto.BuscarProductoPorCodigoBarra(txtBuscarCodigoBarra.Text.Trim());
                    //dgdProductos.ItemsSource = lista;

                    if (lista.Count > 0)
                    {
                        ModuloPOSCantidad moduloPOSCantidad = new ModuloPOSCantidad();

                        moduloPOSCantidad.DetalleProducto(lista[0].IdProducto, lista[0].Precio, lblNumTransaccion.Content.ToString());
                        moduloPOSCantidad.ShowDialog();

                        MostrarUltimasTransacciones(lblNumTransaccion.Content.ToString());

                        txtBuscarCodigoBarra.Clear();
                        txtBuscarCodigoBarra.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Productos en el carrito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            transaccion = dgdProductos.SelectedItem as TransaccionModel;

            if (producto != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este producto del carrito de compras?", "Eliminar categoria", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    transaccion.Estado = EntityState.Eliminado;

                    string resultado = transaccion.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    MostrarUltimasTransacciones(lblNumTransaccion.Content.ToString());
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar producto", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #region Validaciones
        private void TxtBuscarCodigoBarra_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);

        #endregion
   
    }
}
