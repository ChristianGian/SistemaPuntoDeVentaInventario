using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using MaterialDesignThemes.Wpf.Converters;
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
    /// Lógica de interacción para ModuloConfiguracionDelSistema.xaml
    /// </summary>
    public partial class ModuloConfiguracionDelSistema : Window
    {
        //Campos
        private TiendaModel tienda = new TiendaModel();
        private List<TiendaModel> lista;

        //Método constructor
        public ModuloConfiguracionDelSistema()
        {
            InitializeComponent();

            DataContext = tienda;
            ObtenerDatosTienda();
           
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
        private void ObtenerDatosTienda()
        {
            lista = tienda.ObtenerTodo();

            if (lista.Count > 0)
            {
                tienda.Nombre = lista[0].Nombre;
                tienda.Direccion = lista[0].Direccion;

                tienda.Estado = EntityState.Actualizado;
            }
            else
            {
                tienda.Estado = EntityState.Agregado;
            }
        }
        #endregion

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Guardar detalle de tienda?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
                {
                    tienda = DataContext as TiendaModel;

                    bool validar = new Helps.DataValidation(tienda).Validar();

                    if (validar)
                    {
                        string resultado = tienda.GuardarCambios();
                        MessageBox.Show(resultado, "Detalle de Tienda", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
