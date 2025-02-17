using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


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
            var caixas = _servicoDAO.List();
            return Ok(caixas);
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
            var servicoAtualizado = new Servico
            {
                Id = servico.Id,
                Nome = servicoDTO.Nome,
                Descricao = servicoDTO.Descricao,
                Preco = servicoDTO.Preco,
            };

            _servicoDAO.Update(servicoAtualizado);
            return Ok(servicoAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var servico = _servicoDAO.GetById(id);
                if (servico == null)
                {
                    return NotFound();
                }

                _servicoDAO.Delete(id);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);

            }
            return NoContent();
        }
    }
}


