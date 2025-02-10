namespace MiceGym_APIs.DTOS
{
    public class EquipamentoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        
    }
}