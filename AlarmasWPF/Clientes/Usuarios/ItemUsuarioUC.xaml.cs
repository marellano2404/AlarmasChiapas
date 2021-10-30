using AlarmasWPF.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace AlarmasWPF.Clientes.Usuarios
{
    /// <summary>
    /// Lógica de interacción para ItemUsuarioUC.xaml
    /// </summary>
    public partial class ItemUsuarioUC : UserControl
    {
        public event EventHandler ModificarOnClick;
        public event EventHandler EliminarOnClick;
        public ItemUsuarioUC()
        {
            InitializeComponent();
        }

        private void ModificarImage_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ModificarOnClick?.Invoke(this, new EventArgs());
        }

        private void EliminarImage_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EliminarOnClick?.Invoke(this, new EventArgs());
        }
    }
}
