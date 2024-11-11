using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace MiceGym_APIs.Controllers
{
    [Route("produtos")]
    [ApiController]
    public class ProdutoController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var listaProdutos = new ProdutoDAO().List();
            return Ok(listaProdutos);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProdutoDTO item)
        {
            var produto = new Produto
            {
                Nome = item.Nome,
                Descricao = item.Descricao,
                Codigo = item.Codigo,
                PrecoCompra = item.PrecoCompra,
                PrecoVenda = item.PrecoVenda,
                Quantidade = item.Quantidade,
                Fornecedor = item.Fornecedor
            };

            try
            {
                var dao = new ProdutoDAO();
                produto.Id = dao.Insert(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created("", produto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var produto = new ProdutoDAO().GetById(id);

                if (produto == null)
                {
                    return NotFound();
                }

                return Ok(produto);
            }
            catch (Exception)
            {
                return Problem("Ocorreram erros ao processar a solicitação");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProdutoDTO item)
        {
            try
            {
                var dao = new ProdutoDAO();
                var produto = dao.GetById(id);

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

                dao.Update(produto);

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dao = new ProdutoDAO();
                var produto = dao.GetById(id);

                if (produto == null)
                {
                    return NotFound();
                }

                dao.Delete(produto.Id);

                return Ok();
            }
            catch (Exception)
            {
                return Problem("Ocorreram erros ao processar a solicitação");
            }
        }
    }
}
