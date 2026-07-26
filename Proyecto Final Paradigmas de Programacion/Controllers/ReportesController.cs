using Microsoft.AspNetCore.Mvc;
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
    }
}