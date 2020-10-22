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
                    IdProducto = item[1].ToString(),
                    NombreProducto = item[2].ToString(),
                    Precio = Convert.ToDecimal(item[3]),
                    Cantidad = Convert.ToInt16(item[4]),
                    Descuento = Convert.ToDecimal(item[5]),
                    Total = Convert.ToDecimal(item[6])
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

        public List<Transaccion> ReadProductosVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaInicio", fechaInicio));
            parametros.Add(new SqlParameter("@FechaFin", fechaFin));

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
                    Fecha = Convert.ToDateTime(item[8])
                });
            }
            return lista;
        }
    }
}
