using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModificarSenhaController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public ModificarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        [HttpPost("alterar")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                var usuarioLogado = _sessao.BuscarSessaoUsuario();
                if (usuarioLogado == null)
                {
                    return Unauthorized(new { Message = "Usuário não está autenticado." });
                }

                alterarSenhaModel.Id = usuarioLogado.Id;

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Message = "Dados inválidos.", Errors = ModelState });
                }

                await _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                return Ok(new { Message = "Senha alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao alterar a senha. Detalhes: {ex.Message}" });
            }
        }
    }
}
