using AlarmasWPF.Clientes;
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

namespace AlarmasWPF.Eventos
{
    /// <summary>
    /// Lógica de interacción para EventosUC.xaml
    /// </summary>
    public partial class EventosListaUC : UserControl
    {
        public Cliente _clienteP;
        public event EventHandler RegresarClick;
        //public event EventHandler AgregarClick;
        public event EventHandler BuscarEventoClick;
        public EventosListaUC(Cliente _cliente)
        {
            InitializeComponent();
            var response = ObtenerEventos(_cliente.Id);

            DatosUserC datosUser = new DatosUserC();
            datosUser.clienteUc = _cliente;
            _clienteP = _cliente;
            ContenedorUsuario.Children.Add(datosUser);
            CargarEventos(response);

            var listaResponse = GetHistorialAlarmas(_cliente.Id);
            CargarhistorialAlarmas(listaResponse);
        }    
        private void CargarhistorialAlarmas(List<AlarmasEmitidasVM> ListaEventos)
        {                      
            DatosStackPanel.Children.Clear();
            foreach (var items in ListaEventos)
            {
                DatosEventosUC datosLEventosUC = new DatosEventosUC();
                datosLEventosUC.AlarmaEmitidaDxC = items;
                DatosStackPanel.Children.Add(datosLEventosUC);
                datosLEventosUC.ModificarEventoClick += (s, a) =>
                {
                    var listaUsuario = ObtenerUsuariosCliente(_clienteP.Id);
                    var listaAlarmas = CargarClavesAlarma();
                    var listaZonas = cargarZonaCliente(_clienteP.Id);

                    var modal = new FormEvento(items.Id, false);
                    modal.ComboBoxUser.ItemsSource = listaUsuario;
                    modal.ComboBoxClave.ItemsSource = listaAlarmas;
                    modal.ComboBoxZona.ItemsSource = listaZonas;

                    modal.ClickAgregarH += (s, a) =>
                    {
                        var listaResponse = GetHistorialAlarmas(_clienteP.Id);
                        CargarhistorialAlarmas(listaResponse);
                        modal.Close();
                    };
                    modal.ShowDialog();
                };
                datosLEventosUC.EliminarEventoClick += (s, a) =>
                {
                    EliminarHistorialEvento(items.Id);
                    var listaResponse = GetHistorialAlarmas(_clienteP.Id);
                    CargarhistorialAlarmas(listaResponse);
                };

            }
        }
        private List<AlarmasEmitidasVM> GetHistorialAlarmas(Guid IdCliente)
        {
            var _alarmasEm = new List<AlarmasEmitidasVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Eventos/GetListaEventosCte/" + IdCliente).Result;
                    _alarmasEm = JsonConvert.DeserializeObject<List<AlarmasEmitidasVM>>(response);
                    return _alarmasEm;
                }
            }
            catch (Exception e)
            {
                return _alarmasEm;
            }
        }
        private List<Cliente> ObtenerEventos(Guid IdCliente)
        {
            var _clientes = new List<Cliente>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/ListarAlarmas/" + IdCliente).Result;
                    _clientes = JsonConvert.DeserializeObject<List<Cliente>>(response);
                    return _clientes;
                }
            }
            catch (Exception e)
            {
                return _clientes;
            }
        }
        private void CargarEventos(List<Cliente> listado)
        {
            DatosStackPanel.Children.Clear();
            foreach (var item in listado)
            {
                DatosClientesUC control = new DatosClientesUC();
                control.ClienteDataConext = item;
                DatosStackPanel.Children.Add(control);
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

        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegresarClick?.Invoke(this, new EventArgs());
        }

        private void Agregar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var listaUsuario = ObtenerUsuariosCliente(_clienteP.Id);
            var listaAlarmas = CargarClavesAlarma();
            var listaZonas = cargarZonaCliente(_clienteP.Id);

            var modal = new FormEvento(_clienteP.Id, true);
            modal.ComboBoxUser.ItemsSource = listaUsuario;
            modal.historialAlarmaEntidad = new HistorialAlarmaVM();
            modal.historialAlarmaEntidad.IdCliente = _clienteP.Id;
            modal.ComboBoxClave.ItemsSource = listaAlarmas;
            modal.ComboBoxZona.ItemsSource = listaZonas;
            modal.ClickAgregarH += (s, a) =>
            {
                var listaResponse = GetHistorialAlarmas(_clienteP.Id);
                CargarhistorialAlarmas(listaResponse);
                modal.Close();
            };
            modal.ShowDialog();

        }
        private List<InstalacionVM> cargarZonaCliente(Guid id)
        {
            var _ZonasInstalaciones = new List<InstalacionVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/GetListaInstalaciones/" + id).Result;
                    _ZonasInstalaciones = JsonConvert.DeserializeObject<List<InstalacionVM>>(response);
                    return _ZonasInstalaciones;
                }
            }
            catch (Exception e)
            {
                return _ZonasInstalaciones;
            }
        }
        private List<UsuarioVM> ObtenerUsuariosCliente(Guid IdCliente)
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
        private void BuscarH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuscarEventoClick?.Invoke(this, new EventArgs());
        }

        private async void EliminarHistorialEvento(Guid Id)
        {
            try
            {
                var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    var response = await client.DeleteAsync("api/Eventos/DelHistorialAlarmaCte/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        MostrarMensaje("El evento se elimino correctamente");
                    }
                    else
                    {
                        MostrarMensaje("No se pudo eliminar el evento.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }
        }

        private List<CodigosAlarmaVM> CargarClavesAlarma()
        {
            var _codigosAl = new List<CodigosAlarmaVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Eventos/GetListaCodigosAlarmas").Result;
                    _codigosAl = JsonConvert.DeserializeObject<List<CodigosAlarmaVM>>(response);

                    return _codigosAl;

                }
            }
            catch (Exception e)
            {
                return _codigosAl;
            }

        }
    }
}
