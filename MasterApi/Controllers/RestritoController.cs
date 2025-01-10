using ContactMasterService;
using Microsoft.AspNetCore.Mvc;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FiltroParaUsuariosLogadosService]
    public class RestritoController : ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(new { Message = "Acesso restrito permitido." });
        }
    }
}
