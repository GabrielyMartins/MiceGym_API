using MiceGym_APIs.DAO;
using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class funcionarioController : ControllerBase
    {
        private readonly FuncionarioDAO _funcionarioDao;

        public funcionarioController()
        {
            _funcionarioDao = new FuncionarioDAO();
        }

        [HttpGet]
        public IActionResult List()
        {
            var funcionarios = _funcionarioDao.List();
            return Ok(funcionarios);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var funcionario = _funcionarioDao.GetById(Id);
            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado.");
            }
            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Create(FuncionarioDTO funcionarioDTO)
        {
            if (funcionarioDTO == null)
            {
                return BadRequest("Dados inválidos. Verifique novamente!");
            }
                

            var funcionario = new Funcionario
            {
                Nome = funcionarioDTO.Nome,
                CPF = funcionarioDTO.CPF,
                CTPS = funcionarioDTO.CTPS,
                RG = funcionarioDTO.RG,
                Funcao = funcionarioDTO.Funcao,
                Setor = funcionarioDTO.Setor,
                Sala = funcionarioDTO.Sala,
                Telefone = funcionarioDTO.Telefone,
            };

            var IdCriado = _funcionarioDao.Insert(funcionario);
            return CreatedAtAction(nameof(GetById), new { Id = IdCriado }, funcionarioDTO);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, FuncionarioDTO funcionarioDTO)
        {
            
            if (funcionarioDTO == null)
            {
                return BadRequest("Funcionario não encontrado");
            }

            var funcionario = _funcionarioDao.GetById(Id);
            if(funcionario == null)
            {
                return NotFound();
            }

            
            var funcionarioAtualizado = new Funcionario
            {
                Id = Id,
                Nome = funcionarioDTO.Nome,
                CTPS = funcionarioDTO.CTPS,
                RG = funcionarioDTO.RG,
                Funcao = funcionarioDTO.Funcao,
                Setor = funcionarioDTO.Setor,
                Sala = funcionarioDTO.Sala,
                Telefone = funcionarioDTO.Telefone,
            };

            _funcionarioDao.Update(funcionario);
            return Ok(funcionarioAtualizado);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var caixa = _funcionarioDao.GetById(Id);
                if (caixa == null)
                {
                    return NotFound();
                }

                _funcionarioDao.Delete(Id);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


            return NoContent();
        }
    }
}
