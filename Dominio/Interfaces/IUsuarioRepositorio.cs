using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<UsuarioModel> BuscarPorEmail (string email);
        Task<UsuarioModel> RedefinirSenhaBuscarPorEmail(string email);
        Task<UsuarioModel> ListarPorId(int id);
        Task<List<UsuarioModel>> BuscarTodos();
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario);
        Task<bool> Apagar(int id);
    }
}
