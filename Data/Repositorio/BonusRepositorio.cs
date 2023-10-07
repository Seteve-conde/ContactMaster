using DataContext;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dados.Repositorio
{
    public class BonusRepositorio : IBonusRepositorio
    {
        private readonly BancoContext _bancoContext;       

        public async Task<BonusModel> ListarPorId(int id)
        {
            return await _bancoContext.BonusModels.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task<List<BonusModel>> BuscarTodos(BonusModel bonus)
        {
            return await _bancoContext.BonusModels.Where(x => x.Id == bonus.Id).ToListAsync();
        }

        public async Task<BonusModel> Adicionar(BonusModel bonus)
        {
            await _bancoContext.BonusModels.AddAsync(bonus);
            await _bancoContext.SaveChangesAsync();

            return bonus;
        }

        public async Task<BonusModel> Atualizar(BonusModel bonus)
        {
            BonusModel contatoDb = await ListarPorId(bonus.Id);

            if (contatoDb == null) throw new System.Exception("Ouve um erro ao atualizar dados do contato!");

            contatoDb.Name = bonus.Name;            
            contatoDb.Price = bonus.Price;

            _bancoContext.BonusModels.Update(contatoDb);
            await _bancoContext.SaveChangesAsync();

            return contatoDb;
        }

        public async Task<bool> Apagar(int id)
        {
            BonusModel contatoDb = await ListarPorId(id);

            if (contatoDb == null) throw new System.Exception("Houve um erro ao tentar Apagar o Contato");

            _bancoContext.BonusModels.Remove(contatoDb);
            await _bancoContext.SaveChangesAsync();

            return true;
        }
    }
}
