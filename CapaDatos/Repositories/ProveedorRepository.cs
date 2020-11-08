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
    public class ProveedorRepository : MasterRepository, IProveedorRepository
    {
        public int Create(Proveedor entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreProveedor", entidad.NombreProveedor));
            parametros.Add(new SqlParameter("@Direccion", entidad.Direccion));
            parametros.Add(new SqlParameter("@PersDeContacto", entidad.PersonaDeContacto));
            parametros.Add(new SqlParameter("@Telefono", entidad.Telefono));
            parametros.Add(new SqlParameter("@Email", entidad.Email));
            parametros.Add(new SqlParameter("@Fax", entidad.Fax));

            return ExecuteNonQuery("CrearProveedor");
        }

        public int Delete(int idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdProveedor", idPK));

            return ExecuteNonQuery("EliminarProveeedor");
        }

        public List<Proveedor> Read()
        {
            var tabla = ExecuteReader("MostrarProveeedor");
            var lista = new List<Proveedor>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Proveedor
                {
                    IdProveedor = Convert.ToInt32(item[0]),
                    NombreProveedor = item[1].ToString(),
                    Direccion = item[2].ToString(),
                    PersonaDeContacto = item[3].ToString(),
                    Telefono = item[4].ToString(),
                    Email = item[5].ToString(),
                    Fax = item[6].ToString()
                });
            }
            return lista;
        }

        public int Update(Proveedor entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdProveedor", entidad.IdProveedor));
            parametros.Add(new SqlParameter("@NombreProveedor", entidad.NombreProveedor));
            parametros.Add(new SqlParameter("@Direccion", entidad.Direccion));
            parametros.Add(new SqlParameter("@PersDeContacto", entidad.PersonaDeContacto));
            parametros.Add(new SqlParameter("@Telefono", entidad.Telefono));
            parametros.Add(new SqlParameter("@Email", entidad.Email));
            parametros.Add(new SqlParameter("@Fax", entidad.Fax));

            return ExecuteNonQuery("ActualizarProveeedor");
        }
    }
}
