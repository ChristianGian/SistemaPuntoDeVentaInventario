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
    /// Lógica de interacción para ModuloProducto.xaml
    /// </summary>
    public partial class ModuloProducto : Window
    {
        //Campos
        private ProductoModel producto = new ProductoModel();

        //Método constructor
        public ModuloProducto()
        {
            InitializeComponent();

            DataContext = producto;
            CargarComboBox();
        }

        #region Métodos de ayuda
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CargarComboBox()
        {
            MarcaModel marca = new MarcaModel();
            CategoriaModel categoria = new CategoriaModel();

            cmbMarca.ItemsSource = marca.ObtenerTodo();
            cmbMarca.DisplayMemberPath = "MarcaNombre";
            cmbMarca.SelectedValuePath = "IdMarca";

            cmbCategoria.ItemsSource = categoria.ObtenerTodo();
            cmbCategoria.DisplayMemberPath = "NombreCategoria";
            cmbCategoria.SelectedValuePath = "IdCategoria";
        }

        private void Limpiar()
        {
            txtCodigo.Clear();
            txtCodigoBarras.Clear();
            txtDescripcion.Clear();
            cmbMarca.SelectedIndex = -1;
            cmbCategoria.SelectedIndex = -1;
            txtPrecio.Clear();
        }
        #endregion

        #region Handler de botones
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                producto = DataContext as ProductoModel;

                bool validar = new Helps.DataValidation(producto).Validar();

                if (validar)
                {
                    string resultado = producto.GuardarCambios();
                    MessageBox.Show(resultado, "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar();
                    txtCodigo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Validando TextBox
        private void TxtPrecio_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.NumeroDecimal(sender, e);

        private void TxtReorden_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);
        #endregion
    }
}
