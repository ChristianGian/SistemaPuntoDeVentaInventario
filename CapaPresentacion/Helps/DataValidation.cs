using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Helps
{
    /// <summary>
    /// Se encarga de recuperar los errores de las entidades modelos.
    /// </summary>
    public class DataValidation
    {
        //Campos
        private ValidationContext contexto;
        private List<ValidationResult> resultados;
        private bool valido;
        private string mensaje;

        //Método constructor
        public DataValidation(object instancia)
        {
            contexto = new ValidationContext(instancia);
            resultados = new List<ValidationResult>();
            valido = Validator.TryValidateObject(instancia, contexto, resultados, true);
        }

        //Métodos
        public bool Validar()
        {
            if (valido == false)
            {
                foreach (ValidationResult item in resultados)
                {
                    mensaje += item.ErrorMessage + "\n";
                }
                System.Windows.MessageBox.Show(mensaje, "Mensaje", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
            return valido;
        }
    }
}
