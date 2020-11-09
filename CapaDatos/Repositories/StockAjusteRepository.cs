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
    public class StockAjusteRepository : MasterRepository, IStockAjusteRepository
    {
        public int Create(StockAjuste entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumReferencia", entidad.NumReferencia));
            parametros.Add(new SqlParameter("@IdProducto", entidad.IdProducto));
            parametros.Add(new SqlParameter("@Cantidad", entidad.Cantidad));
            parametros.Add(new SqlParameter("@Accion", entidad.Accion));
            parametros.Add(new SqlParameter("@Observacion", entidad.Observacion));
            parametros.Add(new SqlParameter("@Fecha", entidad.Fecha));
            parametros.Add(new SqlParameter("@Username", entidad.Username));

            return ExecuteNonQuery("CrearStockAjuste");
        }

        public int Delete(int idPK)
        {
            throw new NotImplementedException();
        }

        public List<StockAjuste> Read()
        {
            throw new NotImplementedException();
        }

        public int Update(StockAjuste entidad)
        {
            throw new NotImplementedException();
        }
    }
}
