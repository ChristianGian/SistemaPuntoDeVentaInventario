using CapaDatos.Contracts;
using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositories
{
    public class OrdenCanceladaRepository : MasterRepository, IOrdenCanceladaRepository
    {
        public int InsertarOrdenCancelada(OrdenCancelada entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", entidad.NumTransaccion));
            parametros.Add(new SqlParameter("@IdProducto", entidad.IdProducto));
            parametros.Add(new SqlParameter("@Precio", entidad.Precio));
            parametros.Add(new SqlParameter("@Cantidad", entidad.Cantidad));
            parametros.Add(new SqlParameter("@Fecha", entidad.Fecha));
            parametros.Add(new SqlParameter("@AnuladoPor", entidad.AnuladoPor));
            parametros.Add(new SqlParameter("@CanceladoPor", entidad.CanceladoPor));
            parametros.Add(new SqlParameter("@Razon", entidad.Razon));
            parametros.Add(new SqlParameter("@Accion", entidad.Accion));

            return ExecuteNonQuery("InsertarOrdenCancelada");
        }

        public List<OrdenCancelada> Read(DateTime fechaInicio, DateTime fechaFin)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaInicio", fechaInicio));
            parametros.Add(new SqlParameter("@FechaFin", fechaFin));

            var tabla = ExecuteReaderParameters("MostrarOrdenCancelada");
            var lista = new List<OrdenCancelada>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new OrdenCancelada
                {
                    NumTransaccion = item[0].ToString(),
                    IdProducto = item[1].ToString(),
                    Descripcion = item[2].ToString(),
                    Precio = Convert.ToDecimal(item[3]),
                    Cantidad = Convert.ToInt32(item[4]),
                    Total = Convert.ToDecimal(item[5]),
                    Fecha = Convert.ToDateTime(item[6]),
                    AnuladoPor = item[7].ToString(),
                    CanceladoPor = item[8].ToString(),
                    Razon = item[9].ToString(),
                    Accion = item[10].ToString()
                });
            }
            return lista;
        }
    }
}
