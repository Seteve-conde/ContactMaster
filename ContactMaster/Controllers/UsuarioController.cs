﻿using ContactMaster.Filters;
using ContactMaster.Services;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Controllers
{
    [FiltroSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly IBonusRepositorio _bonusRepositorio;

        public UsuarioController(IUsuarioService usuarioService,
                                 IContatoRepositorio contatoRepositorio, IBonusRepositorio  bonusRepositorio )
        {
            _usuarioService = usuarioService;
            _contatoRepositorio = contatoRepositorio;
            _bonusRepositorio = bonusRepositorio;
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

        public async Task<IActionResult> ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = await _contatoRepositorio.BuscarTodos();
            return PartialView("_ContatosUsuario", contatos);
        }

        public async Task<IActionResult> ListarBonusPorUsuarioId(int id)
        {
            List<BonusModel> bonus = await _bonusRepositorio.BuscarTodos(id);
            return PartialView("_ContatosUsuario", bonus);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(UsuarioModel usuario)
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
