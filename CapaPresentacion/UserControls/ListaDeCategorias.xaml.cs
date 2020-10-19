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
    /// Lógica de interacción para ListaDeCategorias.xaml
    /// </summary>
    public partial class ListaDeCategorias : UserControl
    {
        //Campos
        private CategoriaModel categoria = new CategoriaModel();

        //Método constructor
        public ListaDeCategorias()
        {
            InitializeComponent();

            ListarCategorias();
        }

        #region Métodos de ayuda
        private void ListarCategorias()
        {
            try
            {
                dgdCategorias.ItemsSource = null;
                dgdCategorias.ItemsSource = categoria.ObtenerTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar categorías", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void BtnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            Modulos.ModuloCategoria moduloCategoria = new Modulos.ModuloCategoria();
            var categoria = new CategoriaModel();

            categoria.Estado = EntityState.Agregado;
            moduloCategoria.DataContext = categoria;

            moduloCategoria.txtNombreCategoria.Focus();
            moduloCategoria.btnActualizar.IsEnabled = false;

            moduloCategoria.ShowDialog();
            ListarCategorias();
            
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            Modulos.ModuloCategoria moduloCategoria = new Modulos.ModuloCategoria();

            categoria = dgdCategorias.SelectedItem as CategoriaModel;

            if (categoria != null)
            {
                categoria.Estado = EntityState.Actualizado;
                moduloCategoria.DataContext = categoria;

                moduloCategoria.btnGuardar.IsEnabled = false;

                moduloCategoria.ShowDialog();
                ListarCategorias();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Editar categoría", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            categoria = dgdCategorias.SelectedItem as CategoriaModel;

            if (categoria != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar esta categoría?", "Eliminar categoria", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    categoria.Estado = EntityState.Eliminado;

                    string resultado = categoria.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar categoría", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListarCategorias();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar categoría", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }  
    }
}
