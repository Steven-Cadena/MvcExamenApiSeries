using Microsoft.AspNetCore.Mvc;
using MvcExamenApiSeries.Models;
using MvcExamenApiSeries.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExamenApiSeries.Controllers
{
    public class SerieServicioController : Controller
    {
        private ServiceApiSerie service;
        public SerieServicioController(ServiceApiSerie service) 
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }
        public async Task<IActionResult> Details(int id) 
        {
            Serie serie = await this.service.FindSerieAsync(id);
            return View(serie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Serie serie =
                await this.service.FindSerieAsync(id);
            return View(serie);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Serie serie)
        {
            await this.service.ModificarSerie(serie.IdSerie,serie.Nombre,serie.Imagen,serie.Puntuacion,serie.Anio);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> IndexPersonajes()
        {
            List<Personaje> personaje = await this.service.GetPersonajesAsync();
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertarPersonajeAsync(personaje.IdPersonaje,personaje.Nombre,personaje.Imagen,personaje.IdSerie);
            return RedirectToAction("IndexPersonajes");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.EliminarPersonajeAsync(id);
            return RedirectToAction("IndexPersonajes");
        }

        public async Task<IActionResult> MostrarPersonaje(int idserie) 
        {
            List<Personaje> personajes = await this.service.MostrarPersonajesAsync(idserie);
            return View(personajes);
        }

        public async Task<IActionResult> CambiarPersonaje() 
        {
            List<Serie> nombreSerie = await this.service.GetSeriesAsync();
            List<Personaje> nombrePersonaje = await this.service.GetPersonajesAsync();
            ViewData["SERIES"] = nombreSerie;
            ViewData["PERSONAJES"] = nombrePersonaje;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPersonaje(int idserie, int idpersonaje)
        {
            List<Serie> nombreSerie = await this.service.GetSeriesAsync();
            List<Personaje> nombrePersonaje = await this.service.GetPersonajesAsync();

            return View();
        }

    }
}
