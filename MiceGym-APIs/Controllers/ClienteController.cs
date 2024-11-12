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
            var clienteDTOs = clientes.Select(c => new ClienteDOTS
            {
                Nome = c.Nome,
                CPF = c.CPF,
                RG = c.RG,
                DataNascimento = c.DataNascimento,
                Sexo = c.Sexo,
                Telefone = c.Telefone,
                Cidade = c.Cidade,
                Email = c.Email
            }).ToList();

            return Ok(clienteDTOs);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCPF(string cpf)
        {
            var cliente = _dao.GetByCPF(cpf);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            var clienteDTO = new ClienteDOTS
            {
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                RG = cliente.RG,
                DataNascimento = cliente.DataNascimento,
                Sexo = cliente.Sexo,
                Telefone = cliente.Telefone,
                Cidade = cliente.Cidade,
                Email = cliente.Email
            };

            return Ok(clienteDTO);
        }

        [HttpPost]
        public IActionResult Create(ClienteDOTS dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos. Verifique novamente!");

            if (_dao.GetByCPF(dto.CPF) != null)
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

            var cpfCriado = _dao.Insert(cliente);
            return CreatedAtAction(nameof(GetByCPF), new { cpf = cpfCriado }, dto);
        }

        [HttpPut("{cpf}")]
        public IActionResult Update(string cpf, ClienteDOTS dto)
        {
            var cliente = _dao.GetByCPF(cpf);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            cliente.Nome = dto.Nome ?? cliente.Nome;
            cliente.RG = dto.RG ?? cliente.RG;
            cliente.DataNascimento = dto.DataNascimento != default ? dto.DataNascimento : cliente.DataNascimento;
            cliente.Sexo = dto.Sexo ?? cliente.Sexo;
            cliente.Telefone = dto.Telefone ?? cliente.Telefone;
            cliente.Cidade = dto.Cidade ?? cliente.Cidade;
            cliente.Email = dto.Email ?? cliente.Email;

            _dao.Update(cliente);
            return Ok(dto);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            var cliente = _dao.GetByCPF(cpf);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            _dao.Delete(cpf);
            return Ok("Cliente excluído com sucesso.");
        }
    }
}
