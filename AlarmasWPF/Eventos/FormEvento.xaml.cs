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
using System.Windows.Shapes;

namespace AlarmasWPF.Eventos
{
    /// <summary>
    /// Lógica de interacción para FormCliente.xaml
    /// </summary>
    public partial class FormEvento : Window
    {
        #region Propiedades
        //private static HttpClient client = new HttpClient();
        public event EventHandler ClickAgregarH;
        public bool _esNuevo { get; set; }
        public HistorialAlarmaVM historialAlarmaEntidad
        {
            get
            {
                try
                {
                    return this.DataContext as HistorialAlarmaVM;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                this.DataContext = value;
            }
        }
        #endregion

        #region Constructor
        public FormEvento(Guid IdEvento,bool esNuevo)
        {
            InitializeComponent();
            var Evento = GetEventoSeleccionado(IdEvento);
            historialAlarmaEntidad = Evento;
            _esNuevo = esNuevo;
        }

        private HistorialAlarmaVM GetEventoSeleccionado(Guid IdhistoriaAlarma)
        {
            var _eventoAlarma = new HistorialAlarmaVM();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Eventos/GetEventoAlarma/" + IdhistoriaAlarma).Result;
                    _eventoAlarma = JsonConvert.DeserializeObject<HistorialAlarmaVM>(response);                    
                    return _eventoAlarma;
                }
            }
            catch (Exception e)
            {
                return _eventoAlarma;
            }
        }

        #endregion

        #region Eventos
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        #endregion

        private async void btnGuardarEvento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (historialAlarmaEntidad.Descripcion is null)
                {
                    historialAlarmaEntidad.Descripcion = "S/D";
                }
                var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigServer.UrlServer);
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(historialAlarmaEntidad);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    if (_esNuevo)
                    {
                        result = await client.PostAsync("api/Eventos/PostHistorialAlarmaCte", data);
                    }
                    else
                    {
                        result = await client.PutAsync("api/Eventos/PutHistorialAlarmaCte", data);
                    }
                    var respuesta = await result.Content.ReadAsStringAsync();
                    if (respuesta == "true") //si el resultado de exito es true
                    {
                        ClickAgregarH?.Invoke(this, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message);
            }           
        }
        private void MostrarMensaje(string mensaje)
        {
            var modal = new MensajeWindowAccion();
            modal.Mensaje = mensaje;
            modal.ShowDialog();
        }

    }
}