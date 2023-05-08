using Dominio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace ContactMaster.Filters
{
    public class FiltroSomenteAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sectionUser = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sectionUser) ) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else 
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sectionUser);

                if (usuario == null ) 
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if (usuario.PerfilEnum != UsuariosPerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
