using CapaComun.Cache;
using CapaNegocio.Models;
using CapaPresentacion.Modulos;
using CapaPresentacion.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
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
using Tulpep.NotificationWindow;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Campos
        private Button botonActual;

        //Método constructor
        public MainWindow()
        {
            InitializeComponent();

            lblUsuario.Content = UserCache.Nombres + " " + UserCache.Apellidos;
            lblRol.Content = UserCache.Rol;

            NotificacionProductosCriticos();
        }

        #region Botones de Sidemenu
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new Dashboard());
        }

        //private void BtnGesVentas_Click(object sender, RoutedEventArgs e)
        //{
        //    PuntoDeVenta puntoDeVenta = new PuntoDeVenta();
        //    puntoDeVenta.ShowDialog();
        //}

        private void BtnGesProducto_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ListaDeProductos());
        }

        private void BtnVendedor_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ListaDeVendedores());
        }

        private void BtnStock_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ModuloStock());
        }

        private void BtnAjusteDeStock_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new AjusteDeStock());
        }

        private void BtnGesCategoria_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ListaDeCategorias());
        }

        private void BtnGesMarca_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ListaDeMarcas());
        }

        private void BtnRegistros_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new Registros());
        }

        private void BtnHistorialVentas_Click(object sender, RoutedEventArgs e)
        {
            ModuloProductosVendidos productosVendidos = new ModuloProductosVendidos();
            productosVendidos.ShowDialog();
        }

        private void BtnConfSistema_Click(object sender, RoutedEventArgs e)
        {
            ModuloConfiguracionDelSistema configuracionDelSistema = new ModuloConfiguracionDelSistema();
            configuracionDelSistema.ShowDialog();
        }

        private void BtnConfUsuario_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ConfiguracionDelUsuario());
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Métodos de ayuda
        private void MostrarContenido(UserControl control)
        {
            //Con ScrollView
            //PanelContenedor.Content = null;
            //PanelContenedor.Content = control;

            PanelContenedor.Children.Clear();
            PanelContenedor.Children.Add(control);
        }

        private void ActivarBoton(object senderBtn)
        {
            DesactivarBoton();

            if (senderBtn != null)
            {
                botonActual = (Button)senderBtn;
                botonActual.Background = new SolidColorBrush(Color.FromRgb(8, 143, 132));
                //botonActual.Content= Brushes.Black;
            }
        }

        private void DesactivarBoton()
        {
            if (botonActual != null)
            {
                botonActual.Background = new SolidColorBrush(Color.FromRgb(36, 36, 36));
            }
        }

        public void NotificacionProductosCriticos()
        {
            ProductoModel producto = new ProductoModel();

            string mensaje = "";
            var listaProCriticos = producto.MostrarProductosCriticos();
            string n = listaProCriticos.Count.ToString();

            for (int i = 0; i < listaProCriticos.Count; i++)
            {
                mensaje += $"{i + 1}. {listaProCriticos[i].Descripcion}.\n";
            }

            PopupNotifier notificar = new PopupNotifier();
            notificar.Image = Properties.Resources.AdvertenciaNotificacion_96px;
            notificar.TitleText = $"{n} Producto(s) crítico(s)";
            notificar.ContentText = mensaje;
            notificar.Popup();
        }
        #endregion

        #region Eventos
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}
