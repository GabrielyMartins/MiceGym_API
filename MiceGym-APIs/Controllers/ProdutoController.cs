using MiceGym_APIs.DAO;
using MiceGym_APIs.DTOS;
using MiceGym_APIs.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace MiceGym_APIs.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class produtoController : Controller
    {
        private readonly ProdutoDAO dao;
        public produtoController()
        {
            dao = new ProdutoDAO();
        }

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
                Quantidade = item.Quantidade
            };

            try
            {
                //var dao = new ProdutoDAO();
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
                //var dao = new ProdutoDAO();
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

                dao.Update(produto);

                return Ok(produto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
                //var dao = new ProdutoDAO();
                var produto = dao.GetById(id);

                if (produto == null)
                {
                    return NotFound();
                }

                dao.Delete(produto.Id);

                return NoContent();
        }
    }
}
