using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EquiposTareas.MVC.Models
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _rolesPermitidos;

        public SessionAuthorizeAttribute(params string[] rolesPermitidos)
        {
            _rolesPermitidos = rolesPermitidos;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.Session.GetString("Usuario");
            var rol = context.HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(usuario) || (_rolesPermitidos.Length > 0 && !_rolesPermitidos.Contains(rol)))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Login", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
