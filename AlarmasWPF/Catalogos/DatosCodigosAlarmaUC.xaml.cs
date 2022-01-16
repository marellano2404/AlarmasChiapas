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

namespace AlarmasWPF.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CodigosAlarmasUC.xaml
    /// </summary>
    public partial class DatosCodigosAlarmaUC : UserControl
    {
        public event EventHandler EliminarClaveOnClick;
        public event EventHandler EditarClaveOnclick;
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
        public DatosCodigosAlarmaUC()
        {
            InitializeComponent();
        }
        private void ModificarCodigo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EditarClaveOnclick?.Invoke(this, new EventArgs());
        }

        private void EliminarCodigo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EliminarClaveOnClick?.Invoke(this, new EventArgs());
        }
    }
}
