using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class OrdenCancelada
    {
        public int IdTransaccion { get; set; }
        public string NumTransaccion { get; set; }
        public string IdProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string AnuladoPor { get; set; }
        public string CanceladoPor { get; set; }
        public string Razon{ get; set; }
        public string Accion { get; set; }
    }
}
