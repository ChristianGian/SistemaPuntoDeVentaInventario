using CapaDatos.Contracts;
using CapaDatos.Entities;
using CapaDatos.Repositories;
using CapaNegocio.ValueObjects;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class VendedorModel
    {
        //Campos
        private int idVendedor;
        private string nombreVendedor;
        private string direccion;
        private string personaDeContacto;
        private string telefono;
        private string email;
        private string fax;

        private IVendedorRepository vendedorRepository;

        public EntityState Estado{ get; set; }

        //Propiedades / Modelos / Validar datos
        public int IdVendedor { get => idVendedor; set => idVendedor = value; }
        [Required(ErrorMessage = "El campo vendedor es obligatorio.")]
        public string NombreVendedor { get => nombreVendedor; set => nombreVendedor = value; }
        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        public string Direccion { get => direccion; set => direccion = value; }
        public string PersonaDeContacto { get => personaDeContacto; set => personaDeContacto = value; }
        [Required(ErrorMessage = "El campo teléfono es obligatorio.")]
        [StringLength(maximumLength:9, MinimumLength = 9, ErrorMessage = "Ingrese un teléfono válido")]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required(ErrorMessage = "El campo email es obligatorio")]
        //[RegularExpression("\\w + ([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Por favor ingrese un email válido.")]
        public string Email { get => email; set => email = value; }
        [StringLength(maximumLength:50, ErrorMessage = "Solo se permiten 50 caracteres como máximo en el fax")]
        //[RegularExpression(@"^(\+?\d{1,}(\s?|\-?)\d*(\s?|\-?)\(?\d{2,}\)?(\s?|\-?)\d{3,}\s?\d{3,})$", ErrorMessage = "Ingrese un fax válido")]
        public string Fax { get => fax; set => fax = value; }

        //Método constructor
        public VendedorModel()
        {
            vendedorRepository = new VendedorRepository();
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
                var vendedor = new Vendedor();
                vendedor.IdVendedor = idVendedor;
                vendedor.NombreVendedor = nombreVendedor;
                vendedor.Direccion = direccion;
                if (personaDeContacto != null) vendedor.PersonaDeContacto = personaDeContacto;
                else vendedor.PersonaDeContacto = "-";
                vendedor.Telefono = telefono;
                vendedor.Email = email;
                if (fax != null) vendedor.Fax = fax;
                else vendedor.Fax = "N/A";

                switch (Estado)
                {
                    case EntityState.Agregado:
                        vendedorRepository.Create(vendedor);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        vendedorRepository.Delete(IdVendedor);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        vendedorRepository.Update(vendedor);
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

        public List<VendedorModel> ObtenerTodo()
        {
            var vendedor = vendedorRepository.Read();
            var listaVendedor = new List<VendedorModel>();

            foreach (Vendedor item in vendedor)
            {
                listaVendedor.Add(new VendedorModel
                {
                    idVendedor = item.IdVendedor,
                    nombreVendedor = item.NombreVendedor,
                    direccion = item.Direccion,
                    personaDeContacto = item.PersonaDeContacto,
                    telefono = item.Telefono,
                    email = item.Email,
                    fax = item.Fax
                });
            }
            return listaVendedor;
        }
    }
}
