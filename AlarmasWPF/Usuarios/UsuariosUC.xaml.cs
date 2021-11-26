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

namespace AlarmasWPF.Usuarios
{
    /// <summary>
    /// Lógica de interacción para UsuariosUC.xaml
    /// </summary>
    public partial class UsuariosUC : UserControl
    {
        public event EventHandler SalirOnClick;
        public event EventHandler AgregarUserOnClick;
        public UsuariosUC(Guid _IdCliente)
        {
            InitializeComponent();
            var lista = ObtenerUsuariosCliente(_IdCliente);
            CargarUsuarioCliente(lista);
        }
        private void CargarUsuarioCliente(List<UsuarioVM> Usuarios)
        {
            foreach (var items in Usuarios)
            {
                ItemUsuarioUC control = new ItemUsuarioUC();
                control.UsuariosClienteDataConext = items;
                DatosStackPanelU.Children.Add(control);
            }
        }
        private List<UsuarioVM> ObtenerUsuariosCliente(Guid IdCliente)
        {
            var _usuarios = new List<UsuarioVM>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44310/");
                    client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync("api/Clientes/GetListaUsuarios/" + IdCliente).Result;
                    _usuarios = JsonConvert.DeserializeObject<List<UsuarioVM>>(response);
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

        private void AgregarU_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarUserOnClick?.Invoke(this, new EventArgs());
        }
    }
}
