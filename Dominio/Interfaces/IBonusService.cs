using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Services
{
    public interface IBonusService
    {
        Task<BonusModel> ObterPorId(int id);
        Task<List<BonusModel>> ObterTodos();
        Task<BonusModel> Adicionar(BonusModel bonus);
        Task<BonusModel> Atualizar(BonusModel bonus);
        Task<bool> Apagar(int id);
    }
}
