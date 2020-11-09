using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class StockAjuste
    {
        public int IdStockAjuste { get; set; }
        public string NumReferencia { get; set; }
        public string IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Accion { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Username { get; set; }

    }
}
