using CapaDatos.Entities.EReportes;
using CapaDatos.Repositories.RReportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models.MReportes
{
    public class R_LiquidarPagoModel
    {
        //Propiedades
        public int IdTransaccion { get; set; }
        public string NumTransaccion { get; set; }
        public string IdProducto { get; set; }
        public string DescripcionPro { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }

        private R_LiquidarPagoRepository liquidarPagoRepository = new R_LiquidarPagoRepository();

        //Métodos
        public List<R_LiquidarPagoModel> InfoLiquidarPago(string numTransaccion)
        {
            var liquidarPago = liquidarPagoRepository.LiquidarPago(numTransaccion);
            var lista = new List<R_LiquidarPagoModel>();

            foreach (R_LiquidarPago item in liquidarPago)
            {
                lista.Add(new R_LiquidarPagoModel
                {
                    IdTransaccion = item.IdTransaccion,
                    NumTransaccion = item.NumTransaccion,
                    IdProducto = item.IdProducto,
                    DescripcionPro = item.DescripcionPro,
                    Precio = item.Precio,
                    Cantidad = item.Cantidad,
                    Descuento = item.Descuento,
                    Total = item.Total,
                    Estado = item.Estado
                });
            }
            return lista;
        }
    }
}
