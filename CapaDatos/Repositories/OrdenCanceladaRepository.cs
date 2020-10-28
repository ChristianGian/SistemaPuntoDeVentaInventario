using CapaDatos.Contracts;
using CapaDatos.Entities;
using System;
using System.Collections.Generic;
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
    }
}
