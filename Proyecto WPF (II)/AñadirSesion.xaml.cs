using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int IdPelicula { get; set; }
        public int IdSala { get; set; }
        public string Hora { get; set; }
        public ObservableCollection<Pelicula> ListaPeliculas { get; set; }
        public ObservableCollection<Sala> Salas { get; set; }
        public List<string> Horarios { get; set; }
        public ConfiguracionSalas()
        {
            InitializeComponent();
            peliculasComboBox.ItemsSource = ListaPeliculas;
            salasComboBox.ItemsSource = Salas;
            horaComboBox.ItemsSource = Horarios;
            DataContext = this;
        }
        private void aceptarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
