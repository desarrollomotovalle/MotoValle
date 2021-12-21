using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace motovalle.Ecommerce.Controllers
{
    public class OfertasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LavadoGratisIlimitado()
        {

            return View();
        }
    }
}
