using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_WPF__II_
{
    class Ventas
    {
        public int IdVenta { get; set; }
        public int Sesion { get; set; }
        public int Cantidad { get; set; }
        public string Pago { get; set; }
        public Ventas() { }
        public Ventas(int idVenta, int sesion, int cantidad, string pago)
        {
            IdVenta = idVenta;
            Sesion = sesion;
            Cantidad = cantidad;
            Pago = pago;
        }
    }
}
