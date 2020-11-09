using CapaComun.Cache;
using CapaNegocio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
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
    /// Lógica de interacción para AjusteDeStock.xaml
    /// </summary>
    public partial class AjusteDeStock : UserControl
    {
        //Campos
        private ProductoModel producto = new ProductoModel();
        private StockAjusteModel stockAjuste = new StockAjusteModel();
        private int cant;

        //Método constructor
        public AjusteDeStock()
        {
            InitializeComponent();

            ListarProductos();
            GenerarNumReferencia();
            txtUsuario.Text = UserCache.Username;
        }

        #region Métodos de ayuda
        private void ListarProductos()
        {
            try
            {
                dgdProductos.ItemsSource = null;
                dgdProductos.ItemsSource = producto.ObtenerTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listar productos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerarNumReferencia()
        {
            Random rd = new Random();
            txtNumReferencia.Text = rd.Next().ToString();
        }

        private void Limpiar()
        {
            txtNumReferencia.Clear();
            txtIdProducto.Clear();
            txtDescripcion.Clear();
            txtCantidad.Clear();
            cmbComando.SelectedIndex = -1;
            txtObservacion.Clear();
        }
        #endregion

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stockAjuste.NumReferencia = txtNumReferencia.Text;
                stockAjuste.IdProducto = txtIdProducto.Text;
                if (txtCantidad.Text == "") stockAjuste.Cantidad = 0;
                else stockAjuste.Cantidad = Convert.ToInt32(txtCantidad.Text);
                stockAjuste.Accion = cmbComando.Text;
                stockAjuste.Observacion = txtObservacion.Text;
                stockAjuste.Fecha = DateTime.Now;
                stockAjuste.Username = txtUsuario.Text;
                stockAjuste.Estado = CapaNegocio.ValueObjects.EntityState.Agregado;

                bool validar = new Helps.DataValidation(stockAjuste).Validar();

                if (validar)
                {
                    //Validar cantidad
                    if (Convert.ToInt32(txtCantidad.Text) > cant)
                    {
                        MessageBox.Show("La cantidad de stock disponible debe ser mayor que la cantidad de ajuste.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Actualizar cantidad
                    if (cmbComando.Text == "Eliminar del inventario")
                    {
                        string idProducto = producto.IdProducto;
                        int cantidad = Convert.ToInt32(txtCantidad.Text);

                        producto.ActualizarProductoCantidad(idProducto, cantidad);
                    }
                    else if (cmbComando.Text == "Agregar al inventario")
                    {
                        string idProducto = producto.IdProducto;
                        int cantidad = Convert.ToInt32(txtCantidad.Text) * -1;

                        producto.ActualizarProductoCantidad(idProducto, cantidad);
                    }

                    //Agregamos los datos a la tabla tblAdjustment
                    string resultado = stockAjuste.GuardarCambios();
                    MessageBox.Show(resultado, "Ajuste de Stock", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (resultado == "Registro exitoso.")
                    {
                        Limpiar();
                        GenerarNumReferencia();
                        ListarProductos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dgdProductos.ItemsSource = null;
                dgdProductos.ItemsSource = producto.BuscarProductoPorDesc(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar productos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            producto = dgdProductos.SelectedItem as ProductoModel;

            txtIdProducto.Text = producto.IdProducto;
            txtDescripcion.Text = producto.Descripcion;
            cant = producto.Cantidad;
        }
    }
}
