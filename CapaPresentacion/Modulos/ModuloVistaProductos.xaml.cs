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
    /// Lógica de interacción para ModuloVistaProductos.xaml
    /// </summary>
    public partial class ModuloVistaProductos : Window
    {
        //Campos
        private ProductoModel producto = new ProductoModel();

        //Método constructor
        public ModuloVistaProductos()
        {
            InitializeComponent();

            ListarProductos();
        }

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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //            producto = dgdProductos.SelectedItem as ProductoModel;
            this.Close();
        }
    }
}
