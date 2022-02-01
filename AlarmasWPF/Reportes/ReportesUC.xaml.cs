using AlarmasWPF.ControlesPersonalizados;
using AlarmasWPF.Core.ViewModels;
using AlarmasWPF.Recursos;
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
            CargarDatos();
        }    
        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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
                    modal.ClickReporte += (s, e) =>
                    {
                        ObtenerDatoReporte(modal.EntidadReporte);
                        modal.Close();
                    };
                    modal.ShowDialog();
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
                    return _alarmasEm = JsonConvert.DeserializeObject<List<ListaAlarmaEmitidasVM>>(response.Result.Content.ReadAsStringAsync().Result);   
                    
                    //if (_alarmasEm == "true") //si el resultado de exito es true
                    //{
                    //    MostrarMensaje("El evento se elimino correctamente");
                    //}
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
            return _alarmasEm;
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
    }
}
