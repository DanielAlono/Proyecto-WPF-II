using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
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
        /*MÉTODO PARA ABRIR CONFIGURAR UNA NUEVA SESIÓN*/
        private void CommandBinding_Executed_Config(object sender, ExecutedRoutedEventArgs e)
        {
            ConfiguracionSalas config = new ConfiguracionSalas();
            config.Owner = this;
            config.Show();
        }
        /*MÉTODO PARA AÑADIR UNA NUEVA SALA*/
        private void CommandBinding_Executed_Add(object sender, ExecutedRoutedEventArgs e)
        {
            CapacidadSala capacidadSala = new CapacidadSala();
            capacidadSala.Owner = this;
            if (capacidadSala.ShowDialog() == true)
            {
                _vistaModelo.AñadirSala(capacidadSala.Capacidad);
                _vistaModelo = new MainWindowVM();
            }
        }
        /*MÉTODO PARA LAS COMPRAS DE LAS ENTRADAS*/
        private void CommandBinding_Executed_Buy(object sender, ExecutedRoutedEventArgs e)
        {
            _vistaModelo.AñadirVenta(_vistaModelo.SesionSeleccionada.IdSesion, _vistaModelo.Entradas);
            VistaVentaEntradas vista = new VistaVentaEntradas();
            vista.Owner = this;
            vista.Titulo = _vistaModelo.PeliculaSeleccionada.Titulo;
            vista.Sala = _vistaModelo.SesionSeleccionada.IdSesion;
            vista.Fecha = DateTime.Today.ToShortDateString();
            vista.Hora = _vistaModelo.SesionSeleccionada.Hora;
            if(vista.ShowDialog() == true)
            {

            }
            ActualizarDataContext();
        }
        private void CommandBinding_CanExecute_Buy(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_vistaModelo != null && _vistaModelo.SesionSeleccionada != null)
            {
                Sesiones sesion = _vistaModelo.SesionSeleccionada;
                Sala sala = _vistaModelo.ObtenerSala(sesion.Sala);
                int ocupadas = 0;
                int disponible = sala.Capacidad;
                ObservableCollection<Ventas> ventas = _vistaModelo.ObtenerVentasPorSesion(sesion);

                foreach (Ventas venta in ventas)
                    if (venta.Sesion == sesion.IdSesion) ocupadas += venta.Cantidad;

                if (disponible >= ocupadas && _vistaModelo.Entradas > 0 && disponible >= _vistaModelo.Entradas)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
        }
        //SUMAR ENTRADAS
        private void sumarEntradaButton_Click(object sender, RoutedEventArgs e)
        {
            _vistaModelo.Entradas += 1;
        }
        //RESTAR ENTRADAS
        private void restarEntradaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_vistaModelo.Entradas > 0)
            {
                _vistaModelo.Entradas -= 1;
            }
        }

        //MÉTODO PARA IR A VENTANA EDITAR SALAS
        private void CommandBinding_Executed_Edit(object sender, ExecutedRoutedEventArgs e)
        {
            GestionarSalas gestionar = new GestionarSalas();
            gestionar.Owner = this;
            gestionar.Capacidad = _vistaModelo.SalaSeleccionada.Capacidad;
            gestionar.Disponible = _vistaModelo.SalaSeleccionada.Disponible;
            if (gestionar.ShowDialog() == true)
            {
                _vistaModelo.SalaSeleccionada.Capacidad = gestionar.Capacidad;
                _vistaModelo.SalaSeleccionada.Disponible = gestionar.Disponible;
                _vistaModelo.ModificarSala(_vistaModelo.SalaSeleccionada);
            }
        }

        private void CommandBinding_CanExecute_Edit(object sender, CanExecuteRoutedEventArgs e)
        {
            if (salasListBox != null && salasListBox.SelectedValue != null) e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void CommandBinding_Executed_EditSesion(object sender, ExecutedRoutedEventArgs e)
        {
            GestionarSesiones gestionarSesiones = new GestionarSesiones();
            gestionarSesiones.Owner = this;
            gestionarSesiones.Show();
        }
        //Actualizar DataContext
        private void ActualizarDataContext()
        {
            _vistaModelo = new MainWindowVM();
            this.DataContext = _vistaModelo;
        }

        private void CommandBinding_Executed_Help(object sender, ExecutedRoutedEventArgs e)
        {
            Help.ShowHelp(null, "../../Help/Ayuda.chm");
        }
    }
}
