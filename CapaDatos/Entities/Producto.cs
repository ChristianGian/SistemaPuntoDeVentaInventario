using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entities
{
    public class Producto
    {
        public string IdProducto { get; set; }
        public string CodigoBarras { get; set; }
        public string Descripcion { get; set; }
        public int MarcaId { get; set; }
        public string NombreMarca { get; set; }
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
