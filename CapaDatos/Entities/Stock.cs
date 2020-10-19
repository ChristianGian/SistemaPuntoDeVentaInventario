using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class Stock
    {
        public int IdStock { get; set; }
        public string NumReferencia { get; set; }
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaHora { get; set; }
        public string IngresadoPor { get; set; }
        public string EstadoProducto { get; set; }
        public int IdVendedor { get; set; }


    }
}
