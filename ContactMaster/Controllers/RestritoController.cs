using ContactMaster.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContactMaster.Controllers
{
    [FiltroParaUsuariosLogados]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
