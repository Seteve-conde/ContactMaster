using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IBonusRepositorio
    {        
        Task<BonusModel> ListarPorId(int id);
        Task<List<BonusModel>> BuscarTodos(int usuarioId);
        Task<BonusModel> Adicionar(BonusModel bonus);
        Task<BonusModel> Atualizar(BonusModel bonus);        
        Task<bool> Apagar(int id);
    }
}
