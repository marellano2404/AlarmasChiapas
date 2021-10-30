using AlarmasWPF.Clientes.Usuarios;
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
        private static HttpClient client = new HttpClient();
        #region Constructor
        public ClientesUC()
        {            
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:44310/");
            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));            
            CargasClientes();
        }
        #endregion        
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Regresar?.Invoke(this, new EventArgs());
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Agregar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FormCliente modal = new FormCliente();
            //modal.OnClickAceptar += (se, en) =>
            //{
            //    CargarListaEmpleados();
            //    modal.Close();
            //};
            modal.ShowDialog();
        }

        #region Métodos
        private void CargasClientes()
        {
            DatosStackPanel.Children.Clear();
            var response = ObtenerClientes();
            foreach (var item in response)
            {
                DatosStackPanelUC control = new DatosStackPanelUC();
                control.ClienteDataConext = item;
                control.OpcionUsuariosOnClick += (s, a) =>
                {
                    UsuariosUC vistaUsuarios = new UsuariosUC();
                    vistaUsuarios.SalirOnClick += (sen, ev) =>
                    {
                        GridListadoClientes.Visibility = Visibility.Visible;
                        GridListadoUsuarios.Visibility = Visibility.Collapsed;
                        CargasClientes();
                    };
                    GridListadoClientes.Visibility = Visibility.Collapsed;
                    GridListadoUsuarios.Visibility = Visibility.Visible;
                    GridListadoUsuarios.Children.Clear();
                    GridListadoUsuarios.Children.Add(vistaUsuarios);
                };
                DatosStackPanel.Children.Add(control);
            }
        }

        private List<Cliente> ObtenerClientes()
        {
            var _clientes = new List<Cliente>();
            try
            {
                var response = client.GetStringAsync("api/Clientes/GetListaClientes").Result;
                _clientes = JsonConvert.DeserializeObject<List<Cliente>>(response);
                return _clientes;
            }
            catch(Exception e)
            {
                return _clientes;
            }
        }
        #endregion
    }
}
