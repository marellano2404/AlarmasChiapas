using AlarmasWPF.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para DatosStackPanelUC.xaml
    /// </summary>
    public partial class DatosStackPanelUC : UserControl
    {
        public event EventHandler OpcionUsuariosOnClick;
        public event EventHandler ModificarUsuarioOnClick;
        public event EventHandler EliminarClienteOnClick;
        public Cliente ClienteDataConext 
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
        public DatosStackPanelUC()
        {
            InitializeComponent();
        }

        private void UsuariosImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpcionUsuariosOnClick?.Invoke(this, new EventArgs());
        }

        private void ModificarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ModificarUsuarioOnClick?.Invoke(this, new EventArgs());
        }

        private void EliminarImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EliminarClienteOnClick?.Invoke(this, new EventArgs());
        }
    }
}
