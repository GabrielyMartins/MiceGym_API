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

        
        [HttpPost]
        public IActionResult Post([FromBody] FornecedorDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var fornecedores = new Fornecedor
            {
                NomeFantasia = dto.NomeFantasia,
                RazaoSocial = dto.RazaoSocial,
                CNPJ = dto.CNPJ,
                Endereco = dto.Endereco,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Responsavel = dto.Responsavel
            };


            _fornecedorDAO.Insert(fornecedores);

            return CreatedAtAction(nameof(GetById), new { id = fornecedores.Id }, fornecedores);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var fornecedor = _fornecedorDAO.GetById(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return Ok(fornecedor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FornecedorDTO dto)
        {
            var fornecedorExistente = _fornecedorDAO.GetById(id);

            if (fornecedorExistente == null)
            {
                return NotFound();
            }

            fornecedorExistente.NomeFantasia = dto.NomeFantasia ?? fornecedorExistente.NomeFantasia;
            fornecedorExistente.RazaoSocial = dto.RazaoSocial  ?? fornecedorExistente.RazaoSocial;
            fornecedorExistente.CNPJ = dto.CNPJ ?? fornecedorExistente.CNPJ;
            fornecedorExistente.Endereco = dto.Endereco ?? fornecedorExistente.Endereco;
            fornecedorExistente.Cidade = dto.Cidade ?? fornecedorExistente.Cidade;
            fornecedorExistente.Estado = dto.Estado ?? fornecedorExistente.Estado;
            fornecedorExistente.Telefone = dto.Telefone ?? fornecedorExistente.Telefone;
            fornecedorExistente.Email = dto.Email ?? fornecedorExistente.Email;
            fornecedorExistente.Responsavel = dto.Responsavel ?? fornecedorExistente.Responsavel;







            _fornecedorDAO.Update(fornecedorExistente);

            return Ok(fornecedorExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var fornecedor = _fornecedorDAO.GetById(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            _fornecedorDAO.Delete(id);

            return NoContent();
        }

    }
}
