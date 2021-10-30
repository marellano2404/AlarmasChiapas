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

namespace AlarmasWPF.Clientes.Usuarios
{
    /// <summary>
    /// Lógica de interacción para UsuariosUC.xaml
    /// </summary>
    public partial class UsuariosUC : UserControl
    {
        public event EventHandler SalirOnClick;
        public event EventHandler AgregarOnClick;
        public UsuariosUC()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SalirOnClick?.Invoke(this, new EventArgs());
        }

        private void Agregar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarOnClick?.Invoke(this, new EventArgs());
        }
    }
}
