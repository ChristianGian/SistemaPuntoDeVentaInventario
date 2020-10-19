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
    /// Lógica de interacción para GestionarMarca.xaml
    /// </summary>
    public partial class ListaDeMarcas : UserControl
    {
        //Campos
        private MarcaModel marca = new MarcaModel();

        //Método constructor
        public ListaDeMarcas()
        {
            InitializeComponent();

            ListarMarcas();
        }

        #region Métodos de ayuda
        private void ListarMarcas()
        {
            try
            {
                dgdMarcas.ItemsSource = null;
                dgdMarcas.ItemsSource = marca.ObtenerTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar marcas", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        private void BtnAgregarMarca_Click(object sender, RoutedEventArgs e)
        {
            Modulos.ModuloMarca moduloMarca = new Modulos.ModuloMarca();
            var marca = new MarcaModel();

            marca.Estado = EntityState.Agregado;
            moduloMarca.DataContext = marca;

            moduloMarca.txtNombreMarca.Focus();
            moduloMarca.btnActualizar.IsEnabled = false;

            moduloMarca.ShowDialog();
            ListarMarcas();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            Modulos.ModuloMarca moduloMarca = new Modulos.ModuloMarca();

            marca = dgdMarcas.SelectedItem as MarcaModel;

            if (marca != null)
            {
                marca.Estado = EntityState.Actualizado;
                moduloMarca.DataContext = marca;

                moduloMarca.btnGuardar.IsEnabled = false;

                moduloMarca.ShowDialog();
                ListarMarcas();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Editar marca", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            marca = dgdMarcas.SelectedItem as MarcaModel;

            if (marca != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar esta marca?", "Eliminar marca", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    marca.Estado = EntityState.Eliminado;

                    string resultado = marca.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar marca", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListarMarcas();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar marca", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
