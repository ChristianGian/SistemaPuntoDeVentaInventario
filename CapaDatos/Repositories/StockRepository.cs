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
    public class StockRepository : MasterRepository, IStockRepository
    {
        public int Create(Stock entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumReferencia", entidad.NumReferencia));
            parametros.Add(new SqlParameter("@IdProducto", entidad.IdProducto));
            parametros.Add(new SqlParameter("@Cantidad", entidad.Cantidad));
            parametros.Add(new SqlParameter("@FechaHora", entidad.FechaHora));
            parametros.Add(new SqlParameter("@IngresadoPor", entidad.IngresadoPor));
            parametros.Add(new SqlParameter("@EstadoProducto", entidad.EstadoProducto));

            return ExecuteNonQuery("CrearStock");
        }

        public int Delete(int idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdStock", idPK));

            return ExecuteNonQuery("EliminarStock");
        }

        public List<Stock> Read()
        {
            var tabla = ExecuteReader("MostrarStock");
            var lista = new List<Stock>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Stock
                {
                    IdStock = Convert.ToInt32(item[0]),
                    NumReferencia = item[1].ToString(),
                    IdProducto = item[2].ToString(),
                    NombreProducto = item[3].ToString(),
                    Cantidad = Convert.ToInt32(item[4]),
                    FechaHora = Convert.ToDateTime(item[5]),
                    IngresadoPor = item[6].ToString(),
                    EstadoProducto = item[7].ToString(),
                    //IdVendedor = Convert.ToInt32(item[8])
                });
            }
            return lista;
        }

        public int Update(Stock entidad)
        {
            throw new NotImplementedException();
        }
    }
}
