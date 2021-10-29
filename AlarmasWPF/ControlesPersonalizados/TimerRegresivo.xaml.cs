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
using System.Windows.Threading;

namespace AlarmasWPF.ControlesPersonalizados
{
    /// <summary>
    /// Lógica de interacción para TimerRegresivo.xaml
    /// </summary>
    public partial class TimerRegresivo : UserControl
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        SolidColorBrush rojo = Brushes.Red;
        int dias, horas=0, min=0, seg = 0;

        public TimerRegresivo(int _dias = 0, bool esTimer = false)
        {
            InitializeComponent();
            dias = _dias;            
            if(!esTimer) // Timer básico solo para mostrar días faltantes (Documentación)
            {
                BorderTimerRegresivo.Visibility = Visibility.Collapsed;
                BorderTimerDia.Visibility = Visibility.Visible;                
                
                if(dias == 0)
                {
                    txtDiasRestantes.Text = "último día";
                    txtDiasRestantes.Foreground = rojo;
                }
                else if(dias < 0)
                {
                    txtDiasRestantes.Text = Math.Abs(dias).ToString();
                    labelDiasRestantes.Text = "Días de retraso";
                    txtDiasRestantes.Foreground = rojo;
                }
                else
                {
                    txtDiasRestantes.Text = dias.ToString();
                    labelDiasRestantes.Text = "Días restantes";
                }                                     
            }
            else // funcionalidad para el Timer con cuenta regresiva
            {
                BorderTimerRegresivo.Visibility = Visibility.Visible;
                BorderTimerDia.Visibility = Visibility.Collapsed;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
                dispatcherTimer.Tick += (a, b) =>
                {
                    txtDias.Text = (dias < 10) ? "0" + dias.ToString() : dias.ToString();
                    txtHoras.Text = (horas < 10) ? "0" + horas.ToString() : horas.ToString();
                    txtMinutos.Text = (min < 10) ? "0" + min.ToString() : min.ToString();
                    txtSegundos.Text = (seg < 10) ? "0" + seg.ToString() : seg.ToString();

                    if (dias > 0 && horas == 0 && min == 0 && seg == 0)
                    {
                        dias -= 1;
                        horas = 23;
                        min = 59;
                        seg = 60;
                    }
                    if (horas > 0 && min == 0 && seg == 0)
                    {
                        horas -= 1;
                        min = 59;
                        seg = 60;
                    }
                    if (min > 0 && seg == 0)
                    {
                        min -= 1;
                        seg = 60;
                    }
                    if (seg != 0)
                    {
                        seg--;
                    }

                    if (horas == 0 && min == 0 && seg == 0)
                    {
                        TimerFinalizado();                      
                    }
                    
                };
                dispatcherTimer.Start();
            }

        }

        private void TimerFinalizado()
        {
            dispatcherTimer.Stop();
            txtHoras.Text = "00";
            txtMinutos.Text = "00";
            txtSegundos.Text = "00";
            txtHoras.Foreground = rojo;
            txtMinutos.Foreground = rojo;
            txtSegundos.Foreground = rojo;
        }
        
    }
}
