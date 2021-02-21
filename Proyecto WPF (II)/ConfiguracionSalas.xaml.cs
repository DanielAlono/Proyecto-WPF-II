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

namespace Proyecto_WPF__II_
{
    public partial class ConfiguracionSalas : Window
    {
        public int Capacidad;
        public ConfiguracionSalas()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed_Add(object sender, ExecutedRoutedEventArgs e)
        {
            CapacidadSala capacidadSala = new CapacidadSala();
            capacidadSala.Owner = this;
            if (capacidadSala.ShowDialog() == true)
            {
                Capacidad = capacidadSala.Capacidad;
            }
        }
    }
}
