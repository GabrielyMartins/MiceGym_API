namespace MiceGym_APIs.DTOS
{
    public class FuncionarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CTPS { get; set; }
        public string RG { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }
        public string Sala { get; set; }
        public string Telefone { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public string UF { get; internal set; }
        public string Cidade { get; internal set; }
        public string Bairro { get; internal set; }
        public string Numero { get; internal set; }
        public string CEP { get; internal set; }
    }

    public class EnderecoDTO
    {
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
    }
}
