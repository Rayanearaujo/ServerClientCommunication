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

namespace MyUdpServer
{
    public partial class MainWindow : Window
    {
        private Model _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new Model();
            _model.InitModel();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _model;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _model.CleanUp();
        }
    }
}
