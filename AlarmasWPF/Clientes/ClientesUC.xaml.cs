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
            CargasClientes();                                 
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
                CargasClientes();
                modal.Close();
            };
            modal.ShowDialog();
        }
        //private void (object sender, MouseButtonEventArgs e)
        //{

        //}
        #endregion
        #region Métodos
        private void CargasClientes()
        {
            DatosStackPanel.Children.Clear();
            var response = ObtenerClientes();
            foreach (var item in response)
            {
                DatosStackPanelUC control = new DatosStackPanelUC();
                control.ClienteDataConext = item;
                control.EliminarClienteOnClick += (s, a) =>
                {
                    EliminarCliente(item.Id);
                };
                control.ModificarUsuarioOnClick += (s, a) =>
                {
                    var modal = new FormCliente(false);
                    modal.cliente = item;
                    modal.ClickAgregar += (s, e) =>
                    {
                        CargasClientes();
                        modal.Close();
                    };
                    modal.ShowDialog();
                };
                control.OpcionUsuariosOnClick += (s, a) =>
                {                                     
                    UsuariosUC vistaUsuarios = new UsuariosUC(item.Id);

                    vistaUsuarios.SalirOnClick += (sen, ev) =>
                    {
                        GridListadoClientes.Visibility = Visibility.Visible;
                        GridListadoUsuarios.Visibility = Visibility.Collapsed;
                        CargasClientes();
                    };

                    vistaUsuarios.AgregarUserOnClick += (s, a) =>
                    {
                        FormUsuario modal = new FormUsuario(true);
                        var entidad = new UsuarioVM();
                        modal.Entidadusuario = entidad;
                        modal.Entidadusuario.IdCliente = item.Id;
                        modal.ClickAgregarUser += (s, e) =>
                        {
                            modal.Close();
                        };
                        modal.ShowDialog();
                    };
                    GridListadoClientes.Visibility = Visibility.Collapsed;
                    GridListadoUsuarios.Visibility = Visibility.Visible;
                    GridListadoUsuarios.Children.Clear();
                    GridListadoUsuarios.Children.Add(vistaUsuarios);

                };
                control.InstalacionesOnClick += (s, a) =>
                {
                    InstalacionesUC vistaInstalaciones = new InstalacionesUC();
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
                        CargasClientes();
                    };
                    vistaInstalaciones.AgregarOnClick += (s, a) =>
                    {
                        var entidad = new InstalacionVM();
                        vistaInstalaciones.EntidadInstalacion = entidad;
                        vistaInstalaciones.EntidadInstalacion.IdCliente = item.Id;
                        vistaInstalaciones.GridDatos.Visibility = Visibility.Collapsed;
                        vistaInstalaciones.GridForm.Visibility = Visibility.Visible;

                        //modal.ClickAgregarUser += (s, e) =>
                        //{
                        //    modal.Close();
                        //};
                        //modal.ShowDialog();
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
                    client.BaseAddress = new Uri("https://localhost:44310/");
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
                    client.BaseAddress = new Uri("https://localhost:44310/");
                    var response = await client.DeleteAsync("api/Clientes/DeleteCliente/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        CargasClientes();
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
        #endregion
    }
}
