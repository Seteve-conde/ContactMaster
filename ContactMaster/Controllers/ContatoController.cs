using ContactMaster.Filters;
using ContactMaster.Services;
using ContactMasterService.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    [FiltroParaUsuariosLogados]
    public class ContatoController : Controller
    {
        private readonly IContatoApiService _contatoApiService;

        public ContatoController(IContatoApiService contatoApiService)
        {
            _contatoApiService = contatoApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<ContatoModel> contatos = await _contatoApiService.ObterTodosAsync();
            return View(contatos);
        }

        public async Task<IActionResult> GerarPdf()
        {
            List<ContatoModel> contatos = await _contatoApiService.ObterTodosAsync();
            return new Rotativa.AspNetCore.ViewAsPdf("Index", contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {

            ContatoModel contato = await _contatoApiService.ObterPorIdAsync(id);
            return View(contato);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            ContatoModel contato = await _contatoApiService.ObterPorIdAsync(id);
            return View(contato);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _contatoApiService.DeletarAsync(id);
                
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
                    await _contatoApiService.CriarAsync(contato);
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
                    await _contatoApiService.AtualizarAsync(contato);
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