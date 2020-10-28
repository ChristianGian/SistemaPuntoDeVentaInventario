using CapaComun.Cache;
using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Lógica de interacción para ModuloCancelarOrden.xaml
    /// </summary>
    public partial class ModuloCancelarOrden : Window
    {
        //Campos
        OrdenCanceladaModel ordenCancelada = new OrdenCanceladaModel();
        private bool valido;

        //Proiedades
        public bool Valido { get => valido; private set => valido = value; }

        public ModuloCancelarOrden()
        {
            InitializeComponent();
            DataContext = ordenCancelada;
        }

        #region Métodos de ayuda
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void BtnCancelarOrden_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPassword.Password != "" )
                {
                    UsuarioModel usuario = new UsuarioModel();
                    var lista = usuario.LoginPermisos(txtUsername.Text, txtPassword.Password);

                    string username = usuario.Username;

                    if (lista.Count > 0)
                    {
                        valido = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
