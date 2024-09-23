using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Models;
using System.Collections.Generic;
using System.Linq;

namespace PDS.Controllers
{
    [Route("Produtos/")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        private static List<Produto> produtos = new List<Produto>();

        
        [HttpGet("Listar")]
        public IActionResult Get()
        {
            return Ok(produtos);
        }

        [HttpGet("Procurar id")]
        public IActionResult GetById(int id)
        {
            var produto = produtos.FirstOrDefault(item => item.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost("Adicionar")]
        public IActionResult Post([FromBody] ProdutoDTO item)
        {
            var novoProduto = new Produto
            {
                Id = produtos.Count + 1, 
                Nome = item.Nome,
                Descricao = item.Descricao,
                Codigo = item.Codigo,
                PrecoCompra = item.PrecoCompra,
                PrecoVenda = item.PrecoVenda,
                Quantidade = item.Quantidade,
                Fornecedor = item.Fornecedor,
            };

            produtos.Add(novoProduto);

            return StatusCode(StatusCodes.Status201Created, novoProduto);
        }

        [HttpPut("Atualizar id")]
        public IActionResult Put(int id, [FromBody] ProdutoDTO item)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            
            produto.Nome = item.Nome;
            produto.Descricao = item.Descricao;
            produto.Codigo = item.Codigo;
            produto.PrecoCompra = item.PrecoCompra;
            produto.PrecoVenda = item.PrecoVenda;
            produto.Quantidade = item.Quantidade;
            produto.Fornecedor = item.Fornecedor;

            return Ok(produto);
        }

        [HttpDelete("Deletar id")]
        public IActionResult Delete(int id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            produtos.Remove(produto);

            return Ok(produto);
        }
    }
}
