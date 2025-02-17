using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;

namespace MiceGym_APIs.Controllers
{
    /*
 public ServicoController(ServicoDAO servicoDAO)
 {
     _servicoDAO = servicoDAO;
 }
 */
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly ServicoDAO _servicoDAO;


        public ServicoController()
        {
            _servicoDAO = new ServicoDAO();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var servicos = _servicoDAO.List();
            return Ok(servicos);
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

            _servicoDAO.Insert(servico);

            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var servico = _servicoDAO.GetById(id);

            if (servico == null)
            {
                return NotFound();
            }
            return Ok(servico);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicoDTO servicoDTO)
        {
            var servicoExistente = _servicoDAO.GetById(id);

            if (servicoExistente == null)
            {
                return NotFound();
            }
            /*
            var servico = _servicoDAO.GetById(id);
            if (servico == null)
            {
                return NotFound();
            }
            */

            servicoExistente.Nome = servicoDTO.Nome;
            servicoExistente.Descricao = servicoDTO.Descricao;
            servicoExistente.Preco = servicoDTO.Preco;

            _servicoDAO.Update(servicoExistente);
            return Ok(servicoExistente);
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