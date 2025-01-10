using ContactMaster.Services;
using Dominio.Interfaces;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMasterService.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<UsuarioModel> ObterPorId(int id)
        {
            return await _usuarioRepositorio.ListarPorId(id);
        }

        public async Task<List<UsuarioModel>> ObterTodos()
        {
            return await _usuarioRepositorio.BuscarTodos();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            // Verifica se já existe algum registro com o mesmo nome ou número de telefone
            var usuariosExistentes = await _usuarioRepositorio.BuscarTodos();
            usuariosExistentes = usuariosExistentes.Where(c => c.Nome == usuario.Nome || c.Email == usuario.Email).ToList();

            if (usuariosExistentes.Any())
            {
                throw new Exception("Já existe um usuário com o mesmo nome, email ou número de telefone.");
            }

            // Insere o novo contato
            return await _usuarioRepositorio.Adicionar(usuario);
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario)
        {
            return await _usuarioRepositorio.Atualizar(usuario);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _usuarioRepositorio.Apagar(id);
        }
    }
}
