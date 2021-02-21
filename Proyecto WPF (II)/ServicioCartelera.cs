using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_WPF__II_
{
    class ServicioCartelera
    {
        public ObservableCollection<Pelicula> GetSamples()
        {
            var client = new RestClient(Properties.Settings.Default.apiEndPoint);
            var request = new RestRequest("peliculas", Method.GET);
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(response.Content);
        }
    }
}
