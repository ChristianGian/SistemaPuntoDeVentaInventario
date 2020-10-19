using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositories
{
    /// <summary>
    /// Se encarga de la conexión con la base de datos.
    /// </summary>
    public abstract class Repository
    {
        //Campos
        private readonly string cadena;

        //Método constructor
        public Repository()
        {
            cadena = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString.ToString();
        }

        //Métodos 
        protected SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
