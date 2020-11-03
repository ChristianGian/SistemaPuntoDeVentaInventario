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
        private string descripcion;
        private decimal precio;
        private int cantidad;
        private decimal total;
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
        public string Descripcion { get => descripcion; set => descripcion = value; }
        [Required]
        public decimal Precio { get => precio; set => precio = value; }
        [Required]
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Total { get => total; set => total = value; }
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

        public List<OrdenCanceladaModel> ObtenerTodo(DateTime fechaInicio, DateTime fechaFin)
        {
            var productoCancelado = ordenCanceladaRepository.Read(fechaInicio, fechaFin);
            var listaordenesCanceladas = new List<OrdenCanceladaModel>();

            foreach (OrdenCancelada item in productoCancelado)
            {
                listaordenesCanceladas.Add(new OrdenCanceladaModel
                {
                    numTransaccion = item.NumTransaccion,
                    idProducto = item.IdProducto,
                    descripcion = item.Descripcion,
                    precio = item.Precio,
                    cantidad = item.Cantidad,
                    total = item.Total,
                    fecha = item.Fecha,
                    anuladoPor = item.AnuladoPor,
                    canceladoPor = item.CanceladoPor,
                    razon = item.Razon,
                    accion = item.Accion
                });
            }
            return listaordenesCanceladas;
        }
    }
}
