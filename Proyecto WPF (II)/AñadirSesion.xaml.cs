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
        private MainWindowVM _vistaModelo;
        public ConfiguracionSalas()
        {
            InitializeComponent();
            _vistaModelo = new MainWindowVM();
            this.DataContext = _vistaModelo;
        }
        //Guardamos la nueva sesión
        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            _vistaModelo.AñadirSesion(_vistaModelo.PeliculaSeleccionada.Id, _vistaModelo.NuevaSala.IdSala, _vistaModelo.Hora);
            this.Close();
        }

        private void CommandBinding_CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {

            if (avisoTextBlock != null && _vistaModelo != null && _vistaModelo.ObtenerNumeroSesionesEnSala(_vistaModelo.NuevaSala) < 3)
            {
                e.CanExecute = true;
                avisoTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandBinding_Executed_Exit(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
