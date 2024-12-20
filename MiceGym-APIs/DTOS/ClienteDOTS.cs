﻿namespace MiceGym_APIs.DTOS
{
    public class ClienteDOTS
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string UF { get; internal set; }
        public string Bairro { get; internal set; }
        public string Numero { get; internal set; }
        public string CEP { get; internal set; }
    }
}
