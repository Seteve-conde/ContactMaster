using ContactMaster.Services;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        public async Task<IActionResult> Index()
        {
            List<ContatoModel> contatos = await _contatoService.ObterTodos();
            return View(contatos);
        }

        public async Task<IActionResult> GerarPdf()
        {
            List<ContatoModel> contatos = await _contatoService.ObterTodos();
            return new Rotativa.AspNetCore.ViewAsPdf("Index", contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            ContatoModel contato = await _contatoService.ObterPorId(id);
            return View(contato);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            ContatoModel contato = await _contatoService.ObterPorId(id);
            return View(contato);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _contatoService.Apagar(id);
                
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "contato apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu contato!";
                }
                
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] =  $"Ops, não conseguimos apagar seu contato, mais detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contatoService.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] =  $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contatoService.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar seu contato, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
