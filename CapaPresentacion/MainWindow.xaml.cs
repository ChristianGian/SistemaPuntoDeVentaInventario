using CapaPresentacion.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        #region Botones de Sidemenu
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGesVentas_Click(object sender, RoutedEventArgs e)
        {
            PuntoDeVenta puntoDeVenta = new PuntoDeVenta();
            puntoDeVenta.ShowDialog();
        }

        private void BtnGesProducto_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new ListaDeProductos());
        }

        private void BtnStock_Click(object sender, RoutedEventArgs e)
        {
            ActivarBoton(sender);
            MostrarContenido(new Modulos.ModuloStock());
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

        private void btnRegistros_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfSistema_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {

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
        #endregion


    }
}
