using CapaComun.Cache;
using CapaNegocio.Models;
using CapaNegocio.ValueObjects;
using CapaPresentacion.Helps;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    /// Lógica de interacción para ModuloPOSCantidad.xaml
    /// </summary>
    public partial class ModuloPOSCantidad : Window
    {
        //Campos
        private TransaccionModel transaccion = new TransaccionModel();

        private string idProducto;
        private decimal precio;
        private string numTransaccion;

        public ModuloPOSCantidad()
        {
            InitializeComponent();

            txtCantidad.Focus();
        }

        private void TxtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int caracter = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((caracter >= 48 && caracter <= 57) || caracter == 13)
                e.Handled = false;
            else
                e.Handled = true;


            if (caracter == 13 && txtCantidad.Text != string.Empty)
            {
                transaccion.Estado = EntityState.Agregado;
                transaccion.NumTransaccion = numTransaccion;
                transaccion.IdProducto = idProducto;
                transaccion.Precio = precio;
                transaccion.Cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
                transaccion.Fecha = DateTime.Now;
                transaccion.Cajero = UserCache.Username;

                bool validar = new Helps.DataValidation(transaccion).Validar();

                //Comprabar si el producto ya se encuentra registrado
                if (transaccion.ComprobarProductosDuplicados(transaccion.NumTransaccion, transaccion.IdProducto))
                {
                    //Actualizar la cantidad del producto
                    transaccion.Estado = EntityState.Actualizado;
                    transaccion.ActualizarCantidadTransaccion(transaccion.IdTransaccion, transaccion.NumTransaccion, transaccion.IdProducto, transaccion.Cantidad);
                    MessageBox.Show("Registro exitoso.", "Resultado de Transacción", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    //Ingresar el producto
                    if (validar)
                    {
                        string resultado = transaccion.GuardarCambios();
                        MessageBox.Show(resultado, "Resultado de Transacción", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                this.Close();
            }
        }

        public void DetalleProducto(string idProducto, decimal precio, string numTransaccion)
        {
            this.idProducto = idProducto;
            this.precio = precio;
            this.numTransaccion = numTransaccion;
        }
    }
}
