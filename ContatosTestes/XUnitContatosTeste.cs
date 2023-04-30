using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMaster.Services;
using ContactMasterService;
using Dominio.Interfaces;
using Dominio.Models;
using Moq;
using Xunit;

namespace ContatosTestes
{
    public class ContatoServiceTeste
    {
        private readonly Mock<IContatoRepositorio> _mockContatoRepositorio;
        private readonly ContatoService _contatoService;

        public ContatoServiceTeste()
        {
            _mockContatoRepositorio = new Mock<IContatoRepositorio>();
            _contatoService = new ContatoService(_mockContatoRepositorio.Object);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarContato()
        {
            // Arrange
            int id = 1;
            var contato = new ContatoModel() { Id = id, Nome = "Seteve", Celular = "47984951269", Email = "steve.count.light@gmail.com" };
            _mockContatoRepositorio.Setup(x => x.ListarPorId(id)).ReturnsAsync(contato);

            // Act
            var resultado = await _contatoService.ObterPorId(id);

            // Assert
            Assert.Equal(contato, resultado);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeContatos()
        {
            // Arrange
            var contatos = new List<ContatoModel>
            {
                new ContatoModel { Id = 1, Nome = "Seteve", Celular = "47984951269", Email = "steve.count.light@gmail.com" },
                new ContatoModel { Id = 2, Nome = "Kathlen", Celular = "41988513200", Email = "kathlen.cbv@gmail.com" }
            };
            _mockContatoRepositorio.Setup(x => x.BuscarTodos()).ReturnsAsync(contatos);

            // Act
            var resultado = await _contatoService.ObterTodos();

            // Assert
            Assert.Equal(contatos, resultado);
        }

        [Fact]
        public async Task Adicionar_DeveAdicionarContato()
        {
            // Arrange
            var novoContato = new ContatoModel { Nome = "Abrao", Celular = "11987654321", Email = "abrao@teste.com" };
            var contatosExistentes = new List<ContatoModel>();
            _mockContatoRepositorio.Setup(x => x.BuscarTodos()).ReturnsAsync(contatosExistentes);
            _mockContatoRepositorio.Setup(x => x.Adicionar(novoContato)).ReturnsAsync(novoContato);

            // Act
            var resultado = await _contatoService.Adicionar(novoContato);

            // Assert
            Assert.Equal(novoContato, resultado);
        }

        [Fact]
        public async Task Adicionar_DeveLancarExceptionSeContatoExistir()
        {
            // Arrange
            var novoContato = new ContatoModel { Nome = "Abrao", Celular = "11987654321", Email = "abrao@teste.com" };
            var contatosExistentes = new List<ContatoModel> { novoContato };
            _mockContatoRepositorio.Setup(x => x.BuscarTodos()).ReturnsAsync(contatosExistentes);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _contatoService.Adicionar(novoContato));
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarContato()
        {
            // Arrange
            int id = 1;
            var contato = new ContatoModel() { Id = id, Nome = "Abrao", Celular = "11987654321", Email = "abrao@teste.com" };
            var contatoAtualizado = new ContatoModel() { Id = id, Nome = "Cilas", Celular = "11912345678", Email = "cilas@teste.com" };
            _mockContatoRepositorio.Setup(x => x.ListarPorId(id)).ReturnsAsync(contato);
            _mockContatoRepositorio.Setup(x => x.Atualizar(contatoAtualizado)).ReturnsAsync(contatoAtualizado);

            // Act
            var resultado = await _contatoService.Atualizar(contatoAtualizado);

            // Assert
            Assert.Equal(contatoAtualizado, resultado);
        }        
    }
}
