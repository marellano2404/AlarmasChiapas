using AlarmasWPF.ControlesPersonalizados;
using AlarmasWPF.Core.ViewModels;
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

namespace AlarmasWPF.Usuarios
{
    /// <summary>
    /// Lógica de interacción para FormUsuario.xaml
    /// </summary>
    public partial class FormUsuario : Window
    {
        #region Propiedades
        //private static HttpClient client = new HttpClient();
        public event EventHandler ClickAgregarUser;
        public bool _esNuevo { get; set; }
        public UsuarioVM Entidadusuario
        {
            get
            {
                try
                {
                    return DataContext as UsuarioVM;
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
        #endregion

        #region Constructor
        public FormUsuario(bool esNuevo)
        {
            InitializeComponent();
            _esNuevo = esNuevo;
        }
        #endregion

        private async void btnAgregar_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(Entidadusuario);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                    if (_esNuevo)
                    {
                        result = await client.PostAsync("api/Clientes/PostNuevoUsuario", data);
                    }
                    else
                    {
                        result = await client.PutAsync("api/Clientes/PostNuevoUsuario", data);
                    }
                    var respuesta = await result.Content.ReadAsStringAsync();
                    if (respuesta == "true") //si el resultado de exito es true
                    {
                        ClickAgregarUser?.Invoke(this, new EventArgs());
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
