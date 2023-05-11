using ContactMasterService;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    public class LoginController : Controller
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

        public IActionResult Index()
        {
            // Se o usuário estiver logado, redireciona para a home.
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha() 
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = await _usuarioRepositorio.BuscarPorEmail(loginModel.Email);                    

                    if (usuario != null) 
                    {
                        if (usuario.SenhaValida(loginModel.Senha)) 
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        
                        TempData["MensagemErro"] = $"A senha do usuário é invalida, tente novamente.";
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s); Por favor, tente novamente.";
                    return RedirectToAction("Index", "Login");
                }

                return View("Index");
            }
            catch (Exception error) 
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }         
        }

        [HttpPost]
        public async Task<IActionResult> EnviarRedefinicaoSenha(RedefinirSenhaModel redefinirSenhaModel) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = await _usuarioRepositorio.RedefinirSenhaBuscarPorEmail(redefinirSenhaModel.Email);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        await _usuarioRepositorio.Atualizar(usuario);
                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Contact Master - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            await _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o e-mail. Por favor, tente novamente.";
                        }
                        
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. verifique os dados informados.";
                    return RedirectToAction("Index", "Login");
                }

                return View("Index");
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir a sua senha, tente novamente, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
