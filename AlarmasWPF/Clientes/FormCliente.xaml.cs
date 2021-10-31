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
using System.Windows.Shapes;

namespace AlarmasWPF.Clientes
{
    /// <summary>
    /// Lógica de interacción para FormCliente.xaml
    /// </summary>
    public partial class FormCliente : Window
    {
        #region Propiedades
        //private static HttpClient client = new HttpClient();
        public event EventHandler ClickAgregar;
        public bool _esNuevo { get; set; }
        public Cliente cliente
        {
            get
            {
                try
                {
                    return this.DataContext as Cliente;
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
        public FormCliente(bool esNuevo)
        {
            InitializeComponent();
            _esNuevo = esNuevo;
        }
        #endregion

        #region Eventos
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(cliente);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    if (_esNuevo)
                    {
                        result = await client.PostAsync("api/Clientes/PostNuevoCliente", data);
                    }
                    else
                    {
                        //Modificar
                    }
                    var respuesta = await result.Content.ReadAsStringAsync();
                    if (respuesta == "true") //si el resultado de exito es true
                    {
                        ClickAgregar?.Invoke(this, new EventArgs());
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
        #endregion
    }
}
