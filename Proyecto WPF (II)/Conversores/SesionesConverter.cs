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
    public class SesionesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<Sesiones> sesionesPorPelicula = new ObservableCollection<Sesiones>();
            if (value != null)
            {
                MainWindowVM vm = new MainWindowVM();
                Pelicula pelicula = (Pelicula)value;

                foreach (Sesiones sesion in vm.Sesiones)
                {
                    if (sesion.Pelicula == pelicula.Id)
                    {
                        sesionesPorPelicula.Add(sesion);
                    }
                }
            }
            return sesionesPorPelicula;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
