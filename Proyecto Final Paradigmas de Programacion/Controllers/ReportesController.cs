using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Paradigmas_de_Programacion.Models;
using Proyecto_Final_Paradigmas_de_Programacion.Repositories;

namespace Proyecto_Final_Paradigmas_de_Programacion.Controllers
{
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


        // Mostrar formulario
        public IActionResult Crear()
        {
            return View();
        }


        // Guardar reporte
        [HttpPost]
        public IActionResult Crear(Reporte reporte)
        {

            if (string.IsNullOrWhiteSpace(reporte.Edificio) ||
               string.IsNullOrWhiteSpace(reporte.Aula) ||
               string.IsNullOrWhiteSpace(reporte.Descripcion) ||
               reporte.IdTipoDanio == 0 ||
               reporte.IdPrioridad == 0)
            {
                ViewBag.Error = "Todos los campos son obligatorios";
                return View(reporte);
            }


            reporte.IdUsuario = 2;

            reporteRepository.CrearReporte(reporte);


            return RedirectToAction("Index");
        }
    }
}