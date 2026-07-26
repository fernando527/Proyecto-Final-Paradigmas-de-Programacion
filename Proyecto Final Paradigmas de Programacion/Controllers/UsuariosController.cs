using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Paradigmas_de_Programacion.Repositories;

namespace Proyecto_Final_Paradigmas_de_Programacion.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly UsuarioRepository usuarioRepository;


        public UsuariosController()
        {
            usuarioRepository = new UsuarioRepository();
        }

        //para cerra cesion
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login",
        "Usuarios");
        }




        // Mostrar pantalla de Login
        public IActionResult Login()
        {
            return View();
        }



        // Procesar Login
        [HttpPost]
        public IActionResult Login(string correo, string contrasena)
        {

            var usuario = usuarioRepository.Login(correo, contrasena);



            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";

                return View();
            }



            // Guardar datos del usuario en sesión
            HttpContext.Session.SetInt32(
                "IdUsuario",
                usuario.IdUsuario
            );


            HttpContext.Session.SetString(
                "NombreUsuario",
                usuario.Nombre
            );


            HttpContext.Session.SetString(
                "Rol",
                usuario.Rol
            );



            return RedirectToAction(
                "Index",
                "Home"
            );
        }


    }
}