using Microsoft.AspNetCore.Mvc;
using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        private readonly CaixaDAO _caixaDAO;

        public CaixaController(CaixaDAO caixaDAO)
        {
            _caixaDAO = caixaDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var caixas = _caixaDAO.List();
            return Ok(caixas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var caixa = _caixaDAO.GetById(id);
            if (caixa == null)
            {
                return NotFound();
            }
            return Ok(caixa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CaixaDTO caixaDTO)
        {
            if (caixaDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var caixa = new Caixa
            {
                SaldoInicial = caixaDTO.SaldoInicial,
                DataAbertura = caixaDTO.DataAbertura,
                DataFechamento = caixaDTO.DataFechamento,
                SaldoFinal = caixaDTO.SaldoFinal
            };

            var novoId = _caixaDAO.Insert(caixa);
            return CreatedAtAction(nameof(GetById), new { id = novoId }, caixa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CaixaDTO caixaDTO)
        {
            if (caixaDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var caixa = _caixaDAO.GetById(id);
            if (caixa == null)
            {
                return NotFound();
            }

            var caixaAtualizado = new Caixa
            {
                Id = id,
                SaldoInicial = caixaDTO.SaldoInicial,
                DataAbertura = caixaDTO.DataAbertura,
                DataFechamento = caixaDTO.DataFechamento,
                SaldoFinal = caixaDTO.SaldoFinal
            };

            _caixaDAO.Update(caixaAtualizado);
            return Ok(caixaAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var caixa = _caixaDAO.GetById(id);
                if (caixa == null)
                {
                    return NotFound();
                }

                _caixaDAO.Delete(id);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


            return NoContent();

        }
    }
}