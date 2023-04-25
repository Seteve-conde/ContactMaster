using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Services
{
    public interface IContatoService
    {
        Task<ContatoModel> ObterPorId(int id);
        Task<List<ContatoModel>> ObterTodos();
        Task<ContatoModel> Adicionar(ContatoModel contato);
        Task<ContatoModel> Atualizar(ContatoModel contato);
        Task<bool> Apagar(int id);
    }
}

