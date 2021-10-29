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

namespace AlarmasWPF.Clientes
{
    /// <summary>
    /// Lógica de interacción para ClientesUC.xaml
    /// </summary>
    /// 
    public partial class ClientesUC : UserControl
    {
        public event EventHandler Regresar;

        HttpClient client = new HttpClient();  
        public ClientesUC()
        {            
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:44310/");
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
            CargasClientes();
        }
        private async void CargasClientes()
        {
            DatosStackPanel.Children.Clear();
            var response = client.GetStringAsync("api/Clientes/").Result;
            var clientes_ = JsonConvert.DeserializeObject<List<Cliente>>(response);
            //DatosStackPanel = clientes_;
            
            foreach (var item in clientes_)
            {
                DatosStackPanelUC control = new DatosStackPanelUC();
                control.ClienteDataConext = item;
                DatosStackPanel.Children.Add(control);
            }


        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Regresar?.Invoke(this, new EventArgs());
        }
    }
}
