using AlarmasWPF.Usuarios;
using AlarmasWPF.ControlesPersonalizados;
using AlarmasWPF.Core.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using AlarmasWPF.Instalaciones;
using AlarmasWPF.Recursos;
using Microsoft.Reporting.NETCore;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Windows.Markup;
using System.Reflection;

namespace AlarmasWPF.Clientes
{
    /// <summary>
    /// Lógica de interacción para ClientesUC.xaml
    /// </summary>
    /// 
    public partial class ClientesUC : UserControl
    {
        public event EventHandler Regresar;
        public string FileName;
        //private readonly ILogger logger;

        //private static HttpClient client = new HttpClient();
        #region Constructor
        public ClientesUC()
        {            
            InitializeComponent();
            var response = ObtenerClientes();
            CargasClientes(response);
            //this.logger = logger;
        }
        #endregion

        #region Eventos
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Regresar?.Invoke(this, new EventArgs());
        }       

        private void Agregar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FormCliente modal = new FormCliente(true);
            var entidad = new Cliente();
            modal.cliente = entidad;
            modal.ClickAgregar += (s, e) =>
            {
                DatosStackPanel.Children.Clear();
                var response = ObtenerClientes();
                CargasClientes(response);
                //CargasClientes();
                modal.Close();
            };
            modal.ShowDialog();
        }
        //private void (object sender, MouseButtonEventArgs e)
        //{

        //}
        #endregion
        #region Métodos
        private void CargasClientes(List<Cliente> listado)
        {
            DatosStackPanel.Children.Clear();
            DateTime fechaHoy = DateTime.Now;
            //var response = ObtenerClientes();
            foreach (var item in listado)
            {
                DatosStackPanelUC control = new DatosStackPanelUC();
                control.ClienteDataConext = item;
                control.EliminarClienteOnClick += (s, a) =>
                {
                    EliminarCliente(item.Id);
                    var response = ObtenerClientes();
                    CargasClientes(response);
                };
                control.ModificarUsuarioOnClick += (s, a) =>
                {
                    var modal = new FormCliente(false);
                    modal.cliente = item;
                    modal.ClickAgregar += (s, e) =>
                    {
                        var response = ObtenerClientes();
                        CargasClientes(response);
                        modal.Close();
                    };
                    modal.ShowDialog();
                };
                control.OpcionUsuariosOnClick += (s, a) =>
                {                                     
                    UsuariosUC vistaUsuarios = new UsuariosUC(item);

                    vistaUsuarios.SalirOnClick += (sen, ev) =>
                    {
                        GridListadoClientes.Visibility = Visibility.Visible;
                        GridListadoUsuarios.Visibility = Visibility.Collapsed;
                        var response = ObtenerClientes();
                        CargasClientes(response);
                    };


                    GridListadoClientes.Visibility = Visibility.Collapsed;
                    GridListadoUsuarios.Visibility = Visibility.Visible;
                    GridListadoUsuarios.Children.Clear();
                    GridListadoUsuarios.Children.Add(vistaUsuarios);

                };
                control.InstalacionesOnClick += (s, a) =>
                {
                    InstalacionesUC vistaInstalaciones = new InstalacionesUC(item);
                    GridListadoClientes.Visibility = Visibility.Collapsed;
                    GridListadoUsuarios.Visibility = Visibility.Collapsed;
                    GridListadoInstalaciones.Visibility = Visibility.Visible;
                    GridListadoInstalaciones.Children.Clear();
                    GridListadoInstalaciones.Children.Add(vistaInstalaciones);

                    vistaInstalaciones.SalirOnClick += (sen, ev) =>
                    {
                        GridListadoClientes.Visibility = Visibility.Visible;
                        GridListadoUsuarios.Visibility = Visibility.Collapsed;
                        GridListadoInstalaciones.Visibility = Visibility.Collapsed;
                        var response = ObtenerClientes();
                        CargasClientes(response);
                    };
                    vistaInstalaciones.ClickAgregarInstalacion += (s, a) =>
                    {
                        vistaInstalaciones.GridDatos.Visibility = Visibility.Visible;
                        vistaInstalaciones.GridForm.Visibility = Visibility.Collapsed;
                    };
                };
                control.ReportesOnClick += (s, v) =>
                {
                    List<Cliente> ListaCliente = new List<Cliente>();
                    ListaCliente.Add(item);
                    var ListaUsuarios = ObtenerDatoUsuarios(item.Id);
                    var ListaInstalaciones = ObtenerDatoInstalaciones(item.Id);
                    if (ListaUsuarios.Count == 0)
                    {
                        MessageBox.Show("Ingrese al menos un usuario");
                        if (ListaInstalaciones.Count == 0)
                        {
                            MessageBox.Show("Ingrese al menos una instalación");
                        }
                    }
                    else
                    {
                        if (ListaInstalaciones.Count == 0)
                        {
                            MessageBox.Show("Ingrese al menos una instalación");
                        }
                        else
                        {
                            //string Documento64 = ImprimirReporte(ListaCliente, ListaUsuarios, ListaInstalaciones);
                            var response = ImprimirReporte(ListaCliente, ListaUsuarios, ListaInstalaciones);
                            if (response.Exito)
                            {                                
                                var visor = new VisorPDF(response.Mensaje);
                                visor.Show();
                                //var rutaDocumento = "C:" + ConfigServer.UrlReport.Substring(1, 10) + item.Rfc + "\\" + FileName;
                                //if (System.IO.File.Exists(rutaDocumento))
                                //{
                                GridDatos.Visibility = Visibility.Collapsed;
                                btnCerrar.Visibility = Visibility.Visible;
                                VisorReporte.Visibility = Visibility.Visible;
                                //VisorReporte.Source = new Uri(Documento64);
                                //}  
                            }
                            else
                            {
                                MostrarMensaje(response.Mensaje);
                            }
                        }
                    }    
                };
                DatosStackPanel.Children.Add(control);
            }
        }

        public MensajeRespuesta ImprimirReporte(List<Cliente> listaCliente, List<UsuarioVM> listaUsuarios, List<InstalacionVM> listaInstalaciones)
        {
            var response = new MensajeRespuesta();
            LocalReport localReportPDF = null;
            DateTime fechaHoy = DateTime.Now;
            //string FileNamePath = "C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc + "\\" + listaCliente.FirstOrDefault().Rfc + fechaHoy.ToString("mm_ss") + ".pdf";
            FileName = listaCliente.FirstOrDefault().Rfc + fechaHoy.ToString("mm_ss") + ".pdf";
            
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Reportes\");
            string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Reportes\");

            if (!Directory.Exists(path)) //@"C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc
            {
                //System.IO.Directory.CreateDirectory("C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc);
                response.Exito = false;
                response.Mensaje = "El directorio no existe: " + path;
                return response;
            }

            try
            {
                FileStream fsPDF = null;
                localReportPDF = new LocalReport();
                //string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
                //string ContentStart = _path + @"\Reportes\RptCliente.rdlc";                
                string ContentStart = path + "RptCliente.rdlc";                
                localReportPDF.ReportPath = ContentStart;
                bool existeRdlc = File.Exists(ContentStart);
                ReportDataSource rdsCabeceraPDF = new ReportDataSource("DataSetCliente", listaCliente);
                ReportDataSource rdsListaUsuariosPDF = new ReportDataSource("DataSetUsuarios", listaUsuarios);
                ReportDataSource rdsListaInstalacionesPDF = new ReportDataSource("DataSetInstalaciones", listaInstalaciones);
                localReportPDF.DataSources.Add(rdsCabeceraPDF);
                localReportPDF.DataSources.Add(rdsListaUsuariosPDF);
                localReportPDF.DataSources.Add(rdsListaInstalacionesPDF);

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

                //var file = System.IO.Path.Combine("C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc + "\\" + FileName);                
                //if (System.IO.File.Exists(file))
                //{
                //    System.IO.File.Delete(file);
                //}
                var file = System.IO.Path.Combine(path + $"/{listaCliente.FirstOrDefault().Rfc}/");
                if (!Directory.Exists(file))
                {
                    Directory.CreateDirectory(file);
                }

                //string PathPDF = @"C:\Reportes\" + listaCliente.FirstOrDefault().Rfc + "\\";                
                string PathPDF = file;              
                using (FileStream fs = System.IO.File.Create(PathPDF + filename))
                {
                    fs.Write(renderedBytesPDF, 0, renderedBytesPDF.Length);
                }
                byte[] data = StreamFile(System.IO.Path.Combine(PathPDF + FileName));

                //return Convert.ToBase64String(data);
                response.Exito = true;
                response.Mensaje = Convert.ToBase64String(data);
                return response;
            }
            catch (Exception ex)
            {
                //localReportPDF.Dispose();
                response.Exito = false;
                response.Mensaje = ex.Message;
                return response;
            }
        
        }

        private List<InstalacionVM> ObtenerDatoInstalaciones(Guid idCliente)
        {
            var _usuarios = new List<InstalacionVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/GetListaInstalaciones/" + idCliente).Result;
                    _usuarios = JsonConvert.DeserializeObject<List<InstalacionVM>>(response);
                    return _usuarios;
                }
            }
            catch (Exception e)
            {
                return _usuarios;
            }
        }
        public byte[] StreamFile(string filename)
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
        private List<UsuarioVM> ObtenerDatoUsuarios(Guid IdCliente)
        {
            var _usuarios = new List<UsuarioVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/GetListaUsuarios/" + IdCliente).Result;
                    _usuarios = JsonConvert.DeserializeObject<List<UsuarioVM>>(response);
                    return _usuarios;
                }
            }
            catch (Exception e)
            {
                return _usuarios;
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
            catch(Exception )
            {
                return _clientes;
            }
        }
        private async void EliminarCliente(Guid Id)
        {            
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    var response = await client.DeleteAsync("api/Clientes/DeleteCliente/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        var respons = ObtenerClientes();
                        CargasClientes(respons);
                        MostrarMensaje("El cliente se elimino correctamente");
                    }
                    else
                    {
                        MostrarMensaje("No se pudo eliminar el cliente.");
                    }
                }            
            }
            catch(Exception e)
            {
                MostrarMensaje(e.Message);
            }
        }
        private void MostrarMensaje(string mensaje)
        {
            var modal = new MensajeWindowAccion();
            modal.Mensaje = mensaje;
            modal.OnClickAceptar += (s, e) =>
            {
                modal.Close();
            };
            modal.ShowDialog();
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

                CargasClientes(_clientes);                    
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
        }
        #endregion

        private void Cerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridDatos.Visibility = Visibility.Visible;
            btnCerrar.Visibility = Visibility.Collapsed;
            VisorReporte.Visibility = Visibility.Collapsed;
        }
    }
}
