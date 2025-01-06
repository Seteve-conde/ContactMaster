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
    public class BonusController : ControllerBase
    {
        private readonly IBonusService _bonusService;

        public BonusController(IBonusService bonusService)
        {
            _bonusService = bonusService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            List<BonusModel> bonus = await _bonusService.ObterTodos();
            return Ok(bonus);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            BonusModel bonus = await _bonusService.ObterPorId(id);

            if (bonus == null)
                return NotFound(new { Mensagem = "Bônus não encontrado." });

            return Ok(bonus);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] BonusModel bonus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _bonusService.Adicionar(bonus);
                return CreatedAtAction(nameof(ObterPorId), new { id = bonus.Id }, bonus);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Mensagem = $"Erro ao criar bônus: {ex.Message}" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody] BonusModel bonus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _bonusService.Atualizar(bonus);
                return Ok(new { Mensagem = "Bônus alterado com sucesso." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Mensagem = $"Erro ao alterar bônus: {ex.Message}" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _bonusService.Apagar(id);

                if (!apagado)
                    return BadRequest(new { Mensagem = "Não foi possível apagar o bônus." });

                return Ok(new { Mensagem = "Bônus apagado com sucesso." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Mensagem = $"Erro ao apagar bônus: {ex.Message}" });
            }
        }
    }
}
