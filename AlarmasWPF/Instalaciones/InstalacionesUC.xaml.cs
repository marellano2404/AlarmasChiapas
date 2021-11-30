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
            }
        }
        public event EventHandler AgregarOnClick;
        public event EventHandler SalirOnClick;
        public event EventHandler ClickAgregarInstalacion;
        public InstalacionesUC()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalirOnClick?.Invoke(this, new EventArgs());
        }

        private void AgregarI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarOnClick?.Invoke(this, new EventArgs());
        }

        private async void AgregarInst_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var result = new HttpResponseMessage();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(EntidadInstalacion);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                    result = await client.PostAsync("api/Clientes/PostNuevaInstalacion", data);
                   
                    var respuesta = await result.Content.ReadAsStringAsync();
                    if (respuesta == "true") //si el resultado de exito es true
                    {
                        ClickAgregarInstalacion?.Invoke(this, new EventArgs());
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
