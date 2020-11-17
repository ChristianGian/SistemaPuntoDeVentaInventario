using CapaDatos.Contracts;
using CapaDatos.Entities;
using CapaDatos.Repositories;
using CapaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class UsuarioModel
    {
        //Campos
        private string username;
        private string password;
        private string rol;
        private string nombres;
        private string apellidos;
        private string estadoUsuario;

        private IUsuarioRepository usuarioRepository;

        public EntityState Estado{ private get; set; }

        //Propiedades / Modelos / VAlidar datos
        [Required(ErrorMessage = "El campo nombre de usuario es obligatorio")]
        public string Username { get => username; set => username = value; }
        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        public string Password { get => password; set => password = value; }
        [Required(ErrorMessage = "El campo rol es obligatorio")]
        public string Rol { get => rol; set => rol = value; }
        [Required(ErrorMessage = "El campo rol es obligatorio")]
        public string Nombres { get => nombres; set => nombres = value; }
        [Required(ErrorMessage = "El campo apellidos es obligatorio")]
        public string Apellidos { get => apellidos; set => apellidos = value; }
        [Required(ErrorMessage = "El campo estado es obligatorio")]
        public string EstadoUsuario { get => estadoUsuario; set => estadoUsuario = value; }

        //Método constructor
        public UsuarioModel()
        {
            usuarioRepository = new UsuarioRepository();
        }

        //Métodos
        /// <summary>
        /// Permite guardar los cambios del estado de la entidad.
        /// </summary>
        /// <returns></returns>
        public string GuardarCambios()
        {
            string mensaje = null;

            try
            {
                var usuario = new Usuario();
                usuario.Username = username;
                usuario.Password = password;
                usuario.Rol = rol;
                usuario.Nombres = nombres;
                usuario.Apellidos = apellidos;
                usuario.EstadoUsuario = estadoUsuario;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        usuarioRepository.Create(usuario);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        usuarioRepository.Delete(username);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        usuarioRepository.Update(usuario);
                        mensaje = "Actualizado exitoso.";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //Recuperar la infracción al insertar Username dublicado en la Tabla tblUser
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Ese nombre de usuario ya está en uso. Prueba con otro.";
                else
                    mensaje = ex.Message;
            }
            return mensaje;
        }

        //Login
        public bool Login(string username, string password)
        {
            return usuarioRepository.Login(username, password);
        }

        public List<UsuarioModel> LoginPermisos(string username, string password)
        {
            var usuario = usuarioRepository.LoginPermisos(username, password);
            var listaUsuario = new List<UsuarioModel>();

            foreach (Usuario item in usuario)
            {
                listaUsuario.Add(new UsuarioModel
                {
                    username = item.Username
                });
            }
            return listaUsuario;
        }

        //Mostrar cajeros
        public List<UsuarioModel> ObtenerCajeros(string username)
        {
            var usuario = usuarioRepository.ReadCajero(username);
            var listaCajeros = new List<UsuarioModel>();

            foreach (Usuario item in usuario)
            {
                listaCajeros.Add(new UsuarioModel
                {
                    username = item.Username,
                    password = item.Password,
                    rol = item.Rol,
                    nombres = item.Nombres,
                    apellidos = item.Apellidos,
                    estadoUsuario = item.EstadoUsuario
                });
            }
            return listaCajeros;
        }

        public List<UsuarioModel> ObtenerAdmin(string username)
        {
            var usuario = usuarioRepository.ReadAdmin(username);
            var listaAdmin = new List<UsuarioModel>();

            foreach (Usuario item in usuario)
            {
                listaAdmin.Add(new UsuarioModel
                {
                    username = item.Username,
                    password = item.Password,
                    rol = item.Rol,
                    nombres = item.Nombres,
                    apellidos = item.Apellidos,
                    estadoUsuario = item.EstadoUsuario
                });
            }
            return listaAdmin;
        }

        public int CambiarPassword(string username, string password)
        {
            return usuarioRepository.CambiarPassword(username, password);
        }

        public int ActualizarEstadoUsuario(string username, string estadoUsuario)
        {
            return usuarioRepository.ActualizarEstadoUsuario(username, estadoUsuario);
        }
    }
}
