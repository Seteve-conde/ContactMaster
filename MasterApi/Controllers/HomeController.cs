﻿using ContactMaster.Filters;
using ContactMaster.Models;
using ContactMasterService;
using ContactMasterService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FiltroParaUsuariosLogados]
    public class HomeController : ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(new { Message = "Welcome to the Home API." });
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return Ok(new { Message = "Privacy information." });
        }

        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return StatusCode(500, errorViewModel);
        }
    }
}
