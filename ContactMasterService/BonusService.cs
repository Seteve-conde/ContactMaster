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

        public BonusService(IBonusRepositorio bonusRepositorio)
        {
            _bonusRepositorio = bonusRepositorio;           
        }

        public async Task<BonusModel> ObterPorId(int id)
        {
            return await _bonusRepositorio.ListarPorId(id);
        }

        public async Task<List<BonusModel>> ObterTodos(BonusModel bonus)
        {            
            return await _bonusRepositorio.BuscarTodos(bonus);
        }

        public async Task<BonusModel> Adicionar(BonusModel bonus)
        {            
            
            var bonusExistentes = await _bonusRepositorio.BuscarTodos(bonus);
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
            return await _bonusRepositorio.Atualizar(bonus);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _bonusRepositorio.Apagar(id);
        }
    }
}

