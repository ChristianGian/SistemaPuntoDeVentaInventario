using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CapaPresentacion
{
    public class Validaciones
    {
        public static void SoloLetras(object sender, TextCompositionEventArgs e)
        {
            int caracter = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((caracter >= 65 && caracter <= 90) || (caracter >= 97 && caracter <= 122))
                e.Handled = false;
            else
                e.Handled = true;
        }

        public static void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            int caracter = Convert.ToInt32(Convert.ToChar(e.Text));
            if (caracter >= 48 && caracter <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        public static void NumeroDecimal(object sender, TextCompositionEventArgs e)
        {
            int caracter = Convert.ToInt32(Convert.ToChar(e.Text));
            if ((caracter >= 48 && caracter <= 57) || (caracter == 8) || caracter == 46)
                e.Handled = false;
            else
                e.Handled = true;


        }
    }
}
