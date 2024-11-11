using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinoController : ControllerBase
    {
        private readonly TreinoDAO _treinoDAO;

        public TreinoController()
        {
            _treinoDAO = new TreinoDAO();
        }

        // Método para listar todos os treinos
        [HttpGet("Listar")]
        public IActionResult Get()
        {
            List<Treino> treinos = _treinoDAO.List();
            return Ok(treinos);
        }

        // Método para buscar um treino pelo ID
        [HttpGet("Procurar/{id}")]
        public IActionResult GetById(int id)
        {
            var treinoItem = _treinoDAO.GetById(id);

            if (treinoItem == null)
            {
                return NotFound();
            }

            return Ok(treinoItem);
        }

        // Método para adicionar um novo treino
        [HttpPost("Adicionar")]
        public IActionResult Post([FromBody] Treino item)
        {
            if (item == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var id = _treinoDAO.Insert(item);
            return StatusCode(StatusCodes.Status201Created, new { id });
        }

        // Método para atualizar um treino existente pelo ID
        [HttpPut("Atualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Treino item)
        {
            var treinoItem = _treinoDAO.GetById(id);

            if (treinoItem == null)
            {
                return NotFound();
            }

            item.Id = id;  // Garantir que o ID não seja alterado
            _treinoDAO.Update(item);

            return Ok(item);
        }

        // Método para deletar um treino pelo ID
        [HttpDelete("Deletar/{id}")]
        public IActionResult Delete(int id)
        {
            var treinoItem = _treinoDAO.GetById(id);

            if (treinoItem == null)
            {
                return NotFound();
            }

            _treinoDAO.Delete(id);
            return Ok(treinoItem);
        }
    }
}
