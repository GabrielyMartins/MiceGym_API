using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {
        private readonly EquipamentoDAO _equipamentoDAO;

        public EquipamentoController()
        {
            _equipamentoDAO = new EquipamentoDAO();
        }

       
        [HttpGet]
        public IActionResult Get()
        {
            var equipamentos = _equipamentoDAO.List();
            return Ok(equipamentos);
        }

        
        [HttpGet("{codigo}")]
        public IActionResult GetByCodigo(string codigo)
        {
            var equipamento = _equipamentoDAO.GetByCodigo(codigo);

            if (equipamento == null)
            {
                return NotFound();
            }

            return Ok(equipamento);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] EquipamentoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var equipamento = new Equipamento
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Codigo = dto.Codigo,
                Quantidade = dto.Quantidade,
                Valor = dto.Valor,
                Fornecedor = dto.Fornecedor
            };

            _equipamentoDAO.Insert(equipamento);

            return CreatedAtAction(nameof(GetByCodigo), new { codigo = equipamento.Codigo }, equipamento);
        }

       
        [HttpPut("{codigo}")]
        public IActionResult Put(string codigo, [FromBody] EquipamentoDTO dto)
        {
            var equipamentoExistente = _equipamentoDAO.GetByCodigo(codigo);

            if (equipamentoExistente == null)
            {
                return NotFound();
            }

            var equipamentoAtualizado = new Equipamento
            {
                Nome = dto.Nome ?? equipamentoExistente.Nome,
                Descricao = dto.Descricao ?? equipamentoExistente.Descricao,
                Codigo = codigo,  
                Quantidade = dto.Quantidade ?? equipamentoExistente.Quantidade,
                Valor = dto.Valor ?? equipamentoExistente.Valor,
                Fornecedor = dto.Fornecedor ?? equipamentoExistente.Fornecedor
            };

            _equipamentoDAO.Update(equipamentoAtualizado);

            return Ok(equipamentoAtualizado);
        }

        
        [HttpDelete("{codigo}")]
        public IActionResult Delete(string codigo)
        {
            var equipamento = _equipamentoDAO.GetByCodigo(codigo);

            if (equipamento == null)
            {
                return NotFound();
            }

            _equipamentoDAO.Delete(codigo);

            return Ok(equipamento);
        }
    }
}
