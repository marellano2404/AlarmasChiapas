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

namespace AlarmasWPF.Reportes
{
    /// <summary>
    /// Lógica de interacción para DatosClientesUC.xaml
    /// </summary>
    public partial class ClientesLineaUC : UserControl
    {       
       
        public ClientesLineaUC()
        {
            InitializeComponent();
        }
        public Cliente ClienteDC
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
    }
}
