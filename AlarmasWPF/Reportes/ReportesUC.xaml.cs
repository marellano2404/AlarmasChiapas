using AlarmasWPF.ControlesPersonalizados;
using AlarmasWPF.Core.ViewModels;
using AlarmasWPF.Recursos;
using Microsoft.Reporting.NETCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public event EventHandler SalirOnClick;
        public string FileName;
        public ReportesUC()
        {
            InitializeComponent();
            CargarDatos();
        }    
        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalirOnClick?.Invoke(this, new EventArgs());
        }
        private void CargarDatos()
        {
            var response = ObtenerClientes();
            CargarClientes(response);
        }
        private void CargarClientes(List<Cliente> listado)
        {     
            DatosStackPanel.Children.Clear();
            foreach (var item in listado)
            {
                ClientesLineaUC control = new ClientesLineaUC();
                control.ClienteDC = item;
                DatosStackPanel.Children.Add(control);

                control.SelDatoFechasOnClick += (s, a) =>
                {
                    FormSelFechas modal = new FormSelFechas();
                    modal.EntidadReporte = new DatoReporte();
                    modal.EntidadReporte.IdCliente = item.Id;
                    if (item.Inst == 0)
                    {
                        MostrarMensaje("Falta agregar lugares de instalación");
                    }
                    else
                    {
                        if (item.Users == 0)
                        {
                            MostrarMensaje("Falta agregar usuarios");
                        }
                        else
                        {
                            modal.ClickReporte += (s, e) =>
                            {
                                List<Cliente> ListaCliente = new List<Cliente>();
                                ListaCliente.Add(item);
                                var Lista = ObtenerDatoReporte(modal.EntidadReporte);

                                if (Lista.Count > 0)
                                {
                                    DateTime fechaHoy = DateTime.Now;
                                    ImprimirReporte(Lista, ListaCliente);                                    
                                    var rutaDocumento = "C:" + ConfigServer.UrlReport.Substring(1, 10) + item.Rfc + "\\" + FileName;
                                    if (System.IO.File.Exists(rutaDocumento))
                                    {
                                        GridDatos.Visibility = Visibility.Collapsed;
                                        btnCerrar.Visibility = Visibility.Visible;
                                        VisorReporte.Visibility = Visibility.Visible;
                                        VisorReporte.Source = new Uri(rutaDocumento);
                                    }
                                }
                                modal.Close();
                            };
                            modal.ShowDialog();
                        }
                    }
                          
                };
            }
        }    
        private List<ListaAlarmaEmitidasVM> ObtenerDatoReporte(DatoReporte Entidad)
        {
            var _alarmasEm = new List<ListaAlarmaEmitidasVM>();
            try
            {
                //var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(Entidad);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                   
                    var response = client.PostAsync("api/Eventos/GetEventoAlarma", data);
                    response.Result.EnsureSuccessStatusCode();                   
                    
                    var _Exist = JsonConvert.DeserializeObject<List<ListaAlarmaEmitidasVM>>(response.Result.Content.ReadAsStringAsync().Result);
                    if (_Exist.Count() > 0)
                    {
                        return _alarmasEm = JsonConvert.DeserializeObject<List<ListaAlarmaEmitidasVM>>(response.Result.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        MostrarMensaje("No Existe Datos para el reporte");
                    }

                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
            return _alarmasEm;
        }
      
        private void ImprimirReporte(List<ListaAlarmaEmitidasVM> lista, List<Cliente> ListadatoCliente)
        {

            LocalReport localReportPDF = null;
            DateTime fechaHoy = DateTime.Now;
            string FileNamePath = "C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc + "\\Eventos" + ListadatoCliente.FirstOrDefault().Rfc + "_" + fechaHoy.ToString("yy" + "-" + "MM" + "-" + "dd" + "ss") + ".pdf";
            FileName = "Eventos" + ListadatoCliente.FirstOrDefault().Rfc + "_" + fechaHoy.ToString("yy"+"-"+"MM"+"-"+ "dd" + "ss") + ".pdf";
            var file = System.IO.Path.Combine("C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc + "\\" + FileName);
            if (System.IO.File.Exists(file))
            {
                File.Delete(file);
            }

            FileStream fsPDF = null;
            if (!Directory.Exists(@"C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc))
            { System.IO.Directory.CreateDirectory(@"C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc); }

            if (!File.Exists(FileNamePath))
            {
                try
                {
                    localReportPDF = new LocalReport();
                    string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
                    string ContentStart = _path + @"\Reportes\RptHistorialAlarmas.rdlc";

        
                    //string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Reportes\");
                    //localReportPDF.ReportPath = System.IO.Path.Combine("C:\\Reportes","RptHistorialAlarmas.rdlc");

                    localReportPDF.ReportPath = ContentStart;

                    ReportDataSource rdsListaPDF = new ReportDataSource("DataSetHistorialAlarma", lista);
                    ReportDataSource rdsCabeceraPDF = new ReportDataSource("DataSetClientes", ListadatoCliente);
                    localReportPDF.DataSources.Add(rdsCabeceraPDF);
                    localReportPDF.DataSources.Add(rdsListaPDF);

                    string filename = FileName;
                    string reportTypePDF = "PDF";
                    string mimeTypePDF;
                    string encodingPDF;
                    string fileNameExtensionPDF;
                    Warning[] warningsPDF;
                    string[] streamsPDF;
                    byte[] renderedBytesPDF;

                    renderedBytesPDF = localReportPDF.Render(
                    reportTypePDF,
                    null,
                    out mimeTypePDF,
                    out encodingPDF,
                    out fileNameExtensionPDF,
                    out streamsPDF,
                    out warningsPDF);

                    if (!Directory.Exists(@"C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc))
                        System.IO.Directory.CreateDirectory(@"C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc);

                    String filePathPDF = @"C:\\Reportes\\" + ListadatoCliente.FirstOrDefault().Rfc + "\\" + FileName;
                    fsPDF = new FileStream(filePathPDF, FileMode.Create);

                    fsPDF.Write(renderedBytesPDF, 0, renderedBytesPDF.Length);
                    fsPDF.Close();
                    fsPDF.Dispose();

                }
                catch (Exception ex)
                {
                    fsPDF.Close();
                    localReportPDF.Dispose();

                }
                finally
                {
                    fsPDF.Close();
                    fsPDF.Dispose();
                    localReportPDF.Dispose();
                }
            }
        }
        private List<Cliente> ObtenerClientes()
        {
            var _clientes = new List<Cliente>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/GetListaClientes").Result;
                    _clientes = JsonConvert.DeserializeObject<List<Cliente>>(response);
                    return _clientes;
                }
            }
            catch (Exception e)
            {
                return _clientes;
            }
        }
        private void Buscar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var _clientes = new List<Cliente>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/BuscarCliente/" + TextBoxBuscar.Text.Trim()).Result;
                    _clientes = JsonConvert.DeserializeObject<List<Cliente>>(response);

                    CargarClientes(_clientes);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
        }

        private void MostrarMensaje(string message)
        {
            var modal = new MensajeWindowAccion();
            modal.Mensaje = message;
            modal.OnClickAceptar += (s, e) =>
            {
                modal.Close();
            };
            modal.ShowDialog();
        }

        private void Cerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridDatos.Visibility = Visibility.Visible;
            btnCerrar.Visibility = Visibility.Collapsed;
            VisorReporte.Visibility = Visibility.Collapsed;           
        }
    }
}
