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
    public class ProductoRepository : MasterRepository, IProductoRepository
    {
        public int Create(Producto entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdProducto", entidad.IdProducto));
            parametros.Add(new SqlParameter("@CodigoBarras", entidad.CodigoBarras));
            parametros.Add(new SqlParameter("@Descripcion", entidad.Descripcion));
            parametros.Add(new SqlParameter("@MarcaId", entidad.MarcaId));
            parametros.Add(new SqlParameter("@CategoriaId", entidad.CategoriaId));
            parametros.Add(new SqlParameter("@Precio", entidad.Precio));

            return ExecuteNonQuery("CrearProducto");
        }

        public int Delete(int idPK)
        {
            throw new NotImplementedException();
        }

        public int Delete(string idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdProducto", idPK));

            return ExecuteNonQuery("EliminarProducto");
        }

        public List<Producto> Read()
        {
            var tabla = ExecuteReader("MostrarProducto");
            var lista = new List<Producto>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Producto
                {
                    IdProducto = item[0].ToString(),
                    CodigoBarras = item[1].ToString(),
                    Descripcion = item[2].ToString(),
                    MarcaId = Convert.ToInt32(item[3]),
                    NombreMarca = item[4].ToString(),
                    CategoriaId = Convert.ToInt32(item[5]),
                    NombreCategoria = item[6].ToString(),
                    Precio = Convert.ToDecimal(item[7]),
                    Cantidad = Convert.ToInt32(item[8])
                });
            }
            return lista;
        }

        public int Update(Producto entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdProducto", entidad.IdProducto));
            parametros.Add(new SqlParameter("@CodigoBarras", entidad.CodigoBarras));
            parametros.Add(new SqlParameter("@Descripcion", entidad.Descripcion));
            parametros.Add(new SqlParameter("@MarcaId", entidad.MarcaId));
            parametros.Add(new SqlParameter("@CategoriaId", entidad.CategoriaId));
            parametros.Add(new SqlParameter("@Precio", entidad.Precio));

            return ExecuteNonQuery("ActualizarProducto");
        }

        //
        public int ActualizarProductoCantidad(string idProducto, int cantidadComprada)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdProducto", idProducto));
            parametros.Add(new SqlParameter("@CantidadComprada", cantidadComprada));

            return ExecuteNonQuery("PA_ACT_CANTIDAD_PRODUCTO");
        }
    }
}
