using CapaNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Lógica de interacción para ModuloVistaProductosPOS.xaml
    /// </summary>
    public partial class ModuloVistaProductosPOS : Window
    {
        //Campos
        private ProductoModel producto = new ProductoModel();
        private string numTransaccion;

        //Propiedades
        public string NumTransaccion { get => numTransaccion; set => numTransaccion = value; }

        //Método constructor
        public ModuloVistaProductosPOS()
        {
            InitializeComponent();

            ListarProductos();
        }

        #region Métodos de ayuda
        private void ListarProductos()
        {
            try
            {
                dgdProductos.ItemsSource = null;
                dgdProductos.ItemsSource = producto.ObtenerTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar productos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

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

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dgdProductos.ItemsSource = null;
                dgdProductos.ItemsSource = producto.BuscarProductoPorDesc(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar productos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var producto = dgdProductos.SelectedItem as ProductoModel;
                //var lista = producto.BuscarProductoPorCodigoBarra(txtBuscarCodigoBarra.Text.Trim());
                //dgdProductos.ItemsSource = lista;


                    ModuloPOSCantidad moduloPOSCantidad = new ModuloPOSCantidad();

                    moduloPOSCantidad.DetalleProducto(producto.IdProducto, producto.Precio, numTransaccion, producto.Cantidad);
                    moduloPOSCantidad.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Productos en el carrito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
