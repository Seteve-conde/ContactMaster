using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Login(UsuarioModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userService.FindByEmailAndPasswordAsync(model.Email, model.Senha);
        //        if (user != null)
        //        {
        //            // Armazena o token de autenticação no cookie do usuário
        //            var token = _authService.GenerateToken(user);
        //            Response.Cookies.Append("AuthToken", token);

        //            // Redireciona o usuário para a página inicial da aplicação
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }

        //    // Exibe uma mensagem de erro ao usuário
        //    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
        //    return View(model);
        //}
    }
}
