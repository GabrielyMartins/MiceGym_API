using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaDAO _despesaDAO;

        public DespesaController()
        {
            _despesaDAO = new DespesaDAO();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var despesas = _despesaDAO.List();
            return Ok(despesas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var despesa = _despesaDAO.GetById(id);

            if (despesa == null)
            {
                return NotFound();
            }

            return Ok(despesa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DespesaDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var despesa = new Despesa
            {
                Valor = dto.Valor,
                Data = dto.Data,
                Descricao = dto.Descricao
            };

            _despesaDAO.Insert(despesa);

            return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, despesa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DespesaDTO dto)
        {
            var despesaExistente = _despesaDAO.GetById(id);

            if (despesaExistente == null)
            {
                return NotFound();
            }

            despesaExistente.Valor = dto.Valor > 0 ? dto.Valor : despesaExistente.Valor;
            despesaExistente.Data = dto.Data != default(DateTime) ? dto.Data : despesaExistente.Data;
            despesaExistente.Descricao = dto.Descricao ?? despesaExistente.Descricao;

            _despesaDAO.Update(despesaExistente);

            return Ok(despesaExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var despesa = _despesaDAO.GetById(id);

            if (despesa == null)
            {
                return NotFound();
            }

            _despesaDAO.Delete(id);

            return NoContent();
        }
    }
}
