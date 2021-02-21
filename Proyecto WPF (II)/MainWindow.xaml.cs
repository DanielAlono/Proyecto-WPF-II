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

namespace Proyecto_WPF__II_
{
    public partial class MainWindow : Window
    {
        private MainWindowVM _vistaModelo;

        public MainWindow()
        {
            InitializeComponent();
            _vistaModelo = new MainWindowVM();
            this.DataContext = _vistaModelo;
        }

        private void CommandBinding_Executed_Config(object sender, ExecutedRoutedEventArgs e)
        {
            ConfiguracionSalas config = new ConfiguracionSalas();
            config.Owner = this;
            config.DataContext = _vistaModelo;

            if(config.ShowDialog() == true)
            {
                _vistaModelo.AñadirSala(config.Capacidad);
            }
        }
    }
}
