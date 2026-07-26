using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Paradigmas_de_Programacion.Data;
using Proyecto_Final_Paradigmas_de_Programacion.Models;
using System.Diagnostics;

namespace Proyecto_Final_Paradigmas_de_Programacion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ConexionDB conexion = new ConexionDB();

            try
            {
                using (var con = conexion.ObtenerConexion())
                {
                    con.Open();
                }

                ViewBag.Mensaje = "Conexión exitosa con la base de datos";
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
