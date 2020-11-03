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
    public class TiendaModel
    {
        //Campos
        private string nombre;
        private string direccion;

        private ITiendaRepository tiendaRepository;
        public EntityState Estado{ private get; set; }

        //Propiedades / Modelos / Validar datos
        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        //Método constructor
        public TiendaModel()
        {
            tiendaRepository = new TiendaRepository();
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
                var tienda = new Tienda();
                tienda.Nombre = nombre;
                tienda.Direccion = direccion;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        tiendaRepository.Create(tienda);
                        mensaje = "Registro exitoso.";
                        break;
                    //case EntityState.Eliminado:
                    //    tiendaRepository.Delete(id);
                    //    mensaje = "Eliminado exitoso.";
                    //    break;
                    case EntityState.Actualizado:
                        tiendaRepository.Update(tienda);
                        mensaje = "Actualizado exitoso.";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        public List<TiendaModel> ObtenerTodo()
        {
            var tienda = tiendaRepository.Read();
            var listaTienda = new List<TiendaModel>();

            foreach (Tienda item in tienda)
            {
                listaTienda.Add(new TiendaModel
                {
                    nombre = item.Nombre,
                    direccion = item.Direccion
                });
            }
            return listaTienda;
        }
    }
}
