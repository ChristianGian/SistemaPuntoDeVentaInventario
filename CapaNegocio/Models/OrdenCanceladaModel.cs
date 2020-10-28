using CapaDatos.Contracts;
using CapaDatos.Entities;
using CapaDatos.Repositories;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class OrdenCanceladaModel
    {
        //Campos
        private int idTransaccion;
        private string numTransaccion;
        private string idProducto;
        private decimal precio;
        private int cantidad;
        private DateTime fecha;
        private string anuladoPor;
        private string canceladoPor;
        private string razon;
        private string accion;

        private IOrdenCanceladaRepository ordenCanceladaRepository;
        public EntityState Estado { private get; set; }

        //Propiedades / Modelos / Validar datos
        public int IdTransaccion { get => idTransaccion; set => idTransaccion = value; }
        [Required]
        public string NumTransaccion { get => numTransaccion; set => numTransaccion = value; }
        [Required]
        public string IdProducto { get => idProducto; set => idProducto = value; }
        [Required]
        public decimal Precio { get => precio; set => precio = value; }
        [Required]
        public int Cantidad { get => cantidad; set => cantidad = value; }
        [Required]
        public DateTime Fecha { get => fecha; set => fecha = value; }
        [Required]
        public string AnuladoPor { get => anuladoPor; set => anuladoPor = value; }
        [Required]
        public string CanceladoPor { get => canceladoPor; set => canceladoPor = value; }
        [Required]
        public string Razon { get => razon; set => razon = value; }
        [Required]
        public string Accion { get => accion; set => accion = value; }

        //Método constructor
        public OrdenCanceladaModel()
        {
            ordenCanceladaRepository = new OrdenCanceladaRepository();
        }

        //Métodos
        /// <summary>
        /// Permite guardar los cambios del estado de la entidad.
        /// </summary>
        /// <returns></returns>
        public string GuardarCambios()
        {
            string mensaje = null;

            try
            {
                var ordenCancelada = new OrdenCancelada();
                ordenCancelada.NumTransaccion = numTransaccion;
                ordenCancelada.IdProducto = idProducto;
                ordenCancelada.Precio = precio;
                ordenCancelada.Cantidad = cantidad;
                ordenCancelada.Fecha = fecha;
                ordenCancelada.AnuladoPor = anuladoPor;
                ordenCancelada.CanceladoPor = canceladoPor;
                ordenCancelada.Razon = razon;
                ordenCancelada.Accion = accion;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        ordenCanceladaRepository.InsertarOrdenCancelada(ordenCancelada);
                        mensaje = "Cancelación de orden exitosa.";
                        break;
                    //case EntityState.Eliminado:
                    //    ordenCanceladaRepository.Delete(idCategoria);
                    //    mensaje = "Eliminado exitoso.";
                    //    break;
                    //case EntityState.Actualizado:
                    //    ordenCanceladaRepository.Update(categoria);
                    //    mensaje = "Actualizado exitoso.";
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }
    }
}
