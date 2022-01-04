using Microsoft.AspNetCore.Mvc;
using motovalle.Ecommerce.Models;
using motovalle.Ecommerce.Models.ViewModels.Landing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace motovalle.Ecommerce.Controllers
{    
    public class OfertasController : Controller
    {
        private readonly msi_ecommerceContext _context;


        public OfertasController(msi_ecommerceContext context)
        {
            _context = context;

        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Muestra la página principal de la Landing Page
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IActionResult LavadoGratisIlimitado()
        {
            ViewBag.msg = TempData["msg"] as string;

            return View();
        }

        /// <summary>
        /// Almacena la informacion a la BD 
        /// </summary>
        /// <param name="contactLavadoGratisViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LavadoGratisIlimitado(ContactLavadoGratisViewModel contactLavadoGratisViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LavadoIlimitadoGratis lavadoIlimitadoGratis = new LavadoIlimitadoGratis();
                    lavadoIlimitadoGratis.Nombres = contactLavadoGratisViewModel.Nombres;
                    lavadoIlimitadoGratis.Apellidos = contactLavadoGratisViewModel.Apellidos;
                    lavadoIlimitadoGratis.Cedula = contactLavadoGratisViewModel.Cedula;
                    lavadoIlimitadoGratis.Celular = contactLavadoGratisViewModel.Correo;
                    lavadoIlimitadoGratis.Correo = contactLavadoGratisViewModel.Nombres;
                    lavadoIlimitadoGratis.MarcaVehiculo = contactLavadoGratisViewModel.MarcaVehiculo;
                    lavadoIlimitadoGratis.Modelo = contactLavadoGratisViewModel.Modelo;
                    lavadoIlimitadoGratis.Placa = contactLavadoGratisViewModel.Placa;
                    lavadoIlimitadoGratis.Factura = contactLavadoGratisViewModel.Placa;
                    lavadoIlimitadoGratis.FechaFactura = contactLavadoGratisViewModel.FechaFactura;
                    lavadoIlimitadoGratis.CiudadResidencia = contactLavadoGratisViewModel.CiudadResidencia;
                    lavadoIlimitadoGratis.TratamientoDatos = contactLavadoGratisViewModel.TratamientoDatos;

                    _context.LavadoIlimitadoGratis.Add(lavadoIlimitadoGratis);
                    _context.SaveChanges();

                    TempData["msg"] = "Agregado";

                    return RedirectToAction(nameof(LavadoGratisIlimitado));
                }
            }
            catch (Exception Ex)
            {
                
                throw;
            }
            return View(contactLavadoGratisViewModel);
            
        }
    }
}
