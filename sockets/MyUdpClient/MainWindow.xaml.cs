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

namespace MyUdpClient
{
    public partial class MainWindow : Window
    {
        private Model _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new Model();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _model;
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            _model.InitModel();
        }
    }
}
