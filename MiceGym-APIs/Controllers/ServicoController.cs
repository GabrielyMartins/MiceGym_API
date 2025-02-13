using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;


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
            List<Servico> servicos = _servicoDAO.ListarServicos();
            return Ok(servicos);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Servico servico = _servicoDAO.BuscarPorId(id);
            if (servico == null)
            {
                return NotFound();
            }

            return Ok(servico);
        }


        [HttpPost]
        public IActionResult Post([FromBody] ServicoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var servico = new Servico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            servico = _servicoDAO.AdicionarServico(servico);

            return CreatedAtAction(nameof(Get), new { id = servico.Id }, servico);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var servico = new Servico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            bool sucesso = _servicoDAO.AtualizarServico(id, servico);
            if (!sucesso)
            {
                return NotFound();
            }

            return Ok(servico);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool sucesso = _servicoDAO.DeletarServico(id);
            if (!sucesso)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
