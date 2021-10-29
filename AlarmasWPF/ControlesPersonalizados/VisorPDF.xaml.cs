using System.Windows;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using GemBox.Pdf;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Net.Http;

namespace AlarmasWPF.ControlesPersonalizados
{
    /// <summary>
    /// Lógica de interacción para VisorPDF.xaml
    /// </summary>
    public partial class VisorPDF : Window
    {
        #region Propiedades
        XpsDocument xpsDocument;
        private string nombreCarpeta;
        private string nombreArchivo;
        private string nombreDirectorio; //aux para saber de que directorio viene si de acádemico o personales.                
        private string ArchivoBase64;
        #endregion

        #region Constructores con sobrecarga
        public VisorPDF(string carpeta, string archivo, string directorio) //string path
        {
            InitializeComponent();
            ComponentInfo.SetLicense("FREE-LIMITED-KEY"); //tiene el límite de visualizar 2 páginas.
            nombreCarpeta = carpeta;
            nombreArchivo = archivo;
            nombreDirectorio = directorio;
            CargarPDF(carpeta, archivo, directorio);
        }

        public VisorPDF(string docBase64)
        {
            InitializeComponent();
            ComponentInfo.SetLicense("FREE-LIMITED-KEY"); //tiene el límite de visualizar 2 páginas.
            ArchivoBase64 = docBase64;
            CargarReportePDF(docBase64);
        }
        #endregion

        #region Eventos
        private void AbrirArchivo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                if (fileDialog.ShowDialog() == true)
                {
                    using (var document = PdfDocument.Load(fileDialog.FileName))
                    {
                        // XpsDocument needs to stay referenced so that DocumentViewer can access additional required resources.
                        // Otherwise, GC will collect/dispose XpsDocument and DocumentViewer will not work.
                        this.xpsDocument = document.ConvertToXpsDocument(SaveOptions.Xps);
                        this.DocumentViewer.Document = this.xpsDocument.GetFixedDocumentSequence();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.ToString());
            }
        }        

        private async void GuardarArchivo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "Archivo (*.pdf)|*.pdf";
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;
                bool? result = dialog.ShowDialog(); //shows save file dialog
                if (result == true)
                {
                    string remoteUri = "";
                    switch (nombreDirectorio)
                    {
                        //case "academico":                            
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + nombreCarpeta + "/Academicos/";
                        //    break;
                        //case "oficios":                            
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + nombreCarpeta + "/Oficios/";
                        //    break;
                        //case "licencias":                            
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + nombreCarpeta + "/Licencias/";
                        //    break;
                        //case "personales":                                                              
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + nombreCarpeta + "/Personales/";
                        //    break;
                        default:  remoteUri = string.Empty;
                            break;
                    }                    
                    if (!string.IsNullOrEmpty(remoteUri))
                    {
                        string fileName = nombreArchivo, myStringWebResource = null;
                        myStringWebResource = remoteUri + fileName;
                        if (File.Exists(myStringWebResource))
                        {
                            WebClient myWebClient = new WebClient();
                            Uri uri = new Uri(myStringWebResource);
                            myWebClient.DownloadFileCompleted += DownloadFileCompleted(dialog.FileName);
                            myWebClient.DownloadFileAsync(uri, dialog.FileName);
                        }
                        else { MostrarMensaje("No se puede descargar, porque el archivo no existe o ha sido eliminado."); }
                    }
                    else
                    {
                        byte[] bytes = Convert.FromBase64String(ArchivoBase64);
                        string ruta = dialog.FileName;                        
                        //System.IO.File.WriteAllBytes(ruta, bytes);
                        using (var fsPDF = new FileStream(ruta, FileMode.Create))
                        {                            
                            await fsPDF.WriteAsync(bytes, 0, bytes.Length);
                            fsPDF.Close();
                        }
                        MostrarMensaje("Archivo descargado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
        }
        #endregion

        #region Metodos
        public AsyncCompletedEventHandler DownloadFileCompleted(string filename)
        {
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                var _filename = filename;
                if (e.Error != null)
                {
                    throw e.Error;
                }
                MostrarMensaje("Archivo descargado correctamente.");
            };
            return new AsyncCompletedEventHandler(action);
        }

        private void CargarPDF(string directoryRFC, string file, string directory)
        {
            try
            {
                string remoteUri = "";
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    switch (directory)
                    {
                        //case "academico":                            
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + directoryRFC + "/Academicos/";
                        //    break;
                        //case "oficios":                            
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + directoryRFC + "/Oficios/";
                        //    break;
                        //case "licencias":
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + directoryRFC + "/Licencias/";                                       
                        //    break;
                        //case "personales":                                                   
                        //    remoteUri = @Repositorio.RutaArchivos() + "/ExpedientesRH/" + directoryRFC + "/Personales/";
                        //    break;
                        default: remoteUri = string.Empty;
                            break;
                    }                  
                }
                string fileName = file, myStringWebResource = null;
                myStringWebResource = remoteUri + fileName;
                
                if (File.Exists(myStringWebResource))
                {
                    using (var documento = PdfDocument.Load(myStringWebResource))
                    {
                        this.xpsDocument = documento.ConvertToXpsDocument(SaveOptions.Xps);
                        this.DocumentViewer.Document = this.xpsDocument.GetFixedDocumentSequence();
                    }                    
                }
                else
                {
                    MostrarMensaje("El documento no existe. -Consultar con soporte a sistemas.");
                }
            }
            catch (Exception ex)
            {
                //StackTrace = "   en System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)\r\n   en System.IO.FileStream.SeekCore(Int64 offset, SeekOrigin origin)\r\n   en System.IO.FileStream.Seek(Int64 offset, SeekOrigin origin)\r\n   en \u0002 ​.Seek(Int64 \u0002, SeekO...
                //se ha intentado mover el archivo más allá del inicio del archivo --> porque en el pdf no hay contenido.
                MostrarMensaje(ex.Message);

            }
                        
        }

        private void CargarReportePDF(string docBase64)
        {
            try
            {
                var bytes = Convert.FromBase64String(docBase64);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (var documento = PdfDocument.Load(ms))
                    {
                        this.xpsDocument = documento.ConvertToXpsDocument(SaveOptions.Xps);
                        this.DocumentViewer.Document = this.xpsDocument.GetFixedDocumentSequence();
                    }
                }
            }
            catch (Exception e)
            {
                MostrarMensaje(e.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            MensajeWindow modal = new MensajeWindow();
            modal.Titulo = "SIA - Recursos Humanos";
            modal.Mensaje = mensaje;
            modal.ShowDialog();
        }
        #endregion

    }
}
