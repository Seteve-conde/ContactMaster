﻿using ContactMaster.Filters;
using ContactMasterService;
using ContactMasterService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FiltroParaUsuariosLogados]
    public class RestritoController : ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(new { Message = "Acesso restrito permitido." });
        }
    }
}
