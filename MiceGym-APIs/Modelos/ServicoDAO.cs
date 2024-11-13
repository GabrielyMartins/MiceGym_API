using MiceGym_APIs.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MiceGym_APIs.DAO
{
    public class ServicoDAO
    {
        private string _connectionString;

        public ServicoDAO(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<Servico> ListarServicos()
        {
            List<Servico> servicos = new List<Servico>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "select * from servico";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    servicos.Add(new Servico
                    {
                        Id = reader.GetInt32("id_ser"),
                        Descricao = reader.GetString("descricao_ser"),
                        Nome = reader.GetString("nome_ser"),
                        preco = reader.GetDecimal("preco_ser")
                    });
                }
            }

            return servicos;
        }


        public Servico BuscarPorId(int id)
        {
            Servico servico = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "select * from servico where id_ser = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    servico = new Servico
                    {
                        Id = reader.GetInt32("id_ser"),
                        Descricao = reader.GetString("descricao_ser"),
                        Nome = reader.GetString("nome_ser"),
                        preco = reader.GetDecimal("preco_ser")

                    };
                }
            }

            return servico;
        }


        public Servico AdicionarServico(Servico servico)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "insert into servico (descricao_ser, nome_ser, preco_ser) VALUES (@Descricao, @Nome, @Preço)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Descricao", servico.Descricao);
                command.Parameters.AddWithValue("@Nome", servico.Nome);
                command.Parameters.AddWithValue("@Preço", servico.preco);

                connection.Open();

                command.ExecuteNonQuery();
            }

            return BuscarPorNome(servico.Nome); 
        }


        public bool AtualizarServico(int id, Servico servico)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "update servico set descricao_ser = @Descricao, nome_ser = @Nome WHERE id_ser = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Descricao", servico.Descricao);
                command.Parameters.AddWithValue("@Nome", servico.Nome);
                command.Parameters.AddWithValue("@Preço", servico.preco);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
        }

        public bool DeletarServico(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "delete from servico where id_ser = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;  
            }
        }


        private Servico BuscarPorNome(string nome)
        {
            Servico servico = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "select * from servico where nome_ser = @Nome";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", nome);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    servico = new Servico
                    {
                        Id = reader.GetInt32("id_ser"),
                        Descricao = reader.GetString("descricao_ser"),
                        Nome = reader.GetString("nome_ser"),
                        preco = reader.GetDecimal("preco_ser")

                    };
                }
            }

            return servico;
        }
    }
}
