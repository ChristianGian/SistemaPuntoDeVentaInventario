﻿using CapaDatos.Contracts;
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
    public class TransaccionModel
    {
        //Campos
        private int idTransaccion;
        private string numTransaccion;
        private string idProducto;
        private string nombreProducto;
        private decimal precio;
        private int cantidad;
        private decimal descuento;
        private decimal total;
        private DateTime fecha;

        private float igv;

        private ITransaccionRepository transaccionRepository;

        public EntityState Estado{ private get; set; }

        //Propiedades / Modelos / Validar datos
        public int IdTransaccion { get => idTransaccion; set => idTransaccion = value; }
        public string NumTransaccion { get => numTransaccion; set => numTransaccion = value; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string IdProducto { get => idProducto; set => idProducto = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        [Required]
        [RegularExpression(@"^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$", ErrorMessage = "El campo precio debe ser mayor que cero.")]
        public decimal Precio { get => precio; set => precio = value; }
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "El campo cantidad debe ser mayor que cero.")]
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal Descuento { get => descuento; set => descuento = value; }
        public decimal Total { get => total; set => total = value; }
        [Required]
        public DateTime Fecha { get => fecha; set => fecha = value; }
        //IGV
        public float Igv { get => igv; private set => igv = value; }

        //Método constructor
        public TransaccionModel()
        {
            transaccionRepository = new TransaccionRepository();
        }

        //Métodos
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
                var transaccion = new Transaccion();
                transaccion.IdTransaccion = idTransaccion;
                transaccion.NumTransaccion = numTransaccion;
                transaccion.IdProducto = idProducto;
                transaccion.Precio = precio;
                transaccion.Cantidad = cantidad;
                transaccion.Descuento = descuento;
                transaccion.Fecha = fecha;

                switch (Estado)
                {
                    case EntityState.Agregado:
                        transaccionRepository.Create(transaccion);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        transaccionRepository.Delete(idTransaccion);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        transaccionRepository.Update(transaccion);
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

        public List<TransaccionModel> Buscar(string numTransaccion)
        {
            var transaccion = transaccionRepository.Search(numTransaccion);
            var listaTransaccion = new List<TransaccionModel>();

            foreach (Transaccion item in transaccion)
            {
                listaTransaccion.Add(new TransaccionModel
                {
                    numTransaccion = item.NumTransaccion
                });
            }
            return listaTransaccion;
        }

        public List<TransaccionModel> UltimasTransacciones(string numTransaccion)
        {
            var transaccion = transaccionRepository.MostrarUltimasTransacciones(numTransaccion);
            var listaTransaccion = new List<TransaccionModel>();

            foreach (Transaccion item in transaccion)
            {
                listaTransaccion.Add(new TransaccionModel
                {
                    idTransaccion = item.IdTransaccion,
                    idProducto = item.IdProducto,
                    nombreProducto = item.NombreProducto,
                    precio = item.Precio,
                    cantidad = item.Cantidad,
                    descuento = item.Descuento,
                    total = item.Total
                });
            }
            return listaTransaccion;
        }

        public decimal ObtenerPorcentajeIgv()
        {
            return transaccionRepository.ObtenerIgv();
        }

        public int ActualizarEstadoTransaccion(int idTransaccion)
        {
            return transaccionRepository.ActualizarEstadoTransaccion(idTransaccion);
        }
    }
}