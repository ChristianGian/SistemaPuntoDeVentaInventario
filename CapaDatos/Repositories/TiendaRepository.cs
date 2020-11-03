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
    public class TiendaRepository : MasterRepository, ITiendaRepository
    {
        public int Create(Tienda entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreTienda", entidad.Nombre));
            parametros.Add(new SqlParameter("@Direccion", entidad.Direccion));

            return ExecuteNonQuery("CrearTienda");
        }

        public int Delete(int idPK)
        {
            throw new NotImplementedException();
        }

        public List<Tienda> Read()
        {
            var tabla = ExecuteReader("MostrarTienda");
            var lista = new List<Tienda>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Tienda
                {
                    Nombre = item[0].ToString(),
                    Direccion = item[1].ToString()
                });
            }
            return lista;
        }

        public int Update(Tienda entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreTienda", entidad.Nombre));
            parametros.Add(new SqlParameter("@Direccion", entidad.Direccion));

            return ExecuteNonQuery("ActualizarTienda");
        }
    }
}
