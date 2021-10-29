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

namespace AlarmasWPF.ControlesPersonalizados
{
    /// <summary>
    /// Lógica de interacción para ModalMensajeFuncionalUC.xaml
    /// </summary>
    public partial class ModalMensajeFuncionalUC : Window
    {
        public event EventHandler ClickAceptar;
        public ModalMensajeFuncionalUC()
        {
            InitializeComponent();
        }

        private string titulo;

        public string Titulo
        {
            get { return titulo; }
            set
            {
                titulo = value;
                TituloTextBlock.Text = value;
            }
        }

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set
            {
                mensaje = value;
                MensajeTextBox.Text = value;
            }
        }

        private string mensajeValidacion;

        public string MensajeValidacion
        {
            get { return mensajeValidacion; }
            set
            {
                mensajeValidacion = value;
                MensajeValidacionTextBlock.Text = value;
            }
        }


        private void Button_OnClickAceptar(object sender, RoutedEventArgs e)
        {
            if (ClickAceptar != null) ClickAceptar(this, new EventArgs());
        }

        private void Button_OnClickCancelar(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
