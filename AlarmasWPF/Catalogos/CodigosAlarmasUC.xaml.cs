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
            }
        }

        private void Regresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Regresar?.Invoke(this, new EventArgs());
        }

        private void Agregar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AgregaClave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
