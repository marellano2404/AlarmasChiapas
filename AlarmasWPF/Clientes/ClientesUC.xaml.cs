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

namespace AlarmasWPF.Clientes
{
    /// <summary>
    /// Lógica de interacción para ClientesUC.xaml
    /// </summary>
    /// 
    public partial class ClientesUC : UserControl
    {
        public event EventHandler Regresar;
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
                DatosStackPanel.Children.Add(control);
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
            catch(Exception e)
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
    }
}
