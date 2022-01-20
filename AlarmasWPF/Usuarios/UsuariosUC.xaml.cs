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

namespace AlarmasWPF.Usuarios
{
    /// <summary>
    /// Lógica de interacción para UsuariosUC.xaml
    /// </summary>
    public partial class UsuariosUC : UserControl
    {
        public event EventHandler SalirOnClick;

        public Cliente _cliente;
        public UsuariosUC(Cliente Cliente)
        {
            InitializeComponent();
            _cliente = Cliente;
            var lista = ObtenerUsuariosCliente(Cliente.Id);            
            CargarUsuarioCliente(lista);
            
            DatosUserC datosUser = new DatosUserC();
            datosUser.clienteUc = Cliente;
            ContenedorUsuario.Children.Add(datosUser);
        }
        private void CargarUsuarioCliente(List<UsuarioVM> Usuarios)
        {
            DatosStackPanelU.Children.Clear();
            foreach (var items in Usuarios)
            {
                ItemUsuarioUC control = new ItemUsuarioUC();
                control.UsuariosClienteDataConext = items;
                DatosStackPanelU.Children.Add(control);
                control.EliminarUserOnClick += (s, a) =>
                {
                    eliminarUsuarioCliente(items.Id);
                    var lista = ObtenerUsuariosCliente(items.IdCliente);
                    CargarUsuarioCliente(lista);
                };
                control.ModificarOnClick += (s, a) =>
                {
                    FormUsuario modal = new FormUsuario(false);
                    var entidad = items;
                    modal.Entidadusuario = entidad;
                    
                    modal.ClickCancelarUser += (s, a) =>
                    {
                        modal.Close();
                    };
                    modal.ShowDialog();
                };
            }
        }

        private async void eliminarUsuarioCliente(Guid Id)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    var response = await client.DeleteAsync("api/Clientes/DeleteUsuario/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        MostrarMensaje("El usuario se elimino correctamente");
                    }
                    else
                    {
                        MostrarMensaje("No se pudo eliminar el usuario, verifique eventos del usuario.");
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
            var modal = new MensajeWindowAccion();
            modal.Mensaje = mensaje;
            modal.OnClickAceptar += (s, e) =>
            {
                modal.Close();
            };
            modal.ShowDialog();
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
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalirOnClick?.Invoke(this, new EventArgs());
        }

        private void AgregarU_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FormUsuario modal = new FormUsuario(true);
            var entidad = new UsuarioVM();
            modal.Entidadusuario = entidad;
            modal.Entidadusuario.IdCliente = _cliente.Id;
            modal.ClickAgregarUser += (s, e) =>
            {
                modal.Close();
                var lista = ObtenerUsuariosCliente(_cliente.Id);
                CargarUsuarioCliente(lista);
                
            };
            modal.ShowDialog();

        }
    }
}
