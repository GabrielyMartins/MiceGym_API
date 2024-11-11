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
        private readonly CaixaDAO _caixaDAO = new CaixaDAO();

        [HttpGet]
        public IActionResult Get()
        {
            var caixas = _caixaDAO.Listar();
            return Ok(caixas);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var caixa = _caixaDAO.Procurar(id);
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

            var novoCaixa = _caixaDAO.Adicionar(caixa);
            return CreatedAtAction(nameof(GetById), new { id = novoCaixa.Id }, novoCaixa);
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CaixaDTO caixaDTO)
        {
            if (caixaDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var caixaAtualizado = new Caixa
            {
                SaldoInicial = caixaDTO.SaldoInicial,
                DataAbertura = caixaDTO.DataAbertura,
                DataFechamento = caixaDTO.DataFechamento,
                SaldoFinal = caixaDTO.SaldoFinal
            };

            var caixa = _caixaDAO.Atualizar(id, caixaAtualizado);
            if (caixa == null)
            {
                return NotFound();
            }
            return Ok(caixa);
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sucesso = _caixaDAO.Deletar(id);
            if (!sucesso)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
