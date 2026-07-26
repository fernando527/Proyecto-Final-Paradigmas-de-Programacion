using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Paradigmas_de_Programacion.Models;
using Proyecto_Final_Paradigmas_de_Programacion.Repositories;
using Proyecto_Final_Paradigmas_de_Programacion.Filters;

namespace Proyecto_Final_Paradigmas_de_Programacion.Controllers
{
    [SesionActiva]
    public class ReportesController : Controller
    {
        private readonly ReporteRepository reporteRepository;

        public ReportesController()
        {
            reporteRepository = new ReporteRepository();
        }


        public IActionResult Index()
        {
            var reportes = reporteRepository.ObtenerReportes();

            return View(reportes);
        }

        public IActionResult MisReportes()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");


            if (idUsuario == null)
            {
                return RedirectToAction(
                    "Login",
                    "Usuarios"
                );
            }


            var reportes =
                reporteRepository.ObtenerReportesPorUsuario(
                    idUsuario.Value
                );


            return View(reportes);
        }



        // Mostrar formulario para crear
        public IActionResult Crear()
        {
            ViewBag.Modo = "Crear";

            return View("Formulario", new Reporte());
        }




        // Guardar reporte
        [HttpPost]
        public IActionResult Guardar(Reporte reporte)
        {

            if (string.IsNullOrWhiteSpace(reporte.Edificio) ||
               string.IsNullOrWhiteSpace(reporte.Aula) ||
               string.IsNullOrWhiteSpace(reporte.Descripcion) ||
               reporte.IdTipoDanio == 0 ||
               reporte.IdPrioridad == 0)
            {
                ViewBag.Error = "Todos los campos son obligatorios";

                if (reporte.IdReporte == 0)
                    ViewBag.Modo = "Crear";
                else
                    ViewBag.Modo = "Editar";


                return View("Formulario", reporte);
            }



            // en caso de crear
            if (reporte.IdReporte == 0)
            {
                var idUsuario = HttpContext.Session.GetInt32("IdUsuario");


                if (idUsuario == null)
                {
                    return RedirectToAction(
                        "Login",
                        "Usuarios"
                    );
                }


                reporte.IdUsuario = idUsuario.Value;


                reporteRepository.CrearReporte(reporte);
            }

            // en caso de editar
            else
            {
                reporteRepository.ActualizarReporte(reporte);
            }



            return RedirectToAction("Index");
        }


        //editar formulario
        public IActionResult EditarFormulario(int id)
        {
            var reporte = reporteRepository.ObtenerReportePorId(id);


            if (reporte == null)
            {
                return NotFound();
            }


            ViewBag.Modo = "Editar";


            return View("Formulario", reporte);
        }

        //eliminar

        public IActionResult Eliminar(int id)
        {
            reporteRepository.EliminarReporte(id);

            return RedirectToAction("Index");
        }


    }
}