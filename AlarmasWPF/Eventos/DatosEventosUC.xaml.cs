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
    public partial class DatosEventosUC : UserControl
    {
        public event EventHandler ModificarEventoClick;
        public event EventHandler EliminarEventoClick;
        public AlarmasEmitidasVM AlarmaEmitidaDxC
        {
            get
            {
                try
                {
                    return this.DataContext as AlarmasEmitidasVM;
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
        public DatosEventosUC()
        {
            InitializeComponent();
        }

        private void Modificar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ModificarEventoClick?.Invoke(this, new EventArgs());
        }

        private void Eliminar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EliminarEventoClick?.Invoke(this, new EventArgs());
        }
    }
}
