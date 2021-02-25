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
    /// Lógica de interacción para GestionarSesiones.xaml
    /// </summary>
    public partial class GestionarSesiones : Window
    {
        private MainWindowVM _vistaModelo;
        public GestionarSesiones()
        {
            InitializeComponent();
            _vistaModelo = new MainWindowVM();
            this.DataContext = _vistaModelo;
            //Obtendremos las sesiones por la película seleccionada
            if (listaPeliculasComboBox.SelectedValue != null)
            {
                _vistaModelo.SesionesPorPelicula = _vistaModelo.ObtenerSesionesPorPelicula(_vistaModelo.PeliculaSeleccionada.Id);
            }
        }

        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            _vistaModelo.ModificarSesion(new Sesiones(_vistaModelo.SesionSeleccionada.IdSesion, _vistaModelo.PeliculaSeleccionada.Id,
                _vistaModelo.NuevaSala.IdSala, _vistaModelo.Hora));
        }

        private void CommandBinding_CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            _vistaModelo.EliminarSesion(_vistaModelo.SesionSeleccionada);
            ActualizarDataContext();
        }

        private void CommandBinding_CanExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_vistaModelo != null && _vistaModelo.PeliculaSeleccionada != null && _vistaModelo.SesionSeleccionada != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void ActualizarDataContext()
        {
            _vistaModelo = new MainWindowVM();
            this.DataContext = _vistaModelo;
        }
    }
}
