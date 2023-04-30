using ContactMaster.Services;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            List<UsuarioModel> usuarios = await _usuarioService.ObterTodos();
            return View(usuarios);
        }

        public async Task<IActionResult> GerarPdf()
        {
            List<UsuarioModel> usuarios = await _usuarioService.ObterTodos();
            return new Rotativa.AspNetCore.ViewAsPdf("Index", usuarios);
        }

        public async Task<IActionResult> Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            UsuarioModel usuarios = await _usuarioService.ObterPorId(id);
            return View(usuarios);
        }

        public async Task<IActionResult> ApagarUsuario(int id)
        {
            UsuarioModel usuarios = await _usuarioService.ObterPorId(id);
            return View(usuarios);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _usuarioService.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu usuário!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu usuário, mais detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _usuarioService.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _usuarioService.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar seu usuário, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }
    }    
}
