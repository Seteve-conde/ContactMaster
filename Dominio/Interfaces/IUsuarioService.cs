using Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMaster.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> ObterPorId(int id);
        Task<List<UsuarioModel>> ObterTodos();
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario);
        Task<bool> Apagar(int id);
    }
}
