using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly ServicoDAO _servicoDAO;

        public ServicoController(ServicoDAO servicoDAO)
        {
            _servicoDAO = servicoDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var servicos = _servicoDAO.List();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var servico = _servicoDAO.GetById(id);
            if (servico == null)
            {
                return NotFound();
            }
            return Ok(servico);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ServicoDTO servicoDTO)
        {
            if (servicoDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var servico = new Servico
            {
                Nome = servicoDTO.Nome,
                Descricao = servicoDTO.Descricao,
                Preco = servicoDTO.Preco,
            };

            var novoId = _servicoDAO.Insert(servico);
            servico.Id = novoId;
            return CreatedAtAction(nameof(Get), new { id = servico.Id }, servico);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicoDTO servicoDTO)
        {
            if (servicoDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var servico = _servicoDAO.GetById(id);
            if (servico == null)
            {
                return NotFound();
            }

            servico.Nome = servicoDTO.Nome;
            servico.Descricao = servicoDTO.Descricao;
            servico.Preco = servicoDTO.Preco;

            _servicoDAO.Update(servico);
            return Ok(servico);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var servico = _servicoDAO.GetById(id);
            if (servico == null)
            {
                return NotFound();
            }

            _servicoDAO.Delete(id);
            return NoContent();
        }
    }
}