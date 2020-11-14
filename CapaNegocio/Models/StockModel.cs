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
    public class StockModel
    {
        //Campos
        private int idStock;
        private string numReferencia;
        private string idProducto;
        private string nombreProducto;
        private int cantidad;
        private DateTime fechaHora;
        private string ingresadoPor;
        private string estadoProducto;
        private int idProveedor;
        private string nombreProveedor;

        private IStockRepository stockRepository;
        public EntityState Estado{ private get; set; }
        private List<StockModel> listaStock;

        //Propiedades / Modelos / Validar datos
        public int IdStock { get => idStock; set => idStock = value; }
        [Required(ErrorMessage = "El campo \"N° de referencia\" es obligatorio.")]
        [StringLength(maximumLength:50)]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "El campo \"N° de referencia\" es numérico.")]
        public string NumReferencia { get => numReferencia; set => numReferencia = value; }
        [Required(ErrorMessage = "El campo \"Producto\" es obligatorio.")]
        public string IdProducto { get => idProducto; set => idProducto = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        [Required(ErrorMessage = "El campo \"Cantidad\" es obligatorio")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Ingrese un \"Cantidad\" mayor que 0")]
        public int Cantidad { get => cantidad; set => cantidad = value; }
        [Required]
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        [Required(ErrorMessage = "El campo \"Ingresado por\" es obligatorio.")]
        public string IngresadoPor { get => ingresadoPor; set => ingresadoPor = value; }
        public string EstadoProducto { get => estadoProducto; set => estadoProducto = value; }
        [Required(ErrorMessage = "Por favor seleccione un vendedor.")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Por favor seleccione un vendedor.")]
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }

        //Método constructor
        public StockModel()
        {
            stockRepository = new StockRepository();
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
                var stock = new Stock();
                stock.IdStock = idStock;
                stock.NumReferencia = numReferencia;
                stock.IdProducto = idProducto;
                stock.Cantidad = cantidad;
                stock.FechaHora = fechaHora;
                stock.IngresadoPor = ingresadoPor;
                stock.EstadoProducto = estadoProducto;
                stock.IdProveedor = idProveedor;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        stockRepository.Create(stock);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        stockRepository.Delete(idStock);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        stockRepository.Update(stock);
                        mensaje = "Actualizado exitoso.";
                        break;
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

        public List<StockModel> ObtenerTodo()
        {
            var stock = stockRepository.Read();
            listaStock = new List<StockModel>();

            foreach (Stock item in stock)
            {
                listaStock.Add(new StockModel
                {
                    idStock = item.IdStock,
                    numReferencia = item.NumReferencia,
                    idProducto = item.IdProducto,
                    nombreProducto = item.NombreProducto,
                    cantidad = item.Cantidad,
                    fechaHora = item.FechaHora,
                    ingresadoPor = item.IngresadoPor,
                    estadoProducto = item.EstadoProducto,
                    nombreProveedor = item.NombreProveedor
                });
            }
            return listaStock;
        }

        //Otros métodos
        //public List<StockModel> BuscarPorFecha(DateTime? inicio, DateTime? fin)
        //{
        //    return listaStock.FindAll(s => s.fechaHora >= inicio && s.fechaHora <= fin);
        //}

        public List<StockModel> BuscarStockPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var stock = stockRepository.BuscarStockPorFecha(fechaInicio, fechaFin);
            listaStock = new List<StockModel>();

            foreach (Stock item in stock)
            {
                listaStock.Add(new StockModel
                {
                    idStock = item.IdStock,
                    numReferencia = item.NumReferencia,
                    idProducto = item.IdProducto,
                    nombreProducto = item.NombreProducto,
                    cantidad = item.Cantidad,
                    fechaHora = item.FechaHora,
                    ingresadoPor = item.IngresadoPor,
                    estadoProducto = item.EstadoProducto,
                    idProveedor = item.IdProveedor
                });
            }
            return listaStock;
        }

        public List<StockModel> ObtenerStockActual(string numReferencia)
        {
            var stock = stockRepository.ReadStockActual(numReferencia);
            listaStock = new List<StockModel>();

            foreach (Stock item in stock)
            {
                listaStock.Add(new StockModel
                {
                    idStock = item.IdStock,
                    numReferencia = item.NumReferencia,
                    idProducto = item.IdProducto,
                    nombreProducto = item.NombreProducto,
                    cantidad = item.Cantidad,
                    fechaHora = item.FechaHora,
                    ingresadoPor = item.IngresadoPor,
                    estadoProducto = item.EstadoProducto,
                    nombreProveedor = item.NombreProveedor
                });
            }
            return listaStock;
        }
    }
}
