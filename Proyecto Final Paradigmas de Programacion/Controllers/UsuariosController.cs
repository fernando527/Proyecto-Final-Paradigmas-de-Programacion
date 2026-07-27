using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Paradigmas_de_Programacion.Models;
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

        //crear usuario nuevo
        public IActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearCuenta(RegistroUsuario usuario)
        {

            // Validar correo institucional

            if (!usuario.Correo.EndsWith("@unah.hn"))
            {
                ViewBag.Error = "Debe utilizar un correo institucional de la UNAH.";

                return View();
            }



            // Validar contraseña igual

            if (usuario.Contrasena != usuario.ConfirmarContrasena)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";

                return View();
            }



            // Validar contraseña mínima

            if (usuario.Contrasena.Length < 6)
            {
                ViewBag.Error = "La contraseña debe tener mínimo 6 caracteres.";

                return View();
            }



            // Validar correo repetido

            if (usuarioRepository.ExisteCorreo(usuario.Correo))
            {
                ViewBag.Error = "El correo ya está registrado.";

                return View();
            }



            Usuario nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,

                Correo = usuario.Correo,

                Contrasena = usuario.Contrasena,

                Rol = "Usuario"
            };



            usuarioRepository.CrearUsuario(nuevoUsuario);



            return RedirectToAction("Login");

        }


    }
}