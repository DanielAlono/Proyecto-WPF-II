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
    /// Lógica de interacción para VistaVentaEntradas.xaml
    /// </summary>
    public partial class VistaVentaEntradas : Window
    {
        public string Titulo { get; set; }
        public int Sala { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public VistaVentaEntradas()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
