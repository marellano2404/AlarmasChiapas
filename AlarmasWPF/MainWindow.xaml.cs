﻿using System;
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

namespace AlarmasWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MouseClientesEvent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ClientesUC = new Clientes.ClientesUC();
            
            MenuGrid.Visibility = Visibility.Collapsed;

            DetallesGrid.Visibility = Visibility.Visible;
            DetallesGrid.Children.Clear();
            DetallesGrid.Children.Add(ClientesUC);
            ClientesUC.Regresar += (s,e) => {
                DetallesGrid.Visibility = Visibility.Collapsed;
                MenuGrid.Visibility = Visibility.Visible;
            };


        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
