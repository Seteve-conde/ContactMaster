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

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
