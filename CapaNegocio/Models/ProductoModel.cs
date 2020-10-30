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
    public class ProductoModel
    {
        //Campos
        private string idProducto;
        private string codigoBarras;
        private string descripcion;
        private int marcaId;
        private string nombreMarca;
        private int categoriaId;
        private string nombreCategoria;
        private decimal precio;
        private int cantidad;
        private int reorden;

        private IProductoRepository productoRepository;

        public EntityState Estado{ private get; set; }

        private List<ProductoModel> listaProductos;

        //Propiedades / Modelos / Validar datos
        [Required(ErrorMessage = "El campo código es obligatorio.")]
        [StringLength(maximumLength:50)]
        public string IdProducto { get => idProducto; set => idProducto = value; }
        [StringLength(maximumLength:50)]
        public string CodigoBarras { get => codigoBarras; set => codigoBarras = value; }
        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Seleccione una marca.")]
        public int MarcaId { get => marcaId; set => marcaId = value; }
        public string NombreMarca { get => nombreMarca; set => nombreMarca = value; }
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Seleccione una categoría.")]
        public int CategoriaId { get => categoriaId; set => categoriaId = value; }
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }
        [Required]
        [RegularExpression(@"^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$", ErrorMessage = "El campo costo debe ser mayor que cero.")]
        public decimal Precio { get => precio; set => precio = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public int Reorden { get => reorden; set => reorden = value; }

        public ProductoModel()
        {
            productoRepository = new ProductoRepository();
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
                var producto = new Producto();
                producto.IdProducto = idProducto;
                producto.CodigoBarras = codigoBarras;
                producto.Descripcion = descripcion;
                producto.MarcaId = marcaId;
                producto.CategoriaId = categoriaId;
                producto.Precio = precio;
                producto.Reorden = reorden;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        productoRepository.Create(producto);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        productoRepository.Delete(idProducto);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        productoRepository.Update(producto);
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

        public List<ProductoModel> ObtenerTodo()
        {
            var producto = productoRepository.Read();
            listaProductos = new List<ProductoModel>();

            foreach (Producto item in producto)
            {
                listaProductos.Add(new ProductoModel
                {
                    idProducto = item.IdProducto,
                    codigoBarras = item.CodigoBarras,
                    descripcion = item.Descripcion,
                    marcaId = item.MarcaId,
                    nombreMarca = item.NombreMarca,
                    categoriaId = item.CategoriaId,
                    nombreCategoria = item.NombreCategoria,
                    precio = item.Precio,
                    cantidad = item.Cantidad,
                    reorden = item.Reorden
                });
            }
            return listaProductos;
        }

        //Otros métodos
        public List<ProductoModel> BuscarProductoPorDesc(string descripcion)
        {
            return listaProductos.FindAll(p => p.Descripcion.ToLower().Contains(descripcion.ToLower()));
        }

        public List<ProductoModel> BuscarProductoPorCodigoBarra(string codigo)
        {
            return listaProductos.FindAll(p => p.codigoBarras == codigo);
        }

        public int ActualizarProductoCantidad(string idProducto, int cantidadComprada)
        {
            return productoRepository.ActualizarProductoCantidad(idProducto, cantidadComprada);
        }

        public List<ProductoModel> MostrarProductosCriticos()
        {
            var producto = productoRepository.ReadProductosCriticos();
            listaProductos = new List<ProductoModel>();

            foreach (Producto item in producto)
            {
                listaProductos.Add(new ProductoModel
                {
                    idProducto = item.IdProducto,
                    codigoBarras = item.CodigoBarras,
                    descripcion = item.Descripcion,
                    nombreMarca = item.NombreMarca,
                    nombreCategoria = item.NombreCategoria,
                    precio = item.Precio,
                    reorden = item.Reorden,
                    cantidad = item.Cantidad
                });
            }
            return listaProductos;
        }

        public List<ProductoModel> MostrarListaDeInvetario()
        {
            var producto = productoRepository.ReadListaDeInventarios();
            listaProductos = new List<ProductoModel>();

            foreach (Producto item in producto)
            {
                listaProductos.Add(new ProductoModel
                {
                    idProducto = item.IdProducto,
                    codigoBarras = item.CodigoBarras,
                    descripcion = item.Descripcion,
                    nombreMarca = item.NombreMarca,
                    nombreCategoria = item.NombreCategoria,
                    precio = item.Precio,
                    reorden = item.Reorden,
                    cantidad = item.Cantidad
                });
            }
            return listaProductos;
        }
    }
}
