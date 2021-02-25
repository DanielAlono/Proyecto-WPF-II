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
    /// <summary>
    /// Lógica de interacción para GestionarSalas.xaml
    /// </summary>
    public partial class GestionarSalas : Window
    {
        public int Capacidad { get; set; }
        public bool Disponible { get; set; }
        public GestionarSalas()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void aceptarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
