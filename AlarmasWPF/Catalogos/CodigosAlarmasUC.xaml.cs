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


namespace AlarmasWPF.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CodigosAlarmasUC.xaml
    /// </summary>
    public partial class CodigosAlarmasUC : UserControl
    {
       
        public CodigosAlarmaVM CodigoAlarmaEntities
        {
            get
            {
                try
                {
                    return DataContext as CodigosAlarmaVM;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                DataContext = value;
            }
        }
        public event EventHandler Regresar;
        public CodigosAlarmasUC()
        {
            InitializeComponent();
            var lista = ObtenerListaClaves();
            CargarClavesdeAlarma(lista);
        }

        private List<CodigosAlarmaVM> ObtenerListaClaves()
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
            catch (Exception)
            {
                return _codigosAl;
            }
        }

        private void CargarClavesdeAlarma(List<CodigosAlarmaVM> lista)
        {
            DatosStackPanel.Children.Clear();
            GridFormCodigos.Visibility = Visibility.Collapsed;
            foreach (var items in lista)
            {
                DatosCodigosAlarmaUC control = new DatosCodigosAlarmaUC();
                control.CodigoAlarmaEntities = items;
                DatosStackPanel.Children.Add(control);
                control.EditarClaveOnclick += (s, a) =>
                {
                    CodigoAlarmaEntities = items;
                    btnActualizarInstalacion.Visibility = Visibility.Visible;
                    btnAgregarInstalacion.Visibility = Visibility.Collapsed;
                    GridFormCodigos.Visibility = Visibility.Visible;
                };
                control.EliminarClaveOnClick += (s, a) =>
                {
                    EliminarClaveAlarma(items.Id);
                    GridFormCodigos.Visibility = Visibility.Collapsed;
                };
            }
        }

        private async void EliminarClaveAlarma(int Id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    var response = await client.DeleteAsync("api/Eventos/DelClaveAlarma/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        MostrarMensaje("La Clave se elimino correctamente");
                        var lista = ObtenerListaClaves();
                        CargarClavesdeAlarma(lista);
                    }
                    else
                    {
                        MostrarMensaje("No se pudo eliminar el codigo de alarma, verifique registros.");
                    }
                }
            }
            catch (Exception e)
            {
                MostrarMensaje(e.Message);
            }
        }

        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Regresar?.Invoke(this, new EventArgs());
        }
        private async void AgregaClave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CodigoAlarmaEntities.Descripcion) || CodigoAlarmaEntities.Descripcion is null)
                {
                    MostrarMensaje("Existen un Campo Vacio!!");
                }
                else
                {
                    var result = new HttpResponseMessage();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigServer.UrlServer);
                        client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(CodigoAlarmaEntities);
                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                        result = await client.PostAsync("api/Eventos/PostClaveAlarma", data);

                        var respuesta = await result.Content.ReadAsStringAsync();
                        if (respuesta == "true") //si el resultado de exito es true
                        {
                            var lista = ObtenerListaClaves();
                            CargarClavesdeAlarma(lista);
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

        private void AgregarCodigo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnActualizarInstalacion.Visibility = Visibility.Collapsed;
            btnAgregarInstalacion.Visibility = Visibility.Visible;
            GridFormCodigos.Visibility = Visibility.Visible;
            CodigoAlarmaEntities = new CodigosAlarmaVM();

        }

        private async void ActualizarClave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CodigoAlarmaEntities.Descripcion) || CodigoAlarmaEntities.Descripcion is null)
                {
                    MostrarMensaje("Existen un Campo Vacio!!");
                }
                else
                {
                    var result = new HttpResponseMessage();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigServer.UrlServer);
                        client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(CodigoAlarmaEntities);
                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                        result = await client.PutAsync("api/Eventos/PutClaveAlarma", data);

                        var respuesta = await result.Content.ReadAsStringAsync();
                        if (respuesta == "true") //si el resultado de exito es true
                        {
                            var lista = ObtenerListaClaves();
                            CargarClavesdeAlarma(lista);
                        }
                    }
                }

            }
            catch (Exception ese)
            {
                MostrarMensaje(ese.Message);
            }
        }
    }
}
