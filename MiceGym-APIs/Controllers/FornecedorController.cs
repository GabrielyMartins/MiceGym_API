using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly FornecedorDAO _fornecedorDAO;

        public FornecedorController()
        {
            _fornecedorDAO = new FornecedorDAO();
        }

        // Método para listar todos os fornecedores
        [HttpGet]
        public IActionResult Get()
        {
            var fornecedores = _fornecedorDAO.List();
            return Ok(fornecedores);
        }

        // Método para buscar fornecedor pelo CNPJ
        [HttpGet("{cnpj}")]
        public IActionResult GetByCNPJ(string cnpj)
        {
            if (!ValidarCNPJ(cnpj))
            {
                return BadRequest("CNPJ inválido.");
            }

            var fornecedor = _fornecedorDAO.GetByCNPJ(cnpj);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        // Método para adicionar um novo fornecedor
        [HttpPost]
        public IActionResult Post([FromBody] FornecedorDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var cnpj = dto.CNPJ;
            if (!ValidarCNPJ(cnpj))
            {
                return BadRequest("CNPJ inválido.");
            }

            var fornecedores = _fornecedorDAO.List();
            if (fornecedores.Any(f => f.CNPJ == cnpj))
            {
                return Conflict("Fornecedor existente.");
            }

            var fornecedor = new Fornecedores
            {
                NomeFantasia = dto.NomeFantasia,
                RazaoSocial = dto.RazaoSocial,
                CNPJ = cnpj,
                Endereco = dto.Endereco,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Responsavel = dto.Responsavel
            };

            _fornecedorDAO.Insert(fornecedor);

            return CreatedAtAction(nameof(GetByCNPJ), new { cnpj = fornecedor.CNPJ }, fornecedor);
        }

        // Método para atualizar um fornecedor existente pelo CNPJ
        [HttpPut("{cnpj}")]
        public IActionResult Put(string cnpj, [FromBody] FornecedorDTO dto)
        {
            if (!ValidarCNPJ(cnpj))
            {
                return BadRequest("CNPJ inválido.");
            }

            var fornecedorExistente = _fornecedorDAO.GetByCNPJ(cnpj);

            if (fornecedorExistente == null)
            {
                return NotFound();
            }

            var fornecedorAtualizado = new Fornecedores
            {
                NomeFantasia = dto.NomeFantasia ?? fornecedorExistente.NomeFantasia,
                RazaoSocial = dto.RazaoSocial ?? fornecedorExistente.RazaoSocial,
                CNPJ = cnpj,  // O CNPJ não pode ser alterado
                Endereco = dto.Endereco ?? fornecedorExistente.Endereco,
                Cidade = dto.Cidade ?? fornecedorExistente.Cidade,
                Estado = dto.Estado ?? fornecedorExistente.Estado,
                Telefone = dto.Telefone ?? fornecedorExistente.Telefone,
                Email = dto.Email ?? fornecedorExistente.Email,
                Responsavel = dto.Responsavel ?? fornecedorExistente.Responsavel
            };

            _fornecedorDAO.Update(fornecedorAtualizado);

            return Ok(fornecedorAtualizado);
        }

        // Método para deletar um fornecedor pelo CNPJ
        [HttpDelete("{cnpj}")]
        public IActionResult Delete(string cnpj)
        {
            if (!ValidarCNPJ(cnpj))
            {
                return BadRequest("CNPJ inválido.");
            }

            var fornecedor = _fornecedorDAO.GetByCNPJ(cnpj);

            if (fornecedor == null)
            {
                return NotFound();
            }

            _fornecedorDAO.Delete(cnpj);

            return Ok(fornecedor);
        }

        // Método para validar o CNPJ
        private bool ValidarCNPJ(string cnpj)
        {
            cnpj = System.Text.RegularExpressions.Regex.Replace(cnpj, @"\D", "");
            return cnpj.Length == 14;
        }
    }
}
