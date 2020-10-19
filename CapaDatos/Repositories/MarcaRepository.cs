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
    public class MarcaRepository : MasterRepository, IMarcaRepository
    {
        public int Create(Marca entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@MarcaNombre", entidad.MarcaNombre));

            return ExecuteNonQuery("CrearMarca");
        }

        public int Delete(int idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdMarca", idPK));

            return ExecuteNonQuery("EliminarMarca");
        }

        public List<Marca> Read()
        {
            var tabla = ExecuteReader("MostrarMarca");
            var lista = new List<Marca>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Marca
                {
                    IdMarca = Convert.ToInt32(item[0]),
                    MarcaNombre = item[1].ToString()
                });
            }
            return lista;
        }

        public int Update(Marca entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdMarca", entidad.IdMarca));
            parametros.Add(new SqlParameter("@MarcaNombre", entidad.MarcaNombre));

            return ExecuteNonQuery("ActualizarMarca");
        }
    }
}
