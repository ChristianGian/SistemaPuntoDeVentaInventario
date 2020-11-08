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
    public class ProveedorModel
    {
        //Campos
        private int idProveedor;
        private string nombreProveedor;
        private string direccion;
        private string personaDeContacto;
        private string telefono;
        private string email;
        private string fax;

        private IProveedorRepository proveedorRepository;

        public EntityState Estado{ get; set; }

        //Propiedades / Modelos / Validar datos
        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        [Required(ErrorMessage = "El campo vendedor es obligatorio.")]
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
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
        public ProveedorModel()
        {
            proveedorRepository = new ProveedorRepository();
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
                var proveedor = new Proveedor();
                proveedor.IdProveedor = idProveedor;
                proveedor.NombreProveedor = nombreProveedor;
                proveedor.Direccion = direccion;
                if (personaDeContacto != "") proveedor.PersonaDeContacto = personaDeContacto;
                else proveedor.PersonaDeContacto = "-";
                proveedor.Telefono = telefono;
                proveedor.Email = email;
                if (fax != "") proveedor.Fax = fax;
                else proveedor.Fax = "N/A";

                switch (Estado)
                {
                    case EntityState.Agregado:
                        proveedorRepository.Create(proveedor);
                        mensaje = "Registro exitoso.";
                        break;
                    case EntityState.Eliminado:
                        proveedorRepository.Delete(IdProveedor);
                        mensaje = "Eliminado exitoso.";
                        break;
                    case EntityState.Actualizado:
                        proveedorRepository.Update(proveedor);
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

        public List<ProveedorModel> ObtenerTodo()
        {
            var proveedor = proveedorRepository.Read();
            var listaProveedor = new List<ProveedorModel>();

            foreach (Proveedor item in proveedor)
            {
                listaProveedor.Add(new ProveedorModel
                {
                    idProveedor = item.IdProveedor,
                    nombreProveedor = item.NombreProveedor,
                    direccion = item.Direccion,
                    personaDeContacto = item.PersonaDeContacto,
                    telefono = item.Telefono,
                    email = item.Email,
                    fax = item.Fax
                });
            }
            return listaProveedor;
        }
    }
}
