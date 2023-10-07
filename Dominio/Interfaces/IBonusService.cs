using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Services
{
    public interface IBonusService
    {
        Task<BonusModel> ObterPorId(int id);
        Task<List<BonusModel>> ObterTodos(BonusModel bonus);
        Task<BonusModel> Adicionar(BonusModel usuario);
        Task<BonusModel> Atualizar(BonusModel usuario);
        Task<bool> Apagar(int id);
    }
}
