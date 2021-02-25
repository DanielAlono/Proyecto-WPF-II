using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Proyecto_WPF__II_
{
    class OcupadasConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Sesiones sesion = (Sesiones)value;
            if (sesion != null)
            {
                MainWindowVM vm = new MainWindowVM();
                ObservableCollection<Ventas> ventas = vm.ObtenerVentasPorSesion(sesion);

                int cantidad = 0;
                foreach (Ventas venta in ventas)
                {
                    if (venta.Sesion == sesion.IdSesion)
                    {
                        cantidad += venta.Cantidad;
                    }
                }
                return "Ocupadas: " + cantidad;
            }
            return "Ocupadas: " + 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
