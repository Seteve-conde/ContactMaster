using ContactMaster.Services;
using ContactMasterService;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [FiltroParaUsuariosLogadosApi] 
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }
       
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            List<ContatoModel> contatos = await _contatoService.ObterTodos();
            return Ok(contatos); 
        }
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            ContatoModel contato = await _contatoService.ObterPorId(id);

            if (contato == null)
                return NotFound(new { Mensagem = "Contato não encontrado." }); 

            return Ok(contato); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ContatoModel contato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            try
            {
                await _contatoService.Adicionar(contato);
                return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato); 
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Mensagem = $"Erro ao criar contato: {ex.Message}" }); 
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody] ContatoModel contato)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _contatoService.Atualizar(contato);
                return Ok(new { Mensagem = "Contato alterado com sucesso!" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Mensagem = $"Erro ao editar contato: {ex.Message}" });
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _contatoService.Apagar(id);

                if (!apagado)
                    return BadRequest(new { Mensagem = "Não foi possível apagar o contato." });

                return Ok(new { Mensagem = "Contato apagado com sucesso!" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Mensagem = $"Erro ao apagar contato: {ex.Message}" });
            }
        }
    }
}
