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
            //Obtendremos las sesiones por la película seleccionada
            if (peliculasListBox.SelectedValue != null)
            {
                _vistaModelo.SesionesPorPelicula = _vistaModelo.ObtenerSesionesPorPelicula(_vistaModelo.PeliculaSeleccionada.Id);
            }
        }

        private void CommandBinding_Executed_Config(object sender, ExecutedRoutedEventArgs e)
        {
            ConfiguracionSalas config = new ConfiguracionSalas();
            config.Owner = this;
            config.ListaPeliculas = _vistaModelo.ListaPeliculas;
            config.Salas = _vistaModelo.Salas;
            config.Horarios = _vistaModelo.Horarios;

            if (config.ShowDialog() == true)
            {
                _vistaModelo.AñadirSesion(config.IdPelicula + 1, config.IdSala + 1, config.Hora);
            }
        }
        private void CommandBinding_Executed_Add(object sender, ExecutedRoutedEventArgs e)
        {
            CapacidadSala capacidadSala = new CapacidadSala();
            capacidadSala.Owner = this;
            if (capacidadSala.ShowDialog() == true)
            {
                _vistaModelo.AñadirSala(capacidadSala.Capacidad);
            }
        }
    }
}
