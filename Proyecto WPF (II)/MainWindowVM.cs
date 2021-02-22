using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_WPF__II_
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public Sala NuevaSala { get; set; }
        public Sesiones NuevaSesion { get; set; }
        public Ventas NuevaVenta { get; set; }
        public Sala SalaSeleccionada { get; set; }
        public Pelicula PeliculaSeleccionada { get; set; }
        public ObservableCollection<Pelicula> ListaPeliculas { get; set; }
        public ObservableCollection<Sala> Salas { get; set; }
        public ObservableCollection<Sesiones> Sesiones { get; set; }
        public ObservableCollection<Sesiones> SesionesPorPelicula { get; set; }
        public List<string> Horarios { get => _horarios; set => _horarios = value; }

        private List<string> _horarios = new List<string>() { "16:00","16:30","17:00","17:30","18:00","18:30","19:00","19:30",
                                                            "20:00","20:30","21:00","21:30","22:00"};

        private ServicioCartelera _servicio;
        private BaseDatosService _datosService;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowVM()
        {
            //Inicializamos
            _servicio = new ServicioCartelera();
            _datosService = new BaseDatosService();

            if (ComprobarSiHaPasadoElDia())
            {
                //Borramos todas las películas que tengamos en la BD y asignamos la nueva lista
                _datosService.BorrarPeliculas();
                foreach (Pelicula pelicula in _servicio.GetSamples())
                {
                    _datosService.InsertarPelicula(pelicula);
                }
            }

            //Recogemos los datos
            ListaPeliculas = _datosService.ObtenerPeliculas();
            Salas = _datosService.ObtenerSalas();
            Sesiones = _datosService.ObtenerSesiones();
        }

        //UTILS
        public ObservableCollection<Sesiones> ObtenerSesionesPorPelicula(int idPelicula)
        {
            ObservableCollection<Sesiones> sesionesPorPelicula = new ObservableCollection<Sesiones>();
            for (int i = 0; i < Sesiones.Count; i++)
            {
                if (Sesiones[i].Pelicula == idPelicula)
                {
                    sesionesPorPelicula.Add(Sesiones[i]);
                }
            }
            return sesionesPorPelicula;
        }
        public Sala ObtenerSala(Sesiones sesion)
        {
            Sala salaEncontrada = null;
            foreach (Sala sala in Salas)
            {
                if (sesion.Sala == sala.IdSala)
                {
                    salaEncontrada = sala;
                }
            }
            return salaEncontrada;
        }
        public Pelicula ObtenerPelicula(Sesiones sesion)
        {
            Pelicula peliculaEncontrada = null;
            foreach (Pelicula pelicula in ListaPeliculas)
            {
                if (sesion.Pelicula == pelicula.Id)
                {
                    peliculaEncontrada = pelicula;
                }
            }
            return peliculaEncontrada;
        }

        //SALAS
        public void AñadirSala(int capacidad)
        {
            int idSala;
            if (_datosService.ObtenerSalas().Count == 0)
            {
                idSala = 1;
            }
            else
            {
                idSala = _datosService.ObtenerSalas().Count() + 1;
            }
            string numero = idSala.ToString();
            bool disponible = true;

            NuevaSala = new Sala(idSala, numero, capacidad, disponible);
            _datosService.InsertarSala(NuevaSala);
            Salas = _datosService.ObtenerSalas();
        }
        public void ModificarSala(Sala sala)
        {
            _datosService.ActualizarSala(sala);
        }

        //SESIONES
        public void AñadirSesion(int pelicula, int sala, string hora)
        {
            int idSesion;
            if (_datosService.ObtenerSesiones().Count == 0)
            {
                idSesion = 1;
            }
            else
            {
                idSesion = _datosService.ObtenerSesiones().Count() + 1;
            }
            NuevaSesion = new Sesiones(idSesion, pelicula, sala, hora);
            _datosService.InsertarSesion(NuevaSesion);
            Sesiones = _datosService.ObtenerSesiones();
        }
        public void ModificarSesion(Sesiones sesion)
        {
            _datosService.ActualizarSesion(sesion);
        }

        //VENTAS
        public void AñadirVenta(int sesion, int cantidad)
        {
            int idVenta = _datosService.ObtenerVentas().Count + 1;
            string pago = "TOTAL: " + cantidad * Properties.Settings.Default.precioEntrada + "€";
            NuevaVenta = new Ventas(idVenta, sesion, cantidad, pago);
            _datosService.InsertarVenta(NuevaVenta);
        }
        private bool ComprobarSiHaPasadoElDia()
        {
            DateTime comprobarDia = Properties.Settings.Default.diaActual;
            DateTime thisDay = DateTime.Today;
            if (comprobarDia.Day != thisDay.Day)
            {
                Properties.Settings.Default.diaActual = thisDay;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
