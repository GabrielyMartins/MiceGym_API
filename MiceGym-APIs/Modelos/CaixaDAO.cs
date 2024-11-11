namespace MiceGym_APIs.Modelos
{
    public class CaixaDAO
    {
        private static List<Caixa> caixas = new List<Caixa>();

        public List<Caixa> Listar()
        {
            return caixas;
        }

        public Caixa Procurar(int id)
        {
            return caixas.FirstOrDefault(c => c.Id == id);
        }

        public Caixa Adicionar(Caixa caixa)
        {
            caixa.Id = caixas.Count + 1;  
            caixas.Add(caixa);
            return caixa;
        }

        public Caixa Atualizar(int id, Caixa caixaAtualizado)
        {
            var caixaExistente = Procurar(id);
            if (caixaExistente != null)
            {
                caixaExistente.SaldoInicial = caixaAtualizado.SaldoInicial;
                caixaExistente.DataAbertura = caixaAtualizado.DataAbertura;
                caixaExistente.DataFechamento = caixaAtualizado.DataFechamento;
                caixaExistente.SaldoFinal = caixaAtualizado.SaldoFinal;
                return caixaExistente;
            }
            return null;
        }

        public bool Deletar(int id)
        {
            var caixaExistente = Procurar(id);
            if (caixaExistente != null)
            {
                caixas.Remove(caixaExistente);
                return true;
            }
            return false;
        }
    }
}
