using CapaComun.Cache;
using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para ModuloStock.xaml
    /// </summary>
    public partial class ModuloStock : UserControl
    {
        //Campos
        private StockModel stock = new StockModel();
        private ProductoModel producto = new ProductoModel();
        private ProveedorModel vendedor = new ProveedorModel();

        List<ProveedorModel> listaVendedor;

        //Método constructor
        public ModuloStock()
        {
            InitializeComponent();

            //Tab Entrada de stock
            CargarComboBox();
            txtIngresadoPor.Text = UserCache.Nombres + " " + UserCache.Apellidos;

            //Tab Historial
            ListarHistorialStock();
        }

        #region Métodos de ayuda
        //Tab Entrada de Stock
        private void CargarComboBox()
        {
            listaVendedor = vendedor.ObtenerTodo();

            cmbProveedor.ItemsSource = listaVendedor;
            cmbProveedor.DisplayMemberPath = "NombreVendedor";
            cmbProveedor.SelectedValuePath = "IdVendedor";
        }

        //Tab Historial
        private void ListarHistorialStock()
        {
            try
            {
                dgdHistorialStock.ItemsSource = null;
                dgdHistorialStock.ItemsSource = stock.ObtenerTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Stock", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region TAB 1: Entrada de Stock
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var listaStock = new List<StockModel>();

                for (int i = 0; i < dgdStock.Items.Count; i++)
                {
                    stock.Estado = EntityState.Agregado;
                    stock = dgdStock.Items[i] as StockModel;

                    stock.EstadoProducto = "Hecho";
                    stock.GuardarCambios();

                    int num = dgdStock.Items.Count;
                    for (int j = 0; j < num; j++)
                    {
                        dgdStock.Items.RemoveAt(i);
                    }

                    ListarHistorialStock();
                }

                MessageBox.Show("Stock registrado", "Stock", MessageBoxButton.OK, MessageBoxImage.Information);

                txtNumReferencia.Clear();
                cmbProveedor.SelectedIndex = -1;
                txtPersonaContacto.Clear();
                txtDireccion.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnQuitar_Click(object sender, RoutedEventArgs e)
        {
            stock = dgdStock.SelectedItem as StockModel;

            if (stock != null)
            {
                if (MessageBox.Show("¿Está seguro de quitar este stock?", "Eliminar stock", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    dgdStock.Items.Remove(dgdStock.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Quitar stock", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            stock = dgdHistorialStock.SelectedItem as StockModel;

            if (stock != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este stock?", "Eliminar stock", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    stock.Estado = EntityState.Eliminado;

                    string resultado = stock.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar stock", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListarHistorialStock();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar stock", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnGenerarNumRef_Click(object sender, RoutedEventArgs e)
        {
            Random rd = new Random();
            txtNumReferencia.Clear();

            txtNumReferencia.Text += rd.Next();
        }

        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            ModuloVistaProductos vistaProductos = new ModuloVistaProductos();

            vistaProductos.ShowDialog();

            producto = vistaProductos.dgdProductos.SelectedItem as ProductoModel;
            if (producto != null) txtProducto.Text = producto.Descripcion;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtProducto.Clear();
            txtCantidad.Clear();
        }

        private void BtnAgregarALista_Click(object sender, RoutedEventArgs e)
        {
            stock.Estado = EntityState.Agregado;
            stock.NumReferencia = txtNumReferencia.Text.Trim();
            stock.IdProducto = producto.IdProducto;
            stock.NombreProducto = producto.Descripcion;
            if (txtCantidad.Text != "") stock.Cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
            else stock.Cantidad = 0;
            stock.FechaHora = DateTime.Now;
            stock.IngresadoPor = txtIngresadoPor.Text.Trim();
            stock.EstadoProducto = "Pendiente";
            stock.IdVendedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            stock.NombreVendedor = cmbProveedor.Text;

            bool validar = new Helps.DataValidation(stock).Validar();

            if (validar)
            {
                dgdStock.Items.Add(stock);

                txtProducto.Clear();
                txtCantidad.Clear();
            }
        }

        private void CmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indice = Convert.ToInt32(cmbProveedor.SelectedIndex);

            if (indice != -1)
            {
                txtPersonaContacto.Text = listaVendedor[indice].PersonaDeContacto;
                txtDireccion.Text = listaVendedor[indice].Direccion;
            }
        }

        #region Validaciones de Textbox
        private void TxtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);
        #endregion

        #endregion

        #region TAB 2: Historial
        private void BtnCargarRegistros_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgdHistorialStock.ItemsSource = null;
                dgdHistorialStock.ItemsSource = stock.BuscarPorFecha(dtpFechaInicio.SelectedDate, dtpFechaFin.SelectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Stock", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnListarTodoHistorial_Click(object sender, RoutedEventArgs e)
        {
            ListarHistorialStock();

            dtpFechaInicio.SelectedDate = null;
            dtpFechaFin.SelectedDate = null;
        }
        #endregion
    }
}
