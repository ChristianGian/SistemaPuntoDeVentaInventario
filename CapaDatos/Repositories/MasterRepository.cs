using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositories
{
    /// <summary>
    /// Se encargará de ejecutar los comandos Transact SQL que se utilizarán en todos los repositorios.
    /// </summary>
    public abstract class MasterRepository : Repository
    {
        //Campos
        protected List<SqlParameter> parametros;

        //Métodos
        /// <summary>
        /// Se encarga de ejecutar comando de no consulta.
        /// </summary>
        /// <param name="transactSql">Comando Transact Sql.</param>
        /// <returns>El número de filas afectadas.</returns>
        protected int ExecuteNonQuery(string transactSql)
        {
            using (var cn = ObtenerConexion())
            {
                cn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = transactSql;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter item in parametros)
                    {
                        cmd.Parameters.Add(item);
                    }
                    int resultado = cmd.ExecuteNonQuery();
                    parametros.Clear();
                    return resultado;
                }
            }
        }

        /// <summary>
        /// Se encarga de ejecutar comandos de consulta.
        /// </summary>
        /// <param name="transactSql">Comando Transact Sql.</param>
        /// <returns>Objeto .SqlDataReader</returns>
        protected DataTable ExecuteReader(string transactSql)
        {
            using (var cn = ObtenerConexion())
            {
                cn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = transactSql;
                    cmd.CommandType = CommandType.StoredProcedure;

                    var drd = cmd.ExecuteReader();

                    using (var tabla = new DataTable())
                    {
                        tabla.Load(drd);
                        drd.Dispose();
                        return tabla;
                    }
                }
            }
        }

        protected DataTable ExecuteReaderParameters(string transactSql)
        {
            using (var cn = ObtenerConexion())
            {
                cn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = transactSql;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter item in parametros)
                    {
                        cmd.Parameters.Add(item);
                    }

                    var drd = cmd.ExecuteReader();

                    using (var tabla = new DataTable())
                    {
                        tabla.Load(drd);
                        drd.Dispose();
                        return tabla;
                    }
                }
            }
        }
    }
}
