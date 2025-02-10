namespace MiceGym_APIs.Modelos
{
    public class Equipamento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        
    }
}