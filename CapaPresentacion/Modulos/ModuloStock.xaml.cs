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
        private ProveedorModel proveedor = new ProveedorModel();

        private DateTime hoy = DateTime.Now;
        List<StockModel> listaStock = new List<StockModel>();

        List<ProveedorModel> listaProveedor;

        //Método constructor
        public ModuloStock()
        {
            InitializeComponent();

            //Tab Entrada de stock
            CargarComboBox();
            txtIngresadoPor.Text = UserCache.Nombres + " " + UserCache.Apellidos;

            //Tab Historial
            dtpFechaInicio.SelectedDate = hoy;
            dtpFechaFin.SelectedDate = hoy;
        }

        #region Métodos de ayuda
        //Tab Entrada de Stock
        private void ListarStockActual(string numReferencia)
        {
            try
            {
                dgdStock.ItemsSource = null;
                listaStock = stock.ObtenerStockActual(numReferencia);
                dgdStock.ItemsSource = listaStock;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Stock", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarComboBox()
        {
            listaProveedor = proveedor.ObtenerTodo();

            cmbProveedor.ItemsSource = listaProveedor;
            cmbProveedor.DisplayMemberPath = "NombreProveedor";
            cmbProveedor.SelectedValuePath = "IdProveedor";
        }

        //Tab Historial
        private void ListarHistorialStock(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                dgdHistorialStock.ItemsSource = null;
                dgdHistorialStock.ItemsSource = stock.BuscarStockPorFecha(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar Stock", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EsRangoCorrecto()
        {
            if (Convert.ToDateTime(dtpFechaInicio.Text) > Convert.ToDateTime(dtpFechaFin.Text))
            {
                MessageBox.Show("Asegúrese de ingresar un rango de fechas válida", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        #region TAB 1: Entrada de Stock
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < listaStock.Count; i++)
                {
                    var miStock = listaStock[0] as StockModel;
                    miStock.Estado = EntityState.Actualizado;

                    miStock.GuardarCambios();
                }

                MessageBox.Show("Stock registrado", "Stock", MessageBoxButton.OK, MessageBoxImage.Information);

                txtNumReferencia.Clear();
                cmbProveedor.SelectedIndex = -1;
                txtPersonaContacto.Clear();
                txtDireccion.Clear();

                dgdStock.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminarStockActual_Click(object sender, RoutedEventArgs e)
        {
            stock = dgdStock.SelectedItem as StockModel;

            if (stock != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este stock?", "Eliminar stock", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    stock.Estado = EntityState.Eliminado;

                    string resultado = stock.GuardarCambios();
                    MessageBox.Show(resultado, "Eliminar stock", MessageBoxButton.OK, MessageBoxImage.Information);
                    ListarStockActual(stock.NumReferencia);
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila", "Eliminar stock", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    ListarHistorialStock(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));
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
            stock.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            stock.NombreProveedor = cmbProveedor.Text;

            bool validar = new Helps.DataValidation(stock).Validar();

            if (validar)
            {
                stock.Estado = EntityState.Agregado;
                stock.GuardarCambios();

                txtProducto.Clear();
                txtCantidad.Clear();

                ListarStockActual(stock.NumReferencia);
            }
        }

        private void CmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indice = Convert.ToInt32(cmbProveedor.SelectedIndex);

            if (indice != -1)
            {
                txtPersonaContacto.Text = listaProveedor[indice].PersonaDeContacto;
                txtDireccion.Text = listaProveedor[indice].Direccion;
            }
        }

        #region Validaciones de Textbox
        private void TxtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);
        #endregion

        #endregion

        #region TAB 2: Historial
        private void BtnCargarRegistros_Click(object sender, RoutedEventArgs e)
        {
            ListarHistorialStock(Convert.ToDateTime(dtpFechaInicio.Text), Convert.ToDateTime(dtpFechaFin.Text));
            EsRangoCorrecto();
        }
        #endregion
    }
}
