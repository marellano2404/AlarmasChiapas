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
using System.Windows.Shapes;

namespace AlarmasWPF.Reportes
{
    /// <summary>
    /// Lógica de interacción para FormSelFechas.xaml
    /// </summary>
    public partial class FormSelFechas : Window
    {
        public event EventHandler ClickReporte;
        public DatoReporte EntidadReporte
        {
            get
            {
                try
                {
                    return DataContext as DatoReporte;
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
        public FormSelFechas()
        {
            InitializeComponent();
        }

        private void btnGrarReporte_Click(object sender, RoutedEventArgs e)
        {
            ClickReporte?.Invoke(this, new EventArgs());
        }
    }
}
