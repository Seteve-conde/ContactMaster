using ContactMaster.Filters;
using ContactMaster.Services;
using ContactMasterService;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    [FiltroParaUsuariosLogados]
    public class BonusController : Controller
    {
        private readonly IBonusService _bonusService;

        public BonusController(IBonusService bonusService)
        {
            _bonusService = bonusService;
        }

        public async Task<IActionResult> Index()
        {
            List<BonusModel> bonus = await _bonusService.ObterTodos();
            return View(bonus);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            BonusModel bonus = await _bonusService.ObterPorId(id);
            return View(bonus);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _bonusService.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "valor apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu montante!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu montante, mais detalhes do erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Criar(BonusModel bonus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bonusService.Adicionar(bonus);
                    TempData["MensagemSucesso"] = "Valor cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(bonus);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(BonusModel bonus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bonusService.Atualizar(bonus);
                    TempData["MensagemSucesso"] = "Valor alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", bonus);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar seu montante, tente novamente, detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }        
    }
}
