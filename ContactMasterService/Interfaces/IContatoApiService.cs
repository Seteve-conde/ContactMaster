using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMasterService.Interfaces
{
    public interface IContatoApiService
    {
        Task<List<ContatoModel>> ObterTodosAsync();
        Task<ContatoModel> ObterPorIdAsync(int id);
        Task CriarAsync(ContatoModel contato);
        Task AtualizarAsync(ContatoModel contato);
        Task<bool> DeletarAsync(int id);
    }
}
