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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapaPresentacion.Modulos
{
    /// <summary>
    /// Lógica de interacción para ModuloPOSCambiarPassword.xaml
    /// </summary>
    public partial class ModuloPOSCambiarPassword : Window
    {
        //Campos
        private UsuarioModel usuario = new UsuarioModel();

        //Método constructor
        public ModuloPOSCambiarPassword()
        {
            InitializeComponent();
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

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cajero = usuario.ObtenerCajeros(UserCache.Username);
                string passAnterior = cajero[0].Password;

                if (txtPasswordAnterior.Password != passAnterior)
                {
                    MessageBox.Show("La contraseña ingresada no coincide con la anterior", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (txtNuevaContraseña.Password != txtConfirmarContraseña.Password)
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (MessageBox.Show("¿Cambiar contraseña?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                    {
                        int respuesta = usuario.CambiarPassword(UserCache.Username, txtNuevaContraseña.Password);
                        if (respuesta > 0)
                        {
                            MessageBox.Show("Su contraseña se ha cambiado correctamente", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
