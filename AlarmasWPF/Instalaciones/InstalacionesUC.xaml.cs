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

namespace AlarmasWPF.Instalaciones
{
    /// <summary>
    /// Lógica de interacción para InstalacionesUC.xaml
    /// </summary>
    /// 
    public partial class InstalacionesUC : UserControl
    {
        public InstalacionVM EntidadInstalacion
        {
            get
            {
                try
                {
                    return DataContext as InstalacionVM;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                DataContext = value;
                //if (string.IsNullOrEmpty(value.Dispositivo))
                //{
                //    MostrarMensaje("Campo d!!");
                //}
            }
        }
        public Cliente _cliente;
        //public event EventHandler AgregarOnClick;
        public event EventHandler SalirOnClick;
        public event EventHandler ClickAgregarInstalacion;
        public InstalacionesUC(Cliente _client)
        {
            InitializeComponent();
            var lista = ObtenerInstalacionesCliente(_client.Id);
            _cliente = _client;
            CargarInstalacionesCliente(lista);

            DatosUserC datosUser = new DatosUserC();
            datosUser.clienteUc = _client;
            ContenedorUsuario.Children.Add(datosUser);

        }

        private void CargarInstalacionesCliente(List<InstalacionVM> lista)
        {
            DatosStackPanelI.Children.Clear();
            foreach (var items in lista)
            {
                ItemInstalacionUC control = new ItemInstalacionUC();
                control.InstalacionCteDataContext = items;
                DatosStackPanelI.Children.Add(control);
                control.ClickEliminarInstalacion += (f, g) =>
                {
                    EliminarInstalacion(items.Id);
                    var lista = ObtenerInstalacionesCliente(items.IdCliente);
                    CargarInstalacionesCliente(lista);
                };
            }
        }

        private async void EliminarInstalacion(Guid? Id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    var response = await client.DeleteAsync("api/Clientes/DeleteInstalacion/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        MostrarMensaje("La Instalación se elimino correctamente");
                    }
                    else
                    {
                        MostrarMensaje("No se pudo eliminar la instalacion.");
                    }
                }
            }
            catch (Exception e)
            {
                MostrarMensaje(e.Message);
            }
        }

        private List<InstalacionVM> ObtenerInstalacionesCliente(Guid idCliente)
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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalirOnClick?.Invoke(this, new EventArgs());
        }
        private void AgregarI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            EntidadInstalacion = new InstalacionVM();
            EntidadInstalacion.IdCliente = _cliente.Id;
            GridDatos.Visibility = Visibility.Collapsed;
            GridForm.Visibility = Visibility.Visible;
        }
        private async void AgregarInst_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(EntidadInstalacion.Dispositivo))
                {
                    MostrarMensaje("Campo Vacio!!");
                }
                else
                {
                    var result = new HttpResponseMessage();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigServer.UrlServer);
                        client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(EntidadInstalacion);
                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                        result = await client.PostAsync("api/Clientes/PostNuevaInstalacion", data);

                        var respuesta = await result.Content.ReadAsStringAsync();
                        if (respuesta == "true") //si el resultado de exito es true
                        {
                            ClickAgregarInstalacion?.Invoke(this, new EventArgs());
                            var lista = ObtenerInstalacionesCliente(_cliente.Id);
                            CargarInstalacionesCliente(lista);
                        }
                    }
                }
               
            }
            catch (Exception ese)
            {
                MostrarMensaje(ese.Message);
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

    }
}
