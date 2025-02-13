using Microsoft.AspNetCore.Mvc;
using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using MiceGym_APIs.DAO;
using System.Linq;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteDAO _dao;

        public ClienteController()
        {
            _dao = new ClienteDAO();
        }

        [HttpGet]
        public IActionResult List()
        {
            var clientes = _dao.List();
            var clienteDTOs = clientes.Select(c => new ClienteDTO
            {
                Nome = c.Nome,
                CPF = c.CPF,
                RG = c.RG,
                DataNascimento = c.DataNascimento,
                Sexo = c.Sexo,
                Telefone = c.Telefone,
                Cidade = c.Cidade,
                Email = c.Email,
                UF = c.UF,
                Bairro = c.Bairro,
                Numero = c.Numero,
                CEP = c.CEP
            }).ToList();

            return Ok(clienteDTOs);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var cliente = _dao.GetById(Id);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            var clienteDTO = new ClienteDTO
            {
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                RG = cliente.RG,
                DataNascimento = cliente.DataNascimento,
                Sexo = cliente.Sexo,
                Telefone = cliente.Telefone,
                Cidade = cliente.Cidade,
                Email = cliente.Email,
                UF = cliente.UF,
                Bairro = cliente.Bairro,
                Numero = cliente.Numero,
                CEP = cliente.CEP
            };

            return Ok(clienteDTO);
        }

        [HttpPost]
        public IActionResult Post(ClienteDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos. Verifique novamente!");

            if (_dao.GetById(dto.Id) != null)
                return Conflict("Cliente já cadastrado.");

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                RG = dto.RG,
                DataNascimento = dto.DataNascimento,
                Sexo = dto.Sexo,
                Telefone = dto.Telefone,
                UF = dto.UF,
                Cidade = dto.Cidade,
                Bairro = dto.Bairro,
                Numero = dto.Numero,
                CEP = dto.CEP,
                Email = dto.Email
            };

            var IdCriado = _dao.Insert(cliente);
            return CreatedAtAction(nameof(GetById), new { id = IdCriado }, dto);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, ClienteDTO dto)
        {
            var cliente = _dao.GetById(Id);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            cliente.Nome = dto.Nome ?? cliente.Nome;
            cliente.RG = dto.RG ?? cliente.RG;
            cliente.DataNascimento = dto.DataNascimento != default ? dto.DataNascimento : cliente.DataNascimento;
            cliente.Sexo = dto.Sexo ?? cliente.Sexo;
            cliente.Telefone = dto.Telefone ?? cliente.Telefone;
            cliente.Cidade = dto.Cidade ?? cliente.Cidade;
            cliente.Email = dto.Email ?? cliente.Email;
            cliente.UF = dto.UF ?? cliente.UF;
            cliente.Bairro = dto.Bairro ?? cliente.Bairro;
            cliente.Numero = dto.Numero ?? cliente.Numero;
            cliente.CEP = dto.CEP ?? cliente.CEP;

            bool updated = _dao.Update(cliente);
            if (!updated)
                return BadRequest("Falha ao atualizar cliente.");

            return Ok("Cliente atualizado com sucesso.");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (!_dao.Delete(Id))
                return NotFound("Cliente não encontrado.");

            return Ok("Cliente excluído com sucesso.");
        }
    }
}
