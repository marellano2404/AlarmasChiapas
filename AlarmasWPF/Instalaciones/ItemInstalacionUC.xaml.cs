using AlarmasWPF.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AlarmasWPF.Instalaciones
{
    /// <summary>
    /// Lógica de interacción para ItemInstalacionUC.xaml
    /// </summary>

    public partial class ItemInstalacionUC : UserControl
    {
        public InstalacionVM InstalacionCteDataContext
        {
            get
            {
                try
                {
                    return DataContext as InstalacionVM;
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
        public event EventHandler ClickEliminarInstalacion;
        public ItemInstalacionUC()
        {
            InitializeComponent();
        }
        private void EliminarIns_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClickEliminarInstalacion?.Invoke(this, new EventArgs());
        }
    }
}
