using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_WPF__II_
{
    class Sala : INotifyPropertyChanged
    {
        public int IdSala { get; set; }
        public string Numero { get; set; }
        public int Capacidad { get; set; }
        public bool Disponible { get; set; }

        public Sala() { }

        public Sala(int idSala, string numero, int capacidad, bool disponible)
        {
            IdSala = idSala;
            Numero = numero;
            Capacidad = capacidad;
            Disponible = disponible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
