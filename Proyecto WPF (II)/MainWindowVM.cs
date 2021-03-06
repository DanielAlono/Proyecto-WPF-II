﻿using System;
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
        public string Hora { get; set; }
        public int Entradas { get; set; }
        public int Ocupadas { get; set; }
        public int Disponibles { get; set; }
        public Sala NuevaSala { get; set; }
        public Sesiones NuevaSesion { get; set; }
        public Ventas NuevaVenta { get; set; }
        public Pelicula PeliculaSeleccionada { get; set; }
        public Sala SalaSeleccionada { get; set; }
        public Sesiones SesionSeleccionada { get; set; }
        public ObservableCollection<Pelicula> ListaPeliculas { get; set; }
        public ObservableCollection<Sala> Salas { get; set; }
        public ObservableCollection<Sesiones> Sesiones { get; set; }
        public ObservableCollection<Ventas> Ventas { get; set; }
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
            Entradas = 0;

            //Insertamos las películas de manera cutre
            if (_datosService.ObtenerPeliculas().Count == 0)
            {
                foreach (Pelicula pelicula in _servicio.GetSamples())
                {
                    _datosService.InsertarPelicula(pelicula);
                }
            }
            if (ComprobarSiHaPasadoElDia())
            {   //Borramos todas las ventas
                _datosService.BorrarVentas();

                //Recogemos las peliculas del API y las peliculas del nuestra BD
                ObservableCollection<Pelicula> peliculasAPI = _servicio.GetSamples();
                ObservableCollection<Pelicula> peliculasBD = _datosService.ObtenerPeliculas();

                //Recorremos nuestra BD de peliculas
                for (int i = 0; i < peliculasAPI.Count; i++)
                {
                    //Modificamos las peliculas de nuestra BD por la del API por su hubiera habido un cambio
                    if (peliculasAPI[i].Id == peliculasBD[i].Id)
                    {
                        _datosService.ActualizarPelicula(peliculasAPI[i]);
                    }
                    //Si hay + ID's insertamos las nuevas películas
                    else
                    {
                        _datosService.InsertarPelicula(peliculasAPI[i]);
                    }
                }
            }

            //Recogemos los datos
            ListaPeliculas = _datosService.ObtenerPeliculas();
            Salas = _datosService.ObtenerSalas();
            Sesiones = _datosService.ObtenerSesiones();
            Ventas = _datosService.ObtenerVentas();
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
        public ObservableCollection<Ventas> ObtenerVentasPorSesion(Sesiones sesion)
        {
            ObservableCollection<Ventas> ventasPorSesion = new ObservableCollection<Ventas>();
            foreach (Ventas venta in Ventas)
            {
                if (sesion.IdSesion == venta.Sesion)
                {
                    ventasPorSesion.Add(venta);
                }
            }
            return ventasPorSesion;
        }
        public int ObtenerNumeroSesionesEnSala(Sala sala)
        {
            int numeroSesiones = 0;
            if (sala != null)
            {
                foreach (Sesiones sesion in Sesiones)
                {
                    if (sesion.Sala == sala.IdSala)
                    {
                        numeroSesiones += 1;
                    }
                }
            }
            return numeroSesiones;
        }
        public Sala ObtenerSala(int idSala)
        {
            Sala salaEncontrada = null;
            foreach (Sala sala in Salas)
            {
                if (idSala == sala.IdSala)
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
            if (_datosService.ObtenerSalas().Count != 0)
                idSala = _datosService.ObtenerSalas().Last().IdSala + 1;
            else idSala = 1;
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
            if (_datosService.ObtenerSesiones().Count != 0)
                idSesion = _datosService.ObtenerSesiones().Last().IdSesion + 1;
            else idSesion = 1;
            NuevaSesion = new Sesiones(idSesion, pelicula, sala, hora);
            _datosService.InsertarSesion(NuevaSesion);
            Sesiones = _datosService.ObtenerSesiones();
        }
        public void ModificarSesion(Sesiones sesion)
        {
            _datosService.ActualizarSesion(sesion);
        }
        public void EliminarSesion(Sesiones sesion)
        {
            _datosService.EliminarSesion(sesion);
        }

        //VENTAS
        public void AñadirVenta(int sesion, int cantidad)
        {
            int idVenta;
            if (_datosService.ObtenerVentas().Count != 0)
                idVenta = _datosService.ObtenerVentas().Last().IdVenta + 1;
            else idVenta = 1;
            string pago = "TOTAL: " + cantidad * Properties.Settings.Default.precioEntrada + "€";
            NuevaVenta = new Ventas(idVenta, sesion, cantidad, pago);
            _datosService.InsertarVenta(NuevaVenta);
        }
        private bool ComprobarSiHaPasadoElDia()
        {
            string comprobarDia = Properties.Settings.Default.diaActual;
            string thisDay = DateTime.Today.ToShortDateString();
            if (comprobarDia!= thisDay)
            {
                Properties.Settings.Default.diaActual = thisDay;
                Properties.Settings.Default.Save();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
