using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para MensajeWindow.xaml
    /// </summary>
    public partial class MensajeWindow : Window
    {
        public MensajeWindow()
        {
            InitializeComponent();
        }
        private string titulo;

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value;
                TituloTextBlock.Text = value;
            }
        }

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value;
                MensajeTextBlock.Text = value;
            }
        }
        //public event EventHandler ClickAceptar;
        //[DefaultValue("Titulo de la vista")]
        //public string Titulo
        //{
        //    get { return (string)GetValue(TituloProperty); }
        //    set { SetValue(TituloProperty, value); }
        //}
        //[DefaultValue("Mensaje de la vista")]
        //public string Mensaje
        //{
        //    get { return (string)GetValue(MensajeProperty); }
        //    set { SetValue(MensajeProperty, value); }
        //}

        //public static readonly DependencyProperty TituloProperty =
        //    DependencyProperty.Register("Titulo", typeof(string), typeof(ModalMensajeUC),
        //    new PropertyMetadata((d, e) =>
        //    {
        //        if (e.NewValue != null) ((ModalMensajeUC)d).TituloTextBlock.Text = e.NewValue.ToString();
        //    }));

        //public static readonly DependencyProperty MensajeProperty =
        //    DependencyProperty.Register("Mensaje", typeof(string), typeof(ModalMensajeUC),
        //    new PropertyMetadata((d, e) =>
        //    {
        //        if (e.NewValue != null) ((ModalMensajeUC)d).MensajeTextBlock.Text = e.NewValue.ToString();
        //    }));

        private void Button_OnClickAceptar(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
