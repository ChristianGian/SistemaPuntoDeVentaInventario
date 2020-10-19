using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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
    /// Lógica de interacción para ModuloMarca.xaml
    /// </summary>
    public partial class ModuloMarca : Window
    {
        //Campos
        private MarcaModel marca = new MarcaModel();

        //Método constructor
        public ModuloMarca()
        {
            InitializeComponent();

            DataContext = marca;
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
                marca = DataContext as MarcaModel;

                bool validar = new Helps.DataValidation(marca).Validar();
                
                if (validar)
                {
                    string resultado = marca.GuardarCambios();
                    MessageBox.Show(resultado, "Guardar marca", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtNombreMarca.Clear();
                    txtNombreMarca.Focus();
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
