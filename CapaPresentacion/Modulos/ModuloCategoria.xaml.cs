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
    /// Lógica de interacción para ModuloCategoria.xaml
    /// </summary>
    public partial class ModuloCategoria : Window
    {
        //Campos
        private CategoriaModel categoria = new CategoriaModel();

        //Método constructor
        public ModuloCategoria()
        {
            InitializeComponent();

            DataContext = categoria;
        }

        #region Métodos de ayuda
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
                categoria = DataContext as CategoriaModel;

                bool validar = new Helps.DataValidation(categoria).Validar();

                if (validar)
                {
                    string resultado = categoria.GuardarCambios();
                    MessageBox.Show(resultado, "Categoría", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtNombreCategoria.Clear();
                    txtNombreCategoria.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
