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

namespace AlarmasWPF.Eventos
{
    /// <summary>
    /// Lógica de interacción para DatosClientesUC.xaml
    /// </summary>
    public partial class DatosClientesUC : UserControl
    {
        public event EventHandler EventosOnClick;
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
        public DatosClientesUC()
        {
            InitializeComponent();
        }

        private void Eventos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EventosOnClick?.Invoke(this, new EventArgs());
        }

      
    }
}
