﻿namespace MiceGym_APIs.DTOS
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public double PrecoCompra { get; set; }

        public double PrecoVenda { get; set; }

        public double Quantidade { get; set; }

        public string Fornecedor { get; set; }


    }
}
