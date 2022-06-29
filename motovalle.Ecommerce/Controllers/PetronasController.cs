using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using motovalle.Ecommerce.Models;
using motovalle.Ecommerce.Models.ViewModels.LandingPetronasQR;
using motovalle.Ecommerce.Models.ViewModelsPetronas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace motovalle.Ecommerce.Controllers
{
    public class PetronasController : Controller
    {

        private readonly motovalle_petronasContext _context;
        private readonly MOTOVALLEContext _contextMotovalle;

        public PetronasController(motovalle_petronasContext context, MOTOVALLEContext contextMotovalle)
        {
            _context = context;
            _contextMotovalle = contextMotovalle;
        }


        public IActionResult Index()
        {
            return View();
        }

        //Portafolios de productos

        [HttpGet]
        public IActionResult PortafolioLubricantes(PortafolioLubricantes portafolioAutomotriz)
        {
            ViewBag.ListaGamaProductos = ListaGamaProductosAutomotriz();
            ViewBag.ListaTipoAceite = ListaTipoAceiteAutomotriz();
            ViewBag.ListaViscosidad = ListaViscosidadAutomotriz();
            List<PortafolioLubricantes> listaPortafolioAutomotriz = new List<PortafolioLubricantes>();

            if (portafolioAutomotriz.GamaProducto == null && portafolioAutomotriz.TipoAceite == null && portafolioAutomotriz.ViscosidadAceite == null)
            {
                listaPortafolioAutomotriz = _context.PortafolioLubricantes.ToList();
            }
            else
            {
                if (portafolioAutomotriz.GamaProducto != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricantes.Where(x => x.GamaProducto == portafolioAutomotriz.GamaProducto).ToList();
                }
                if (portafolioAutomotriz.TipoAceite != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricantes.Where(x => x.TipoAceite == portafolioAutomotriz.TipoAceite).ToList();
                }
                if (portafolioAutomotriz.ViscosidadAceite != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricantes.Where(x => x.ViscosidadAceite == portafolioAutomotriz.ViscosidadAceite).ToList();
                }
            }


            return View(listaPortafolioAutomotriz);
        }

        public IActionResult PortafolioLubricantesDetalle(int id)
        {
            //PortafolioLubricantesDetalle detalleProductoAutomotriz = new PortafolioLubricantesDetalle();
            //detalleProductoAutomotriz = _context.PortafolioLubricantesDetalle.FirstOrDefault(x => x.IdDetallePortafolioAutomotriz == id);

            //return View(detalleProductoAutomotriz);


            List<PortafolioDetalleViewModel> portafolioDetalleViewModel = new List<PortafolioDetalleViewModel>();

            portafolioDetalleViewModel = (from p in _context.PortafolioLubricantes
                                          join d in _context.PortafolioLubricantesDetalle
                                          on p.IdDetallePortafolioAutomotriz equals d.IdDetallePortafolioAutomotriz
                                          where p.IdDetallePortafolioAutomotriz == id
                                          select new PortafolioDetalleViewModel
                                          {
                                              UrlImagen = p.UrlImagen,
                                              portafolioLubricantesDetalle = d
                                          }).ToList();







            return View(portafolioDetalleViewModel);
        }


        [HttpGet]
        public IActionResult PortafolioLubricanteAutmotor(PortafolioLubricanteAutomotor portafolioLubricanteAutomotor)
        {
            ViewBag.ListaGamaProductos = ListaGamaProductosAutomotriz();
            ViewBag.ListaTipoAceite = ListaTipoAceiteAutomotriz();
            ViewBag.ListaViscosidad = ListaViscosidadAutomotriz();
            List<PortafolioLubricanteAutomotor> listaPortafolioAutomotriz = new List<PortafolioLubricanteAutomotor>();

            if (portafolioLubricanteAutomotor.GamaProducto == null && portafolioLubricanteAutomotor.TipoAceite == null && portafolioLubricanteAutomotor.ViscosidadAceite == null)
            {
                listaPortafolioAutomotriz = _context.PortafolioLubricanteAutomotor.ToList();
            }
            else
            {
                if (portafolioLubricanteAutomotor.GamaProducto != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricanteAutomotor.Where(x => x.GamaProducto == portafolioLubricanteAutomotor.GamaProducto).ToList();
                }
                if (portafolioLubricanteAutomotor.TipoAceite != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricanteAutomotor.Where(x => x.TipoAceite == portafolioLubricanteAutomotor.TipoAceite).ToList();
                }
                if (portafolioLubricanteAutomotor.ViscosidadAceite != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricanteAutomotor.Where(x => x.ViscosidadAceite == portafolioLubricanteAutomotor.ViscosidadAceite).ToList();
                }
            }


            return View(listaPortafolioAutomotriz);
        }



        // Listado Gama de productos
        public List<SelectListItem> ListaGamaProductosAutomotriz()
        {
            List<SelectListItem> listaGamaProductos = new List<SelectListItem>();
            using (motovalle_petronasContext Db = new motovalle_petronasContext())
            {
                listaGamaProductos = (from f in Db.PortafolioLubricantes
                                      select new SelectListItem
                                      {
                                          Text = f.GamaProducto,
                                          Value = f.GamaProducto.ToString()
                                      }).Distinct().ToList();
                listaGamaProductos.Insert(0, new SelectListItem
                {
                    Text = "- Alguna -",
                    Value = ""
                });
            }
            return listaGamaProductos;
        }

        public List<SelectListItem> ListaTipoAceiteAutomotriz()
        {
            List<SelectListItem> listaTipoAceite = new List<SelectListItem>();
            using (motovalle_petronasContext Db = new motovalle_petronasContext())
            {
                listaTipoAceite = (from f in Db.PortafolioLubricantes
                                   select new SelectListItem
                                   {
                                       Text = f.TipoAceite,
                                       Value = f.TipoAceite.ToString()
                                   }).Distinct().ToList();
                listaTipoAceite.Insert(0, new SelectListItem
                {
                    Text = "- Alguna -",
                    Value = ""
                });
            }
            return listaTipoAceite;
        }

        public List<SelectListItem> ListaViscosidadAutomotriz()
        {
            List<SelectListItem> listaViscosidad = new List<SelectListItem>();
            using (motovalle_petronasContext Db = new motovalle_petronasContext())
            {
                listaViscosidad = (from f in Db.PortafolioLubricantes
                                   select new SelectListItem
                                   {
                                       Text = f.ViscosidadAceite,
                                       Value = f.ViscosidadAceite.ToString()
                                   }).Distinct().ToList();
                listaViscosidad.Insert(0, new SelectListItem
                {
                    Text = "- Alguna -",
                    Value = ""
                });
            }
            return listaViscosidad;
        }

        [HttpGet]
        public IActionResult PortafolioMotocicletas(PortafolioLubricantes portafolioAutomotriz)
        {
            ViewBag.ListaGamaProductos = ListaGamaProductosAutomotriz();
            ViewBag.ListaTipoAceite = ListaTipoAceiteAutomotriz();
            ViewBag.ListaViscosidad = ListaViscosidadAutomotriz();
            List<PortafolioLubricantes> listaPortafolioAutomotriz = new List<PortafolioLubricantes>();

            if (portafolioAutomotriz.GamaProducto == null && portafolioAutomotriz.TipoAceite == null && portafolioAutomotriz.ViscosidadAceite == null)
            {
                listaPortafolioAutomotriz = _context.PortafolioLubricantes.ToList();
            }
            else
            {
                if (portafolioAutomotriz.GamaProducto != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricantes.Where(x => x.GamaProducto == portafolioAutomotriz.GamaProducto).ToList();
                }
                if (portafolioAutomotriz.TipoAceite != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricantes.Where(x => x.TipoAceite == portafolioAutomotriz.TipoAceite).ToList();
                }
                if (portafolioAutomotriz.ViscosidadAceite != null)
                {
                    listaPortafolioAutomotriz = _context.PortafolioLubricantes.Where(x => x.ViscosidadAceite == portafolioAutomotriz.ViscosidadAceite).ToList();
                }
            }


            return View(listaPortafolioAutomotriz);
        }

        public IActionResult Portafolio()
        {

            return View();
        }
        public IActionResult PortafolioDetalle()
        {

            return View();
        }
        public IActionResult Automotor()
        {

            return View();
        }
        public IActionResult LubricanteVehiculosLivianos()
        {

            return View();
        }
        public IActionResult LubricanteMotocicletas()
        {

            return View();
        }
        public IActionResult LubricanteVehiculosComerciales()
        {

            return View();
        }
        public IActionResult Syntium()
        {

            return View();
        }
        public IActionResult Sprinta()
        {

            return View();
        }
        public IActionResult Urania()
        {

            return View();
        }
        public IActionResult Industria()
        {

            return View();
        }
        public IActionResult IndustriaAplicacion()
        {

            return View();
        }
        public IActionResult IndustriaSegmento()
        {

            return View();
        }
        public IActionResult IndustriaDetalle()
        {

            return View();
        }
        public IActionResult Deportes()
        {

            return View();
        }
        public IActionResult Agricola()
        {

            return View();
        }
        public IActionResult Hidraulico()
        {

            return View();
        }
        public IActionResult Transmision()
        {

            return View();
        }
        public IActionResult Formula1()
        {

            return View();
        }

        public IActionResult RallyDakar()
        {

            return View();
        }

        //Acerca de 

        public IActionResult Acerca()
        {
            return View();
        }

        public IActionResult AcercaDetalle()
        {
            return View();
        }

        //Contactenos

        public IActionResult Contactenos()
        {
            return View();
        }

        //Expertos

        public IActionResult Expertos()
        {
            return View();
        }

        //Quienes somos

        public IActionResult QuienesSomos()
        {
            return View();
        }

        //Tarjetas de industria por aplicacion
        public IActionResult IndustriaAplicacionMotosierra()
        {

            return View();
        }
        public IActionResult IndustriaAplicacionTransferencia()
        {

            return View();
        }
        public IActionResult IndustriaAplicacionCirculantes()
        {

            return View();
        }
        public IActionResult IndustriaAplicacionEngranajes()
        {

            return View();
        }
        public IActionResult IndustriaAplicacionFerrocarril()
        {

            return View();
        }
        public IActionResult IndustriaAplicacionMotorGas()
        {

            return View();
        }

        //Tarjetas de industria por segmento
        public IActionResult IndustriaSegmentoAcero()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoAzucar()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoCemento()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoFerrocarril()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoCarbon()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoNuclear()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoCieloAbrierto()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoMineriaSubterranea()
        {

            return View();
        }
        public IActionResult IndustriaSegmentoPapelCelulosa()
        {

            return View();
        }

        //Ruta para codigo QR
        public IActionResult CambioDeAceite()
        {
            return View();
        }

        // Ruta para petronas redime tu premio

        [HttpGet]
        public IActionResult RedimeTuPremio(int id)
        {
            TMercadeoClientesViewModel tMercadeoClientesViewModel = new TMercadeoClientesViewModel();
            tMercadeoClientesViewModel.IdQR = id;

            tMercadeoClientesViewModel.Control = "";


            return View(tMercadeoClientesViewModel);
        }

        [HttpPost]
        public IActionResult RedimeTuPremio(TMercadeoClientesViewModel tMercadeoClientesViewModel)
        {

            try
            {
                // Valida si el Id del QR es enviado en la URL del navegador
                if (tMercadeoClientesViewModel.IdQR == 0)
                {
                    tMercadeoClientesViewModel.Control = "IdQRNull";
                    return View(tMercadeoClientesViewModel);
                }


                // Valida sí el Id del QR enviado por parametro existe en la base de datos
                var validarPropietarioQR = _contextMotovalle.TMercadeoConvenio.FirstOrDefault(i => i.IdConvenio == tMercadeoClientesViewModel.IdQR);
                if (validarPropietarioQR == null)
                {
                    tMercadeoClientesViewModel.Control = "IdQRNoExiste";
                    return View(tMercadeoClientesViewModel);
                }


                // Valida si el Modelo es valido
                if (ModelState.IsValid)
                {
                    // Valida si la placa enviada en el modelo existe en la BD
                    var obtenerPlaca = _contextMotovalle.TMercadeoClientes.FirstOrDefault(i => i.Placa == tMercadeoClientesViewModel.Placa);
                    if (obtenerPlaca != null)
                    {
                        tMercadeoClientesViewModel.Control = "MsjPlaca";
                        return View(tMercadeoClientesViewModel);
                    }

                    // Envia la información a la tabla TMercadeoClientes
                    TMercadeoClientes tMercadeoClientes = new TMercadeoClientes();

                    tMercadeoClientes.NroDocumento = tMercadeoClientesViewModel.NroDocumento;
                    tMercadeoClientes.NombreCompleto = tMercadeoClientesViewModel.NombreCompleto;
                    tMercadeoClientes.Correo = tMercadeoClientesViewModel.Correo;
                    tMercadeoClientes.Marca = tMercadeoClientesViewModel.Marca;
                    tMercadeoClientes.Modelo = tMercadeoClientesViewModel.Modelo;
                    tMercadeoClientes.Linea = tMercadeoClientesViewModel.Linea;
                    tMercadeoClientes.FechaRegistro = DateTime.Now;
                    tMercadeoClientes.NroContacto = tMercadeoClientesViewModel.NroContacto;
                    tMercadeoClientes.Placa = tMercadeoClientesViewModel.Placa.ToUpper();
                    tMercadeoClientes.CiudadRegistro = tMercadeoClientesViewModel.CiudadRegistro;

                    _contextMotovalle.TMercadeoClientes.Add(tMercadeoClientes);
                    _contextMotovalle.SaveChanges();

                    tMercadeoClientesViewModel.Control = "MsjGuardar";

                    // Consulta el ID asignado por la BD segun la placa almacenada anteriormente
                    var consultaPlaca = _contextMotovalle.TMercadeoClientes.FirstOrDefault(i => i.Placa == tMercadeoClientesViewModel.Placa);

                    if (consultaPlaca != null)
                    {
                        TMercadeoClienteConvenio tMercadeoClienteConvenio = new TMercadeoClienteConvenio();
                        tMercadeoClienteConvenio.TMercadeoConvenioIdConvenio = tMercadeoClientesViewModel.IdQR;
                        tMercadeoClienteConvenio.TMercadeoClientesIdCliente = consultaPlaca.IdCliente;

                        _contextMotovalle.TMercadeoClienteConvenio.Add(tMercadeoClienteConvenio);
                        _contextMotovalle.SaveChanges();

                        // Retorna en el modelo la descripcion del convenio para enviarla a la API de los MSJ de texto y correo de bienvenida 
                        tMercadeoClientesViewModel.DescripcionConvenio = validarPropietarioQR.DesConvenio;
                    }
                }
                else
                {
                    tMercadeoClientesViewModel.Control = "MsjCamposRequeridos";
                    return View(tMercadeoClientesViewModel);
                }
            }
            catch (Exception Ex)
            {

                var mensaje = Ex.Message;

                tMercadeoClientesViewModel.Control = "errorInesperado";
                return View(tMercadeoClientesViewModel);

            }


            return View(tMercadeoClientesViewModel);
        }
    }
}
