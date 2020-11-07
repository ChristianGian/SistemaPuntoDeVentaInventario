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
    public class VendedorRepository : MasterRepository, IVendedorRepository
    {
        public int Create(Vendedor entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreVendedor", entidad.NombreVendedor));
            parametros.Add(new SqlParameter("@Direccion", entidad.Direccion));
            parametros.Add(new SqlParameter("@PersDeContacto", entidad.PersonaDeContacto));
            parametros.Add(new SqlParameter("@Telefono", entidad.Telefono));
            parametros.Add(new SqlParameter("@Email", entidad.Email));
            parametros.Add(new SqlParameter("@Fax", entidad.Fax));

            return ExecuteNonQuery("CrearVendedor");
        }

        public int Delete(int idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdVendedor", idPK));

            return ExecuteNonQuery("EliminarVendedor");
        }

        public List<Vendedor> Read()
        {
            var tabla = ExecuteReader("MostrarVendedor");
            var lista = new List<Vendedor>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Vendedor
                {
                    IdVendedor = Convert.ToInt32(item[0]),
                    NombreVendedor = item[1].ToString(),
                    Direccion = item[2].ToString(),
                    PersonaDeContacto = item[3].ToString(),
                    Telefono = item[4].ToString(),
                    Email = item[5].ToString(),
                    Fax = item[6].ToString()
                });
            }
            return lista;
        }

        public int Update(Vendedor entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdVendedor", entidad.IdVendedor));
            parametros.Add(new SqlParameter("@NombreVendedor", entidad.NombreVendedor));
            parametros.Add(new SqlParameter("@Direccion", entidad.Direccion));
            parametros.Add(new SqlParameter("@PersDeContacto", entidad.PersonaDeContacto));
            parametros.Add(new SqlParameter("@Telefono", entidad.Telefono));
            parametros.Add(new SqlParameter("@Email", entidad.Email));
            parametros.Add(new SqlParameter("@Fax", entidad.Fax));

            return ExecuteNonQuery("ActualizarVendedor");
        }
    }
}
