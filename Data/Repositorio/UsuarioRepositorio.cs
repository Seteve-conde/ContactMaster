using DataContext;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<UsuarioModel> BuscarPorEmail(string email)
        {
            return await _bancoContext.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UsuarioModel> ListarPorId(int id)
        {
            return await _bancoContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodos()
        {
            return await _bancoContext.Usuarios.ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            usuario.DataUserCreated = DateTime.Now;            
            await _bancoContext.Usuarios.AddAsync(usuario);
            await _bancoContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = await ListarPorId(usuario.Id);

            if (usuarioDb == null) throw new System.Exception("Ouve um erro ao atualizar dados do usuário!");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;            
            usuarioDb.Login = usuario.Login;            
            usuarioDb.Perfil = usuario.Perfil;            
            usuarioDb.AtualizationDate = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            await _bancoContext.SaveChangesAsync();

            return usuarioDb;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioDb = await ListarPorId(id);

            if (usuarioDb == null) throw new System.Exception("Houve um erro ao tentar Apagar o Usuário");

            _bancoContext.Usuarios.Remove(usuarioDb);
            await _bancoContext.SaveChangesAsync();

            return true;
        }
    }
}
