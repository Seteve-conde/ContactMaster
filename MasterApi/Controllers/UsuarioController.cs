using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactMaster.Services;
using ContactMasterService;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FiltroSomenteAdminApi]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly IBonusRepositorio _bonusRepositorio;

        public UsuarioController(IUsuarioService usuarioService,
                                 IContatoRepositorio contatoRepositorio,
                                 IBonusRepositorio bonusRepositorio)
        {
            _usuarioService = usuarioService;
            _contatoRepositorio = contatoRepositorio;
            _bonusRepositorio = bonusRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObterTodos();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.ObterPorId(id);
            if (usuario == null)
                return NotFound(new { Message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usuarioService.Adicionar(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            usuario.Id = id;
            await _usuarioService.Atualizar(usuario);
            return Ok(new { Message = "Usuário alterado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var apagado = await _usuarioService.Apagar(id);

            if (!apagado)
                return BadRequest(new { Message = "Não foi possível apagar o usuário." });

            return Ok(new { Message = "Usuário apagado com sucesso." });
        }

        [HttpGet("{id}/contatos")]
        public async Task<IActionResult> GetContatosByUsuarioId(int id)
        {
            var contatos = await _contatoRepositorio.BuscarTodos(id);
            return Ok(contatos);
        }

        [HttpGet("{id}/bonus")]
        public async Task<IActionResult> GetBonusByUsuarioId(int id)
        {
            var bonus = await _bonusRepositorio.BuscarTodos(id);
            return Ok(bonus);
        }
    }
}
