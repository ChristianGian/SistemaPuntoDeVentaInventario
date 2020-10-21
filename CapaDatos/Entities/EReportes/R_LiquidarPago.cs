using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities.EReportes
{
    public class R_LiquidarPago
    {
        public int IdTransaccion { get; set; }
        public string NumTransaccion { get; set; }
        public string IdProducto { get; set; }
        public string DescripcionPro { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}
