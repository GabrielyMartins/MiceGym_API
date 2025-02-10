using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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

        [HttpGet]
        public IActionResult Get()
        {
            var fornecedores = _fornecedorDAO.List();
            return Ok(fornecedores);
        }

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

            var fornecedor = new Fornecedor
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

            fornecedorExistente.NomeFantasia = dto.NomeFantasia ?? fornecedorExistente.NomeFantasia;
            fornecedorExistente.RazaoSocial = dto.RazaoSocial ?? fornecedorExistente.RazaoSocial;
            fornecedorExistente.Endereco = dto.Endereco ?? fornecedorExistente.Endereco;
            fornecedorExistente.Cidade = dto.Cidade ?? fornecedorExistente.Cidade;
            fornecedorExistente.Estado = dto.Estado ?? fornecedorExistente.Estado;
            fornecedorExistente.Telefone = dto.Telefone ?? fornecedorExistente.Telefone;
            fornecedorExistente.Email = dto.Email ?? fornecedorExistente.Email;
            fornecedorExistente.Responsavel = dto.Responsavel ?? fornecedorExistente.Responsavel;

            _fornecedorDAO.Update(fornecedorExistente);

            return Ok(fornecedorExistente);
        }

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

        private bool ValidarCNPJ(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"[^\d]", ""); 

            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj, digito;
            int soma, resto;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
