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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion.UserControls
{
    /// <summary>
    /// Lógica de interacción para ListaDeVendedores.xaml
    /// </summary>
    public partial class ListaDeVendedores : UserControl
    {
        //Campos
        private ProveedorModel vendedor = new ProveedorModel();

        //Método constructor
        public ListaDeVendedores()
        {
            InitializeComponent();

            ListarVendedores();
        }

        #region Métodos de ayuda
        private void ListarVendedores()
        {
            try
            {
                dgdVendedores.ItemsSource = null;
                dgdVendedores.ItemsSource = vendedor.ObtenerTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar vendedores", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void BtnAgregarVendedor_Click(object sender, RoutedEventArgs e)
        {
            ModuloVendedor moduloVendedor = new ModuloVendedor();
            var vendedor = new ProveedorModel();

            vendedor.Estado = EntityState.Agregado;
            moduloVendedor.DataContext = vendedor;

            moduloVendedor.txtVendedor.Focus();
            moduloVendedor.btnActualizar.IsEnabled = false;

            moduloVendedor.ShowDialog();
            ListarVendedores();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            ModuloVendedor moduloVendedor = new ModuloVendedor();

            vendedor = dgdVendedores.SelectedItem as ProveedorModel;

            if (vendedor != null)
            {
                vendedor.Estado = EntityState.Actualizado;
                moduloVendedor.DataContext = vendedor;

                moduloVendedor.btnGuardar.IsEnabled = false;

                moduloVendedor.ShowDialog();
                ListarVendedores();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Editar vendedor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            vendedor = dgdVendedores.SelectedItem as ProveedorModel;

            if (vendedor != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este vendedor?", "Eliminar vendedor", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    vendedor.Estado = EntityState.Eliminado;

                    string resultado = vendedor.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar vendedor", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListarVendedores();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar vendedor", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
    }
}
