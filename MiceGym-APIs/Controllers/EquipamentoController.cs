using MiceGym_APIs.DAO;
using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {
        private readonly EquipamentoDAO _equipamentoDAO;

        public EquipamentoController(EquipamentoDAO equipamentoDAO)
        {
            _equipamentoDAO = equipamentoDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var equipamentos = _equipamentoDAO.List();
            if (equipamentos == null || !equipamentos.Any())
            {
                return NotFound("Nenhum equipamento encontrado.");
            }
            return Ok(equipamentos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var equipamento = _equipamentoDAO.GetById(id);
            if (equipamento == null)
            {
                return NotFound($"Equipamento com ID {id} não encontrado.");
            }
            return Ok(equipamento);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EquipamentoDTO equipamentoDTO)
        {
            if (equipamentoDTO == null || string.IsNullOrWhiteSpace(equipamentoDTO.Nome))
            {
                return BadRequest("Dados inválidos. O nome do equipamento é obrigatório.");
            }

            var equipamento = new Equipamento
            {
                Nome = equipamentoDTO.Nome.Trim(),
                Descricao = equipamentoDTO.Descricao?.Trim() ?? string.Empty,
                Codigo = equipamentoDTO.Codigo?.Trim() ?? string.Empty,
                Quantidade = equipamentoDTO.Quantidade > 0 ? equipamentoDTO.Quantidade : 0,
                Valor = equipamentoDTO.Valor > 0 ? equipamentoDTO.Valor : 0
            };

            try
            {
                var novoId = _equipamentoDAO.Insert(equipamento);
                return CreatedAtAction(nameof(GetById), new { id = novoId }, equipamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir equipamento: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EquipamentoDTO equipamentoDTO)
        {
            if (equipamentoDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var equipamentoExistente = _equipamentoDAO.GetById(id);
            if (equipamentoExistente == null)
            {
                return NotFound($"Equipamento com ID {id} não encontrado.");
            }

            equipamentoExistente.Nome = !string.IsNullOrWhiteSpace(equipamentoDTO.Nome) ? equipamentoDTO.Nome.Trim() : equipamentoExistente.Nome;
            equipamentoExistente.Descricao = equipamentoDTO.Descricao ?? equipamentoExistente.Descricao;
            equipamentoExistente.Codigo = equipamentoDTO.Codigo ?? equipamentoExistente.Codigo;
            equipamentoExistente.Quantidade = equipamentoDTO.Quantidade > 0 ? equipamentoDTO.Quantidade : equipamentoExistente.Quantidade;
            equipamentoExistente.Valor = equipamentoDTO.Valor > 0 ? equipamentoDTO.Valor : equipamentoExistente.Valor;

            try
            {
                _equipamentoDAO.Update(equipamentoExistente);
                return Ok(equipamentoExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar equipamento: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var equipamento = _equipamentoDAO.GetById(id);
            if (equipamento == null)
            {
                return NotFound($"Equipamento com ID {id} não encontrado.");
            }

            try
            {
                _equipamentoDAO.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar equipamento: {ex.Message}");
            }
        }
    }
}
