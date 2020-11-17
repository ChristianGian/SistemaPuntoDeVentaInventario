using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapaPresentacion.Reportes
{
    /// <summary>
    /// Lógica de interacción para ProductosVendidos.xaml
    /// </summary>
    public partial class ReporteStockEnHistoria : Window
    {
        //Campos
        string reporte;
        List<ReportDataSource> origenes;
        bool cargado;

        //Método constructor
        public ReporteStockEnHistoria(string nombreReporte, List<ReportDataSource> datos)
        {
            InitializeComponent();

            reporte = nombreReporte;
            origenes = datos;

            Contenedor.Load += Contenedor_Load;
        }

        private void Contenedor_Load(object sender, EventArgs e)
        {
            if (!cargado)
            {
                Contenedor.LocalReport.ReportEmbeddedResource = reporte;

                foreach (var item in origenes)
                {
                    Contenedor.LocalReport.DataSources.Add(item);
                }

                Contenedor.RefreshReport();
                cargado = true;
            }
        }

        #region Métodos de ayuda
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
