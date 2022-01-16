using AlarmasWPF.Clientes;
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
    public partial class EventosListaUC : UserControl
    {
        public event EventHandler RegresarClick;
        public event EventHandler AgregarClick;
        public event EventHandler BuscarEventoClick;
        public EventosListaUC(Cliente _cliente)
        {
            InitializeComponent();
            var response = ObtenerEventos(_cliente.Id);

            DatosUserC datosUser = new DatosUserC();
            datosUser.clienteUc = _cliente;
            ContenedorUsuario.Children.Add(datosUser);

            CargarEventos(response);
        }
        private List<Cliente> ObtenerEventos(Guid IdCliente)
        {
            var _clientes = new List<Cliente>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
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
            AgregarClick?.Invoke(this, new EventArgs());
        }

        private void BuscarH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuscarEventoClick?.Invoke(this, new EventArgs());
        }
    }
}
