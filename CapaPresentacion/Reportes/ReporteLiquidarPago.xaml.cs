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
    /// Lógica de interacción para ReporteLiquidarPago.xaml
    /// </summary>
    public partial class ReporteLiquidarPago : Window
    {
        //Campos
        string reporte;
        List<ReportDataSource> origenes;
        bool cargado;

        //Método constructor
        public ReporteLiquidarPago(string nombreReporte, List<ReportDataSource> datos)
        {
            InitializeComponent();

            reporte = nombreReporte;
            origenes = datos;

            Contenedor.Load += Contendor_Load;
        }

        private void Contendor_Load(object sender, EventArgs e)
        {
            if (!cargado)
            {
                Contenedor.LocalReport.ReportEmbeddedResource = reporte;

                foreach (var item in origenes)
                {
                    Contenedor.LocalReport.DataSources.Add(item);
                }
                Contenedor.ZoomMode = ZoomMode.Percent;
                Contenedor.ZoomPercent = 100;
                Contenedor.RefreshReport();
                cargado = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
        }
    }
}
