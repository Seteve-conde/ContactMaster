using ContactMaster.Services;
using Dominio.Interfaces;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMasterService
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoService(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public async Task<ContatoModel> ObterPorId(int id)
        {
            return await _contatoRepositorio.ListarPorId(id);
        }

        public async Task<List<ContatoModel>> ObterTodos()
        {
            return await  _contatoRepositorio.BuscarTodos();
        }

        public async Task<ContatoModel> Adicionar(ContatoModel contato)
        {
            // Verifica se já existe algum registro com o mesmo nome ou número de telefone
            var contatosExistentes = await _contatoRepositorio.BuscarTodos();
            contatosExistentes = contatosExistentes.Where(c => c.Nome == contato.Nome || c.Celular == contato.Celular || c.Email == contato.Email).ToList();

            if (contatosExistentes.Any())
            {
                throw new Exception("Já existe um contato com o mesmo nome, email ou número de telefone.");
            }

            // Insere o novo contato
            return await _contatoRepositorio.Adicionar(contato);
        }

        public async Task<ContatoModel> Atualizar(ContatoModel contato)
        {
            return await _contatoRepositorio.Atualizar(contato);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _contatoRepositorio.Apagar(id);
        }
    }
}

