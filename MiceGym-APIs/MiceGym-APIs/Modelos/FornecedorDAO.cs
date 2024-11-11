using MiceGym_APIs.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace MiceGym_APIs.DAO
{
    public class FornecedorDAO
    {
        private const string Arquivo = "Dados Fornecedor.txt";

        public List<Fornecedores> List()
        {
            var fornecedores = new List<Fornecedores>();

            if (!System.IO.File.Exists(Arquivo))
            {
                return fornecedores;
            }

            var linhas = System.IO.File.ReadAllLines(Arquivo);
            foreach (var linha in linhas)
            {
                var dados = linha.Split('|');
                fornecedores.Add(new Fornecedores
                {
                    NomeFantasia = dados[0],
                    RazaoSocial = dados[1],
                    CNPJ = dados[2],
                    Endereco = dados[3],
                    Cidade = dados[4],
                    Estado = dados[5],
                    Telefone = dados[6],
                    Email = dados[7],
                    Responsavel = dados[8]
                });
            }

            return fornecedores;
        }

        public Fornecedores GetByCNPJ(string cnpj)
        {
            var fornecedores = List();
            return fornecedores.FirstOrDefault(f => f.CNPJ == cnpj);
        }

        public void Insert(Fornecedores fornecedor)
        {
            var fornecedores = List();
            fornecedores.Add(fornecedor);
            GravarArquivo(fornecedores);
        }

        public void Update(Fornecedores fornecedor)
        {
            var fornecedores = List();
            var index = fornecedores.FindIndex(f => f.CNPJ == fornecedor.CNPJ);

            if (index != -1)
            {
                fornecedores[index] = fornecedor;
                GravarArquivo(fornecedores);
            }
        }

        public void Delete(string cnpj)
        {
            var fornecedores = List();
            var fornecedor = fornecedores.FirstOrDefault(f => f.CNPJ == cnpj);

            if (fornecedor != null)
            {
                fornecedores.Remove(fornecedor);
                GravarArquivo(fornecedores);
            }
        }

        private void GravarArquivo(List<Fornecedores> fornecedores)
        {
            var linhas = fornecedores.Select(f => $"{f.NomeFantasia}|{f.RazaoSocial}|{f.CNPJ}|{f.Endereco}|{f.Cidade}|{f.Estado}|{f.Telefone}|{f.Email}|{f.Responsavel}");
            System.IO.File.WriteAllLines(Arquivo, linhas);
        }
    }
}
