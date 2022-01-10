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
    /// Lógica de interacción para DatosUserC.xaml
    /// </summary>
   
    public partial class DatosUserC : UserControl
    {
        public Cliente clienteUc
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
        public DatosUserC()
        {
            InitializeComponent();
        }
    }
}
