using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Paradigmas_de_Programacion.Filters;
using Proyecto_Final_Paradigmas_de_Programacion.Repositories;
using Proyecto_Final_Paradigmas_de_Programacion.Models;

namespace Proyecto_Final_Paradigmas_de_Programacion.Controllers
{
    [SesionActiva]
    [Administrador]
    public class AdministracionController : Controller
    {

        private readonly ReporteRepository reporteRepository;


        public AdministracionController()
        {
            reporteRepository = new ReporteRepository();
        }



        public IActionResult Index()
        {
            var reportes = reporteRepository.ObtenerReportes();

            return View(reportes);
        }
        public IActionResult CambiarEstado(int id)
        {
            var reporte = reporteRepository.ObtenerReportePorId(id);


            if (reporte == null)
            {
                return NotFound();
            }


            return View(reporte);
        }

        [HttpPost]
        public IActionResult GuardarEstado(int IdReporte, int IdEstado)
        {
            reporteRepository.ActualizarEstadoReporte(
                IdReporte,
                IdEstado
            );


            return RedirectToAction("Index");
        }

        public IActionResult Dashboard()
        {
            var datos = reporteRepository.ObtenerDashboard();


            return View(datos);
        }

    }
}