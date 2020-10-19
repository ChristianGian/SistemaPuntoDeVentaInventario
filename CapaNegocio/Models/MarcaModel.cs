using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Contracts;
using CapaDatos.Entities;
using CapaDatos.Repositories;
using CapaNegocio.ValueObjects;

namespace CapaNegocio.Models
{
    public class MarcaModel
    {
        //Campos
        private int idMarca;
        private string marcaNombre;

        private IMarcaRepository marcaRepository;

        public EntityState Estado { private get; set; }


        //Propiedades / Modelos / Validar datos
        public int IdMarca { get => idMarca; set => idMarca = value; }
        [Required]
        public string MarcaNombre { get => marcaNombre; set => marcaNombre = value; }

        //Método constructor
        public MarcaModel()
        {
            marcaRepository = new MarcaRepository();
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
                var marca = new Marca();
                marca.IdMarca = idMarca;
                marca.MarcaNombre = marcaNombre;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        marcaRepository.Create(marca);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        marcaRepository.Delete(idMarca);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        marcaRepository.Update(marca);
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

        public List<MarcaModel> ObtenerTodo()
        {
            var marca = marcaRepository.Read();
            var listaMarcas = new List<MarcaModel>();

            foreach (Marca item in marca)
            {
                listaMarcas.Add(new MarcaModel
                {
                    idMarca = item.IdMarca,
                    marcaNombre = item.MarcaNombre
                });
            }
            return listaMarcas;
        }
    }
}
