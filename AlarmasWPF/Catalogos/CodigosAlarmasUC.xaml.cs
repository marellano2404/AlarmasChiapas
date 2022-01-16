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

        private void CargarClavesdeAlarma(List<CodigosAlarmaVM> lista)
        {
            foreach (var items in lista)
            {
                DatosCodigosAlarmaUC control = new DatosCodigosAlarmaUC();
                control.CodigoAlarmaEntities = items;
                DatosStackPanel.Children.Add(control);
                control.EditarClaveOnclick += (s, a) =>
                {
                    CodigoAlarmaEntities = items;
                    GridFormCodigos.Visibility = Visibility.Visible;
                    btnActualizarInstalacion.Visibility = Visibility.Visible;
                    btnAgregarInstalacion.Visibility = Visibility.Collapsed;
                };
                control.EliminarClaveOnClick += (s, a) =>
                {
                    EliminarClaveAlarma(items.Id);
                };
            }
        }

        private async void EliminarClaveAlarma(int Id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
                    var response = await client.DeleteAsync("api/Clientes/DeleteCliente/" + Id);
                    if (response.IsSuccessStatusCode)
                    {
                        //CargasClientes();
                        MostrarMensaje("El cliente se elimino correctamente");
                    }
                    else
                    {
                        MostrarMensaje("No se pudo eliminar el cliente.");
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
                if (string.IsNullOrEmpty(CodigoAlarmaEntities.Descripcion))
                {
                    MostrarMensaje("Campo Vacio!!");
                }
                else
                {
                    var result = new HttpResponseMessage();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44310/");
                        client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(CodigoAlarmaEntities);
                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                        result = await client.PostAsync("api/Clientes/PostNuevaInstalacion", data);

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
                if (string.IsNullOrEmpty(CodigoAlarmaEntities.Descripcion))
                {
                    MostrarMensaje("Campo Vacio!!");
                }
                else
                {
                    var result = new HttpResponseMessage();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44310/");
                        client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json"));

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(CodigoAlarmaEntities);
                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                        result = await client.PostAsync("api/Clientes/PostNuevaInstalacion", data);

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
