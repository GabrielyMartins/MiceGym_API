using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly VendaDAO _vendaDAO;

        public VendaController()
        {
            _vendaDAO = new VendaDAO();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var vendas = _vendaDAO.List();
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var venda = _vendaDAO.GetById(id);

            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        [HttpPost]
        public IActionResult Post([FromBody] VendaDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var venda = new Venda
            {
                Data = dto.Data,
                Valor = dto.Valor
            };

            _vendaDAO.Insert(venda);

            return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VendaDTO dto)
        {
            var vendaExistente = _vendaDAO.GetById(id);

            if (vendaExistente == null)
            {
                return NotFound();
            }

            vendaExistente.Data = dto.Data != default(DateTime) ? dto.Data : vendaExistente.Data;
            vendaExistente.Valor = dto.Valor > 0 ? dto.Valor : vendaExistente.Valor;

            _vendaDAO.Update(vendaExistente);

            return Ok(vendaExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var venda = _vendaDAO.GetById(id);

            if (venda == null)
            {
                return NotFound();
            }

            _vendaDAO.Delete(id);

            return NoContent();
        }
    }
}
