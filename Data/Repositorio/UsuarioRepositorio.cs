using ContactMasterService;
using DataContext;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.AspNetCore.Http.Internal;
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

        public async Task<UsuarioModel> RedefinirSenhaBuscarPorEmail(string email)
        {
            return await _bancoContext.Usuarios.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task<UsuarioModel> ListarPorId(int id)
        {
            return await _bancoContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodos()
        {
            return await _bancoContext.Usuarios
                .Include(x => x.Contatos)
                .Include(x => x.Bonus)
                .ToListAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {          
            
            usuario.DataUserCreated = DateTime.Now;
            usuario.SetSenhaHash();

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
            usuarioDb.PerfilEnum = usuario.PerfilEnum;
            //usuarioDb.Senha = usuario.SetSenhaHash();
            usuarioDb.AtualizationDate = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            await _bancoContext.SaveChangesAsync();

            return usuarioDb;
        }

        public async Task<UsuarioModel> AlterarSenha(AlterarSenhaModel alterarSenhaModel) 
        {
            UsuarioModel usuarioDb = await ListarPorId(alterarSenhaModel.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioDb.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDb.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
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
