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
    public class UsuarioRepository : MasterRepository, IUsuarioRepository
    {
        public int Create(Usuario entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", entidad.Username));
            parametros.Add(new SqlParameter("@Password", entidad.Password));
            parametros.Add(new SqlParameter("@Rol", entidad.Rol));
            parametros.Add(new SqlParameter("@Nombres", entidad.Nombres));
            parametros.Add(new SqlParameter("@Apellidos", entidad.Apellidos));
            parametros.Add(new SqlParameter("@Estado", entidad.EstadoUsuario));

            return ExecuteNonQuery("CrearUsuario");
        }

        public int Delete(int idPK)
        {
            throw new NotImplementedException();
        }

        public int Delete(string username)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> Read()
        {
            throw new NotImplementedException();
        }

        public int Update(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        //Métodos propios
        public bool Login(string username, string password)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", username));
            parametros.Add(new SqlParameter("@Password", password));

            return ExecuteLogin("Login");
        }

        public List<Usuario> LoginPermisos(string username, string password)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", username));
            parametros.Add(new SqlParameter("@Password", password));

            var tabla = ExecuteReaderParameters("Login");
            var lista = new List<Usuario>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Usuario
                {
                    Username = item[0].ToString()
                });
            }
            return lista;
        }

        public List<Usuario> ReadCajero(string username)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", username));

            var tabla = ExecuteReaderParameters("MostrarUsuarioCajero");
            var lista = new List<Usuario>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Usuario
                {
                    Username = item[0].ToString(),
                    Password = item[1].ToString(),
                    Rol = item[2].ToString(),
                    Nombres = item[3].ToString(),
                    Apellidos = item[4].ToString(),
                    EstadoUsuario = item[5].ToString()
                });
            }
            return lista;
        }

        public List<Usuario> ReadAdmin(string username)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", username));

            var tabla = ExecuteReaderParameters("MostrarUsuarioAdmin");
            var lista = new List<Usuario>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Usuario
                {
                    Username = item[0].ToString(),
                    Password = item[1].ToString(),
                    Rol = item[2].ToString(),
                    Nombres = item[3].ToString(),
                    Apellidos = item[4].ToString(),
                    EstadoUsuario = item[5].ToString()
                });
            }
            return lista;
        }

        public int CambiarPassword(string username, string password)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", username));
            parametros.Add(new SqlParameter("@Password", password));

            return ExecuteNonQuery("CambiarPassword");
        }

        public int ActualizarEstadoUsuario(string username, string estadoUsuario)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Username", username));
            parametros.Add(new SqlParameter("@EstadoUsuario", estadoUsuario));

            return ExecuteNonQuery("ActualizarEstado");
        }
    }
}
