using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tarefajoaoAPI;
using System.Collections.Generic;
using System.Linq;

namespace PDS.Controllers
{
    [Route("Treino/")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        private static List<Treinoacad> treinos = new List<Treinoacad>();

        // Método para listar todos os treinos
        [HttpGet("Listar")]
        public IActionResult Get()
        {
            return Ok(treinos);
        }

        // Método para buscar um treino pelo ID
        [HttpGet("Procurar/{id}")]
        public IActionResult GetById(int id)
        {
            var treinoItem = treinos.FirstOrDefault(t => t.Id == id);

            if (treinoItem == null)
            {
                return NotFound();
            }

            return Ok(treinoItem);
        }

        // Método para adicionar um novo treino
        [HttpPost("Adicionar")]
        public IActionResult Post([FromBody] Treinoacad item)
        {
            var novoTreino = new Treinoacad
            {
                Id = treinos.Count + 1,
                Data = item.Data,
                Frequencia = item.Frequencia,
                Exercicios = item.Exercicios,
                SeriesReps = item.SeriesReps,
                Status = item.Status,
                Tempodesc = item.Tempodesc,
                Observacoes = item.Observacoes,
                Objetivo = item.Objetivo,
                Carga = item.Carga
            };

            treinos.Add(novoTreino);

            return StatusCode(StatusCodes.Status201Created, novoTreino);
        }

        // Método para atualizar um treino existente pelo ID
        [HttpPut("Atualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Treinoacad item)
        {
            var treinoItem = treinos.FirstOrDefault(t => t.Id == id);

            if (treinoItem == null)
            {
                return NotFound();
            }

            treinoItem.Data = item.Data;
            treinoItem.Frequencia = item.Frequencia;
            treinoItem.Exercicios = item.Exercicios;
            treinoItem.SeriesReps = item.SeriesReps;
            treinoItem.Status = item.Status;
            treinoItem.Tempodesc = item.Tempodesc;
            treinoItem.Observacoes = item.Observacoes;
            treinoItem.Objetivo = item.Objetivo;
            treinoItem.Carga = item.Carga;

            return Ok(treinoItem);
        }

        // Método para deletar um treino pelo ID
        [HttpDelete("Deletar/{id}")]
        public IActionResult Delete(int id)
        {
            var treinoItem = treinos.FirstOrDefault(t => t.Id == id);

            if (treinoItem == null)
            {
                return NotFound();
            }

            treinos.Remove(treinoItem);

            return Ok(treinoItem);
        }
    }
}
