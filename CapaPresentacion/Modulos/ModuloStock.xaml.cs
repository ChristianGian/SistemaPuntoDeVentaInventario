﻿using CapaNegocio.Models;
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

        //Método constructor
        public ModuloStock()
        {
            InitializeComponent();

            ListarHistorialStock();

            dtpFechaHora.SelectedDate = DateTime.Now;
        }

        #region Métodos de ayuda
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

        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            ModuloVistaProductos vistaProductos = new ModuloVistaProductos();

            vistaProductos.ShowDialog();

            producto = vistaProductos.dgdProductos.SelectedItem as ProductoModel;
            if (producto != null) txtProducto.Text = producto.Descripcion;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtNumReferencia.Clear();
            txtIngresadoPor.Clear();
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

            bool validar = new Helps.DataValidation(stock).Validar();

            if (validar)
            {
                dgdStock.Items.Add(stock);

                txtProducto.Clear();
                txtCantidad.Clear();
            }
        }

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

        #region Validaciones de Textbox
        private void TxtNumReferencia_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);

        private void TxtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e) => Validaciones.SoloNumeros(sender, e);



        #endregion

        
    }
}