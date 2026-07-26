using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proyecto_Final_Paradigmas_de_Programacion.Filters
{
    public class AdministradorAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(
            ActionExecutingContext context)
        {

            var rol = context.HttpContext.Session.GetString("Rol");


            if (rol != "Administrador")
            {
                context.Result = new RedirectToActionResult(
                    "Index",
                    "Home",
                    null
                );
            }

        }

    }
}