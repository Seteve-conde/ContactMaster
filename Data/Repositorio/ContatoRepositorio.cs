using DataContext;
using Dominio.Interfaces;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dados.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task<ContatoModel> ListarPorId(int id)
        {
           return await _bancoContext.Contatos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ContatoModel>> BuscarTodos()
        {
            return await _bancoContext.Contatos.ToListAsync();
        }       

        public async Task<ContatoModel> Adicionar(ContatoModel contato)
        {
            await _bancoContext.Contatos.AddAsync(contato);
            await _bancoContext.SaveChangesAsync();

            return contato;
        }

        public async Task<ContatoModel> Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDb = await ListarPorId(contato.Id);

            if (contatoDb == null) throw new System.Exception("Ouve um erro ao atualizar dados do contato!");

            contatoDb.Nome = contato.Nome;
            contatoDb.Email = contato.Email;
            contatoDb.Celular = contato.Celular;
            contatoDb.Data = contato.Data;
            contatoDb.Valor = contato.Valor;

            _bancoContext.Contatos.Update(contatoDb);
            await _bancoContext.SaveChangesAsync();

            return contatoDb;
        }

        public async Task<bool> Apagar(int id)
        {
            ContatoModel contatoDb = await ListarPorId(id);

            if (contatoDb == null) throw new System.Exception("Houve um erro ao tentar Apagar o Contato");

            _bancoContext.Contatos.Remove(contatoDb);
            await _bancoContext.SaveChangesAsync();

            return true;
        }
    }
}
