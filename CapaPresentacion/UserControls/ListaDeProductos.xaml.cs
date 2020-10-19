using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
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
    /// Lógica de interacción para ListaDeProductos.xaml
    /// </summary>
    public partial class ListaDeProductos : UserControl
    {
        //Campos
        private ProductoModel producto = new ProductoModel();

        //Método constructor
        public ListaDeProductos()
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
        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            Modulos.ModuloProducto moduloProducto = new Modulos.ModuloProducto();
            var producto = new ProductoModel();

            producto.Estado = EntityState.Agregado;
            moduloProducto.DataContext = producto;

            moduloProducto.txtCodigo.Focus();
            moduloProducto.btnActualizar.IsEnabled = false;

            moduloProducto.ShowDialog();
            ListarProductos();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            Modulos.ModuloProducto moduloProducto = new Modulos.ModuloProducto();

            producto = dgdProductos.SelectedItem as ProductoModel;

            if (producto != null)
            {
                producto.Estado = EntityState.Actualizado;
                moduloProducto.DataContext = producto;

                moduloProducto.btnGuardar.IsEnabled = false;

                moduloProducto.ShowDialog();
                ListarProductos();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Editar producto", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            producto = dgdProductos.SelectedItem as ProductoModel;

            if (producto != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este producto?", "Eliminar producto", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    producto.Estado = EntityState.Eliminado;

                    string resultado = producto.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListarProductos();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar producto", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
