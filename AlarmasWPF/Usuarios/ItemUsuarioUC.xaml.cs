using AlarmasWPF.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace AlarmasWPF.Usuarios
{
    /// <summary>
    /// Lógica de interacción para ItemUsuarioUC.xaml
    /// </summary>
    public partial class ItemUsuarioUC : UserControl
    {
        public event EventHandler ModificarOnClick;
        public event EventHandler EliminarOnClick;
        public UsuarioVM UsuariosClienteDataConext
        {
            get
            {
                try
                {
                    return this.DataContext as UsuarioVM;
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
