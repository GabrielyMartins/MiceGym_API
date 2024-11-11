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
                string query = "SELECT * FROM Servicos";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    servicos.Add(new Servico
                    {
                        Id = reader.GetInt32("Id"),
                        Descricao = reader.GetString("Descricao"),
                        Nome = reader.GetString("Nome")
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
                string query = "SELECT * FROM Servicos WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    servico = new Servico
                    {
                        Id = reader.GetInt32("Id"),
                        Descricao = reader.GetString("Descricao"),
                        Nome = reader.GetString("Nome")
                    };
                }
            }

            return servico;
        }


        public Servico AdicionarServico(Servico servico)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Servicos (Descricao, Nome) VALUES (@Descricao, @Nome)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Descricao", servico.Descricao);
                command.Parameters.AddWithValue("@Nome", servico.Nome);
                connection.Open();

                command.ExecuteNonQuery();
            }

            return BuscarPorNome(servico.Nome); 
        }


        public bool AtualizarServico(int id, Servico servico)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Servicos SET Descricao = @Descricao, Nome = @Nome WHERE Id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Descricao", servico.Descricao);
                command.Parameters.AddWithValue("@Nome", servico.Nome);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
        }

        public bool DeletarServico(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Servicos WHERE Id = @Id";
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
                string query = "SELECT * FROM Servicos WHERE Nome = @Nome";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", nome);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    servico = new Servico
                    {
                        Id = reader.GetInt32("Id"),
                        Descricao = reader.GetString("Descricao"),
                        Nome = reader.GetString("Nome")
                    };
                }
            }

            return servico;
        }
    }
}
