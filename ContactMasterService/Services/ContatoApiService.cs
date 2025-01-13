using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dominio.Models;
using ContactMaster.Services;
using ContactMasterService.Interfaces;

namespace ContactMasterService.Services
{   
    public class ContatoApiService : IContatoApiService
    {
        private readonly HttpClient _httpClient;

        public ContatoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<ContatoModel>> ObterTodosAsync()
        {
            var response = await _httpClient.GetAsync("/api/contatos");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ContatoModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        public async Task<ContatoModel> ObterPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/contatos/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ContatoModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        
        public async Task CriarAsync(ContatoModel contato)
        {
            var content = new StringContent(JsonSerializer.Serialize(contato), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/contatos", content);
            response.EnsureSuccessStatusCode();
        }
       
        public async Task AtualizarAsync(ContatoModel contato)
        {
            var content = new StringContent(JsonSerializer.Serialize(contato), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/contatos/{contato.Id}", content);
            response.EnsureSuccessStatusCode();
        }       

        public async Task<bool> DeletarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/contatos/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}

