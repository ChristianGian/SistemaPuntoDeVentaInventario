using CapaDatos.Contracts;
using CapaDatos.Entities;
using CapaDatos.Repositories;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class StockAjusteModel
    {
        //Campos
        private int idStockAjuste;
        private string numReferencia;
        private string idProducto;
        private int cantidad;
        private string accion;
        private string observacion;
        private DateTime fecha;
        private string username;

        private IStockAjusteRepository stockAjusteRepository;

        public EntityState Estado { get; set; }

        //Propiedades / Modelos / Validar datos
        [Required]
        public int IdStockAjuste { get => idStockAjuste; set => idStockAjuste = value; }
        [Required]
        public string NumReferencia { get => numReferencia; set => numReferencia = value; }
        [Required(ErrorMessage = "Por favor, seleccione un producto.")]
        public string IdProducto { get => idProducto; set => idProducto = value; }
        [Required]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get => cantidad; set => cantidad = value; }
        [Required(ErrorMessage = "Por favor, seleccione un acción.")]
        public string Accion { get => accion; set => accion = value; }
        [Required(ErrorMessage = "El campo observación es obligatorio.")]
        public string Observacion { get => observacion; set => observacion = value; }
        [Required]
        public DateTime Fecha { get => fecha; set => fecha = value; }
        [Required]
        public string Username { get => username; set => username = value; }

        //Método constructor
        public StockAjusteModel()
        {
            stockAjusteRepository = new StockAjusteRepository();
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
                var stockAjuste = new StockAjuste();
                stockAjuste.IdStockAjuste = idStockAjuste;
                stockAjuste.NumReferencia = numReferencia;
                stockAjuste.IdProducto = idProducto;
                stockAjuste.Cantidad = cantidad;
                stockAjuste.Accion = accion;
                stockAjuste.Observacion = observacion;
                stockAjuste.Fecha = fecha;
                stockAjuste.Username = username;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        stockAjusteRepository.Create(stockAjuste);
                        mensaje = "Registro exitoso.";
                        break;
                    //case EntityState.Eliminado:
                    //    stockAjusteRepository.Delete(IdStockAjuste);
                    //    mensaje = "Eliminado exitoso.";
                    //    break;
                    //case EntityState.Actualizado:
                    //    stockAjusteRepository.Update(stockAjuste);
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
