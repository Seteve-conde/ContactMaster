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

        public BonusRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<BonusModel> ListarPorId(int id)
        {
            return await _bancoContext.BonusModels.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task<List<BonusModel>> BuscarTodos(int usuarioId)
        {
            return await _bancoContext.BonusModels.Where(x => x.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<BonusModel> Adicionar(BonusModel bonus)
        {
            await _bancoContext.BonusModels.AddAsync(bonus);
            await _bancoContext.SaveChangesAsync();

            return bonus;
        }

        public async Task<BonusModel> Atualizar(BonusModel bonus)
        {
            BonusModel BonusDb = await ListarPorId(bonus.Id);

            if (BonusDb == null) throw new System.Exception("Ouve um erro ao atualizar dados do valor!");

            BonusDb.Name = bonus.Name;            
            BonusDb.Price = bonus.Price;

            _bancoContext.BonusModels.Update(BonusDb);
            await _bancoContext.SaveChangesAsync();

            return BonusDb;
        }

        public async Task<bool> Apagar(int id)
        {
            BonusModel BonusDb = await ListarPorId(id);

            if (BonusDb == null) throw new System.Exception("Houve um erro ao tentar Apagar o valor");

            _bancoContext.BonusModels.Remove(BonusDb);
            await _bancoContext.SaveChangesAsync();

            return true;
        }
    }
}
