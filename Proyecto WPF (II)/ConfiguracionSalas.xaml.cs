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
        private MainWindowVM _vistamodelo;
        public Pelicula Pelicula { get; set; }
        public ObservableCollection<Pelicula> ListaPeliculas { get; set; }
        public ObservableCollection<Sala> Salas { get; set; }
        public int IdSala { get; set; }
        public int IdPelicula { get; set; }
        public string Hora { get; set; }
        public ConfiguracionSalas(MainWindowVM _vistamodelo)
        {
            InitializeComponent();
            this._vistamodelo = _vistamodelo;
            ListaPeliculas = _vistamodelo.ListaPeliculas;
            Salas = _vistamodelo.Salas;
        }

        private void CommandBinding_Executed_Add(object sender, ExecutedRoutedEventArgs e)
        {
            CapacidadSala capacidadSala = new CapacidadSala();
            capacidadSala.Owner = this;
            if (capacidadSala.ShowDialog() == true)
            {
                _vistamodelo.AñadirSala(capacidadSala.Capacidad);
            }
        }

        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            _vistamodelo.AñadirSesion(IdPelicula, IdSala, Hora);
            peliculaComboBox.SelectedValue = null;
            salasListBox.SelectedValue = null;
            horaComboBox.SelectedValue = null;
            DialogResult = true;
        }

        private void CommandBinding_CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (peliculaComboBox.SelectedValue != null && salasListBox.SelectedValue != null && horaComboBox.SelectedValue != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
    }
}
