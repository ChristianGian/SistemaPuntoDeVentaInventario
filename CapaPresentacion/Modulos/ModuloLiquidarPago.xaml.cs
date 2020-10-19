﻿using CapaNegocio.Models;
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
    /// Lógica de interacción para ModuloLiquidarPago.xaml
    /// </summary>
    public partial class ModuloLiquidarPago : Window
    {
        //Campos
        private ProductoModel producto = new ProductoModel();
        private TransaccionModel transaccion = new TransaccionModel();
        private List<TransaccionModel> listaIdProductoAPagar;

        public List<TransaccionModel> ListaIdProductoAPagar { private get => listaIdProductoAPagar; set => listaIdProductoAPagar = value; }

        //Método constructor
        public ModuloLiquidarPago()
        {
            InitializeComponent();

            txtPagoCon.Focus();
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

        private void TxtPagoCon_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtPagoCon.Text != "")
                {
                    string total = txtTotalAPagar.Text.Replace("S", "").Replace("/", "").Replace(",", "").Replace(" ", "");

                    decimal totalAPagar = Convert.ToDecimal(total);
                    decimal pagoCon = Convert.ToDecimal(txtPagoCon.Text);

                    if (pagoCon > totalAPagar)
                    {
                        decimal vuelto = pagoCon - totalAPagar;
                        txtVuelto.Text = vuelto.ToString();
                    }
                }
                else
                {
                    txtVuelto.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Calcular vuelto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Botones calculadora
        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "7";
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "8";
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "9";
        }

        private void BtnC_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Clear();
            txtPagoCon.Focus();
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "4";
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "5";
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "6";
        }

        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "0";
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "1";
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "2";
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "3";
        }

        private void Btn00_Click(object sender, RoutedEventArgs e)
        {
            txtPagoCon.Text += "00";
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtVuelto.Text) <= 0 || txtVuelto.Text == "")
                {
                    MessageBox.Show("Por favor ingrese la cantidad de pago correcta.", "Liquidar pago", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    //Actulizar la Cantidad en la tabla tblProduct y Estado de la tabla tblCart
                    for (int i = 0; i < listaIdProductoAPagar.Count; i++)
                    {
                        producto.ActualizarProductoCantidad(listaIdProductoAPagar[i].IdProducto, listaIdProductoAPagar[i].Cantidad);
                        transaccion.ActualizarEstadoTransaccion(listaIdProductoAPagar[i].IdTransaccion);
                    }

                    MessageBox.Show("Pago exitoso", "Liquidar pago", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Liquidar pago", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
