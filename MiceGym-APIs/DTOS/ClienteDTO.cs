namespace MiceGym_APIs.DTOS
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string UF { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
    }
}