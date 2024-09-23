using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : Controller
    {
        private const string ArquivoTxt = "funcionarios.txt";

        private List<Funcionario> LendoArquivo()
        {
            var funcionarios = new List<Funcionario>();

            if (System.IO.File.Exists(ArquivoTxt))
            {
                return funcionarios;
            }

            var linhas = System.IO.File.ReadAllLines(ArquivoTxt);
            foreach (var linha in linhas)
            {
                var dados = linha.Split('|');
                funcionarios.Add(new Funcionario
                {
                    Nome = dados[0],
                    CPF = dados[1],
                    CTPS = dados[2],
                    RG = dados[3],
                    Funcao = dados[4],
                    Setor = dados[5],
                    Sala = dados[6],
                    Telefone = dados[7],
                    Endereco = new Endereco
                    {
                        UF = dados[8],
                        Cidade = dados[9],
                        Bairro = dados[10],
                        Numero = dados[11],
                        CEP = dados[12]
                    }
                });
            }

            return funcionarios;
        }

        private void FuncionariosNoArquivo(List<Funcionario> funcionarios)
        {
            var linhas = funcionarios.Select(f => $"{f.Nome}|" +
                $"{f.CPF}|" +
                $"{f.CTPS}|" +
                $"{f.RG}|" +
                $"{f.Funcao}|" +
                $"{f.Setor}|" +
                $"{f.Sala}|" +
                $"{f.Telefone}|" +
                $"{f.Endereco.UF}|" +
                $"{f.Endereco.Cidade}|" +
                $"{f.Endereco.Bairro}|" +
                $"{f.Endereco.Numero}|" +
                $"{f.Endereco.CEP}");
            System.IO.File.WriteAllLines(ArquivoTxt, linhas);
        }
        private bool ValidarCPF(string cpf)
        {
            cpf = Regex.Replace(cpf, @"\D", "");
            if (cpf.Length != 11)
            {
                return false;
            }

            // Verificando se o CPF é inválido: (Funcionando!)
            if (new string(cpf[0], 11) == cpf)
            {
                return false;
            }

            int[] multiplicador1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * multiplicador1[i];
            }
            int resto = soma % 11;
            string digito1 = resto < 2 ? "0" : (11 - resto).ToString();

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            string digito2 = resto < 2 ? "0" : (11 - resto).ToString();

            return cpf.EndsWith(digito1 + digito2);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var funcionarios = LendoArquivo();
            return Ok(funcionarios);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCPF(string cpf)
        {
            if (ValidarCPF(cpf))
            {
                return BadRequest("CPF inválido.");
            }

            var funcionarios = LendoArquivo();
            var funcionario = funcionarios.FirstOrDefault(f => f.CPF == cpf);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FuncionarioDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados inválidos. Verifique novamente!");
            }

            if (ValidarCPF(dto.CPF))
            {
                return BadRequest("Desculpe, CPF inválido.");
            }

            var funcionarios = LendoArquivo();
            if (funcionarios.Any(f => f.CPF == dto.CPF))
            {
                return Conflict("Funcionário já cadastrado.");
            }

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

            funcionarios.Add(funcionario);
            FuncionariosNoArquivo(funcionarios);

            return CreatedAtAction(nameof(GetByCPF), new { cpf = funcionario.CPF }, funcionario);
        }

        [HttpPut("{cpf}")]
        public IActionResult Put(string cpf, [FromBody] FuncionarioDTO dto)
        {
            if (ValidarCPF(cpf))
            {
                return BadRequest("CPF inválido.");
            }

            var funcionarios = LendoArquivo();
            var funcionario = funcionarios.FirstOrDefault(f => f.CPF == cpf);

            if (funcionario == null)
            {
                return NotFound();
            }

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

            FuncionariosNoArquivo(funcionarios);

            return Ok(funcionario);
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            if (ValidarCPF(cpf))
            {
                return BadRequest("CPF é inválido.");
            }

            var funcionarios = LendoArquivo();
            var funcionario = funcionarios.FirstOrDefault(f => f.CPF == cpf);

            if (funcionario == null)
            {
                return NotFound();
            }

            funcionarios.Remove(funcionario);
            FuncionariosNoArquivo(funcionarios);

            return Ok(funcionario);
        }
    }
}
