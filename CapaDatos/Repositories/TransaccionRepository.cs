using CapaDatos.Contracts;
using CapaDatos.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositories
{
    public class TransaccionRepository : MasterRepository, ITransaccionRepository
    {
        public int Create(Transaccion entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", entidad.NumTransaccion));
            parametros.Add(new SqlParameter("@IdProducto", entidad.IdProducto));
            parametros.Add(new SqlParameter("@Precio", entidad.Precio));
            parametros.Add(new SqlParameter("@Cantidad", entidad.Cantidad));
            parametros.Add(new SqlParameter("@Fecha", entidad.Fecha));
            parametros.Add(new SqlParameter("@Cajero", entidad.Cajero));

            return ExecuteNonQuery("InsertarTransaccion");
        }

        public int Delete(int idPK)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdTransaccion", idPK));

            return ExecuteNonQuery("EliminarTransaccion");
        }

        public List<Transaccion> Read()
        {
            throw new NotImplementedException();
        }

        public int Update(Transaccion entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdTransaccion", entidad.IdTransaccion));
            parametros.Add(new SqlParameter("@Descuento", entidad.Descuento));

            return ExecuteNonQuery("ActualizarTransaccionDescuento");
        }

        //Métodos propios
        public List<Transaccion> Search(string numTransaccion)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", numTransaccion));

            var tabla = ExecuteReaderParameters("BuscarTransaccion");
            var lista = new List<Transaccion>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Transaccion
                {
                    NumTransaccion = item[0].ToString()
                });
            }
            return lista;
        }

        public List<Transaccion> MostrarUltimasTransacciones(string numTransaccion)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", numTransaccion));

            var tabla = ExecuteReaderParameters("MostrarUltimasTransacciones");
            var lista = new List<Transaccion>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Transaccion
                {
                    IdTransaccion = Convert.ToInt32(item[0]),
                    CodigoBarras = item[1].ToString(),
                    IdProducto = item[2].ToString(),
                    NombreProducto = item[3].ToString(),
                    Precio = Convert.ToDecimal(item[4]),
                    Cantidad = Convert.ToInt16(item[5]),
                    Descuento = Convert.ToDecimal(item[6]),
                    Total = Convert.ToDecimal(item[7])
                });
            }
            return lista;
        }

        public decimal ObtenerIgv()
        {
            var tabla = ExecuteReader("ObtenerIGV");
            decimal igv = 0;

            foreach (DataRow item in tabla.Rows)
            {
                igv = Convert.ToDecimal(item[0]);
            }
            return igv;
        }

        public int ActualizarEstadoTransaccion(int idTransaccion)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdTransaccion", idTransaccion));

            return ExecuteNonQuery("ActualizarTransaccionEstado");
        }

        public int ActualizarCantidadTransaccion(int idTransaccion, string numTransaccion, string idProducto, int cantidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdTransaccion", idTransaccion));
            parametros.Add(new SqlParameter("@NumTransaccion", numTransaccion));
            parametros.Add(new SqlParameter("@IdProducto", idProducto));
            parametros.Add(new SqlParameter("@Cantidad", cantidad));

            return ExecuteNonQuery("ActualizarTransaccionCantidad");
        }

        public List<Transaccion> ComprobarProductosDuplicados(string numTransaccion, string idProducto)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", numTransaccion));
            parametros.Add(new SqlParameter("@IdProducto", idProducto));

            var tabla = ExecuteReaderParameters("TransaccionComprobarProductoDuplicado");
            var lista = new List<Transaccion>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Transaccion
                {
                    Cantidad = Convert.ToInt16(item[4])
                });
            }
            return lista;
        }

        public List<Transaccion> ReadProductosVendidos(DateTime fechaInicio, DateTime fechaFin, string username)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaInicio", fechaInicio));
            parametros.Add(new SqlParameter("@FechaFin", fechaFin));
            parametros.Add(new SqlParameter("@Username", username));

            var tabla = ExecuteReaderParameters("ProductosVendidos");
            var lista = new List<Transaccion>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Transaccion
                {
                    IdTransaccion = Convert.ToInt32(item[0]),
                    NumTransaccion = item[1].ToString(),
                    IdProducto = item[2].ToString(),
                    NombreProducto = item[3].ToString(),
                    Precio = Convert.ToDecimal(item[4]),
                    Cantidad = Convert.ToInt16(item[5]),
                    Descuento = Convert.ToDecimal(item[6]),
                    Total = Convert.ToDecimal(item[7]),
                    Fecha = Convert.ToDateTime(item[8]),
                    Cajero = item[9].ToString()
                });
            }
            return lista;
        }

        public List<Transaccion> RegistroProductosVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaInicio", fechaInicio));
            parametros.Add(new SqlParameter("@FechaFin", fechaFin));

            var tabla = ExecuteReaderParameters("RegistrosDeProductosVendidos");
            var lista = new List<Transaccion>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Transaccion
                {
                    IdProducto = item[0].ToString(),
                    NombreProducto = item[1].ToString(),
                    Cantidad = Convert.ToInt32(item[2]),
                    Fecha = Convert.ToDateTime(item[3]),
                    EstadoTransaccion = item[4].ToString(),
                    Total = Convert.ToDecimal(item[5])
                });
            }
            return lista;
        }

        public List<Transaccion> ReadProductosVendidosAgrupados(DateTime fechaInicio, DateTime fechaFin)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaInicio", fechaInicio));
            parametros.Add(new SqlParameter("@FechaFin", fechaFin));

            var tabla = ExecuteReaderParameters("ProductosVendidosAgrupados");
            var lista = new List<Transaccion>();

            foreach (DataRow item in tabla.Rows)
            {
                lista.Add(new Transaccion
                {
                    IdProducto = item[0].ToString(),
                    NombreProducto = item[1].ToString(),
                    Precio = Convert.ToDecimal(item[2]),
                    Cantidad = Convert.ToInt32(item[3]),
                    Descuento = Convert.ToDecimal(item[4]),
                    Total = Convert.ToDecimal(item[5])
                });
            }
            return lista;
        }

        public int EliminarProductosDeCarrito(string numTransaccion)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NumTransaccion", numTransaccion));

            return ExecuteNonQuery("EliminarTransaccionesActuales");
        }
    }
}
