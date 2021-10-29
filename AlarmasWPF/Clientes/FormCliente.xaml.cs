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

namespace AlarmasWPF.Clientes
{
    /// <summary>
    /// Lógica de interacción para FormCliente.xaml
    /// </summary>
    public partial class FormCliente : Window
    {
        public event EventHandler ClickAceptar;
        public event EventHandler ClickAgregar;
        public FormCliente()
        {
            InitializeComponent();
        }
             
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (ClickAceptar != null) ClickAceptar(this, new EventArgs());
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (ClickAgregar != null) ClickAgregar(this, new EventArgs());
        }
    }
}
