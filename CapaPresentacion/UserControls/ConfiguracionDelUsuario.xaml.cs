using CapaComun.Cache;
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
            txtUsernameTab2.Text = UserCache.Username;
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

        private void LimpiarTab2()
        {
            txtPasswordAnteriorTab2.Clear();
            txtNuevoPasswordTab2.Clear();
            txtConfirmarPasswordTab2.Clear();
        }
        #endregion

        #region Tab 1: Crear cuenta
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
        #endregion

        #region Tab 2: Cambiar contraseña
        private void BtnGuardarTab2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var admin = usuario.ObtenerAdmin(UserCache.Username);
                string passAnterior = admin[0].Password;

                if (txtPasswordAnteriorTab2.Password != passAnterior)
                {
                    MessageBox.Show("La contraseña ingresada no coincide con la anterior", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txtNuevoPasswordTab2.Password != txtConfirmarPasswordTab2.Password)
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (MessageBox.Show("¿Cambiar contraseña?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        int respuesta = usuario.CambiarPassword(UserCache.Username, txtNuevoPasswordTab2.Password);
                        if (respuesta > 0)
                        {
                            MessageBox.Show("Su contraseña se ha cambiado correctamente", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                            LimpiarTab2();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelarTab2_Click(object sender, RoutedEventArgs e)
        {
            LimpiarTab2();
        }
        #endregion

        #region Tab 3: Activar/Desactivar cuenta
        private void TxtUsernameTab3_TextChanged(object sender, TextChangedEventArgs e)
        {
            var usuarioEstado = usuario.ObtenerCajeros(txtUsernameTab3.Text);

            if (usuarioEstado.Count > 0)
            {
                if (usuarioEstado[0].EstadoUsuario == "A") chkEstadoUsuario.IsChecked = true;
            }
            else
            {
                chkEstadoUsuario.IsChecked = false;
            }
        }

        private void BtnGuardarTab3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUsernameTab3.Text == "")
                {
                    MessageBox.Show("Por favor ingrese el nombre de usuario.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    string estado;

                    if (chkEstadoUsuario.IsChecked == true) estado = "A";
                    else estado = "D";

                    int resultado = usuario.ActualizarEstadoUsuario(txtUsernameTab3.Text, estado);

                    if (resultado > 0)
                    {
                        MessageBox.Show("El estado del usuario se ha actualizado.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtUsernameTab3.Clear();
                        chkEstadoUsuario.IsChecked = false;
                    }
                    else
                    {
                        MessageBox.Show("Esta cuenta no existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        #endregion
    }
}
