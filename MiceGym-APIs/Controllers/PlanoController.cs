using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly PlanoDAO _planoDAO;

        public PlanoController()
        {
            _planoDAO = new PlanoDAO();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var planos = _planoDAO.List();
            return Ok(planos);
        }

        [HttpGet("{codPlano}")]
        public IActionResult GetById(int codPlano)
        {
            var plano = _planoDAO.GetById(codPlano);

            if (plano == null)
            {
                return NotFound();
            }

            return Ok(plano);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlanoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var plano = new Plano
            {
                NomePlano = dto.NomePlano,
                Preco = dto.Preco,
                Duracao = dto.Duracao
            };

            _planoDAO.Insert(plano);

            return CreatedAtAction(nameof(GetById), new { codPlano = plano.CodPlano }, plano);
        }

        [HttpPut("{codPlano}")]
        public IActionResult Put(int codPlano, [FromBody] PlanoDTO dto)
        {
            var planoExistente = _planoDAO.GetById(codPlano);

            if (planoExistente == null)
            {
                return NotFound();
            }

            planoExistente.NomePlano = dto.NomePlano ?? planoExistente.NomePlano;
            planoExistente.Preco = dto.Preco > 0 ? dto.Preco : planoExistente.Preco;
            planoExistente.Duracao = dto.Duracao > 0 ? dto.Duracao : planoExistente.Duracao;

            _planoDAO.Update(planoExistente);

            return Ok(planoExistente);
        }

        [HttpDelete("{codPlano}")]
        public IActionResult Delete(int codPlano)
        {
            var plano = _planoDAO.GetById(codPlano);

            if (plano == null)
            {
                return NotFound();
            }

            _planoDAO.Delete(codPlano);

            return NoContent();
        }
    }
}
