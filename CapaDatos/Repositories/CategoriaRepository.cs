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
    public class CategoriaRepository : MasterRepository, ICategoriaRepository
    {
        public int Create(Categoria entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreCategoria", entidad.NombreCategoria));

            return ExecuteNonQuery("CrearCategoria");
        }

        public int Delete(int idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdCategoria", idPK));

            return ExecuteNonQuery("EliminarCategoria");
        }

        public List<Categoria> Read()
        {
            var tabla = ExecuteReader("MostrarCategoria");
            var lista = new List<Categoria>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Categoria
                {
                    IdCategoria = Convert.ToInt32(item[0]),
                    NombreCategoria = item[1].ToString()
                });
            }
            return lista;
        }

        public int Update(Categoria entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdCategoria", entidad.IdCategoria));
            parametros.Add(new SqlParameter("@NombreCategoria", entidad.NombreCategoria));

            return ExecuteNonQuery("ActualizarCategoria");
        }
    }
}
