using CapaDatos.Entities.EReportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositories.RReportes
{
    public class R_LiquidarPagoRepository : MasterRepository
    {
        public List<R_LiquidarPago> LiquidarPago(string numTransaccion)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", numTransaccion));

            var tabla = ExecuteReaderParameters("Reporte_LiquidarPago");
            var lista = new List<R_LiquidarPago>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new R_LiquidarPago
                {
                    IdTransaccion = Convert.ToInt32(item[0]),
                    NumTransaccion = item[1].ToString(),
                    IdProducto = item[2].ToString(),
                    DescripcionPro = item[3].ToString(),
                    Precio = Convert.ToDecimal(item[4]),
                    Cantidad = Convert.ToInt32(item[5]),
                    Descuento = Convert.ToDecimal(item[6]),
                    Total = Convert.ToDecimal(item[7]),
                    Estado = item[8].ToString()
                });
            }
            return lista;
        }
    }
}
