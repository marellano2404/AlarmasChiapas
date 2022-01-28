using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlarmasWPF.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReportesUC.xaml
    /// </summary>
    public partial class ReportesUC : UserControl
    {
        public ReportesUC()
        {
            InitializeComponent();
            
        }

        public string generarReport()
        {
            LocalReport localReportPDF = null;
            //ReportDataSource rdsCabeceraPDF = null;
            //FileStream fsPDF = null;

            localReportPDF = new LocalReport();

            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Reportes\Informes\Licencias\");
            localReportPDF.ReportPath = System.IO.Path.Combine(path + "RptSolicitudLicencia.rdlc");

            //rdsCabeceraPDF = new ReportDataSource("SolicitudLicenciaDataSet", ListaDatosLicencia);
            //localReportPDF.DataSources.Add(rdsCabeceraPDF);

            //localReportPDF.SubreportProcessing += new SubreportProcessingEventHandler(
            //    (s, e) =>
            //    {
            //        var rds = new ReportDataSource("SolicitudLicenciaFirmasDataSet", ListaDatosFirmantes);
            //        e.DataSources.Add(rds);
            //    });
            localReportPDF.Refresh();
            localReportPDF.ReportEmbeddedResource = System.IO.Path.Combine(path + "SubRptFirmaSolicitudLicencia.rdlc");

            string filename = "SolicitudLicencia.pdf";
            string reportTypePDF = "PDF";
            string mimeTypePDF;
            string encodingPDF;
            string fileNameExtensionPDF;
            Warning[] warningsPDF;
            string[] streamsPDF;
            byte[] renderedBytesPDF;
            string deviceInfo = "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <EmbedFonts>None</EmbedFonts>" +
            "</DeviceInfo>";

            renderedBytesPDF = localReportPDF.Render(
            reportTypePDF,
            //deviceInfo,
            null,
            out mimeTypePDF,
            out encodingPDF,
            out fileNameExtensionPDF,
            out streamsPDF,
            out warningsPDF);

            using (FileStream fs = System.IO.File.Create(path + filename))
            {
                fs.Write(renderedBytesPDF, 0, renderedBytesPDF.Length);
            }
            byte[] data = StreamFile(System.IO.Path.Combine(path + filename));

            //Elimina El archivo despues de descargar
            var file = System.IO.Path.Combine(path + filename);
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }

            return Convert.ToBase64String(data);
        }

        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Reporte_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           

        }
        private byte[] StreamFile(string filename)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

                // Create a byte array of file stream length
                byte[] ImageData = new byte[fs.Length];

                //Read block of bytes from stream into the byte array
                fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

                //Close the File Stream
                fs.Close();
                return ImageData; //return the byte data
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
