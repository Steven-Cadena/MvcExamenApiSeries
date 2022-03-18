using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcExamenApiSeries.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcExamenApiSeries.Services
{
    public class ServiceApiSerie
    {
        private Uri UrlApi;
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceApiSerie(string url) 
        {
            this.UrlApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync() 
        {
            string request = "/api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesAsync() 
        {
            string request = "/api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Serie> FindSerieAsync(int id) 
        {
            string request = "/api/series/" + id;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        //mostrar personajes respecto a una serie 
        public async Task<List<Personaje>> MostrarPersonajesAsync(int idserie) 
        {
            string request = "/api/series/personajesseries/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task InsertarPersonajeAsync(int idpersonaje, string nombre, string imagen, int idserie) 
        {
            using (HttpClient client = new HttpClient()) 
            {
                string request = "/api/personajes";
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje per = new Personaje
                {
                    Nombre = nombre,
                    Imagen = imagen,
                    IdSerie = idserie
                };
                string json = JsonConvert.SerializeObject(per);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PostAsync(request, content);
            }
        }

        public async Task ModificarSerie(int idserie, string nombre, string imagen, double puntuacion, int anio) 
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series";
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = await this.FindSerieAsync(idserie);

                if (serie != null) 
                {
                    serie.Nombre = nombre;
                    serie.Imagen = imagen;
                    serie.Puntuacion = puntuacion;
                    serie.Anio = anio;
                }
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await client.PutAsync(request, content);
            }
        }

        public async Task EliminarPersonajeAsync(int idpersonaje) 
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + idpersonaje;
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage message = await client.DeleteAsync(request);
            }
        }

        public async Task CambiarPersonaje(int idpersonaje, int idserie) 
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + idpersonaje + "/" + idserie;
                client.BaseAddress = this.UrlApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
            }
        }
    }
}
