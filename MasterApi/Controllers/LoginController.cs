using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            _sessao.RemoverSessaoUsuario();
            return Ok(new { Message = "Logout realizado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Message = "Dados inválidos." });

                var usuario = await _usuarioRepositorio.BuscarPorEmail(loginModel.Email);

                if (usuario != null && usuario.SenhaValida(loginModel.Senha))
                {
                    _sessao.CriarSessaoUsuario(usuario);
                    return Ok(new { Message = "Login realizado com sucesso." });
                }

                return Unauthorized(new { Message = "Usuário e/ou senha inválido(s)." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpPost("redefinir-senha")]
        public async Task<IActionResult> RedefinirSenha([FromBody] RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Message = "Dados inválidos." });

                var usuario = await _usuarioRepositorio.RedefinirSenhaBuscarPorEmail(redefinirSenhaModel.Email);

                if (usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();
                    usuario.Senha = novaSenha;

                    await _usuarioRepositorio.Atualizar(usuario);
                    string mensagem = $"Sua nova senha é: {novaSenha}";

                    var emailEnviado = await _email.Enviar(usuario.Email, "Contact Master - Nova Senha", mensagem);

                    if (emailEnviado)
                        return Ok(new { Message = "Nova senha enviada para o e-mail cadastrado." });

                    return StatusCode(500, new { Message = "Erro ao enviar o e-mail." });
                }

                return NotFound(new { Message = "Usuário não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro interno: {ex.Message}" });
            }
        }
    }
}
