using ContactMaster.Services;
using Dominio.Interfaces;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMasterService
{
    public class BonusService : IBonusService
    {
        private readonly IBonusRepositorio _bonusRepositorio;
        private readonly ISessao _sessao;

        public BonusService(IBonusRepositorio bonusRepositorio, ISessao sessao)
        {
            _bonusRepositorio = bonusRepositorio;
            _sessao = sessao;
        }

        public async Task<BonusModel> ObterPorId(int id)
        {
            return await _bonusRepositorio.ListarPorId(id);
        }

        public async Task<List<BonusModel>> ObterTodos()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            return await _bonusRepositorio.BuscarTodos(usuarioLogado.Id);
        }

        public async Task<BonusModel> Adicionar(BonusModel bonus)
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            bonus.UsuarioId = usuarioLogado.Id;
            var bonusExistentes = await _bonusRepositorio.BuscarTodos(usuarioLogado.Id);
            var bonusExiste = bonusExistentes.Where(c => c.Name == bonus.Name || c.Price == bonus.Price).ToList();

            if (bonusExiste.Any())
            {
                throw new Exception("Já existe um contato com o mesmo nome, email ou número de telefone.");
            }

            // Insere o novo contato
            return await _bonusRepositorio.Adicionar(bonus);
        }

        public async Task<BonusModel> Atualizar(BonusModel bonus)
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            bonus.UsuarioId = usuarioLogado.Id;
            return await _bonusRepositorio.Atualizar(bonus);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _bonusRepositorio.Apagar(id);
        }
    }
}

