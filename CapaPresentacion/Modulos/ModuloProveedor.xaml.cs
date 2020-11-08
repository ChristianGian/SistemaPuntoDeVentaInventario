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
using System.Windows.Shapes;

namespace CapaPresentacion.Modulos
{
    /// <summary>
    /// Lógica de interacción para ModuloVendedor.xaml
    /// </summary>
    public partial class ModuloVendedor : Window
    {
        //Campos
        private ProveedorModel proveedor = new ProveedorModel();

        //Método constructor
        public ModuloVendedor()
        {
            InitializeComponent();

            DataContext = proveedor;
        }

        #region Eventos
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Métodos de ayuda
        private void Limpiar()
        {
            txtVendedor.Clear();
            txtDireccion.Clear();
            txtPersDeContacto.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtFax.Clear();
        }
        #endregion

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                proveedor = DataContext as ProveedorModel;

                bool validar = new Helps.DataValidation(proveedor).Validar();

                if (validar && ValidarEmail())
                {
                    string resultado = proveedor.GuardarCambios();
                    MessageBox.Show(resultado, "Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Limpiar();
                    //txtVendedor.Focus();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Validaciones
        private void TxtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);

        private void TxtFax_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.NumeroFax(sender, e);

        private bool ValidarEmail()
        {
            if (Validaciones.EsEmail(txtEmail.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Por favor ingrese un email válido.", "Email", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private void TxtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidarEmail();
        }
        #endregion
    }
}
