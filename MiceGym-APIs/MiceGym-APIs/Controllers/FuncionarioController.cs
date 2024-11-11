using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioDAO _dao;

        public FuncionarioController()
        {
            _dao = new FuncionarioDAO();
        }

        [HttpGet]
        public IActionResult List()
        {
            var funcionarios = _dao.List();
            var funcionarioDTOs = funcionarios.Select(f => new FuncionarioDTO
            {
                Nome = f.Nome,
                CPF = f.CPF,
                CTPS = f.CTPS,
                RG = f.RG,
                Funcao = f.Funcao,
                Setor = f.Setor,
                Sala = f.Sala,
                Telefone = f.Telefone,
                Endereco = new EnderecoDTO
                {
                    UF = f.Endereco.UF,
                    Cidade = f.Endereco.Cidade,
                    Bairro = f.Endereco.Bairro,
                    Numero = f.Endereco.Numero,
                    CEP = f.Endereco.CEP
                }
            }).ToList();

            return Ok(funcionarioDTOs);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCPF(string cpf)
        {
            var funcionario = _dao.GetByCPF(cpf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            var funcionarioDTO = new FuncionarioDTO
            {
                Nome = funcionario.Nome,
                CPF = funcionario.CPF,
                CTPS = funcionario.CTPS,
                RG = funcionario.RG,
                Funcao = funcionario.Funcao,
                Setor = funcionario.Setor,
                Sala = funcionario.Sala,
                Telefone = funcionario.Telefone,
                Endereco = new EnderecoDTO
                {
                    UF = funcionario.Endereco.UF,
                    Cidade = funcionario.Endereco.Cidade,
                    Bairro = funcionario.Endereco.Bairro,
                    Numero = funcionario.Endereco.Numero,
                    CEP = funcionario.Endereco.CEP
                }
            };

            return Ok(funcionarioDTO);
        }

        [HttpPost]
        public IActionResult Create(FuncionarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos. Verifique novamente!");

            if (_dao.GetByCPF(dto.CPF) != null)
                return Conflict("Funcionário já cadastrado.");

            var funcionario = new Funcionario
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                CTPS = dto.CTPS,
                RG = dto.RG,
                Funcao = dto.Funcao,
                Setor = dto.Setor,
                Sala = dto.Sala,
                Telefone = dto.Telefone,
                Endereco = new Endereco
                {
                    UF = dto.Endereco.UF,
                    Cidade = dto.Endereco.Cidade,
                    Bairro = dto.Endereco.Bairro,
                    Numero = dto.Endereco.Numero,
                    CEP = dto.Endereco.CEP
                }
            };

            var cpfCriado = _dao.Insert(funcionario);
            return CreatedAtAction(nameof(GetByCPF), new { cpf = cpfCriado }, dto);
        }

        [HttpPut("{cpf}")]
        public IActionResult Update(string cpf, FuncionarioDTO dto)
        {
            var funcionario = _dao.GetByCPF(cpf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            funcionario.Nome = dto.Nome ?? funcionario.Nome;
            funcionario.CTPS = dto.CTPS ?? funcionario.CTPS;
            funcionario.RG = dto.RG ?? funcionario.RG;
            funcionario.Funcao = dto.Funcao ?? funcionario.Funcao;
            funcionario.Setor = dto.Setor ?? funcionario.Setor;
            funcionario.Sala = dto.Sala ?? funcionario.Sala;
            funcionario.Telefone = dto.Telefone ?? funcionario.Telefone;
            funcionario.Endereco.UF = dto.Endereco.UF ?? funcionario.Endereco.UF;
            funcionario.Endereco.Cidade = dto.Endereco.Cidade ?? funcionario.Endereco.Cidade;
            funcionario.Endereco.Bairro = dto.Endereco.Bairro ?? funcionario.Endereco.Bairro;
            funcionario.Endereco.Numero = dto.Endereco.Numero ?? funcionario.Endereco.Numero;
            funcionario.Endereco.CEP = dto.Endereco.CEP ?? funcionario.Endereco.CEP;

            _dao.Update(funcionario);

            return Ok(dto);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            var funcionario = _dao.GetByCPF(cpf);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            _dao.Delete(cpf);
            return Ok("Funcionário excluído com sucesso.");
        }
    }
}
