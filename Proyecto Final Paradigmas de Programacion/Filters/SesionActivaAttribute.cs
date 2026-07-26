using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proyecto_Final_Paradigmas_de_Programacion.Filters
{
    public class SesionActivaAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(
            ActionExecutingContext context)
        {

            var usuario =
                context.HttpContext.Session.GetString("NombreUsuario");


            if (usuario == null)
            {
                context.Result =
                    new RedirectToActionResult(
                        "Login",
                        "Usuarios",
                        null
                    );
            }


            base.OnActionExecuting(context);

        }

    }
}