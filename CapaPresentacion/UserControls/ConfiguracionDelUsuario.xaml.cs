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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion.UserControls
{
    /// <summary>
    /// Lógica de interacción para ConfiguracionDelUsuario.xaml
    /// </summary>
    public partial class ConfiguracionDelUsuario : UserControl
    {
        //Campos
        private UsuarioModel usuario = new UsuarioModel();

        //Método constructor
        public ConfiguracionDelUsuario()
        {
            InitializeComponent();

            DataContext = usuario;
        }

        #region Métodos de ayuda
        private void Limpiar()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmarPass.Clear();
            cmbRol.SelectedIndex = -1;
            txtNombres.Clear();
            txtApellidos.Clear();
            cmbEstado.SelectedIndex = -1;
        }
        #endregion
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPassword.Password != txtConfirmarPass.Password)
                {
                    MessageBox.Show("La contraseña con coincide", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                usuario = DataContext as UsuarioModel;
                usuario.Password = txtPassword.Password;

                bool validar = new Helps.DataValidation(usuario).Validar();

                if (validar)
                {
                    string resultado = usuario.GuardarCambios();
                    MessageBox.Show(resultado, "Usuario", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == "Registro exitoso.") Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
