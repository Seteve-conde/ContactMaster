using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IContatoRepositorio
    {
        Task<ContatoModel> ListarPorId(int id);
        Task<List<ContatoModel>> BuscarTodos(int usuarioId);
        Task<ContatoModel> Adicionar(ContatoModel contato);
        Task<ContatoModel> Atualizar(ContatoModel contato);
        Task<bool> Apagar(int id);
    }
}
