namespace MiceGym_APIs.Modelos
{
    public class Caixa
    {
        public int Id { get; set; }
        public decimal SaldoInicial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; } 
        public decimal SaldoFinal { get; set; }
    }
}
