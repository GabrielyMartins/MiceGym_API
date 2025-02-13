using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;

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

            return CreatedAtAction(nameof(GetById), new { id = plano.Id }, plano);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var plano = _planoDAO.GetById(id);

            if (plano == null)
            {
                return NotFound();
            }

            return Ok(plano);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlanoDTO dto)
        {
            var planoExistente = _planoDAO.GetById(id);

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var plano = _planoDAO.GetById(id);

            if (plano == null)
            {
                return NotFound();
            }

            _planoDAO.Delete(id);

            return NoContent();
        }
    }
}
