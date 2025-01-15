using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dominio.Models;
using ContactMasterService.Interfaces;
using ContactMaster.Services;
using System;

namespace ContactMasterService.Services
{
    public class ContatoApiService : IContatoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessaoService _sessaoService;

        public ContatoApiService(HttpClient httpClient, ISessaoService sessaoService)
        {
            _httpClient = httpClient;
            _sessaoService = sessaoService;
        }

        private void ConfigurarCabecalhosRequisicao()
        {
            UsuarioModel usuarioLogado = _sessaoService.BuscarSessaoUsuario();
            if (usuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }

            // Remove o cabeçalho, caso já exista
            if (_httpClient.DefaultRequestHeaders.Contains("userId"))
            {
                _httpClient.DefaultRequestHeaders.Remove("userId");
            }

            // Adiciona o ID do usuário ao cabeçalho
            _httpClient.DefaultRequestHeaders.Add("userId", usuarioLogado.Id.ToString());
        }

        public async Task<List<ContatoModel>> ObterTodosAsync()
        {
            ConfigurarCabecalhosRequisicao();

            var response = await _httpClient.GetAsync("/api/contatoApi");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ContatoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<ContatoModel> ObterPorIdAsync(int id)
        {
            ConfigurarCabecalhosRequisicao();

            var response = await _httpClient.GetAsync($"/api/contatoApi/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ContatoModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task CriarAsync(ContatoModel contato)
        {
            ConfigurarCabecalhosRequisicao();

            UsuarioModel usuarioLogado = _sessaoService.BuscarSessaoUsuario();
            if (usuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }

            contato.UsuarioId = usuarioLogado.Id;

            var content = new StringContent(JsonSerializer.Serialize(contato), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/contatoApi", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task AtualizarAsync(ContatoModel contato)
        {
            ConfigurarCabecalhosRequisicao();

            UsuarioModel usuarioLogado = _sessaoService.BuscarSessaoUsuario();
            if (usuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }

            contato.UsuarioId = usuarioLogado.Id;

            var content = new StringContent(JsonSerializer.Serialize(contato), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/contatoApi/{contato.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeletarAsync(int id)
        {
            ConfigurarCabecalhosRequisicao();

            var response = await _httpClient.DeleteAsync($"/api/contatoApi/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
