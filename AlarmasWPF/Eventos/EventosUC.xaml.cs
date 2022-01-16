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

namespace AlarmasWPF.Eventos
{
    /// <summary>
    /// Lógica de interacción para EventosUC.xaml
    /// </summary>
    public partial class EventosUC : UserControl
    {
        public event EventHandler Regresar;
        public EventosUC()
        {
            InitializeComponent();           
            CargarDatos();           
        }
        private void CargarDatos()
        { 
            var response = ObtenerClientes();
            CargarClientes(response);
        }
        private List<CodigosAlarmaVM> CargarClavesAlarma()
        {
            var _codigosAl = new List<CodigosAlarmaVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
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

        private List<UsuarioVM> ObtenerUsuariosCliente(Guid IdCliente)
        {
            var _usuarios = new List<UsuarioVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
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

        private void CargarClientes(List<Cliente> listado)
        {
            DatosStackPanel.Children.Clear();
            //var response = ObtenerClientes();
            foreach (var item in listado)
            {
                DatosClientesUC control = new DatosClientesUC();
                control.ClienteDataConext = item;
                DatosStackPanel.Children.Add(control);

                control.EventosOnClick += (s, a) =>
                {
                    EventosListaUC eventosUC = new EventosListaUC(item);
                    GridListadoClientes.Visibility = Visibility.Collapsed;
                    
                    var listaHistorialAlarmas = GetHistorialAlarmas(item.Id);
                    //CargarHistorialAlarmasCliente(listaHistorialAlarmas);

                    foreach (var items in listaHistorialAlarmas)
                    {
                        DatosStackPanel.Children.Clear();
                        DatosEventosUC datosLEventosUC = new DatosEventosUC();
                        datosLEventosUC.AlarmaEmitidaDxC = items;                        
                        eventosUC.DatosStackPanel.Children.Add(datosLEventosUC);
                        datosLEventosUC.ModificarEventoClick += (s, a) =>
                        {
                            var listaUsuario = ObtenerUsuariosCliente(item.Id);
                            var listaAlarmas = CargarClavesAlarma();

                            var modal = new FormEvento(items.Id, false);
                            modal.ComboBoxUser.ItemsSource = listaUsuario;   
                            modal.ComboBoxClave.ItemsSource = listaAlarmas;
                            modal.ClickAgregarH += (s, a) =>
                            {
                                eventosUC.DatosStackPanel.Children.Clear();
                                var listaHistorialAlarmas = GetHistorialAlarmas(item.Id);
                                foreach (var items in listaHistorialAlarmas)
                                {
                                    DatosEventosUC datosLEventosUC = new DatosEventosUC();
                                    datosLEventosUC.AlarmaEmitidaDxC = items;
                                    eventosUC.DatosStackPanel.Children.Add(datosLEventosUC);
                                }
                                modal.Close();
                            };
                            modal.ShowDialog();
                        };
                        //datosLEventosUC.EliminarEventoClick += (s,a) =>
                        //{
                        //    eliminarHistorialEvento(item.Id);
                        //};
                       
                    }           

                    GridListadoEventos.Visibility = Visibility.Visible;
                    GridListadoEventos.Children.Clear();
                    GridListadoEventos.Children.Add(eventosUC);

                    eventosUC.RegresarClick += (s, a) =>
                    {
                        GridListadoClientes.Visibility = Visibility.Visible;
                        GridListadoEventos.Visibility = Visibility.Collapsed;
                        var response = ObtenerClientes();
                        CargarClientes(response);
                    };
                    eventosUC.AgregarClick += (s, a) =>
                    {
                        var listaUsuario = ObtenerUsuariosCliente(item.Id);                        
                        var listaAlarmas = CargarClavesAlarma();
                        

                        var modal = new FormEvento(item.Id, true);
                        modal.ComboBoxUser.ItemsSource = listaUsuario;
                        modal.historialAlarmaEntidad = new HistorialAlarmaVM();
                        modal.historialAlarmaEntidad.IdCliente = item.Id;
                        modal.ComboBoxClave.ItemsSource = listaAlarmas;                      

                        modal.ClickAgregarH += (s, a) =>
                        { 
                            eventosUC.DatosStackPanel.Children.Clear();
                            var listaHistorialAlarmas = GetHistorialAlarmas(item.Id);
                            foreach (var items in listaHistorialAlarmas)
                            {                               
                                DatosEventosUC datosLEventosUC = new DatosEventosUC();
                                datosLEventosUC.AlarmaEmitidaDxC = items;
                                eventosUC.DatosStackPanel.Children.Add(datosLEventosUC);
                            }
                            modal.Close();
                        };
                        modal.ShowDialog();
                    };
                    
                };
            }
        }

        //private void CargarHistorialAlarmasCliente(List<AlarmasEmitidasVM> response)
        //{
        //    throw new NotImplementedException();
        //}

        private List<AlarmasEmitidasVM> GetHistorialAlarmas(Guid IdCliente)
        {
            var _alarmasEm = new List<AlarmasEmitidasVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
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

        private List<Cliente> ObtenerClientes()
        {
            var _clientes = new List<Cliente>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
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
                    client.BaseAddress = new Uri("https://localhost:44310/");
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

        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Regresar?.Invoke(this, new EventArgs());
        }
    }
}
