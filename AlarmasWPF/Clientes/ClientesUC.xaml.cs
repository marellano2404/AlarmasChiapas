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
        //private static HttpClient client = new HttpClient();
        #region Constructor
        public ClientesUC()
        {            
            InitializeComponent();
            var response = ObtenerClientes();
            CargasClientes(response);                                 
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
                    var Mensaje =  ImprimirReporte(ListaCliente,ListaUsuarios,ListaInstalaciones);
                    if (Mensaje == string.Empty)
                    {
                        var rutaDocumento = "C:" + ConfigServer.UrlReport.Substring(1, 10) + item.Rfc + "\\" + FileName;
                        if (System.IO.File.Exists(rutaDocumento))
                        {
                            GridDatos.Visibility = Visibility.Collapsed;
                            btnCerrar.Visibility = Visibility.Visible;
                            VisorReporte.Visibility = Visibility.Visible;
                            VisorReporte.Source = new Uri(rutaDocumento);
                        }
                    }
                    else
                        MessageBox.Show(Mensaje);

                };
                DatosStackPanel.Children.Add(control);
            }
        }

        private string ImprimirReporte(List<Cliente> listaCliente, List<UsuarioVM> listaUsuarios, List<InstalacionVM> listaInstalaciones)
        {
            LocalReport localReportPDF = null;
            DateTime fechaHoy = DateTime.Now;
            string FileNamePath = "C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc + "\\Rpt" + listaCliente.FirstOrDefault().Rfc + fechaHoy.ToString("ss") + ".pdf";
            FileName = "Rpt" + listaCliente.FirstOrDefault().Rfc + fechaHoy.ToString("ss") + ".pdf";
            var file = System.IO.Path.Combine("C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc + "\\" + FileName);
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }

            FileStream fsPDF = null;
            if (!Directory.Exists(@"C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc))
            { System.IO.Directory.CreateDirectory("C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc); }

            if (!File.Exists(FileNamePath))
            {
                try
                {
                    localReportPDF = new LocalReport();
                    string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
                    string ContentStart = _path + @"\Reportes\RptCliente.rdlc";

                    //localReportPDF.ReportPath = "./Reportes/RptHistorialAlarmas.rdlc";
                    //localReportPDF.ReportPath = "~/Reportes/RptCliente.rdlc";
                    localReportPDF.ReportPath = ContentStart;

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

                    if (!Directory.Exists(@"C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc))
                        System.IO.Directory.CreateDirectory(@"C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc);

                    String filePathPDF = @"C:\\Reportes\\" + listaCliente.FirstOrDefault().Rfc + "\\" + FileName;
                    fsPDF = new FileStream(filePathPDF, FileMode.Create);

                    fsPDF.Write(renderedBytesPDF, 0, renderedBytesPDF.Length);
                    //Elimina El archivo despues de descargar
                    //var file = System.IO.Path.Combine("C:\\Sistemas\\Reportes\\" + NombreRpt + ".pdf");
                    //if (System.IO.File.Exists(file))
                    //{
                    //    System.IO.File.Delete(file);
                    //}
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    return ex.InnerException.ToString();
                }
                finally
                {
                    localReportPDF.Dispose();
                    fsPDF.Close();
                    fsPDF.Dispose();
                }
            }
            else 
                return "La Ruta no Existe" + FileNamePath;
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
