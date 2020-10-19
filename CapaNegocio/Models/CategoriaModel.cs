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
    public class CategoriaModel
    {
        //Campos
        private int idCategoria;
        private string nombreCategoria;

        private ICategoriaRepository categoriaRepository;

        public EntityState Estado {private get; set; }

        //Propiedades / Modelos / Validar datos
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        [Required]
        [StringLength(maximumLength:50)]
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }

        //Método constructor
        public CategoriaModel()
        {
            categoriaRepository = new CategoriaRepository();
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
                var categoria = new Categoria();
                categoria.IdCategoria = idCategoria;
                categoria.NombreCategoria = nombreCategoria;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        categoriaRepository.Create(categoria);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        categoriaRepository.Delete(idCategoria);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        categoriaRepository.Update(categoria);
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

        public List<CategoriaModel> ObtenerTodo()
        {
            var categoria = categoriaRepository.Read();
            var listaCategorias = new List<CategoriaModel>();

            foreach (Categoria item in categoria)
            {
                listaCategorias.Add(new CategoriaModel
                {
                    idCategoria = item.IdCategoria,
                    nombreCategoria = item.NombreCategoria
                });
            }
            return listaCategorias;
        }
    }
}
