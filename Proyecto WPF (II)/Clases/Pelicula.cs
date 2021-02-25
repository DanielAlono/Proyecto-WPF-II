using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_WPF__II_
{
    public class Pelicula : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cartel { get; set; }
        public int Año { get; set; }
        public string Genero { get; set; }
        public string Calificacion { get; set; }

        public Pelicula () { }

        public Pelicula(int id, string titulo, string cartel, int año, string genero, string calificacion)
        {
            Id = id;
            Titulo = titulo;
            Cartel = cartel;
            Año = año;
            Genero = genero;
            Calificacion = calificacion;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
