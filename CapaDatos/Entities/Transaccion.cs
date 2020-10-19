using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class Transaccion
    {
        public int IdTransaccion { get; set; }
        public string NumTransaccion { get; set; }
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoTransaccion { get; set; }
        public string Cajero { get; set; }
    }
}
