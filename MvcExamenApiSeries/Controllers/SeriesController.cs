using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExamenApiSeries.Controllers
{
    public class SeriesController : Controller
    {
        public IActionResult IndexSeriesJquery()
        {
            return View();
        }
    }
}
