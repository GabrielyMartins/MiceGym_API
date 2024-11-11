using MiceGym_APIs.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace MiceGym_APIs.DAO
{
    public class EquipamentoDAO
    {
        private const string Arquivo = "Dados Equipamento.txt";

        public List<Equipamento> List()
        {
            var equipamentos = new List<Equipamento>();

            if (!System.IO.File.Exists(Arquivo))
            {
                return equipamentos;
            }

            var linhas = System.IO.File.ReadAllLines(Arquivo);
            foreach (var linha in linhas)
            {
                var dados = linha.Split('|');
                equipamentos.Add(new Equipamento
                {
                    Nome = dados[0],
                    Descricao = dados[1],
                    Codigo = dados[2],
                    Quantidade = dados[3],
                    Valor = dados[4],
                    Fornecedor = dados[5]
                });
            }

            return equipamentos;
        }

        public Equipamento GetByCodigo(string codigo)
        {
            var equipamentos = List();
            return equipamentos.FirstOrDefault(e => e.Codigo == codigo);
        }

        public void Insert(Equipamento equipamento)
        {
            var equipamentos = List();
            equipamentos.Add(equipamento);
            GravarArquivo(equipamentos);
        }

        public void Update(Equipamento equipamento)
        {
            var equipamentos = List();
            var index = equipamentos.FindIndex(e => e.Codigo == equipamento.Codigo);

            if (index != -1)
            {
                equipamentos[index] = equipamento;
                GravarArquivo(equipamentos);
            }
        }

        public void Delete(string codigo)
        {
            var equipamentos = List();
            var equipamento = equipamentos.FirstOrDefault(e => e.Codigo == codigo);

            if (equipamento != null)
            {
                equipamentos.Remove(equipamento);
                GravarArquivo(equipamentos);
            }
        }

        private void GravarArquivo(List<Equipamento> equipamentos)
        {
            var linhas = equipamentos.Select(e => $"{e.Nome}|{e.Descricao}|{e.Codigo}|{e.Quantidade}|{e.Valor}|{e.Fornecedor}");
            System.IO.File.WriteAllLines(Arquivo, linhas);
        }
    }
}
