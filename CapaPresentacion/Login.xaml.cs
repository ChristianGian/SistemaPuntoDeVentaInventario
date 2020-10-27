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
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        #region Métodos de ayuda
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MensajeError(string msj)
        {
            lblMensajeError.Content = msj;
            stackMensajeError.Visibility = Visibility;
        }

        private void CerrarSesion(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();

            stackMensajeError.Visibility = Visibility.Collapsed;
            this.Show();
        }
        #endregion

        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text != "")
            {
                if (txtPassword.Password != "")
                {
                    //Comprobamos si el nombre de usuario y contraseña son correctos
                    UsuarioModel usuario = new UsuarioModel();
                    bool validarLogin = usuario.Login(txtUsername.Text, txtPassword.Password);

                    if (validarLogin)
                    {
                        if (UserCache.EstadoUsuario == "A")
                        {
                            this.Hide();

                            if (UserCache.Rol == Rol.Administrador)
                            {
                                MainWindow principal = new MainWindow();
                                principal.Show();

                                principal.Closed += CerrarSesion;
                            }
                            else if (UserCache.Rol == Rol.Cajero)
                            {
                                PuntoDeVenta puntoDeVenta = new PuntoDeVenta();
                                puntoDeVenta.Show();

                                puntoDeVenta.Closed += CerrarSesion;
                            }
                        }
                        else
                        {
                            MensajeError("Usuario deshabilitado.\nPóngase en contacto con el administrador.");
                        }
                    }
                    else
                    {
                        MensajeError("Usuario o contraseña incorrecta.\nPor favor intente otra vez.");
                        txtPassword.Focus();
                    }
                }else
                {
                    MensajeError("Por favor ingrese su contraseña");
                }
            }
            else
            {
                MensajeError("Por favor ingrese su nombre de usuario");
            }
        }
    }
}
